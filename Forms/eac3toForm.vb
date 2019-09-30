Imports System.ComponentModel
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports StaxRip.UI

Public Class eac3toForm
    Inherits DialogBase

#Region " Designer "

    Protected Overloads Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents CommandLink1 As StaxRip.UI.CommandLink
    Friend WithEvents cbVideoOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdlOptions As StaxRip.CommandLineControl
    Friend WithEvents bnBrowse As StaxRip.UI.ButtonEx
    Friend WithEvents bnCancel As StaxRip.UI.ButtonEx
    Friend WithEvents bnOK As StaxRip.UI.ButtonEx
    Friend WithEvents lvAudio As ListViewEx
    Friend WithEvents lvSubtitles As StaxRip.UI.ListViewEx
    Friend WithEvents cbVideoStream As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents flpAudioLinks As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flpSubtitleLinks As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cbChapters As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tlpTarget As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbAudio As System.Windows.Forms.GroupBox
    Friend WithEvents gbSubtitles As System.Windows.Forms.GroupBox
    Friend WithEvents cbAudioOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bnMenu As StaxRip.UI.ButtonEx
    Friend WithEvents cms As ContextMenuStripEx
    Friend WithEvents bnAudioAll As ButtonEx
    Friend WithEvents bnAudioNone As ButtonEx
    Friend WithEvents bnAudioEnglish As ButtonEx
    Friend WithEvents bnAudioNative As ButtonEx
    Friend WithEvents bnSubtitleAll As ButtonEx
    Friend WithEvents bnSubtitleNone As ButtonEx
    Friend WithEvents bnSubtitleEnglish As ButtonEx
    Friend WithEvents bnSubtitleNative As ButtonEx
    Friend WithEvents tlpVideo As TableLayoutPanel
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents tlpAudioOptions As TableLayoutPanel
    Friend WithEvents tlpAudio As TableLayoutPanel
    Friend WithEvents tlpSubtitles As TableLayoutPanel
    Friend WithEvents teTempDir As TextEdit
    Friend WithEvents tlpMain As TableLayoutPanel
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdlOptions = New StaxRip.CommandLineControl()
        Me.cbVideoOutput = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.bnBrowse = New StaxRip.UI.ButtonEx()
        Me.bnCancel = New StaxRip.UI.ButtonEx()
        Me.bnOK = New StaxRip.UI.ButtonEx()
        Me.lvAudio = New StaxRip.UI.ListViewEx()
        Me.lvSubtitles = New StaxRip.UI.ListViewEx()
        Me.flpSubtitleLinks = New System.Windows.Forms.FlowLayoutPanel()
        Me.bnSubtitleAll = New StaxRip.UI.ButtonEx()
        Me.bnSubtitleNone = New StaxRip.UI.ButtonEx()
        Me.bnSubtitleEnglish = New StaxRip.UI.ButtonEx()
        Me.bnSubtitleNative = New StaxRip.UI.ButtonEx()
        Me.flpAudioLinks = New System.Windows.Forms.FlowLayoutPanel()
        Me.bnAudioAll = New StaxRip.UI.ButtonEx()
        Me.bnAudioNone = New StaxRip.UI.ButtonEx()
        Me.bnAudioEnglish = New StaxRip.UI.ButtonEx()
        Me.bnAudioNative = New StaxRip.UI.ButtonEx()
        Me.cbVideoStream = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tlpTarget = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.teTempDir = New StaxRip.UI.TextEdit()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.bnMenu = New StaxRip.UI.ButtonEx()
        Me.cms = New StaxRip.UI.ContextMenuStripEx(Me.components)
        Me.cbChapters = New System.Windows.Forms.CheckBox()
        Me.gbAudio = New System.Windows.Forms.GroupBox()
        Me.tlpAudio = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpAudioOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbAudioOutput = New System.Windows.Forms.ComboBox()
        Me.gbSubtitles = New System.Windows.Forms.GroupBox()
        Me.tlpSubtitles = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpVideo = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.flpSubtitleLinks.SuspendLayout()
        Me.flpAudioLinks.SuspendLayout()
        Me.tlpTarget.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        Me.gbAudio.SuspendLayout()
        Me.tlpAudio.SuspendLayout()
        Me.tlpAudioOptions.SuspendLayout()
        Me.gbSubtitles.SuspendLayout()
        Me.tlpSubtitles.SuspendLayout()
        Me.tlpVideo.SuspendLayout()
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdlOptions
        '
        Me.cmdlOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdlOptions.Location = New System.Drawing.Point(520, 8)
        Me.cmdlOptions.Margin = New System.Windows.Forms.Padding(3, 8, 3, 8)
        Me.cmdlOptions.Name = "cmdlOptions"
        Me.cmdlOptions.Size = New System.Drawing.Size(488, 70)
        Me.cmdlOptions.TabIndex = 5
        '
        'cbVideoOutput
        '
        Me.cbVideoOutput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbVideoOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVideoOutput.FormattingEnabled = True
        Me.cbVideoOutput.Location = New System.Drawing.Point(129, 3)
        Me.cbVideoOutput.Name = "cbVideoOutput"
        Me.cbVideoOutput.Size = New System.Drawing.Size(206, 56)
        Me.cbVideoOutput.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 48)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Video:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bnBrowse
        '
        Me.bnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.bnBrowse.Location = New System.Drawing.Point(959, 4)
        Me.bnBrowse.Margin = New System.Windows.Forms.Padding(0)
        Me.bnBrowse.Size = New System.Drawing.Size(70, 70)
        Me.bnBrowse.Text = "..."
        '
        'bnCancel
        '
        Me.bnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bnCancel.Location = New System.Drawing.Point(779, 3)
        Me.bnCancel.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.bnCancel.Size = New System.Drawing.Size(250, 70)
        Me.bnCancel.Text = "Cancel"
        '
        'bnOK
        '
        Me.bnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.bnOK.Location = New System.Drawing.Point(518, 3)
        Me.bnOK.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.bnOK.Size = New System.Drawing.Size(250, 70)
        Me.bnOK.Text = "OK"
        '
        'lvAudio
        '
        Me.lvAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvAudio.Location = New System.Drawing.Point(10, 0)
        Me.lvAudio.Margin = New System.Windows.Forms.Padding(10, 0, 10, 10)
        Me.lvAudio.Name = "lvAudio"
        Me.lvAudio.Size = New System.Drawing.Size(997, 33)
        Me.lvAudio.TabIndex = 8
        Me.lvAudio.UseCompatibleStateImageBehavior = False
        '
        'lvSubtitles
        '
        Me.lvSubtitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvSubtitles.Location = New System.Drawing.Point(10, 0)
        Me.lvSubtitles.Margin = New System.Windows.Forms.Padding(10, 0, 10, 10)
        Me.lvSubtitles.Name = "lvSubtitles"
        Me.lvSubtitles.Size = New System.Drawing.Size(997, 24)
        Me.lvSubtitles.TabIndex = 9
        Me.lvSubtitles.UseCompatibleStateImageBehavior = False
        '
        'flpSubtitleLinks
        '
        Me.flpSubtitleLinks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.flpSubtitleLinks.AutoSize = True
        Me.flpSubtitleLinks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpSubtitleLinks.Controls.Add(Me.bnSubtitleAll)
        Me.flpSubtitleLinks.Controls.Add(Me.bnSubtitleNone)
        Me.flpSubtitleLinks.Controls.Add(Me.bnSubtitleEnglish)
        Me.flpSubtitleLinks.Controls.Add(Me.bnSubtitleNative)
        Me.flpSubtitleLinks.Location = New System.Drawing.Point(10, 34)
        Me.flpSubtitleLinks.Margin = New System.Windows.Forms.Padding(10, 0, 0, 10)
        Me.flpSubtitleLinks.Name = "flpSubtitleLinks"
        Me.flpSubtitleLinks.Size = New System.Drawing.Size(910, 70)
        Me.flpSubtitleLinks.TabIndex = 19
        '
        'bnSubtitleAll
        '
        Me.bnSubtitleAll.Location = New System.Drawing.Point(0, 0)
        Me.bnSubtitleAll.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnSubtitleAll.Size = New System.Drawing.Size(220, 70)
        Me.bnSubtitleAll.Text = "All"
        '
        'bnSubtitleNone
        '
        Me.bnSubtitleNone.Location = New System.Drawing.Point(230, 0)
        Me.bnSubtitleNone.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnSubtitleNone.Size = New System.Drawing.Size(220, 70)
        Me.bnSubtitleNone.Text = "None"
        '
        'bnSubtitleEnglish
        '
        Me.bnSubtitleEnglish.Location = New System.Drawing.Point(460, 0)
        Me.bnSubtitleEnglish.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnSubtitleEnglish.Size = New System.Drawing.Size(220, 70)
        Me.bnSubtitleEnglish.Text = "English"
        '
        'bnSubtitleNative
        '
        Me.bnSubtitleNative.Location = New System.Drawing.Point(690, 0)
        Me.bnSubtitleNative.Margin = New System.Windows.Forms.Padding(0)
        Me.bnSubtitleNative.Size = New System.Drawing.Size(220, 70)
        Me.bnSubtitleNative.Text = "Native"
        '
        'flpAudioLinks
        '
        Me.flpAudioLinks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.flpAudioLinks.AutoSize = True
        Me.flpAudioLinks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpAudioLinks.Controls.Add(Me.bnAudioAll)
        Me.flpAudioLinks.Controls.Add(Me.bnAudioNone)
        Me.flpAudioLinks.Controls.Add(Me.bnAudioEnglish)
        Me.flpAudioLinks.Controls.Add(Me.bnAudioNative)
        Me.flpAudioLinks.Location = New System.Drawing.Point(10, 43)
        Me.flpAudioLinks.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.flpAudioLinks.Name = "flpAudioLinks"
        Me.flpAudioLinks.Size = New System.Drawing.Size(910, 70)
        Me.flpAudioLinks.TabIndex = 18
        '
        'bnAudioAll
        '
        Me.bnAudioAll.Location = New System.Drawing.Point(0, 0)
        Me.bnAudioAll.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnAudioAll.Size = New System.Drawing.Size(220, 70)
        Me.bnAudioAll.Text = "All"
        '
        'bnAudioNone
        '
        Me.bnAudioNone.Location = New System.Drawing.Point(230, 0)
        Me.bnAudioNone.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnAudioNone.Size = New System.Drawing.Size(220, 70)
        Me.bnAudioNone.Text = "None"
        '
        'bnAudioEnglish
        '
        Me.bnAudioEnglish.Location = New System.Drawing.Point(460, 0)
        Me.bnAudioEnglish.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.bnAudioEnglish.Size = New System.Drawing.Size(220, 70)
        Me.bnAudioEnglish.Text = "English"
        '
        'bnAudioNative
        '
        Me.bnAudioNative.Location = New System.Drawing.Point(690, 0)
        Me.bnAudioNative.Margin = New System.Windows.Forms.Padding(0)
        Me.bnAudioNative.Size = New System.Drawing.Size(220, 70)
        Me.bnAudioNative.Text = "Native"
        '
        'cbVideoStream
        '
        Me.cbVideoStream.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbVideoStream.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVideoStream.FormattingEnabled = True
        Me.cbVideoStream.Location = New System.Drawing.Point(486, 3)
        Me.cbVideoStream.Name = "cbVideoStream"
        Me.cbVideoStream.Size = New System.Drawing.Size(540, 56)
        Me.cbVideoStream.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(341, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(139, 48)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Stream:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tlpTarget
        '
        Me.tlpTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpTarget.ColumnCount = 3
        Me.tlpTarget.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpTarget.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTarget.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpTarget.Controls.Add(Me.bnBrowse, 2, 0)
        Me.tlpTarget.Controls.Add(Me.Label2, 0, 0)
        Me.tlpTarget.Controls.Add(Me.teTempDir, 1, 0)
        Me.tlpTarget.Location = New System.Drawing.Point(10, 537)
        Me.tlpTarget.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.tlpTarget.Name = "tlpTarget"
        Me.tlpTarget.RowCount = 1
        Me.tlpTarget.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTarget.Size = New System.Drawing.Size(1029, 79)
        Me.tlpTarget.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(282, 48)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Target Directory:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'teTempDir
        '
        Me.teTempDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.teTempDir.Location = New System.Drawing.Point(282, 4)
        Me.teTempDir.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.teTempDir.Name = "teTempDir"
        Me.teTempDir.Size = New System.Drawing.Size(666, 70)
        Me.teTempDir.TabIndex = 3
        '
        'tlpBottom
        '
        Me.tlpBottom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpBottom.ColumnCount = 4
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpBottom.Controls.Add(Me.bnCancel, 3, 0)
        Me.tlpBottom.Controls.Add(Me.bnOK, 2, 0)
        Me.tlpBottom.Controls.Add(Me.bnMenu, 1, 0)
        Me.tlpBottom.Controls.Add(Me.cbChapters, 0, 0)
        Me.tlpBottom.Location = New System.Drawing.Point(10, 626)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(10, 10, 10, 15)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(1029, 76)
        Me.tlpBottom.TabIndex = 20
        '
        'bnMenu
        '
        Me.bnMenu.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.bnMenu.ContextMenuStrip = Me.cms
        Me.bnMenu.Location = New System.Drawing.Point(437, 3)
        Me.bnMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.bnMenu.ShowMenuSymbol = True
        Me.bnMenu.Size = New System.Drawing.Size(70, 70)
        '
        'cms
        '
        Me.cms.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cms.Name = "cms"
        Me.cms.Size = New System.Drawing.Size(61, 4)
        '
        'cbChapters
        '
        Me.cbChapters.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbChapters.AutoSize = True
        Me.cbChapters.Location = New System.Drawing.Point(5, 12)
        Me.cbChapters.Margin = New System.Windows.Forms.Padding(5)
        Me.cbChapters.Name = "cbChapters"
        Me.cbChapters.Size = New System.Drawing.Size(323, 52)
        Me.cbChapters.TabIndex = 2
        Me.cbChapters.Text = "Extract Chapters"
        Me.cbChapters.UseVisualStyleBackColor = True
        Me.cbChapters.Visible = False
        '
        'gbAudio
        '
        Me.gbAudio.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAudio.Controls.Add(Me.tlpAudio)
        Me.gbAudio.Location = New System.Drawing.Point(10, 82)
        Me.gbAudio.Margin = New System.Windows.Forms.Padding(10, 0, 10, 10)
        Me.gbAudio.Name = "gbAudio"
        Me.gbAudio.Padding = New System.Windows.Forms.Padding(5)
        Me.gbAudio.Size = New System.Drawing.Size(1027, 263)
        Me.gbAudio.TabIndex = 23
        Me.gbAudio.TabStop = False
        Me.gbAudio.Text = "Audio"
        '
        'tlpAudio
        '
        Me.tlpAudio.ColumnCount = 1
        Me.tlpAudio.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAudio.Controls.Add(Me.flpAudioLinks, 0, 1)
        Me.tlpAudio.Controls.Add(Me.lvAudio, 0, 0)
        Me.tlpAudio.Controls.Add(Me.tlpAudioOptions, 0, 2)
        Me.tlpAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAudio.Location = New System.Drawing.Point(5, 53)
        Me.tlpAudio.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tlpAudio.Name = "tlpAudio"
        Me.tlpAudio.RowCount = 3
        Me.tlpAudio.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAudio.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpAudio.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpAudio.Size = New System.Drawing.Size(1017, 205)
        Me.tlpAudio.TabIndex = 23
        '
        'tlpAudioOptions
        '
        Me.tlpAudioOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpAudioOptions.AutoSize = True
        Me.tlpAudioOptions.ColumnCount = 4
        Me.tlpAudioOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpAudioOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpAudioOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpAudioOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAudioOptions.Controls.Add(Me.Label1, 0, 0)
        Me.tlpAudioOptions.Controls.Add(Me.Label3, 2, 0)
        Me.tlpAudioOptions.Controls.Add(Me.cbAudioOutput, 1, 0)
        Me.tlpAudioOptions.Controls.Add(Me.cmdlOptions, 3, 0)
        Me.tlpAudioOptions.Location = New System.Drawing.Point(3, 116)
        Me.tlpAudioOptions.Name = "tlpAudioOptions"
        Me.tlpAudioOptions.RowCount = 1
        Me.tlpAudioOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAudioOptions.Size = New System.Drawing.Size(1011, 86)
        Me.tlpAudioOptions.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 48)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Output:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(361, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 48)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Options:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbAudioOutput
        '
        Me.cbAudioOutput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbAudioOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudioOutput.FormattingEnabled = True
        Me.cbAudioOutput.Location = New System.Drawing.Point(149, 20)
        Me.cbAudioOutput.Name = "cbAudioOutput"
        Me.cbAudioOutput.Size = New System.Drawing.Size(206, 56)
        Me.cbAudioOutput.TabIndex = 21
        '
        'gbSubtitles
        '
        Me.gbSubtitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSubtitles.Controls.Add(Me.tlpSubtitles)
        Me.gbSubtitles.Location = New System.Drawing.Point(10, 355)
        Me.gbSubtitles.Margin = New System.Windows.Forms.Padding(10, 0, 10, 10)
        Me.gbSubtitles.Name = "gbSubtitles"
        Me.gbSubtitles.Padding = New System.Windows.Forms.Padding(5)
        Me.gbSubtitles.Size = New System.Drawing.Size(1027, 172)
        Me.gbSubtitles.TabIndex = 24
        Me.gbSubtitles.TabStop = False
        Me.gbSubtitles.Text = "Subtitles"
        '
        'tlpSubtitles
        '
        Me.tlpSubtitles.ColumnCount = 1
        Me.tlpSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpSubtitles.Controls.Add(Me.flpSubtitleLinks, 0, 1)
        Me.tlpSubtitles.Controls.Add(Me.lvSubtitles, 0, 0)
        Me.tlpSubtitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpSubtitles.Location = New System.Drawing.Point(5, 53)
        Me.tlpSubtitles.Margin = New System.Windows.Forms.Padding(1, 10, 10, 10)
        Me.tlpSubtitles.Name = "tlpSubtitles"
        Me.tlpSubtitles.RowCount = 2
        Me.tlpSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpSubtitles.Size = New System.Drawing.Size(1017, 114)
        Me.tlpSubtitles.TabIndex = 20
        '
        'tlpVideo
        '
        Me.tlpVideo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpVideo.AutoSize = True
        Me.tlpVideo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpVideo.ColumnCount = 4
        Me.tlpVideo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpVideo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpVideo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpVideo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpVideo.Controls.Add(Me.cbVideoStream, 3, 0)
        Me.tlpVideo.Controls.Add(Me.cbVideoOutput, 1, 0)
        Me.tlpVideo.Controls.Add(Me.Label8, 2, 0)
        Me.tlpVideo.Controls.Add(Me.Label5, 0, 0)
        Me.tlpVideo.Location = New System.Drawing.Point(10, 10)
        Me.tlpVideo.Margin = New System.Windows.Forms.Padding(10)
        Me.tlpVideo.Name = "tlpVideo"
        Me.tlpVideo.RowCount = 1
        Me.tlpVideo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpVideo.Size = New System.Drawing.Size(1029, 62)
        Me.tlpVideo.TabIndex = 25
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpVideo, 0, 0)
        Me.tlpMain.Controls.Add(Me.gbAudio, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 4)
        Me.tlpMain.Controls.Add(Me.gbSubtitles, 0, 2)
        Me.tlpMain.Controls.Add(Me.tlpTarget, 0, 3)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 5
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.Size = New System.Drawing.Size(1049, 717)
        Me.tlpMain.TabIndex = 26
        '
        'eac3toForm
        '
        Me.AcceptButton = Me.bnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(288.0!, 288.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.bnCancel
        Me.ClientSize = New System.Drawing.Size(1049, 717)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(10, 10, 10, 10)
        Me.Name = "eac3toForm"
        Me.Text = "eac3to"
        Me.flpSubtitleLinks.ResumeLayout(False)
        Me.flpAudioLinks.ResumeLayout(False)
        Me.tlpTarget.ResumeLayout(False)
        Me.tlpTarget.PerformLayout()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.gbAudio.ResumeLayout(False)
        Me.tlpAudio.ResumeLayout(False)
        Me.tlpAudio.PerformLayout()
        Me.tlpAudioOptions.ResumeLayout(False)
        Me.tlpAudioOptions.PerformLayout()
        Me.gbSubtitles.ResumeLayout(False)
        Me.tlpSubtitles.ResumeLayout(False)
        Me.tlpSubtitles.PerformLayout()
        Me.tlpVideo.ResumeLayout(False)
        Me.tlpVideo.PerformLayout()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Property M2TSFile As String
    Property PlaylistFolder As String
    Property OutputFolder As String
    Property PlaylistID As Integer

    Private Output As String
    Private Streams As New BindingList(Of M2TSStream)
    Private AudioOutputFormats As String() = {"m4a", "ac3", "dts", "flac", "wav", "dtsma", "dtshr", "eac3", "thd", "thd+ac3"}
    Private Project As Project

    Sub New(proj As Project)
        MyBase.New()
        InitializeComponent()
        Project = proj
        ScaleClientSize(40, 30)

        cbAudioOutput.Sorted = True
        cbAudioOutput.Items.AddRange(AudioOutputFormats)

        For Each ctrl As Control In Controls
            ctrl.Enabled = False
        Next

        cbChapters.Checked = s.Storage.GetBool("demux Blu-ray chapters", True)

        lvAudio.View = View.Details
        lvAudio.Columns.Add("")
        lvAudio.CheckBoxes = True
        lvAudio.HeaderStyle = ColumnHeaderStyle.None
        lvAudio.ShowItemToolTips = True
        lvAudio.FullRowSelect = True
        lvAudio.MultiSelect = False
        lvAudio.SendMessageHideFocus()

        lvSubtitles.View = View.SmallIcon
        lvSubtitles.CheckBoxes = True
        lvSubtitles.HeaderStyle = ColumnHeaderStyle.None
        lvSubtitles.AutoCheckMode = AutoCheckMode.SingleClick

        cmdlOptions.Presets = s.CmdlPresetsEac3to
        cmdlOptions.RestoreFunc = Function() ApplicationSettings.GetDefaultEac3toMenu.FormatColumn("=")

        bnAudioNative.Visible = False
        bnAudioEnglish.Visible = False
        bnSubtitleNative.Visible = False
        bnSubtitleEnglish.Visible = False

        If CultureInfo.CurrentCulture.TwoLetterISOLanguageName <> "en" Then
            Try
                bnAudioNative.Text = New CultureInfo(CultureInfo.CurrentCulture.TwoLetterISOLanguageName).EnglishName
                bnSubtitleNative.Text = bnAudioNative.Text
            Catch ex As Exception
                g.ShowException(ex)
                bnAudioNative.Visible = False
                bnSubtitleNative.Visible = False
            End Try
        End If

        cms.Items.Add(New ActionMenuItem("Audio Stream Profiles...", AddressOf ShowAudioStreamProfiles))
        cms.Items.Add(New ActionMenuItem("Show eac3to wikibook", Sub() g.StartProcess("http://en.wikibooks.org/wiki/Eac3to")))
        cms.Items.Add(New ActionMenuItem("Show eac3to support forum", Sub() g.StartProcess("http://forum.doom9.org/showthread.php?t=125966")))
        cms.Items.Add(New ActionMenuItem("Execute eac3to.exe -test", Sub() g.StartProcess("cmd.exe", "/k """ + Package.eac3to.Path + """ -test")))

        ActiveControl = Nothing
    End Sub

    Sub ShowAudioStreamProfilesHelp()
        Dim f As New HelpForm
        f.Doc.WriteStart("Audio Stream Profiles")
        f.Doc.WriteP("Allows to automatically apply default values for audio streams.")
        f.Doc.WriteTable({New StringPair("Match All", "space separated, if all match then the Output Format and Options are applied"),
                          New StringPair("Output Format", "applied to the stream if Match All succeeds"),
                          New StringPair("Options", "applied to the stream if Match All succeeds")})
        f.Show()
    End Sub

    Sub ShowAudioStreamProfiles()
        Using f As New DataForm
            f.Text = "Audio Stream Profiles"
            f.FormBorderStyle = FormBorderStyle.Sizable
            f.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            f.dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            f.dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            f.dgv.AllowUserToDeleteRows = True
            f.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            f.HelpAction = AddressOf ShowAudioStreamProfilesHelp

            Dim match = f.dgv.AddTextBoxColumn()
            match.DataPropertyName = "Match"
            match.HeaderText = "Match All"

            Dim out = f.dgv.AddComboBoxColumn()
            out.DataPropertyName = "Output"
            out.HeaderText = "Output Format"
            out.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            out.Items.AddRange(AudioOutputFormats)

            Dim opt = f.dgv.AddTextBoxColumn()
            opt.DataPropertyName = "Options"
            opt.HeaderText = "Options"

            f.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            Dim bs As New BindingSource

            bs.DataSource = ObjectHelp.GetCopy(s.eac3toProfiles)
            f.dgv.DataSource = bs

            If f.ShowDialog = DialogResult.OK Then
                s.eac3toProfiles = DirectCast(bs.DataSource, List(Of eac3toProfile))
            End If
        End Using
    End Sub

    Sub StartAnalyze()
        Dim args = ""

        If File.Exists(M2TSFile) Then
            args = M2TSFile.Escape + " -progressnumbers"
            Project.Log.Write("Process M2TS file using eac3to", Package.eac3to.Path.Escape + " " + args + BR2)
        ElseIf Directory.Exists(PlaylistFolder) Then
            args = PlaylistFolder.Escape + " " & PlaylistID & ") -progressnumbers"
            Project.Log.Write("Process playlist file using eac3to", Package.eac3to.Path.Escape + " " + args + BR2)
        End If

        Using o As New Process
            AddHandler o.OutputDataReceived, AddressOf OutputDataReceived
            o.StartInfo.FileName = Package.eac3to.Path
            o.StartInfo.Arguments = args
            o.StartInfo.CreateNoWindow = True
            o.StartInfo.UseShellExecute = False
            o.StartInfo.RedirectStandardOutput = True
            o.Start()
            o.BeginOutputReadLine()
            o.WaitForExit()

            If o.ExitCode <> 0 Then
                Dim exitCode = o.ExitCode

                BeginInvoke(Sub()
                                MsgError("eac3to failed with error code " & exitCode, Output)
                                Cancel()
                            End Sub)
            Else
                BeginInvoke(Sub() Init())
            End If
        End Using
    End Sub

    Sub Cancel()
        For Each ctrl As Control In Controls
            ctrl.Enabled = True 'bnCancel is child of tlp
        Next

        bnCancel.PerformClick()
    End Sub

    Sub OutputDataReceived(sender As Object, e As DataReceivedEventArgs)
        If Not e.Data Is Nothing Then
            BeginInvoke(Sub() Text = e.Data)
            If Not e.Data.StartsWith("analyze: ") Then Output += e.Data + BR
        End If
    End Sub

    Sub Init()
        Text = "eac3to"

        For Each ctrl As Control In Controls
            ctrl.Enabled = True
        Next

        If Output = "" Then
            MsgWarn("eac3to output was empty")
            Cancel()
        ElseIf Output.ContainsAll({"left eye", "right eye"}) Then
            MsgError("3D demuxing isn't supported.")
            Cancel()
        ElseIf Output <> "" Then
            Project.Log.WriteLine(Output)

            If Output.Contains(BR + "   (embedded: ") Then
                Output = Output.Replace(BR + "   (embedded: ", "(embedded: ")
            End If

            While Output.Contains("  (embedded: ")
                Output = Output.Replace("  (embedded: ", " (embedded: ")
            End While

            If Output.Contains(BR + "   (core: ") Then
                Output = Output.Replace(BR + "   (core: ", "(core: ")
            End If

            While Output.Contains("  (core: ")
                Output = Output.Replace("  (core: ", " (core: ")
            End While

            Output = Output.Replace(" channels, ", "ch, ").Replace(" bits, ", "bits, ").Replace("dialnorm", "dn")
            Output = Output.Replace("(core: ", "(").Replace("(embedded: ", "(")

            For Each line In Output.SplitLinesNoEmpty
                If line.Contains("Subtitle (DVB)") Then Continue For

                Dim match = Regex.Match(line, "^(\d+): (.+)$")

                If match.Success Then
                    Dim ms As New M2TSStream
                    ms.Text = line.Trim
                    ms.ID = match.Groups(1).Value.ToInt
                    ms.Codec = match.Groups(2).Value

                    If ms.Codec.Contains(",") Then ms.Codec = ms.Codec.Left(",")

                    ms.IsVideo = ms.Codec.EqualsAny("h264/AVC", "h265/HEVC", "VC-1", "MPEG2")
                    ms.IsAudio = ms.Codec.EqualsAny("DTS Master Audio", "DTS", "DTS-ES", "DTS Hi-Res", "DTS Express", "AC3", "AC3 EX", "AC3 Headphone", "AC3 Surround", "EAC3", "E-AC3", "E-AC3 EX", "E-AC3 Surround", "TrueHD/AC3", "TrueHD/AC3 (Atmos)", "TrueHD (Atmos)", "RAW/PCM", "MP2", "AAC")
                    ms.IsSubtitle = ms.Codec.StartsWith("Subtitle")
                    ms.IsChapters = ms.Codec.StartsWith("Chapters")

                    If ms.IsAudio OrElse ms.IsSubtitle Then
                        For Each i2 In Language.Languages
                            If ms.Text.Contains(", " + i2.CultureInfo.EnglishName) Then
                                ms.Language = i2
                                Exit For
                            End If
                        Next

                        Select Case ms.Codec
                            Case "AC3 EX", "AC3 Surround", "AC3 Headphone"
                                ms.OutputType = "ac3"
                            Case "E-AC3", "E-AC3 EX"
                                ms.OutputType = "eac3"
                            Case "TrueHD/AC3 (Atmos)", "TrueHD/AC3"
                                ms.OutputType = "thd+ac3"
                            Case "TrueHD (Atmos)"
                                ms.OutputType = "thd"
                            Case "DTS-ES", "DTS Express"
                                ms.OutputType = "dts"
                            Case "DTS Master Audio"
                                ms.OutputType = "dtsma"
                            Case "DTS Hi-Res"
                                ms.OutputType = "dtshr"
                            Case "RAW/PCM"
                                ms.OutputType = "flac"
                            Case Else
                                ms.OutputType = ms.Codec.ToLower.Replace("-", "")
                        End Select
                    End If

                    For Each iProfile In s.eac3toProfiles
                        Dim searchWords = iProfile.Match.SplitNoEmptyAndWhiteSpace(" ")
                        If searchWords.NothingOrEmpty Then Continue For

                        If ms.Text.ContainsAll(searchWords) Then
                            ms.OutputType = iProfile.Output
                            ms.Options = iProfile.Options
                        End If
                    Next

                    If Not ms.IsVideo AndAlso Not ms.IsAudio AndAlso
                        Not ms.IsSubtitle AndAlso Not ms.IsChapters Then

                        Throw New Exception("Failed to detect stream: " + line)
                    End If

                    Streams.Add(ms)
                End If
            Next

            For Each stream In Streams
                If stream.IsAudio Then
                    stream.ListViewItem = lvAudio.Items.Add(stream.ToString)
                    stream.ListViewItem.Tag = stream

                    If stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName Then
                        bnAudioNative.Visible = True
                        stream.ListViewItem.Checked = True
                    ElseIf stream.Language.TwoLetterCode = "en" Then
                        bnAudioEnglish.Visible = True
                        stream.ListViewItem.Checked = True
                    ElseIf stream.Language.TwoLetterCode = "iv" Then
                        stream.ListViewItem.Checked = True
                    End If
                ElseIf stream.IsVideo Then
                    cbVideoStream.Items.Add(stream)
                ElseIf stream.IsSubtitle Then
                    If stream.Language.CultureInfo.TwoLetterISOLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName Then
                        bnSubtitleNative.Visible = True
                    ElseIf stream.Language.CultureInfo.TwoLetterISOLanguageName = "en" Then
                        bnSubtitleEnglish.Visible = True
                    End If

                    Dim item = lvSubtitles.Items.Add(stream.Language.ToString)
                    item.Tag = stream

                    Dim autoCode = Project.PreferredSubtitles.ToLower.SplitNoEmptyAndWhiteSpace(",", ";", " ")
                    item.Checked = autoCode.ContainsAny("all", stream.Language.TwoLetterCode, stream.Language.ThreeLetterCode)
                ElseIf stream.IsChapters Then
                    cbChapters.Visible = True
                End If
            Next

            If cbVideoStream.Items.Count < 2 Then cbVideoStream.Enabled = False

            If cbVideoStream.Items.Count > 0 Then
                cbVideoStream.SelectedIndex = 0
            Else
                cbVideoOutput.Enabled = False
            End If

            If lvAudio.Items.Count > 0 Then
                lvAudio.Items(0).Selected = True
            Else
                gbAudio.Enabled = False
            End If

            If lvSubtitles.Items.Count = 0 Then
                lvSubtitles.Enabled = False
                flpSubtitleLinks.Enabled = False
            End If

            lvAudio.Columns(0).Width = lvAudio.ClientSize.Width
        End If
    End Sub

    Private Sub AddCmdlControl_PresetsChanged(presets As String) Handles cmdlOptions.PresetsChanged
        cmdlOptions.Presets = presets
    End Sub

    Function GetArgs(src As String, baseName As String) As String
        Dim r = src

        Dim videoStream = TryCast(cbVideoStream.SelectedItem, M2TSStream)

        If Not videoStream Is Nothing AndAlso Not cbVideoOutput.Text = "Nothing" Then
            r += " " & videoStream.ID & ": " + (OutputFolder + baseName +
                "." + cbVideoOutput.Text.ToLower).Escape
        End If

        For Each i In Streams
            If i.IsAudio AndAlso i.Checked Then
                r += " " & i.ID & ": """ + OutputFolder + baseName + " ID" & i.ID

                If i.Language.CultureInfo.TwoLetterISOLanguageName <> "iv" Then
                    r += " " + i.Language.CultureInfo.EnglishName
                End If

                r += "." + i.OutputType + """"

                If i.Options <> "" Then r += " " + i.Options.Trim
            End If
        Next

        For Each i In Streams
            If i.IsSubtitle AndAlso i.Checked Then
                r += " " & i.ID & ": """ + OutputFolder + baseName + " ID" & i.ID

                If i.Language.CultureInfo.TwoLetterISOLanguageName <> "iv" Then
                    r += " " + i.Language.CultureInfo.EnglishName
                End If

                r += ".sup"""
            End If

            If i.IsChapters AndAlso cbChapters.Checked Then
                r += " " & i.ID & ": """ + OutputFolder + baseName + "_chapters.txt"""
            End If
        Next

        Return r + " -progressnumbers"
    End Function

    Private Sub bnBrowse_Click() Handles bnBrowse.Click
        Using d As New FolderBrowserDialog
            d.SetSelectedPath(teTempDir.Text)
            If d.ShowDialog = DialogResult.OK Then teTempDir.Text = d.SelectedPath
        End Using
    End Sub

    Private Sub lvSubtitles_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvSubtitles.ItemCheck
        DirectCast(lvSubtitles.Items(e.Index).Tag, M2TSStream).Checked = e.NewValue = CheckState.Checked
    End Sub

    Private Sub cbVideoStream_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVideoStream.SelectedIndexChanged
        cbVideoOutput.Items.Clear()
        cbVideoOutput.Items.Add("Nothing")
        cbVideoOutput.Enabled = True

        Dim stream = TryCast(cbVideoStream.SelectedItem, M2TSStream)

        Select Case stream.Codec
            Case "h264/AVC"
                cbVideoOutput.Items.Add("H264")
                cbVideoOutput.Items.Add("MKV")
                cbVideoOutput.Text = If(M2TSFile = "", "MKV", "Nothing")
            Case "h265/HEVC"
                cbVideoOutput.Items.Add("H265")
                cbVideoOutput.Text = If(M2TSFile = "", "MKV", "Nothing")
            Case "VC-1"
                cbVideoOutput.Items.Add("MKV")
                cbVideoOutput.Text = If(M2TSFile = "", "MKV", "Nothing")
            Case "MPEG2"
                cbVideoOutput.Items.Add("M2V")
                cbVideoOutput.Items.Add("MKV")
                cbVideoOutput.Text = If(M2TSFile = "", "M2V", "Nothing")
        End Select
    End Sub

    Private Sub cbAudioOutput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAudioOutput.SelectedIndexChanged
        If Not cbAudioOutput.SelectedItem Is Nothing AndAlso Not GetSelectedStream() Is Nothing Then
            Dim ms = GetSelectedStream()
            ms.OutputType = cbAudioOutput.SelectedItem.ToString
            ms.ListViewItem.Text = ms.ToString

            If ms.OutputType = "dts" AndAlso {"DTS Master Audio", "DTS Hi-Res"}.Contains(ms.Codec) AndAlso Not ms.Options.Contains("-core") Then
                If ms.Options = "" Then ms.Options = "-core"
                ms.UpdateListViewItem()
                cmdlOptions.tb.Text = ms.Options
            ElseIf {"dtsma", "dtshr"}.Contains(ms.OutputType) AndAlso ms.Options.Contains("-core") Then
                ms.Options = ms.Options.Replace(" -core ", "").Replace(" -core", "").Replace("-core ", "").Replace("-core", "")
                ms.UpdateListViewItem()
                cmdlOptions.tb.Text = ms.Options
            ElseIf ms.OutputType <> "dts" AndAlso ms.Options.Contains("-core") Then
                ms.Options = ms.Options.Replace(" -core ", "").Replace(" -core", "").Replace("-core ", "").Replace("-core", "")
                ms.UpdateListViewItem()
                cmdlOptions.tb.Text = ms.Options
            End If
        End If
    End Sub

    Private Sub cmdlOptions_ValueChanged(value As String) Handles cmdlOptions.ValueChanged
        If cmdlOptions.tb.Focused OrElse cmdlOptions.bu.Focused Then
            Dim ms = GetSelectedStream()

            If Not ms Is Nothing Then
                ms.Options = value

                If ms.Options <> "" Then
                    ms.ListViewItem.Checked = True
                End If

                If ms.Options = "-core" AndAlso ms.Codec.StartsWith("DTS") Then
                    cbAudioOutput.SelectedItem = "dts"
                End If

                If ms.Options.Contains("-quality=") Then
                    cbAudioOutput.SelectedItem = "m4a"
                End If

                lvAudio.SelectedItems(0).Text = ms.ToString
            End If
        End If
    End Sub

    Private Sub lvAudio_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvAudio.ItemCheck
        DirectCast(lvAudio.Items(e.Index).Tag, M2TSStream).Checked = e.NewValue = CheckState.Checked
    End Sub

    Function GetSelectedStream() As M2TSStream
        If lvAudio.SelectedItems.Count > 0 Then Return DirectCast(lvAudio.SelectedItems(0).Tag, M2TSStream)
    End Function

    Private Sub lvAudio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvAudio.SelectedIndexChanged
        Dim ms = GetSelectedStream()

        If Not ms Is Nothing Then
            cmdlOptions.tb.Text = ms.Options
            cbAudioOutput.SelectedItem = ms.OutputType
        End If
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
            Dim stream = DirectCast(i.Tag, M2TSStream)

            If stream.Language.TwoLetterCode = "en" Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub bnAudioNative_Click(sender As Object, e As EventArgs) Handles bnAudioNative.Click
        For Each i As ListViewItem In lvAudio.Items
            Dim stream = DirectCast(i.Tag, M2TSStream)

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
            Dim stream = DirectCast(i.Tag, M2TSStream)

            If stream.Language.TwoLetterCode = "en" Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub bnSubtitleNative_Click(sender As Object, e As EventArgs) Handles bnSubtitleNative.Click
        For Each i As ListViewItem In lvSubtitles.Items
            Dim stream = DirectCast(i.Tag, M2TSStream)

            If stream.Language.TwoLetterCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName Then
                i.Checked = True
            End If
        Next
    End Sub

    Private Sub teTempDir_TextChanged() Handles teTempDir.TextChanged
        OutputFolder = teTempDir.Text.FixDir
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Dim hdCounter As Integer

        For Each i In Streams
            If i.Checked AndAlso i.IsAudio Then
                If {"dtsma", "dtshr"}.Contains(i.OutputType) Then
                    hdCounter += 1
                ElseIf i.OutputType = "dts" AndAlso {"DTS Master Audio", "DTS Hi-Res"}.Contains(i.Codec) Then
                    hdCounter -= 1
                End If
            End If
        Next

        s.CmdlPresetsEac3to = cmdlOptions.Presets

        If Not bnOK.Enabled Then e.Cancel = True

        If DialogResult = DialogResult.OK Then
            If cbVideoOutput.Text = "MKV" AndAlso Not Package.Haali.VerifyOK(True) Then
                e.Cancel = True
            End If

            s.Storage.SetBool("demux Blu-ray chapters", cbChapters.Checked)
        End If

        MyBase.OnFormClosing(e)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        Task.Run(AddressOf StartAnalyze)
        MyBase.OnShown(e)
    End Sub
End Class