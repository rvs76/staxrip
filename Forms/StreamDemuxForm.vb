﻿Imports System.Globalization
Imports StaxRip.UI

Public Class StreamDemuxForm
    Property AudioStreams As List(Of AudioStream)
    Property Subtitles As List(Of Subtitle)

    Sub New(demuxer As Demuxer, sourceFile As String, attachments As List(Of Attachment))
        InitializeComponent()

        cbDemuxChapters.Checked = demuxer.ChaptersDemuxing
        cbDemuxChapters.Visible = MediaInfo.GetMenu(sourceFile, "Chapters_Pos_End").ToInt - MediaInfo.GetMenu(sourceFile, "Chapters_Pos_Begin").ToInt > 0
        cbDemuxVideo.Checked = demuxer.VideoDemuxing

        ScaleClientSize(42, 29)
        StartPosition = FormStartPosition.CenterParent

        lvAudio.View = View.Details
        lvAudio.Columns.Add("")
        lvAudio.CheckBoxes = True
        lvAudio.HeaderStyle = ColumnHeaderStyle.None
        lvAudio.ShowItemToolTips = True
        lvAudio.FullRowSelect = True
        lvAudio.MultiSelect = False
        lvAudio.SendMessageHideFocus()
        lvAudio.AutoCheckMode = AutoCheckMode.SingleClick

        lvSubtitles.View = View.SmallIcon
        lvSubtitles.CheckBoxes = True
        lvSubtitles.HeaderStyle = ColumnHeaderStyle.None
        lvSubtitles.AutoCheckMode = AutoCheckMode.SingleClick

        lvAttachments.View = View.SmallIcon
        lvAttachments.CheckBoxes = True
        lvAttachments.HeaderStyle = ColumnHeaderStyle.None
        lvAttachments.AutoCheckMode = AutoCheckMode.SingleClick

        AddHandler Load, Sub() lvAudio.Columns(0).Width = lvAudio.ClientSize.Width

        AudioStreams = MediaInfo.GetAudioStreams(sourceFile)
        Subtitles = MediaInfo.GetSubtitles(sourceFile)

        gbAudio.Enabled = AudioStreams.Count > 0
        gbSubtitles.Enabled = Subtitles.Count > 0
        gbAttachments.Enabled = Not attachments.NothingOrEmpty

        bnAudioEnglish.Enabled = AudioStreams.Where(Function(stream) stream.Language.TwoLetterCode = "en").Count > 0
        bnAudioNative.Visible = CultureInfo.CurrentCulture.TwoLetterISOLanguageName <> "en"
        bnAudioNative.Text = CultureInfo.CurrentCulture.NeutralCulture.EnglishName
        bnAudioNative.Enabled = AudioStreams.Where(Function(stream) stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName).Count > 0

        bnSubtitleEnglish.Enabled = Subtitles.Where(Function(stream) stream.Language.TwoLetterCode = "en").Count > 0
        bnSubtitleNative.Visible = CultureInfo.CurrentCulture.TwoLetterISOLanguageName <> "en"
        bnSubtitleNative.Text = CultureInfo.CurrentCulture.NeutralCulture.EnglishName
        bnSubtitleNative.Enabled = Subtitles.Where(Function(stream) stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName).Count > 0

        For Each audioStream In AudioStreams
            Dim item = lvAudio.Items.Add(audioStream.Name)
            item.Tag = audioStream
            item.Checked = audioStream.Enabled
        Next

        For Each subtitle In Subtitles
            Dim text = subtitle.Language.ToString + " (" + subtitle.TypeName + ")" + If(subtitle.Title <> "", " - " + subtitle.Title, "")
            Dim item = lvSubtitles.Items.Add(text)
            item.Tag = subtitle
            item.Checked = subtitle.Enabled
        Next

        If Not attachments Is Nothing Then
            For Each attachment In attachments
                Dim item = lvAttachments.Items.Add(attachment.Name)
                item.Tag = attachment
                item.Checked = attachment.Enabled
            Next
        End If
    End Sub

    Private Sub lvAudio_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvAudio.ItemChecked
        If Visible Then DirectCast(e.Item.Tag, AudioStream).Enabled = e.Item.Checked
    End Sub

    Private Sub lvSubtitles_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvSubtitles.ItemChecked
        If Visible Then DirectCast(e.Item.Tag, Subtitle).Enabled = e.Item.Checked
    End Sub

    Private Sub lvAttachments_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvAttachments.ItemChecked
        If Visible Then DirectCast(e.Item.Tag, Attachment).Enabled = e.Item.Checked
    End Sub

    Private Sub bnAudioAll_Click(sender As Object, e As EventArgs) Handles bnAudioAll.Click
        For Each i As ListViewItem In lvAudio.Items
            i.Checked = True
        Next
    End Sub

    Private Sub bnAudioNone_Click(sender As Object, e As EventArgs) Handles bnAudioNone.Click
        For Each i As ListViewItem In lvAudio.Items
            i.Checked = False
        Next
    End Sub

    Private Sub bnAudioEnglish_Click(sender As Object, e As EventArgs) Handles bnAudioEnglish.Click
        For Each i As ListViewItem In lvAudio.Items
            Dim stream = DirectCast(i.Tag, AudioStream)

            If stream.Language.TwoLetterCode = "en" Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub bnAudioNative_Click(sender As Object, e As EventArgs) Handles bnAudioNative.Click
        For Each i As ListViewItem In lvAudio.Items
            Dim stream = DirectCast(i.Tag, AudioStream)

            If stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub bnSubtitleAll_Click(sender As Object, e As EventArgs) Handles bnSubtitleAll.Click
        For Each i As ListViewItem In lvSubtitles.Items
            i.Checked = True
        Next
    End Sub

    Private Sub bnSubtitleNone_Click(sender As Object, e As EventArgs) Handles bnSubtitleNone.Click
        For Each i As ListViewItem In lvSubtitles.Items
            i.Checked = False
        Next
    End Sub

    Private Sub bnSubtitleEnglish_Click(sender As Object, e As EventArgs) Handles bnSubtitleEnglish.Click
        For Each i As ListViewItem In lvSubtitles.Items
            Dim stream = DirectCast(i.Tag, Subtitle)

            If stream.Language.TwoLetterCode = "en" Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub bnSubtitleNative_Click(sender As Object, e As EventArgs) Handles bnSubtitleNative.Click
        For Each i As ListViewItem In lvSubtitles.Items
            Dim stream = DirectCast(i.Tag, Subtitle)

            If stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName Then
                i.Checked = True
            End If
        Next
    End Sub
End Class