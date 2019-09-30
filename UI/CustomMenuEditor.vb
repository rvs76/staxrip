Imports System.Reflection

Namespace UI
    Public Class CustomMenuEditor
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

        Private components As System.ComponentModel.IContainer

        Public WithEvents lParameters As System.Windows.Forms.Label
        Public WithEvents pg As PropertyGridEx
        Public WithEvents lText As System.Windows.Forms.Label
        Public WithEvents lHotkey As System.Windows.Forms.Label
        Private WithEvents tbText As System.Windows.Forms.TextBox
        Friend WithEvents TipProvider As StaxRip.UI.TipProvider
        Friend WithEvents tv As StaxRip.UI.TreeViewEx
        Friend WithEvents tbHotkey As System.Windows.Forms.TextBox
        Friend WithEvents tbCommand As System.Windows.Forms.TextBox
        Friend WithEvents bnCommand As ButtonEx
        Friend WithEvents cmsCommand As ContextMenuStripEx
        Friend WithEvents lCommand As System.Windows.Forms.Label
        Friend WithEvents bnCancel As StaxRip.UI.ButtonEx
        Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents Label1 As Label
        Friend WithEvents tlpSymbol As TableLayoutPanel
        Friend WithEvents laSymbol As Label
        Friend WithEvents pbSymbol As PictureBox
        Friend WithEvents bnSymbol As ButtonEx
        Friend WithEvents cmsSymbol As ContextMenuStrip
        Friend WithEvents ToolStrip As ToolStrip
        Friend WithEvents tsbNew As ToolStripButton
        Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
        Friend WithEvents tsbCut As ToolStripButton
        Friend WithEvents tsbCopy As ToolStripButton
        Friend WithEvents tsbPaste As ToolStripButton
        Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
        Friend WithEvents tsbMoveLeft As ToolStripButton
        Friend WithEvents tsbMoveRight As ToolStripButton
        Friend WithEvents tsbMoveUp As ToolStripButton
        Friend WithEvents tsbMoveDown As ToolStripButton
        Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
        Friend WithEvents tsbRemove As ToolStripButton
        Friend WithEvents ToolsToolStripDropDownButton As ToolStripDropDownButton
        Friend WithEvents NewFromDefaultsToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ResetToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents bnOK As StaxRip.UI.ButtonEx
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.lHotkey = New System.Windows.Forms.Label()
            Me.lParameters = New System.Windows.Forms.Label()
            Me.pg = New StaxRip.UI.PropertyGridEx()
            Me.tbText = New System.Windows.Forms.TextBox()
            Me.lText = New System.Windows.Forms.Label()
            Me.tv = New StaxRip.UI.TreeViewEx()
            Me.TipProvider = New StaxRip.UI.TipProvider(Me.components)
            Me.tbHotkey = New System.Windows.Forms.TextBox()
            Me.tbCommand = New System.Windows.Forms.TextBox()
            Me.bnCommand = New StaxRip.UI.ButtonEx()
            Me.cmsCommand = New StaxRip.UI.ContextMenuStripEx(Me.components)
            Me.lCommand = New System.Windows.Forms.Label()
            Me.bnCancel = New StaxRip.UI.ButtonEx()
            Me.bnOK = New StaxRip.UI.ButtonEx()
            Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
            Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
            Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
            Me.ToolStrip = New System.Windows.Forms.ToolStrip()
            Me.tsbNew = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
            Me.tsbCut = New System.Windows.Forms.ToolStripButton()
            Me.tsbCopy = New System.Windows.Forms.ToolStripButton()
            Me.tsbPaste = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
            Me.tsbMoveLeft = New System.Windows.Forms.ToolStripButton()
            Me.tsbMoveRight = New System.Windows.Forms.ToolStripButton()
            Me.tsbMoveUp = New System.Windows.Forms.ToolStripButton()
            Me.tsbMoveDown = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.tsbRemove = New System.Windows.Forms.ToolStripButton()
            Me.ToolsToolStripDropDownButton = New System.Windows.Forms.ToolStripDropDownButton()
            Me.NewFromDefaultsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.tlpSymbol = New System.Windows.Forms.TableLayoutPanel()
            Me.laSymbol = New System.Windows.Forms.Label()
            Me.pbSymbol = New System.Windows.Forms.PictureBox()
            Me.bnSymbol = New StaxRip.UI.ButtonEx()
            Me.cmsSymbol = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tlpMain.SuspendLayout()
            Me.FlowLayoutPanel1.SuspendLayout()
            Me.TableLayoutPanel2.SuspendLayout()
            Me.ToolStrip.SuspendLayout()
            Me.tlpSymbol.SuspendLayout()
            CType(Me.pbSymbol, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lHotkey
            '
            Me.lHotkey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lHotkey.AutoSize = True
            Me.lHotkey.Location = New System.Drawing.Point(686, 224)
            Me.lHotkey.Margin = New System.Windows.Forms.Padding(0, 20, 0, 0)
            Me.lHotkey.Name = "lHotkey"
            Me.lHotkey.Size = New System.Drawing.Size(676, 48)
            Me.lHotkey.TabIndex = 5
            Me.lHotkey.Text = "Shortcut Key:"
            '
            'lParameters
            '
            Me.lParameters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lParameters.AutoSize = True
            Me.lParameters.Location = New System.Drawing.Point(686, 647)
            Me.lParameters.Margin = New System.Windows.Forms.Padding(0, 20, 0, 0)
            Me.lParameters.Name = "lParameters"
            Me.lParameters.Size = New System.Drawing.Size(676, 48)
            Me.lParameters.TabIndex = 9
            Me.lParameters.Text = "Command Parameters:"
            Me.lParameters.Visible = False
            '
            'pg
            '
            Me.pg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pg.HelpVisible = False
            Me.pg.LineColor = System.Drawing.SystemColors.ScrollBar
            Me.pg.Location = New System.Drawing.Point(686, 695)
            Me.pg.Margin = New System.Windows.Forms.Padding(0)
            Me.pg.Name = "pg"
            Me.pg.PropertySort = System.Windows.Forms.PropertySort.NoSort
            Me.pg.Size = New System.Drawing.Size(676, 364)
            Me.pg.TabIndex = 10
            Me.pg.ToolbarVisible = False
            Me.pg.Visible = False
            '
            'tbText
            '
            Me.tbText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbText.Location = New System.Drawing.Point(686, 149)
            Me.tbText.Margin = New System.Windows.Forms.Padding(0)
            Me.tbText.Name = "tbText"
            Me.tbText.Size = New System.Drawing.Size(676, 55)
            Me.tbText.TabIndex = 4
            '
            'lText
            '
            Me.lText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lText.AutoSize = True
            Me.lText.Location = New System.Drawing.Point(686, 101)
            Me.lText.Margin = New System.Windows.Forms.Padding(0)
            Me.lText.Name = "lText"
            Me.lText.Size = New System.Drawing.Size(676, 48)
            Me.lText.TabIndex = 3
            Me.lText.Text = "Text:"
            '
            'tv
            '
            Me.tv.AllowDrop = True
            Me.tv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tv.HideSelection = False
            Me.tv.Location = New System.Drawing.Point(10, 101)
            Me.tv.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
            Me.tv.Name = "tv"
            Me.tlpMain.SetRowSpan(Me.tv, 10)
            Me.tv.Size = New System.Drawing.Size(666, 958)
            Me.tv.TabIndex = 2
            '
            'tbHotkey
            '
            Me.tbHotkey.AcceptsReturn = True
            Me.tbHotkey.AcceptsTab = True
            Me.tbHotkey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbHotkey.Location = New System.Drawing.Point(686, 272)
            Me.tbHotkey.Margin = New System.Windows.Forms.Padding(0)
            Me.tbHotkey.Name = "tbHotkey"
            Me.tbHotkey.Size = New System.Drawing.Size(676, 55)
            Me.tbHotkey.TabIndex = 6
            '
            'tbCommand
            '
            Me.tbCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbCommand.Location = New System.Drawing.Point(0, 7)
            Me.tbCommand.Margin = New System.Windows.Forms.Padding(0)
            Me.tbCommand.Name = "tbCommand"
            Me.tbCommand.Size = New System.Drawing.Size(596, 55)
            Me.tbCommand.TabIndex = 8
            '
            'bnCommand
            '
            Me.bnCommand.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.bnCommand.Location = New System.Drawing.Point(606, 0)
            Me.bnCommand.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.bnCommand.ShowMenuSymbol = True
            Me.bnCommand.Size = New System.Drawing.Size(70, 70)
            '
            'cmsCommand
            '
            Me.cmsCommand.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.cmsCommand.ImageScalingSize = New System.Drawing.Size(24, 24)
            Me.cmsCommand.Name = "cmsCommand"
            Me.cmsCommand.Size = New System.Drawing.Size(61, 4)
            '
            'lCommand
            '
            Me.lCommand.AutoSize = True
            Me.lCommand.Location = New System.Drawing.Point(686, 509)
            Me.lCommand.Margin = New System.Windows.Forms.Padding(0, 20, 0, 0)
            Me.lCommand.Name = "lCommand"
            Me.lCommand.Size = New System.Drawing.Size(192, 48)
            Me.lCommand.TabIndex = 7
            Me.lCommand.Text = "Command:"
            '
            'bnCancel
            '
            Me.bnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.bnCancel.Location = New System.Drawing.Point(260, 0)
            Me.bnCancel.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
            Me.bnCancel.Size = New System.Drawing.Size(250, 70)
            Me.bnCancel.Text = "Cancel"
            '
            'bnOK
            '
            Me.bnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.bnOK.Location = New System.Drawing.Point(0, 0)
            Me.bnOK.Margin = New System.Windows.Forms.Padding(0)
            Me.bnOK.Size = New System.Drawing.Size(250, 70)
            Me.bnOK.Text = "OK"
            '
            'ImageList1
            '
            Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
            Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
            Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 4
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
            Me.tlpMain.Controls.Add(Me.FlowLayoutPanel1, 2, 11)
            Me.tlpMain.Controls.Add(Me.pg, 2, 10)
            Me.tlpMain.Controls.Add(Me.lParameters, 2, 9)
            Me.tlpMain.Controls.Add(Me.TableLayoutPanel2, 2, 8)
            Me.tlpMain.Controls.Add(Me.lCommand, 2, 7)
            Me.tlpMain.Controls.Add(Me.ToolStrip, 0, 0)
            Me.tlpMain.Controls.Add(Me.tbHotkey, 2, 4)
            Me.tlpMain.Controls.Add(Me.tv, 1, 1)
            Me.tlpMain.Controls.Add(Me.lHotkey, 2, 3)
            Me.tlpMain.Controls.Add(Me.tbText, 2, 2)
            Me.tlpMain.Controls.Add(Me.lText, 2, 1)
            Me.tlpMain.Controls.Add(Me.Label1, 2, 5)
            Me.tlpMain.Controls.Add(Me.tlpSymbol, 2, 6)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 12
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.Size = New System.Drawing.Size(1372, 1149)
            Me.tlpMain.TabIndex = 11
            '
            'FlowLayoutPanel1
            '
            Me.FlowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.FlowLayoutPanel1.AutoSize = True
            Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel1.Controls.Add(Me.bnOK)
            Me.FlowLayoutPanel1.Controls.Add(Me.bnCancel)
            Me.FlowLayoutPanel1.Location = New System.Drawing.Point(852, 1069)
            Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0, 10, 0, 10)
            Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
            Me.FlowLayoutPanel1.Size = New System.Drawing.Size(510, 70)
            Me.FlowLayoutPanel1.TabIndex = 12
            '
            'TableLayoutPanel2
            '
            Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel2.AutoSize = True
            Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel2.ColumnCount = 2
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel2.Controls.Add(Me.bnCommand, 1, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.tbCommand, 0, 0)
            Me.TableLayoutPanel2.Location = New System.Drawing.Point(686, 557)
            Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
            Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
            Me.TableLayoutPanel2.RowCount = 1
            Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel2.Size = New System.Drawing.Size(676, 70)
            Me.TableLayoutPanel2.TabIndex = 12
            '
            'ToolStrip
            '
            Me.ToolStrip.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ToolStrip.AutoSize = False
            Me.tlpMain.SetColumnSpan(Me.ToolStrip, 4)
            Me.ToolStrip.Dock = System.Windows.Forms.DockStyle.None
            Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
            Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.ToolStripSeparator3, Me.tsbCut, Me.tsbCopy, Me.tsbPaste, Me.ToolStripSeparator4, Me.tsbMoveLeft, Me.tsbMoveRight, Me.tsbMoveUp, Me.tsbMoveDown, Me.ToolStripSeparator1, Me.tsbRemove, Me.ToolsToolStripDropDownButton})
            Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
            Me.ToolStrip.Name = "ToolStrip"
            Me.ToolStrip.Padding = New System.Windows.Forms.Padding(7, 2, 2, 0)
            Me.ToolStrip.Size = New System.Drawing.Size(1372, 91)
            Me.ToolStrip.TabIndex = 1
            Me.ToolStrip.Text = "ToolStrip"
            '
            'tsbNew
            '
            Me.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbNew.Name = "tsbNew"
            Me.tsbNew.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbNew.Size = New System.Drawing.Size(24, 86)
            Me.tsbNew.Text = "New"
            '
            'ToolStripSeparator3
            '
            Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
            Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 89)
            '
            'tsbCut
            '
            Me.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbCut.Name = "tsbCut"
            Me.tsbCut.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbCut.Size = New System.Drawing.Size(24, 86)
            Me.tsbCut.Text = "Cut"
            '
            'tsbCopy
            '
            Me.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbCopy.Name = "tsbCopy"
            Me.tsbCopy.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbCopy.Size = New System.Drawing.Size(24, 86)
            Me.tsbCopy.Text = "Copy"
            '
            'tsbPaste
            '
            Me.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbPaste.Name = "tsbPaste"
            Me.tsbPaste.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbPaste.Size = New System.Drawing.Size(24, 86)
            Me.tsbPaste.Text = "Paste"
            '
            'ToolStripSeparator4
            '
            Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
            Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 89)
            '
            'tsbMoveLeft
            '
            Me.tsbMoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbMoveLeft.Name = "tsbMoveLeft"
            Me.tsbMoveLeft.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbMoveLeft.Size = New System.Drawing.Size(24, 86)
            Me.tsbMoveLeft.ToolTipText = "Move Left"
            '
            'tsbMoveRight
            '
            Me.tsbMoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbMoveRight.Name = "tsbMoveRight"
            Me.tsbMoveRight.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbMoveRight.Size = New System.Drawing.Size(24, 86)
            Me.tsbMoveRight.ToolTipText = "Move Right"
            '
            'tsbMoveUp
            '
            Me.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbMoveUp.Name = "tsbMoveUp"
            Me.tsbMoveUp.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbMoveUp.Size = New System.Drawing.Size(24, 86)
            Me.tsbMoveUp.ToolTipText = "Move Up"
            '
            'tsbMoveDown
            '
            Me.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbMoveDown.Name = "tsbMoveDown"
            Me.tsbMoveDown.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbMoveDown.Size = New System.Drawing.Size(24, 86)
            Me.tsbMoveDown.ToolTipText = "Move Down"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 89)
            '
            'tsbRemove
            '
            Me.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbRemove.Name = "tsbRemove"
            Me.tsbRemove.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.tsbRemove.Size = New System.Drawing.Size(24, 86)
            Me.tsbRemove.Text = "Remove"
            '
            'ToolsToolStripDropDownButton
            '
            Me.ToolsToolStripDropDownButton.AutoToolTip = False
            Me.ToolsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolsToolStripDropDownButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewFromDefaultsToolStripMenuItem, Me.ResetToolStripMenuItem})
            Me.ToolsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolsToolStripDropDownButton.Name = "ToolsToolStripDropDownButton"
            Me.ToolsToolStripDropDownButton.Padding = New System.Windows.Forms.Padding(2)
            Me.ToolsToolStripDropDownButton.Size = New System.Drawing.Size(35, 86)
            Me.ToolsToolStripDropDownButton.Text = "Tools"
            '
            'NewFromDefaultsToolStripMenuItem
            '
            Me.NewFromDefaultsToolStripMenuItem.Name = "NewFromDefaultsToolStripMenuItem"
            Me.NewFromDefaultsToolStripMenuItem.Size = New System.Drawing.Size(476, 54)
            Me.NewFromDefaultsToolStripMenuItem.Text = "New From Defaults..."
            '
            'ResetToolStripMenuItem
            '
            Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
            Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(476, 54)
            Me.ResetToolStripMenuItem.Text = "Reset Everything"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(686, 347)
            Me.Label1.Margin = New System.Windows.Forms.Padding(0, 20, 0, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 48)
            Me.Label1.TabIndex = 13
            Me.Label1.Text = "Icon:"
            '
            'tlpSymbol
            '
            Me.tlpSymbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlpSymbol.ColumnCount = 3
            Me.tlpSymbol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpSymbol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpSymbol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpSymbol.Controls.Add(Me.laSymbol, 1, 0)
            Me.tlpSymbol.Controls.Add(Me.pbSymbol, 0, 0)
            Me.tlpSymbol.Controls.Add(Me.bnSymbol, 2, 0)
            Me.tlpSymbol.Location = New System.Drawing.Point(686, 395)
            Me.tlpSymbol.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpSymbol.Name = "tlpSymbol"
            Me.tlpSymbol.RowCount = 1
            Me.tlpSymbol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpSymbol.Size = New System.Drawing.Size(676, 94)
            Me.tlpSymbol.TabIndex = 14
            '
            'laSymbol
            '
            Me.laSymbol.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.laSymbol.Location = New System.Drawing.Point(98, 0)
            Me.laSymbol.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
            Me.laSymbol.Name = "laSymbol"
            Me.laSymbol.Size = New System.Drawing.Size(501, 94)
            Me.laSymbol.TabIndex = 1
            Me.laSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'pbSymbol
            '
            Me.pbSymbol.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pbSymbol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
            Me.pbSymbol.Location = New System.Drawing.Point(0, 0)
            Me.pbSymbol.Margin = New System.Windows.Forms.Padding(0)
            Me.pbSymbol.Name = "pbSymbol"
            Me.pbSymbol.Size = New System.Drawing.Size(91, 94)
            Me.pbSymbol.TabIndex = 2
            Me.pbSymbol.TabStop = False
            '
            'bnSymbol
            '
            Me.bnSymbol.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.bnSymbol.ContextMenuStrip = Me.cmsSymbol
            Me.bnSymbol.Location = New System.Drawing.Point(606, 12)
            Me.bnSymbol.Margin = New System.Windows.Forms.Padding(0)
            Me.bnSymbol.ShowMenuSymbol = True
            Me.bnSymbol.Size = New System.Drawing.Size(70, 70)
            '
            'cmsSymbol
            '
            Me.cmsSymbol.ImageScalingSize = New System.Drawing.Size(48, 48)
            Me.cmsSymbol.Name = "cmsSymbol"
            Me.cmsSymbol.Size = New System.Drawing.Size(61, 4)
            '
            'CustomMenuEditor
            '
            Me.AcceptButton = Me.bnOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(288.0!, 288.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.CancelButton = Me.bnCancel
            Me.ClientSize = New System.Drawing.Size(1372, 1149)
            Me.Controls.Add(Me.tlpMain)
            Me.KeyPreview = True
            Me.Margin = New System.Windows.Forms.Padding(13, 14, 13, 14)
            Me.Name = "CustomMenuEditor"
            Me.Text = "Menu Editor"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.FlowLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel2.ResumeLayout(False)
            Me.TableLayoutPanel2.PerformLayout()
            Me.ToolStrip.ResumeLayout(False)
            Me.ToolStrip.PerformLayout()
            Me.tlpSymbol.ResumeLayout(False)
            CType(Me.pbSymbol, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Block As Boolean
        Private GridTypeDescriptor As GridTypeDescriptor
        Private ClipboardNode As TreeNode
        Private IsClosing As Boolean

        Property GenericMenu As CustomMenu

        Sub New(menu As CustomMenu)
            MyBase.New()
            InitializeComponent()
            ScaleClientSize(32, 30)
            g.SetRenderer(ToolStrip)

            tsbNew.Image = ImageHelp.GetSymbolImage(Symbol.Page)
            tsbCopy.Image = ImageHelp.GetSymbolImage(Symbol.Copy)
            tsbCut.Image = ImageHelp.GetSymbolImage(Symbol.Cut)
            tsbPaste.Image = ImageHelp.GetSymbolImage(Symbol.Paste)
            tsbRemove.Image = ImageHelp.GetSymbolImage(Symbol.Delete)

            tsbMoveLeft.Image = ImageHelp.GetSymbolImage(Symbol.Back)
            tsbMoveUp.Image = ImageHelp.GetSymbolImage(Symbol.Up)
            tsbMoveRight.Image = ImageHelp.GetSymbolImage(Symbol.Forward)
            tsbMoveDown.Image = ImageHelp.GetSymbolImage(Symbol.Down)

            ToolsToolStripDropDownButton.Image = ImageHelp.GetSymbolImage(Symbol.More)

            GenericMenu = menu
            GridTypeDescriptor = New GridTypeDescriptor
            PopulateTreeView(menu.MenuItem.GetClone, Nothing)
            tv.ExpandAll()

            Command.PopulateCommandMenu(cmsCommand.Items, GenericMenu.CommandManager.Commands.Values.ToList, AddressOf SetCommand)

            g.SetRenderer(cmsSymbol)

            tv.SelectedNode = tv.Nodes(0)
            CancelButton = Nothing
            ToolStrip.ImageScalingSize = SystemInformation.SmallIconSize

            For Each i In ToolStrip.Items.OfType(Of ToolStripItem)()
                i.Image = i.Image.ResizeToSmallIconSize
            Next

            TipProvider.SetTip("Parameters used when the command is executed. Please make a feature request if useful parameters are missing.", pg, lParameters)
            TipProvider.SetTip("Text to be displayed. Enter minus to create a separator.", tbText, lText)
            TipProvider.SetTip("A key can be deleted by pressing it two times.", tbHotkey, lHotkey)
            TipProvider.SetTip("Command to be executed. Please make a feature request if useful commands are missing.", tbCommand, lCommand)
        End Sub

        Sub PopulateSymbolMenu()
            ActionMenuItem.Add(Of Symbol)(cmsSymbol.Items, "No Icon", AddressOf HandleSymbol, Symbol.None)

            Dim enumNames = System.Enum.GetNames(GetType(Symbol)).ToList
            enumNames.Sort()

            For Each iName In enumNames
                If iName.StartsWith("fa_") Then Continue For
                If IsClosing Then Exit For
                Dim symbol = DirectCast(System.Enum.Parse(GetType(Symbol), iName), Symbol)
                Dim path = "Segoe MDL2 Assets | " + iName.Substring(0, 1).ToUpper + " | " + iName
                ActionMenuItem.Add(Of Symbol)(cmsSymbol.Items, path, AddressOf HandleSymbol, symbol).SetImage(symbol)
                Application.DoEvents()
            Next

            For Each iName In enumNames
                If Not iName.StartsWith("fa_") Then Continue For
                If IsClosing Then Exit For
                Dim symbol = DirectCast(System.Enum.Parse(GetType(Symbol), iName), Symbol)
                Dim path = "FontAwesome | " + iName.Substring(3, 1).ToUpper + " | " + iName.Substring(3).ToTitleCase.Replace("_", " ")
                ActionMenuItem.Add(Of Symbol)(cmsSymbol.Items, path, AddressOf HandleSymbol, symbol).SetImage(symbol)
                Application.DoEvents()
            Next
        End Sub

        Sub HandleSymbol(symbol As Symbol)
            If Not Block AndAlso Not tv.SelectedNode Is Nothing Then
                Dim item = DirectCast(tv.SelectedNode.Tag, CustomMenuItem)
                item.Symbol = symbol
                UpdateControls()
            End If
        End Sub

        Private Sub SetCommand(c As Command)
            tbCommand.Text = c.MethodInfo.Name
        End Sub

        Private Sub tv_DragDrop(sender As Object, e As DragEventArgs) Handles tv.DragDrop
            Block = False
        End Sub

        Private Sub tv_DragEnter(sender As Object, e As DragEventArgs) Handles tv.DragEnter
            If e.Data.GetDataPresent(GetType(TreeNode)) Then
                e.Effect = DragDropEffects.Move
            End If
        End Sub

        Private Sub tv_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles tv.ItemDrag
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Block = True
                DoDragDrop(e.Item, DragDropEffects.Move)
            End If
        End Sub

        Private Sub tv_DragOver(sender As Object, e As DragEventArgs) Handles tv.DragOver
            If e.Data.GetDataPresent(GetType(TreeNode)) Then
                Dim node As TreeNode = CType(e.Data.GetData(GetType(TreeNode)), TreeNode)
                Dim destNode As TreeNode = tv.GetNodeAt(tv.PointToClient(New Point(e.X, e.Y)))
                Dim nodes As New ArrayList
                AddNodes(node, nodes)

                If Not destNode Is node AndAlso Not destNode Is Nothing AndAlso
                    Not nodes.Contains(destNode) Then

                    If Control.ModifierKeys = Keys.Control Then
                        If Not destNode.Nodes.Contains(node) AndAlso
                            Not destNode.Text = "-" Then

                            node.Remove()
                            destNode.Nodes.Add(node)
                            destNode.ExpandAll()
                            tv.SelectedNode = node
                        End If
                    Else
                        If Not destNode.Parent Is Nothing Then
                            If destNode.Bounds.Top < node.Bounds.Top Then
                                node.Remove()

                                destNode.Parent.Nodes.Insert(
                                    destNode.Parent.Nodes.IndexOf(destNode), node)

                                destNode.ExpandAll()
                                tv.SelectedNode = node
                            Else
                                node.Remove()

                                destNode.Parent.Nodes.Insert(
                                    destNode.Parent.Nodes.IndexOf(destNode) + 1, node)

                                destNode.ExpandAll()
                                tv.SelectedNode = node
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub UpdateControls()
            If Not Block Then
                Block = True
                Dim n As TreeNode = tv.SelectedNode
                If n Is Nothing Then Exit Sub
                Dim item = CType(n.Tag, CustomMenuItem)
                tbText.Text = item.Text

                If item.Symbol = Symbol.None Then
                    pbSymbol.BackgroundImage = Nothing
                Else
                    pbSymbol.BackgroundImage = ImageHelp.GetSymbolImage(item.Symbol)
                End If

                laSymbol.Text = If(item.Symbol = Symbol.None, "", item.Symbol.ToString)
                tbHotkey.Text = KeysHelp.GetKeyString(item.KeyData)
                Dim found As Boolean

                For Each i As Command In GenericMenu.CommandManager.Commands.Values
                    If i.MethodInfo.Name = item.MethodName Then
                        If tbCommand.Text = i.MethodInfo.Name Then
                            tbCommand_TextChanged()
                        Else
                            tbCommand.Text = i.MethodInfo.Name
                        End If

                        found = True
                    End If
                Next

                If Not found Then
                    tbCommand.Text = ""
                End If

                Dim notRoot = Not n.Parent Is Nothing

                bnSymbol.Enabled = notRoot
                bnCommand.Enabled = notRoot
                tbCommand.Enabled = notRoot
                tbHotkey.Enabled = notRoot
                tsbCopy.Enabled = notRoot
                tsbCut.Enabled = notRoot
                tsbMoveDown.Enabled = notRoot
                tsbMoveLeft.Enabled = notRoot
                tsbMoveRight.Enabled = notRoot
                tsbMoveUp.Enabled = notRoot
                tsbPaste.Enabled = Not ClipboardNode Is Nothing
                tsbRemove.Enabled = notRoot
                tbText.Enabled = notRoot

                Block = False
            End If
        End Sub

        Private Sub tbText_TextChanged() Handles tbText.TextChanged
            If Not Block AndAlso Not tv.SelectedNode Is Nothing Then
                Dim item = DirectCast(tv.SelectedNode.Tag, CustomMenuItem)

                tbCommand.Enabled = tbText.Text <> "-"
                bnCommand.Enabled = tbText.Text <> "-"
                tbHotkey.Enabled = tbText.Text <> "-"

                If tbText.Text = "-" Then
                    tbCommand.Text = ""
                    item.KeyData = Keys.None
                End If

                item.Text = tbText.Text
                tv.SelectedNode.Text = item.Text
            End If
        End Sub

        Private Sub PopulateGrid(item As CustomMenuItem)
            GridTypeDescriptor.Items.Clear()

            If GenericMenu.CommandManager.HasCommand(item.MethodName) Then
                Dim c = GenericMenu.CommandManager.GetCommand(item.MethodName)

                If Not c.MethodInfo Is Nothing Then
                    Dim params = c.MethodInfo.GetParameters

                    For i = 0 To params.Length - 1
                        Dim gp As New GridProperty
                        gp.Name = DispNameAttribute.GetValue(params(i).GetCustomAttributes(False))
                        If gp.Name Is Nothing Then gp.Name = params(i).Name.ToTitleCase
                        gp.Value = c.FixParameters(item.Parameters)(i)
                        gp.Description = DescriptionAttributeHelp.GetDescription(params(i).GetCustomAttributes(False))
                        gp.TypeEditor = EditorAttributeHelp.GetEditor(params(i).GetCustomAttributes(False))

                        GridTypeDescriptor.Add(gp)
                    Next
                End If
            End If

            pg.SelectedObject = GridTypeDescriptor
        End Sub

        Private Sub pg_PropertyValueChanged() Handles pg.PropertyValueChanged
            If Not tv.SelectedNode Is Nothing Then
                Dim item = DirectCast(tv.SelectedNode.Tag, CustomMenuItem)
                item.Parameters.Clear()

                For Each i As DictionaryEntry In GridTypeDescriptor.Items
                    item.Parameters.Add(DirectCast(i.Value, GridProperty).Value)
                Next
            End If
        End Sub

        Private Sub AddNodes(node As TreeNode, list As ArrayList)
            For Each i As TreeNode In node.Nodes
                list.Add(i)
                AddNodes(i, list)
            Next
        End Sub

        Private Sub PopulateTreeView(item As CustomMenuItem, node As TreeNode)
            Dim newNode As New TreeNode(item.Text)
            newNode.Tag = item

            If node Is Nothing Then
                tv.Nodes.Add(newNode)
            Else
                node.Nodes.Add(newNode)
            End If

            For Each i As CustomMenuItem In item.SubItems
                PopulateTreeView(i, newNode)
            Next
        End Sub

        Function GetState() As CustomMenuItem
            Dim item = DirectCast(tv.Nodes(0).Tag, CustomMenuItem)
            BuildState(item, tv.Nodes(0))
            Return item
        End Function

        Private Sub BuildState(item As CustomMenuItem, node As TreeNode)
            item.SubItems.Clear()

            For Each i As TreeNode In node.Nodes
                Dim subItem As CustomMenuItem = CType(i.Tag, CustomMenuItem)
                item.SubItems.Add(subItem)
                BuildState(subItem, i)
            Next
        End Sub

        Protected Overridable Function GetTemplateForm(item As CustomMenuItem) As MenuTemplateForm
            Return New MenuTemplateForm(item)
        End Function

        Private Sub NewFromDefaultsToolStripMenuItem_Click() Handles NewFromDefaultsToolStripMenuItem.Click
            If Not tv.SelectedNode Is Nothing Then
                Dim f = GetTemplateForm(GenericMenu.DefaultMenu.Invoke)

                If f.ShowDialog = DialogResult.OK Then
                    Dim n = DirectCast(f.TreeNode.Clone, TreeNode)
                    tv.SelectedNode.Nodes.Add(n)
                    tv.SelectedNode.ExpandAll()
                    tv.SelectedNode = n
                End If
            End If
        End Sub

        Private Sub RemoveSelectedItem()
            If Not tv.SelectedNode Is Nothing AndAlso
                Not tv.SelectedNode.Parent Is Nothing Then

                tv.SelectedNode.Remove()
            End If
        End Sub

        Private Sub tv_KeyUp(sender As Object, e As KeyEventArgs) Handles tv.KeyUp
            UpdateControls()
        End Sub

        Private Sub tbHotkey_KeyDown(sender As Object, e As KeyEventArgs) Handles tbHotkey.KeyDown
            If Not Block AndAlso Not tv.SelectedNode Is Nothing AndAlso
                Not e.KeyCode = Keys.ControlKey AndAlso
                Not e.KeyCode = Keys.Menu AndAlso
                Not e.KeyCode = Keys.ShiftKey Then

                Dim item = DirectCast(tv.SelectedNode.Tag, CustomMenuItem)

                If item.KeyData = e.KeyData Then
                    item.KeyData = Keys.None
                    tbHotkey.Text = KeysHelp.GetKeyString(item.KeyData)
                Else
                    item.KeyData = e.KeyData
                    tbHotkey.Text = KeysHelp.GetKeyString(item.KeyData)

                    For Each i In tv.GetNodes
                        If TypeOf i.Tag Is CustomMenuItem Then
                            Dim current = DirectCast(i.Tag, CustomMenuItem)

                            If current.KeyData = item.KeyData AndAlso Not item Is current Then
                                current.KeyData = Keys.None
                                MsgInfo(KeysHelp.GetKeyString(item.KeyData) + " detached from " + current.Text.TrimEnd("."c) + " and assigned to " + item.Text.TrimEnd("."c) + " instead.")
                            End If
                        End If
                    Next
                End If
            End If

            e.Handled = True
        End Sub

        Private Sub tbHotkey_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbHotkey.KeyPress
            e.Handled = True
        End Sub

        Private Sub tv_AfterSelect() Handles tv.AfterSelect
            UpdateControls()
        End Sub

        Private Sub tsbCut_Click() Handles tsbCut.Click
            If Not tv.SelectedNode Is Nothing Then
                tsbCopy.PerformClick()
                RemoveSelectedItem()
            End If
        End Sub

        Private Sub tsbCopy_Click() Handles tsbCopy.Click
            If Not tv.SelectedNode Is Nothing Then
                ClipboardNode = DirectCast(ObjectHelp.GetCopy(tv.SelectedNode), TreeNode)
                UpdateControls()
            End If
        End Sub

        Private Sub tsbPaste_Click() Handles tsbPaste.Click
            If Not tv.SelectedNode Is Nothing Then
                tv.SelectedNode.Nodes.Add(CType(ObjectHelp.GetCopy(ClipboardNode), TreeNode))
                tv.SelectedNode.ExpandAll()
            End If
        End Sub

        Private Sub tsbRemove_Click() Handles tsbRemove.Click
            RemoveSelectedItem()
        End Sub

        Private Sub ResetToolStripMenuItem_Click() Handles ResetToolStripMenuItem.Click
            If MsgOK("Please confirm to reset the entire menu.") Then
                tv.Nodes.Clear()
                PopulateTreeView(GenericMenu.DefaultMenu.Invoke, Nothing)
                tv.ExpandAll()
                tv.SelectedNode = tv.Nodes(0)
            End If
        End Sub

        Private Sub tsbMoveLeft_Click() Handles tsbMoveLeft.Click
            Dim n = tv.SelectedNode

            If Not n Is Nothing AndAlso n.Parent.Parent Is Nothing Then
                Exit Sub
            End If

            Block = True
            tv.MoveSelectionLeft()
            Block = False
        End Sub

        Private Sub tsbMoveRight_Click() Handles tsbMoveRight.Click
            Block = True
            tv.MoveSelectionRight()
            Block = False
        End Sub

        Private Sub tsbMoveUp_Click() Handles tsbMoveUp.Click
            Dim n = tv.SelectedNode

            If Not n Is Nothing AndAlso
                Not n.Parent Is Nothing AndAlso
                n.Parent.Parent Is Nothing AndAlso
                n.Index = 0 Then

                Exit Sub
            End If

            Block = True
            tv.MoveSelectionUp()
            Block = False
        End Sub

        Private Sub tsbMoveDown_Click() Handles tsbMoveDown.Click
            Dim n = tv.SelectedNode

            If Not n Is Nothing AndAlso
                n.Parent.Parent Is Nothing AndAlso
                n.NextNode Is Nothing Then

                Exit Sub
            End If

            Block = True
            tv.MoveSelectionDown()
            Block = False
        End Sub

        Private Sub bCommand_Click() Handles bnCommand.Click
            cmsCommand.Show(bnCommand, 0, bnCommand.Height)
        End Sub

        Private Sub tbCommand_TextChanged() Handles tbCommand.TextChanged
            If Not tv.SelectedNode Is Nothing Then
                Dim item = DirectCast(tv.SelectedNode.Tag, CustomMenuItem)

                If Not Block Then
                    Dim selectedCommand As Command = Nothing

                    For Each i As Command In GenericMenu.CommandManager.Commands.Values
                        If i.MethodInfo.Name = tbCommand.Text Then
                            selectedCommand = i
                        End If
                    Next

                    If selectedCommand Is Nothing OrElse selectedCommand.MethodInfo Is Nothing Then
                        item.MethodName = ""
                        item.Parameters = Nothing
                    Else
                        item.MethodName = selectedCommand.MethodInfo.Name
                        item.Parameters = selectedCommand.GetDefaultParameters
                    End If
                End If

                Dim mi As MethodInfo = Nothing

                If GenericMenu.CommandManager.HasCommand(item.MethodName) Then
                    mi = GenericMenu.CommandManager.GetCommand(item.MethodName).MethodInfo
                End If

                pg.Visible = Not mi Is Nothing AndAlso mi.GetParameters.Length > 0
                lParameters.Visible = pg.Visible
                PopulateGrid(item)
            End If
        End Sub

        Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
            If Not tv.SelectedNode Is Nothing Then
                Dim newNode As New TreeNode
                newNode.Text = "???"
                newNode.Tag = New CustomMenuItem("???")
                tv.SelectedNode.Nodes.Add(newNode)
                tv.SelectedNode = newNode
            End If
        End Sub

        Protected Overrides Sub OnShown(e As EventArgs)
            PopulateSymbolMenu()
            MyBase.OnShown(e)
        End Sub

        Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
            IsClosing = True
            MyBase.OnFormClosing(e)
        End Sub

        Private Sub bnOK_Click(sender As Object, e As EventArgs) Handles bnOK.Click
            IsClosing = True
        End Sub

        Private Sub bnCancel_Click(sender As Object, e As EventArgs) Handles bnCancel.Click
            IsClosing = True
        End Sub

        Private Sub CustomMenuEditor_HelpRequested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested
            Dim f As New HelpForm()
            f.Doc.WriteStart(Text)
            f.Doc.WriteP("The menu editor allows to customize the text, location, shortcut key and command of a menu item. Menu items can be rearranged with '''Drag & Drop'''. Pressing Ctrl while dragging moves as sub-item.")
            f.Doc.WriteP("[http://fontawesome.io/cheatsheet FontAwesome icons]")
            f.Doc.WriteP("[https://docs.microsoft.com/en-us/windows/uwp/style/segoe-ui-symbol-font Segoe MDL2 icons]")
            f.Doc.WriteTable("Commands", GenericMenu.CommandManager.GetTips)
            f.Show()
        End Sub
    End Class
End Namespace