Imports StaxRip.UI

Imports VB6 = Microsoft.VisualBasic
Imports System.Text.RegularExpressions
Imports System.Text

<Serializable()>
Public MustInherit Class AudioProfile
    Inherits Profile

    Property Language As New Language
    Property Delay As Integer
    Property Depth As Integer = 24
    Property StreamName As String = ""
    Property Gain As Single
    Property Streams As List(Of AudioStream) = New List(Of AudioStream)
    Property [Default] As Boolean
    Property Forced As Boolean
    Property Decoder As AudioDecoderMode
    Property DecodingMode As AudioDecodingMode

    Overridable Property Channels As Integer = 6
    Overridable Property OutputFileType As String = "unknown"
    Overridable Property Bitrate As Double
    Overridable Property SupportedInput As String()

    Overridable Property CommandLines As String

    Sub New(name As String)
        MyBase.New(name)
    End Sub

    Sub New(name As String,
            bitrate As Integer,
            input As String(),
            fileType As String,
            channels As Integer)

        MyBase.New(name)

        Me.Channels = channels
        Me.Bitrate = bitrate
        SupportedInput = input
        OutputFileType = fileType
    End Sub

    Private FileValue As String = ""

    Property File As String
        Get
            Return FileValue
        End Get
        Set(value As String)
            If FileValue <> value Then
                FileValue = value
                Stream = Nothing
                OnFileChanged()
            End If
        End Set
    End Property

    Private StreamValue As AudioStream

    Property Stream As AudioStream
        Get
            Return StreamValue
        End Get
        Set(value As AudioStream)
            If Not value Is StreamValue Then
                StreamValue = value

                If Not Stream Is Nothing Then
                    If Not p.Script.GetFilter("Source").Script.Contains("DirectShowSource") Then
                        Delay = Stream.Delay
                    End If

                    Language = Stream.Language
                    StreamName = Stream.Title
                    Forced = Stream.Forced
                    [Default] = Stream.Default
                End If

                OnStreamChanged()
            End If
        End Set
    End Property

    Property DisplayName As String
        Get
            Dim ret = ""

            If Stream Is Nothing Then
                Dim streams = MediaInfo.GetAudioStreams(File)

                If streams.Count > 0 Then
                    ret = GetAudioText(streams(0), File)
                Else
                    ret = File.FileName
                End If
            Else
                ret = Stream.Name + " (" + FilePath.GetExt(File) + ")"
            End If

            Return ret
        End Get
        Set(value As String)
        End Set
    End Property

    Private SourceSamplingRateValue As Integer

    ReadOnly Property SourceSamplingRate As Integer
        Get
            If SourceSamplingRateValue = 0 Then
                If Stream Is Nothing Then
                    If File <> "" AndAlso IO.File.Exists(File) Then
                        SourceSamplingRateValue = MediaInfo.GetAudio(File, "SamplingRate").ToInt
                    End If
                Else
                    SourceSamplingRateValue = Stream.SamplingRate
                End If
            End If

            If SourceSamplingRateValue = 0 Then SourceSamplingRateValue = 48000
            Return SourceSamplingRateValue
        End Get
    End Property

    ReadOnly Property HasStream As Boolean
        Get
            Return Stream IsNot Nothing
        End Get
    End Property

    Overridable Sub Migrate()
        If Depth = 0 Then Depth = 24
    End Sub

    ReadOnly Property ConvertExt As String
        Get
            Dim ret As String

            Select Case DecodingMode
                Case AudioDecodingMode.FLAC
                    ret = "flac"
                Case AudioDecodingMode.W64
                    ret = "w64"
                Case AudioDecodingMode.WAVE
                    ret = "wav"
                Case Else
                    Throw New NotImplementedException
            End Select

            If Not SupportedInput.Contains(ret) Then ret = "wav"
            Return ret
        End Get
    End Property

    Overridable Sub OnFileChanged()
    End Sub

    Overridable Sub OnStreamChanged()
    End Sub

    Function GetAudioText(stream As AudioStream, path As String) As String
        For Each i In Language.Languages
            If path.Contains(i.CultureInfo.EnglishName) Then
                stream.Language = i
                Exit For
            End If
        Next

        Dim matchDelay = Regex.Match(path, " (-?\d+)ms")
        If matchDelay.Success Then stream.Delay = matchDelay.Groups(1).Value.ToInt

        Dim matchID = Regex.Match(path, " ID(\d+)")
        Dim name As String
        name = stream.Name.Substring(3)

        If File.Base = p.SourceFile.Base Then
            Return name + " (" + File.Ext + ")"
        Else
            Return name + " (" + FilePath.GetName(File) + ")"
        End If
    End Function

    Sub SetStreamOrLanguage()
        If File = "" Then Exit Sub

        If File <> p.LastOriginalSourceFile Then
            For Each i In Language.Languages
                If File.Contains(i.CultureInfo.EnglishName) Then
                    Language = i
                    Exit Sub
                End If
            Next
        Else
            For Each i In Streams
                If i.Language.Equals(Language) Then
                    Stream = i
                    Exit For
                End If
            Next

            If Stream Is Nothing AndAlso Streams.Count > 0 Then
                Stream = Streams(0)
            End If
        End If
    End Sub

    Function IsInputSupported() As Boolean
        Return SupportedInput.NothingOrEmpty OrElse SupportedInput.Contains(File.Ext)
    End Function

    Function IsMuxProfile() As Boolean
        Return TypeOf Me Is MuxAudioProfile
    End Function

    Overridable Sub Encode()
    End Sub

    Overridable Sub EditProject()
    End Sub

    Overridable Function HandlesDelay() As Boolean
    End Function

    Function GetTrackID() As Integer
        If Me Is p.Audio0 Then Return 1
        If Me Is p.Audio1 Then Return 2

        For x = 0 To p.AudioTracks.Count - 1
            If Me Is p.AudioTracks(x) Then Return x + 3
        Next
    End Function

    Function GetOutputFile() As String
        Dim base = File.Base

        If Delay <> 0 Then
            If HandlesDelay() Then
                If base.Contains("ms") Then
                    Dim re As New Regex(" (-?\d+)ms")
                    If re.IsMatch(base) Then base = re.Replace(base, "")
                End If
            Else
                If Not base.Contains("ms") Then base += " " & Delay & "ms"
            End If
        End If

        Return p.TempDir + base + "_a" & GetTrackID() & "." + OutputFileType
    End Function

    Function ExpandMacros(value As String) As String
        Return ExpandMacros(value, True)
    End Function

    Function ExpandMacros(value As String, silent As Boolean) As String
        If value = "" Then Return ""
        If value.Contains("""%input%""") Then value = value.Replace("""%input%""", File.Escape)
        If value.Contains("%input%") Then value = value.Replace("%input%", File.Escape)
        If value.Contains("""%output%""") Then value = value.Replace("""%output%""", GetOutputFile.Escape)
        If value.Contains("%output%") Then value = value.Replace("%output%", GetOutputFile.Escape)
        If value.Contains("%bitrate%") Then value = value.Replace("%bitrate%", Bitrate.ToString)
        If value.Contains("%channels%") Then value = value.Replace("%channels%", Channels.ToString)
        If value.Contains("%language_native%") Then value = value.Replace("%language_native%", Language.CultureInfo.NativeName)
        If value.Contains("%language_english%") Then value = value.Replace("%language_english%", Language.Name)
        If value.Contains("%delay%") Then value = value.Replace("%delay%", Delay.ToString)
        Return Macro.Expand(value)
    End Function

    Shared Function GetDefaults() As List(Of AudioProfile)
        Dim ret As New List(Of AudioProfile)
        ret.Add(New GUIAudioProfile(AudioCodec.AAC, 0.4))
        ret.Add(New GUIAudioProfile(AudioCodec.Opus, 1) With {.Bitrate = 250})
        ret.Add(New GUIAudioProfile(AudioCodec.FLAC, 0.3))
        ret.Add(New GUIAudioProfile(AudioCodec.Vorbis, 1))
        ret.Add(New GUIAudioProfile(AudioCodec.MP3, 4))
        ret.Add(New GUIAudioProfile(AudioCodec.AC3, 1.0) With {.Channels = 6, .Bitrate = 640})
        ret.Add(New BatchAudioProfile(640, {}, "ac3", 6, "ffmpeg -i %input% -b:a %bitrate%k -y -hide_banner %output%"))
        ret.Add(New MuxAudioProfile())
        ret.Add(New NullAudioProfile())
        Return ret
    End Function
End Class

<Serializable()>
Public Class BatchAudioProfile
    Inherits AudioProfile

    Sub New(bitrate As Integer,
            input As String(),
            fileType As String,
            channels As Integer,
            batchCode As String)

        MyBase.New("Command Line", bitrate, input, fileType, channels)
        Me.CommandLines = batchCode
        CanEditValue = True
    End Sub

    Overrides Function Edit() As DialogResult
        Using f As New BatchAudioEncoderForm(Me)
            f.mbLanguage.Enabled = False
            f.lLanguage.Enabled = False
            f.tbDelay.Enabled = False
            f.lDelay.Enabled = False
            Return f.ShowDialog()
        End Using
    End Function

    Function GetCode() As String
        Dim cl = ExpandMacros(CommandLines).Trim

        Return {
            Package.ffmpeg,
            Package.eac3to,
            Package.qaac}.
            Where(Function(pack) cl.ToLower.Contains(pack.Name.ToLower)).
            Select(Function(pack) "set PATH=%PATH%;" + pack.GetDir).
            Join(BR) + BR2 + "cd /D " + p.TempDir.Escape + BR2 + cl
    End Function

    Public Overrides Sub Encode()
        If File <> "" Then
            Dim bitrateBefore = p.VideoBitrate
            Dim targetPath = GetOutputFile()

            Proc.ExecuteBatch(
                GetCode(),
                "Audio encoding: " + Name,
                "_a" & GetTrackID(),
                {"Maximum Gain Found", "transcoding ...", "size=", "process: ", "analyze: "})

            If g.FileExists(targetPath) Then
                File = targetPath

                If Not p.BitrateIsFixed Then
                    Bitrate = Calc.GetBitrateFromFile(File, p.TargetSeconds)
                    p.VideoBitrate = CInt(Calc.GetVideoBitrate)

                    If Not p.VideoEncoder.QualityMode Then
                        Log.WriteLine("Video Bitrate: " + bitrateBefore.ToString() + " -> " & p.VideoBitrate & BR)
                    End If
                End If

                Log.WriteLine(MediaInfo.GetSummary(File))
            Else
                Log.Write("Error", "no output found")

                If Not File.Ext = "wav" Then
                    Audio.Convert(Me)
                    If File.Ext = "wav" Then Encode()
                End If
            End If
        End If
    End Sub

    Overrides Sub EditProject()
        Using f As New BatchAudioEncoderForm(Me)
            f.ShowDialog()
        End Using
    End Sub

    Overrides Function HandlesDelay() As Boolean
        Return CommandLines.Contains("%delay%")
    End Function
End Class

<Serializable()>
Public Class NullAudioProfile
    Inherits AudioProfile

    Sub New()
        MyBase.New("No Audio", 0, {}, "ignore", 0)
    End Sub

    Overrides Function HandlesDelay() As Boolean
    End Function

    Overrides Sub EditProject()
        Using form As New SimpleSettingsForm("Null Audio Profile Options")
            form.ScaleClientSize(20, 10)
            Dim ui = form.SimpleUI
            ui.Store = Me

            Dim n = ui.AddNum()
            n.Text = "Reserved Bitrate:"
            n.Config = {0, Integer.MaxValue, 8}
            n.Property = NameOf(Bitrate)

            If form.ShowDialog() = DialogResult.OK Then ui.Save()
        End Using
    End Sub

    Public Overrides Property OutputFileType As String
        Get
            Return "ignore"
        End Get
        Set(value As String)
        End Set
    End Property

    Public Overrides Sub Encode()
    End Sub
End Class

<Serializable()>
Public Class MuxAudioProfile
    Inherits AudioProfile

    Sub New()
        MyBase.New("Copy/Mux", 0, Nothing, "ignore", 2)
        CanEditValue = True
    End Sub

    Public Overrides Property OutputFileType As String
        Get
            If Stream Is Nothing Then
                Return File.Ext
            Else
                Return Stream.Extension.TrimStart("."c)
            End If
        End Get
        Set(value As String)
        End Set
    End Property

    Overrides Property SupportedInput As String()
        Get
            Return {}
        End Get
        Set(value As String())
        End Set
    End Property

    Overrides Function Edit() As DialogResult
        Return Edit(False)
    End Function

    Overrides Sub EditProject()
        Edit(True)
    End Sub

    Overrides Sub Encode()
    End Sub

    Overrides Sub OnFileChanged()
        MyBase.OnFileChanged()
        SetBitrate()
    End Sub

    Overrides Sub OnStreamChanged()
        MyBase.OnStreamChanged()
        SetBitrate()
    End Sub

    Sub SetBitrate()
        If Stream Is Nothing Then
            Bitrate = Calc.GetBitrateFromFile(File, p.SourceSeconds)
        Else
            Bitrate = Stream.Bitrate + Stream.Bitrate2
        End If
    End Sub

    Private Overloads Function Edit(showProjectSettings As Boolean) As DialogResult
        Using form As New SimpleSettingsForm("Audio Mux Options", "The Audio Mux options allow to add a audio file without reencoding.")
            form.ScaleClientSize(30, 15)

            Dim ui = form.SimpleUI
            Dim page = ui.CreateFlowPage("main page")
            page.SuspendLayout()

            Dim tbb = ui.AddTextButton(page)
            tbb.Label.Text = "Stream Name:"
            tbb.Label.Help = "Stream name used by the muxer. The stream name may contain macros."
            tbb.Edit.Expand = True
            tbb.Edit.Text = StreamName
            tbb.Edit.SaveAction = Sub(value) StreamName = value
            tbb.Button.Text = "Macro Editor..."
            tbb.Button.ClickAction = AddressOf tbb.Edit.EditMacro

            Dim nb = ui.AddNum(page)
            nb.Label.Text = "Delay:"
            nb.Label.Help = "Delay used by the muxer."
            nb.NumEdit.Config = {Integer.MinValue, Integer.MaxValue, 1}
            nb.NumEdit.Value = Delay
            nb.NumEdit.SaveAction = Sub(value) Delay = CInt(value)

            Dim mbi = ui.AddMenu(Of Language)(page)
            mbi.Label.Text = "Language:"
            mbi.Label.Help = "Language of the audio track."
            mbi.Button.Value = Language
            mbi.Button.SaveAction = Sub(value) Language = value

            For Each i In Language.Languages
                If i.IsCommon Then
                    mbi.Button.Add(i.ToString, i)
                Else
                    mbi.Button.Add("More | " + i.ToString.Substring(0, 1).ToUpper + " | " + i.ToString, i)
                End If
            Next

            Dim cb = ui.AddBool(page)
            cb.Text = "Default"
            cb.Help = "Flaged as default in MKV."
            cb.Checked = [Default]
            cb.SaveAction = Sub(value) [Default] = value

            cb = ui.AddBool(page)
            cb.Text = "Forced"
            cb.Help = "Flaged as forced in MKV."
            cb.Checked = Forced
            cb.SaveAction = Sub(value) Forced = value

            page.ResumeLayout()

            Dim ret = form.ShowDialog()
            If ret = DialogResult.OK Then ui.Save()

            Return ret
        End Using
    End Function
End Class

<Serializable()>
Public Class GUIAudioProfile
    Inherits AudioProfile

    Property Params As New Parameters

    Sub New(codec As AudioCodec, quality As Single)
        MyBase.New(Nothing)

        Params.Codec = codec
        Params.Quality = quality

        Select Case codec
            Case AudioCodec.DTS, AudioCodec.AC3
                Params.RateMode = AudioRateMode.CBR
            Case Else
                Params.RateMode = AudioRateMode.VBR
        End Select

        Bitrate = GetBitrate()
    End Sub

    Public Overrides Property Channels As Integer
        Get
            Select Case Params.ChannelsMode
                Case ChannelsMode.Original
                    If Not Stream Is Nothing Then
                        If Stream.Channels > Stream.Channels2 Then
                            Return Stream.Channels
                        Else
                            Return Stream.Channels2
                        End If
                    ElseIf File <> "" AndAlso IO.File.Exists(File) Then
                        Return MediaInfo.GetChannels(File)
                    Else
                        Return 6
                    End If
                Case ChannelsMode._1
                    Return 1
                Case ChannelsMode._2
                    Return 2
                Case ChannelsMode._6
                    Return 6
                Case ChannelsMode._7
                    Return 7
                Case ChannelsMode._8
                    Return 8
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
        Set(value As Integer)
        End Set
    End Property

    ReadOnly Property TargetSamplingRate As Integer
        Get
            If Params.SamplingRate <> 0 Then
                Return Params.SamplingRate
            Else
                Return SourceSamplingRate
            End If
        End Get
    End Property

    Public Overrides Sub Migrate()
        MyBase.Migrate()
        Params.Migrate()
    End Sub

    Function GetBitrate() As Integer
        If Params.RateMode = AudioRateMode.VBR Then
            Select Case Params.Codec
                Case AudioCodec.AAC
                    Select Case Params.Encoder
                        Case GuiAudioEncoder.qaac
                            Return Calc.GetYFromTwoPointForm(0, CInt(50 / 8 * Channels), 127, CInt(1000 / 8 * Channels), Params.Quality)
                        Case GuiAudioEncoder.fdkaac
                            Return Calc.GetYFromTwoPointForm(1, CInt(50 / 8 * Channels), 5, CInt(900 / 8 * Channels), Params.Quality)
                        Case Else
                            Return Calc.GetYFromTwoPointForm(0.01, CInt(50 / 8 * Channels), 1, CInt(1000 / 8 * Channels), Params.Quality)
                    End Select
                Case AudioCodec.MP3
                    Return Calc.GetYFromTwoPointForm(9, 65, 0, 245, Params.Quality)
                Case AudioCodec.Vorbis
                    If Channels >= 6 Then
                        Return Calc.GetYFromTwoPointForm(0, 120, 10, 1440, Params.Quality)
                    Else
                        Return Calc.GetYFromTwoPointForm(0, 64, 10, 500, Params.Quality)
                    End If
                Case AudioCodec.Opus
                    Return CInt(Bitrate)
            End Select
        End If

        Select Case Params.Codec
            Case AudioCodec.FLAC
                Return CInt(((TargetSamplingRate * Depth * Channels) / 1000) * 0.55)
            Case AudioCodec.W64, AudioCodec.WAV
                Return CInt((TargetSamplingRate * Depth * Channels) / 1000)
        End Select

        Return CInt(Bitrate)
    End Function

    Public Overrides Sub Encode()
        If File <> "" Then
            Dim bitrateBefore = p.VideoBitrate
            Dim targetPath = GetOutputFile()
            Dim cl = GetCommandLine(True)

            Using proc As New Proc
                proc.Header = "Audio encoding " & GetTrackID()

                If cl.Contains("|") Then
                    proc.File = "cmd.exe"
                    proc.Arguments = "/S /C """ + cl + """"
                Else
                    proc.CommandLine = cl
                End If

                If cl.Contains("qaac64.exe") Then
                    proc.Package = Package.qaac
                    proc.SkipStrings = {", ETA ", "x)"}
                ElseIf cl.Contains("fdkaac.exe") Then
                    proc.Package = Package.fdkaac
                    proc.SkipString = "%]"
                ElseIf cl.Contains("eac3to.exe") Then
                    proc.Package = Package.eac3to
                    proc.SkipStrings = {"process: ", "analyze: "}
                    proc.TrimChars = {"-"c, " "c}
                    proc.RemoveChars = {VB6.ChrW(8)} 'backspace
                ElseIf cl.Contains("ffmpeg.exe") Then
                    proc.Package = Package.ffmpeg
                    proc.SkipStrings = {"frame=", "size="}
                    proc.Encoding = Encoding.UTF8
                End If

                proc.Start()
            End Using

            If g.FileExists(targetPath) Then
                File = targetPath

                If Not p.BitrateIsFixed Then
                    Bitrate = Calc.GetBitrateFromFile(File, p.TargetSeconds)
                    p.VideoBitrate = CInt(Calc.GetVideoBitrate)

                    If Not p.VideoEncoder.QualityMode Then
                        Log.WriteLine("Video Bitrate: " + bitrateBefore.ToString() + " -> " & p.VideoBitrate & BR)
                    End If
                End If

                Log.WriteLine(MediaInfo.GetSummary(File))
            Else
                Throw New ErrorAbortException("Error audio encoding", "The output file is missing")
            End If
        End If
    End Sub

    Sub NormalizeFF()
        If Not Params.Normalize OrElse Not {ffNormalizeMode.loudnorm, ffNormalizeMode.volumedetect}.Contains(Params.ffNormalizeMode) Then
            Exit Sub
        End If

        Dim args = "-i " + File.Escape
        If Not Stream Is Nothing AndAlso Streams.Count > 1 Then args += " -map 0:a:" & Stream.Index
        args += " -sn -vn -hide_banner"

        If Params.ffNormalizeMode = ffNormalizeMode.volumedetect Then
            args += " -af volumedetect"
        ElseIf Params.ffNormalizeMode = ffNormalizeMode.loudnorm Then
            args += " -af loudnorm=I=" & Params.ffmpegLoudnormIntegrated.ToInvariantString +
                ":TP=" & Params.ffmpegLoudnormTruePeak.ToInvariantString + ":LRA=" &
                Params.ffmpegLoudnormLRA.ToInvariantString + ":print_format=summary"
        End If

        args += " -f null NUL"

        Using proc As New Proc
            proc.Header = "Find Gain " & GetTrackID()
            proc.SkipStrings = {"frame=", "size="}
            proc.Encoding = Encoding.UTF8
            proc.Package = Package.ffmpeg
            proc.Arguments = args
            proc.Start()

            Dim match = Regex.Match(proc.Log.ToString, "max_volume: -(\d+\.\d+) dB")
            If match.Success Then Gain = match.Groups(1).Value.ToSingle()

            match = Regex.Match(proc.Log.ToString, "Input Integrated:\s*([-\.0-9]+)")
            If match.Success Then Params.ffmpegLoudnormIntegratedMeasured = match.Groups(1).Value.ToDouble

            match = Regex.Match(proc.Log.ToString, "Input True Peak:\s*([-\.0-9]+)")
            If match.Success Then Params.ffmpegLoudnormTruePeakMeasured = match.Groups(1).Value.ToDouble

            match = Regex.Match(proc.Log.ToString, "Input LRA:\s*([-\.0-9]+)")
            If match.Success Then Params.ffmpegLoudnormLraMeasured = match.Groups(1).Value.ToDouble

            match = Regex.Match(proc.Log.ToString, "Input Threshold:\s*([-\.0-9]+)")
            If match.Success Then Params.ffmpegLoudnormThresholdMeasured = match.Groups(1).Value.ToDouble
        End Using
    End Sub

    Overrides Function Edit() As DialogResult
        Using form As New AudioForm()
            form.LoadProfile(Me)
            form.mbLanguage.Enabled = False
            form.numDelay.Enabled = False
            form.numGain.Enabled = False
            Return form.ShowDialog()
        End Using
    End Function

    Overrides Sub EditProject()
        Using form As New AudioForm()
            form.LoadProfile(Me)
            form.ShowDialog()
        End Using
    End Sub

    Public Overrides Property OutputFileType As String
        Get
            Select Case Params.Codec
                Case AudioCodec.AAC
                    Return "m4a"
                Case AudioCodec.Vorbis
                    Return "ogg"
                Case Else
                    Return Params.Codec.ToString.ToLower
            End Select
        End Get
        Set(value As String)
        End Set
    End Property

    Function GetEac3toCommandLine(includePaths As Boolean) As String
        Dim ret, id As String

        If File.Ext.EqualsAny("ts", "m2ts", "mkv") AndAlso Not Stream Is Nothing Then
            id = (Stream.StreamOrder + 1) & ": "
        End If

        If includePaths Then
            ret = Package.eac3to.Path.Escape + " " + id + File.Escape + " " + GetOutputFile.Escape
        Else
            ret = "eac3to"
        End If

        If Not (Params.Codec = AudioCodec.DTS AndAlso Params.eac3toExtractDtsCore) Then
            Select Case Params.Codec
                Case AudioCodec.AAC
                    ret += " -quality=" & Params.Quality.ToInvariantString
                Case AudioCodec.AC3
                    ret += " -" & Bitrate

                    If Not {192, 224, 384, 448, 640}.Contains(CInt(Bitrate)) Then
                        Return "Invalid bitrate, select 192, 224, 384, 448 or 640"
                    End If
                Case AudioCodec.DTS
                    ret += " -" & Bitrate
            End Select

            If Params.Normalize Then ret += " -normalize"
            If Depth = 16 Then ret += " -down16"
            If Params.SamplingRate <> 0 Then ret += " -resampleTo" & Params.SamplingRate
            If Params.FrameRateMode = AudioFrameRateMode.Speedup Then ret += " -speedup"
            If Params.FrameRateMode = AudioFrameRateMode.Slowdown Then ret += " -slowdown"
            If Delay <> 0 Then ret += " " + If(Delay > 0, "+", "") & Delay & "ms"
            If Gain < 0 Then ret += " " & CInt(Gain) & "dB"
            If Gain > 0 Then ret += " +" & CInt(Gain) & "dB"

            Select Case Channels
                Case 6
                    If Params.ChannelsMode <> ChannelsMode.Original Then ret += " -down6"
                Case 2
                    If Params.eac3toStereoDownmixMode = 0 Then
                        If Params.ChannelsMode <> ChannelsMode.Original Then ret += " -downStereo"
                    Else
                        ret += " -downDpl"
                    End If
            End Select

            If Params.CustomSwitches <> "" Then ret += " " + Params.CustomSwitches
        ElseIf Params.eac3toExtractDtsCore Then
            ret += " -core"
        End If

        If includePaths Then ret += " -progressnumbers"

        Return ret
    End Function

    Function GetfdkaacCommandLine(includePaths As Boolean) As String
        Dim ret As String
        includePaths = includePaths And File <> ""
        If includePaths Then ret += Package.fdkaac.Path.Escape Else ret = "fdkaac"
        If Params.fdkaacProfile <> 2 Then ret += " --profile " & Params.fdkaacProfile

        If Params.SimpleRateMode = SimpleAudioRateMode.CBR Then
            ret += " --bitrate " & CInt(Bitrate)
        Else
            ret += " --bitrate-mode " & Params.Quality
        End If

        If Params.fdkaacGaplessMode <> 0 Then ret += " --gapless-mode " & Params.fdkaacGaplessMode
        If Params.fdkaacBandwidth <> 0 Then ret += " --bandwidth " & Params.fdkaacBandwidth
        If Not Params.fdkaacAfterburner Then ret += " --afterburner 0"
        If Params.fdkaacAdtsCrcCheck Then ret += " --adts-crc-check"
        If Params.fdkaacMoovBeforeMdat Then ret += " --moov-before-mdat"
        If Params.fdkaacIncludeSbrDelay Then ret += " --include-sbr-delay"
        If Params.fdkaacHeaderPeriod Then ret += " --header-period"
        If Params.fdkaacLowDelaySBR <> 0 Then ret += " --lowdelay-sbr " & Params.fdkaacLowDelaySBR
        If Params.fdkaacSbrRatio <> 0 Then ret += " --sbr-ratio " & Params.fdkaacSbrRatio
        If Params.fdkaacTransportFormat <> 0 Then ret += " --transport-format " & Params.fdkaacTransportFormat

        If Params.CustomSwitches <> "" Then ret += " " + Params.CustomSwitches
        If includePaths Then ret += " --ignorelength -o " + GetOutputFile.Escape + " " + File.Escape
        Return ret
    End Function

    Function GetQaacCommandLine(includePaths As Boolean) As String
        Dim ret As String
        includePaths = includePaths And File <> ""
        If DecodingMode = AudioDecodingMode.Pipe Then ret = GetPipeCommandLine(includePaths)
        If includePaths Then ret += Package.qaac.Path.Escape Else ret = "qaac"

        Select Case Params.qaacRateMode
            Case 0
                ret += " --tvbr " & CInt(Params.Quality)
            Case 1
                ret += " --cvbr " & CInt(Bitrate)
            Case 2
                ret += " --abr " & CInt(Bitrate)
            Case 3
                ret += " --cbr " & CInt(Bitrate)
        End Select

        If Params.qaacHE Then ret += " --he"
        If Delay <> 0 Then ret += " --delay " + (Delay / 1000).ToInvariantString
        If Params.qaacQuality <> 2 Then ret += " --quality " & Params.qaacQuality
        If Params.SamplingRate <> 0 Then ret += " --rate " & Params.SamplingRate
        If Params.qaacLowpass <> 0 Then ret += " --lowpass " & Params.qaacLowpass
        If Params.qaacNoDither Then ret += " --no-dither"
        If Gain <> 0 Then ret += " --gain " & Gain.ToInvariantString
        If Params.CustomSwitches <> "" Then ret += " " + Params.CustomSwitches
        Dim input = If(DecodingMode = AudioDecodingMode.Pipe, "-", File.Escape)
        If includePaths Then ret += " " + input + " -o " + GetOutputFile.Escape

        Return ret
    End Function

    Function GetPipeCommandLine(includePaths As Boolean) As String
        Dim ret As String

        If includePaths AndAlso File <> "" Then
            ret = Package.ffmpeg.Path.Escape + " -i " + File.Escape
        Else
            ret = "ffmpeg"
        End If

        If Not Stream Is Nothing AndAlso Streams.Count > 1 Then ret += " -map 0:a:" & Stream.Index
        If Params.ChannelsMode <> ChannelsMode.Original Then ret += " -ac " & Channels

        If Params.Normalize Then
            If Params.ffNormalizeMode = ffNormalizeMode.dynaudnorm Then
                ret += " " + Audio.GetDynAudNormArgs(Params)
            ElseIf Params.ffNormalizeMode = ffNormalizeMode.loudnorm Then
                ret += " " + Audio.GetLoudNormArgs(Params)
            End If
        End If

        If includePaths AndAlso File <> "" Then ret += " -loglevel fatal -hide_banner -f wav - | "

        Return ret
    End Function

    Function GetffmpegCommandLine(includePaths As Boolean) As String
        Dim ret As String

        If includePaths AndAlso File <> "" Then
            ret = Package.ffmpeg.Path.Escape + " -i " + File.Escape
        Else
            ret = "ffmpeg"
        End If

        If Not Stream Is Nothing AndAlso Streams.Count > 1 Then ret += " -map 0:a:" & Stream.Index

        Select Case Params.Codec
            Case AudioCodec.MP3
                If Not Params.CustomSwitches.Contains("-c:a ") Then ret += " -c:a libmp3lame"

                Select Case Params.RateMode
                    Case AudioRateMode.ABR
                        ret += " -b:a " & CInt(Bitrate) & "k -abr 1"
                    Case AudioRateMode.CBR
                        ret += " -b:a " & CInt(Bitrate) & "k"
                    Case AudioRateMode.VBR
                        ret += " -q:a " & CInt(Params.Quality)
                End Select
            Case AudioCodec.AC3
                If Not {192, 224, 384, 448, 640}.Contains(CInt(Bitrate)) Then
                    Return "Invalid bitrate, select 192, 224, 384, 448 or 640"
                End If

                ret += " -b:a " & CInt(Bitrate) & "k"
            Case AudioCodec.DTS
                ret += " -strict -2 -b:a " & CInt(Bitrate) & "k"
            Case AudioCodec.Vorbis
                If Not Params.CustomSwitches.Contains("-c:a ") Then ret += " -c:a libvorbis"

                If Params.RateMode = AudioRateMode.VBR Then
                    ret += " -q:a " & CInt(Params.Quality)
                Else
                    ret += " -b:a " & CInt(Bitrate) & "k"
                End If
            Case AudioCodec.Opus
                If Not Params.CustomSwitches.Contains("-c:a ") Then ret += " -c:a libopus"

                If Params.RateMode = AudioRateMode.VBR Then
                    ret += " -vbr on"
                Else
                    ret += " -vbr off"
                End If

                ret += " -b:a " & CInt(Bitrate) & "k"
            Case AudioCodec.AAC
                If Params.RateMode = AudioRateMode.VBR Then
                    ret += " -q:a " & Calc.GetYFromTwoPointForm(0.1, 1, 1, 10, Params.Quality)
                Else
                    ret += " -b:a " & CInt(Bitrate) & "k"
                End If
            Case AudioCodec.W64, AudioCodec.WAV
                If Depth = 24 Then
                    ret += " -c:a pcm_s24le"
                Else
                    ret += " -c:a pcm_s16le"
                End If
        End Select

        If Gain <> 0 Then ret += " -af volume=" + Gain.ToInvariantString + "dB"

        If Params.Normalize Then
            If Params.ffNormalizeMode = ffNormalizeMode.loudnorm Then
                ret += " " + Audio.GetLoudNormArgs(Params)
            ElseIf Params.ffNormalizeMode = ffNormalizeMode.dynaudnorm Then
                ret += " " + Audio.GetDynAudNormArgs(Params)
            End If
        End If

        'opus fails if -ac is missing
        If Params.ChannelsMode <> ChannelsMode.Original OrElse Params.Codec = AudioCodec.Opus Then
            ret += " -ac " & Channels
        End If

        If Params.SamplingRate <> 0 Then ret += " -ar " & Params.SamplingRate
        If Params.CustomSwitches <> "" Then ret += " " + Params.CustomSwitches

        If includePaths AndAlso File <> "" Then
            ret += " -y -hide_banner"
            ret += " " + GetOutputFile.Escape
        End If

        Return ret
    End Function

    Function SupportsNormalize() As Boolean
        Return GetEncoder() = GuiAudioEncoder.Eac3to OrElse GetEncoder() = GuiAudioEncoder.qaac
    End Function

    Public Overrides ReadOnly Property DefaultName As String
        Get
            If Params Is Nothing Then Exit Property
            Dim ch As String

            Select Case Params.ChannelsMode
                Case ChannelsMode._8
                    ch += " 7.1"
                Case ChannelsMode._7
                    ch += " 6.1"
                Case ChannelsMode._6
                    ch += " 5.1"
                Case ChannelsMode._2
                    ch += " 2.0"
                Case ChannelsMode._1
                    ch += " Mono"
            End Select

            Dim circa = If(Params.RateMode = AudioRateMode.VBR OrElse Params.Codec = AudioCodec.FLAC, "~", "")
            Dim bitrate = If(Params.RateMode = AudioRateMode.VBR, GetBitrate(), Me.Bitrate)

            Return Params.Codec.ToString + ch & " " & circa & bitrate & " Kbps"
        End Get
    End Property

    Overrides Property CommandLines() As String
        Get
            Return GetCommandLine(True)
        End Get
        Set(Value As String)
        End Set
    End Property

    Overrides ReadOnly Property CanEdit() As Boolean
        Get
            Return True
        End Get
    End Property

    Overrides Function HandlesDelay() As Boolean
        If {GuiAudioEncoder.Eac3to, GuiAudioEncoder.qaac}.Contains(GetEncoder()) Then Return True
    End Function

    Function GetEncoder() As GuiAudioEncoder
        Select Case Params.Encoder
            Case GuiAudioEncoder.Eac3to
                If {AudioCodec.AAC, AudioCodec.AC3, AudioCodec.FLAC, AudioCodec.DTS, AudioCodec.W64, AudioCodec.WAV}.Contains(Params.Codec) Then Return GuiAudioEncoder.Eac3to
            Case GuiAudioEncoder.ffmpeg
                Return GuiAudioEncoder.ffmpeg
            Case GuiAudioEncoder.qaac
                If Params.Codec = AudioCodec.AAC Then Return GuiAudioEncoder.qaac
            Case GuiAudioEncoder.fdkaac
                If Params.Codec = AudioCodec.AAC Then Return GuiAudioEncoder.fdkaac
        End Select

        If Params.Codec = AudioCodec.AAC Then Return GuiAudioEncoder.Eac3to

        Return GuiAudioEncoder.ffmpeg
    End Function

    Function GetCommandLine(includePaths As Boolean) As String
        Select Case GetEncoder()
            Case GuiAudioEncoder.Eac3to
                Return GetEac3toCommandLine(includePaths)
            Case GuiAudioEncoder.qaac
                Return GetQaacCommandLine(includePaths)
            Case GuiAudioEncoder.fdkaac
                Return GetfdkaacCommandLine(includePaths)
            Case Else
                Return GetffmpegCommandLine(includePaths)
        End Select
    End Function

    Overrides Property SupportedInput As String()
        Get
            Select Case GetEncoder()
                Case GuiAudioEncoder.Eac3to
                    Return FileTypes.eac3toInput
                Case GuiAudioEncoder.qaac
                    If DecodingMode <> AudioDecodingMode.Pipe Then Return FileTypes.qaacInput
                Case GuiAudioEncoder.fdkaac
                    Return {"wav"}
            End Select

            Return {}
        End Get
        Set(value As String())
        End Set
    End Property

    <Serializable()>
    Public Class Parameters
        Property Codec As AudioCodec
        Property CustomSwitches As String = ""
        Property eac3toExtractDtsCore As Boolean
        Property eac3toStereoDownmixMode As Integer
        Property Encoder As GuiAudioEncoder
        Property FrameRateMode As AudioFrameRateMode
        Property Normalize As Boolean = True
        Property Quality As Single = 0.3
        Property RateMode As AudioRateMode
        Property SamplingRate As Integer
        Property ChannelsMode As ChannelsMode

        Property Migrate1 As Boolean = True

        Property qaacHE As Boolean
        Property qaacLowpass As Integer
        Property qaacNoDither As Boolean
        Property qaacQuality As Integer = 2
        Property qaacRateMode As Integer

        Property opusencMode As Integer = 2
        Property opusencComplexity As Integer = 10
        Property opusencFramesize As Double = 20
        Property opusencMigrateVersion As Integer = 1

        Property fdkaacProfile As Integer = 2
        Property fdkaacBandwidth As Integer
        Property fdkaacAfterburner As Boolean = True
        Property fdkaacLowDelaySBR As Integer
        Property fdkaacSbrRatio As Integer
        Property fdkaacTransportFormat As Integer
        Property fdkaacGaplessMode As Integer
        Property fdkaacAdtsCrcCheck As Boolean
        Property fdkaacHeaderPeriod As Boolean
        Property fdkaacIncludeSbrDelay As Boolean
        Property fdkaacMoovBeforeMdat As Boolean

        Property ffNormalizeMode As ffNormalizeMode
        Property ffmpegLoudnormIntegrated As Double = -24
        Property ffmpegLoudnormLRA As Double = 7
        Property ffmpegLoudnormTruePeak As Double = -2

        Property ffmpegLoudnormIntegratedMeasured As Double
        Property ffmpegLoudnormLraMeasured As Double
        Property ffmpegLoudnormTruePeakMeasured As Double
        Property ffmpegLoudnormThresholdMeasured As Double

        Property ffmpegDynaudnormF As Integer = 500
        Property ffmpegDynaudnormG As Integer = 31
        Property ffmpegDynaudnormP As Double = 0.95
        Property ffmpegDynaudnormM As Double = 10
        Property ffmpegDynaudnormR As Double
        Property ffmpegDynaudnormN As Boolean = True
        Property ffmpegDynaudnormC As Boolean
        Property ffmpegDynaudnormB As Boolean
        Property ffmpegDynaudnormS As Double

        Property SimpleRateMode As SimpleAudioRateMode
            Get
                If RateMode = AudioRateMode.CBR Then
                    Return SimpleAudioRateMode.CBR
                Else
                    Return SimpleAudioRateMode.VBR
                End If
            End Get
            Set(value As SimpleAudioRateMode)
                If value = SimpleAudioRateMode.CBR Then
                    RateMode = AudioRateMode.CBR
                Else
                    RateMode = AudioRateMode.VBR
                End If
            End Set
        End Property

        Sub Migrate()
            If opusencMigrateVersion <> 1 Then
                opusencFramesize = 20
                opusencComplexity = 10
                opusencMode = 2
                opusencMigrateVersion = 1
            End If

            If fdkaacProfile = 0 Then
                fdkaacProfile = 2
                SimpleRateMode = SimpleAudioRateMode.VBR
                fdkaacAfterburner = True
            End If

            If Not Migrate1 Then
                Normalize = True

                ffmpegLoudnormIntegrated = -24
                ffmpegLoudnormLRA = 7
                ffmpegLoudnormTruePeak = -2

                ffmpegDynaudnormF = 500
                ffmpegDynaudnormG = 31
                ffmpegDynaudnormP = 0.95
                ffmpegDynaudnormM = 10
                ffmpegDynaudnormN = True

                Migrate1 = True
            End If
        End Sub
    End Class
End Class

Public Enum AudioCodec
    AAC
    AC3
    DTS
    FLAC
    MP3
    Opus
    Vorbis
    W64
    WAV
End Enum

Public Enum AudioRateMode
    CBR
    ABR
    VBR
End Enum

Public Enum SimpleAudioRateMode
    CBR
    VBR
End Enum

Public Enum AudioAacProfile
    Automatic
    LC
    SBR
    <DispName("SBR+PS")> SBRPS = 300
End Enum

Public Enum GuiAudioEncoder
    Automatic
    Eac3to
    ffmpeg
    qaac
    fdkaac
End Enum

Public Enum AudioFrameRateMode
    Keep
    <DispName("Apply PAL speedup")> Speedup
    <DispName("Reverse PAL speedup")> Slowdown
End Enum

Public Enum AudioDownMixMode
    <DispName("Simple")> Stereo
    <DispName("Dolby Surround")> Surround
    <DispName("Dolby Surround 2")> Surround2
End Enum

Public Enum ChannelsMode
    Original
    <DispName("1 (Mono)")> _1
    <DispName("2 (Stereo)")> _2
    <DispName("5.1")> _6
    <DispName("6.1")> _7
    <DispName("7.1")> _8
End Enum