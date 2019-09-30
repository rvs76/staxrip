Imports System.Reflection
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

Namespace UI
    Public Class TreeViewEx
        Inherits TreeView

        Private AutoCollapsValue As Boolean

        <DefaultValue(False)>
        Property AutoCollaps As Boolean

        <DefaultValue(GetType(TreeNodeExpandMode), "Disabled")>
        Property ExpandMode As TreeNodeExpandMode

        <DefaultValue(False)>
        Property SelectOnMouseDown() As Boolean

        Protected Overrides Sub OnAfterSelect(e As TreeViewEventArgs)
            If AutoCollaps Then
                For Each i As TreeNode In If(e.Node.Parent Is Nothing, Nodes, e.Node.Parent.Nodes)
                    If Not i Is e.Node Then
                        i.Collapse()
                    End If
                Next
            End If

            Select Case ExpandMode
                Case TreeNodeExpandMode.Normal
                    e.Node.Expand()
                Case TreeNodeExpandMode.InclusiveChilds
                    e.Node.ExpandAll()
            End Select

            MyBase.OnAfterSelect(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If SelectOnMouseDown AndAlso m.Msg = &H201 Then 'WM_LBUTTONDOWN
                Dim n = GetNodeAt(ClientMousePos)

                If Not n Is Nothing AndAlso n.Nodes.Count = 0 Then
                    SelectedNode = n
                    Focus()
                    Exit Sub
                End If
            End If

            MyBase.WndProc(m)
        End Sub

        Sub MoveSelectionLeft()
            Dim n As TreeNode = SelectedNode

            If Not n Is Nothing AndAlso Not n.Parent Is Nothing Then
                Dim parentParentNodes As TreeNodeCollection = GetParentParentNodes(n)
                Dim parentIndex As Integer = n.Parent.Index

                If Not parentParentNodes Is Nothing Then
                    n.Remove()
                    parentParentNodes.Insert(parentIndex + 1, n)
                    SelectedNode = n
                End If
            End If
        End Sub

        Sub MoveSelectionUp()
            Dim n As TreeNode = SelectedNode

            If Not n Is Nothing Then
                If n.Index = 0 Then
                    Dim parentParentNodes As TreeNodeCollection = GetParentParentNodes(n)

                    If Not parentParentNodes Is Nothing Then
                        Dim index As Integer = n.Parent.Index
                        n.Remove()
                        parentParentNodes.Insert(index, n)
                    End If
                Else
                    Dim index As Integer = n.Index
                    Dim parentNodes As TreeNodeCollection = GetParentNodes(n)
                    n.Remove()
                    parentNodes.Insert(index - 1, n)
                End If

                SelectedNode = n
            End If
        End Sub

        Sub MoveSelectionRight()
            Dim n As TreeNode = SelectedNode

            If Not n Is Nothing Then
                If n.Index > 0 Then
                    Dim previousNode As TreeNode = n.PrevNode
                    n.Remove()
                    previousNode.Nodes.Add(n)
                    SelectedNode = n
                End If
            End If
        End Sub

        Sub MoveSelectionDown()
            Dim n As TreeNode = SelectedNode

            If Not n Is Nothing Then
                If n.NextNode Is Nothing Then
                    Dim parentParentNodes As TreeNodeCollection = GetParentParentNodes(n)

                    If Not parentParentNodes Is Nothing Then
                        Dim index As Integer = n.Parent.Index
                        n.Remove()
                        parentParentNodes.Insert(index + 1, n)
                    End If
                Else
                    Dim index As Integer = n.Index
                    Dim parentNodes As TreeNodeCollection = GetParentNodes(n)
                    n.Remove()
                    parentNodes.Insert(index + 1, n)
                End If

                SelectedNode = n
            End If
        End Sub

        Private Function GetParentParentNodes(n As TreeNode) As TreeNodeCollection
            Dim parent = n.Parent

            If parent Is Nothing Then
                Return Nothing
            End If

            If parent.Parent Is Nothing Then
                Return Nodes
            Else
                Return parent.Parent.Nodes
            End If
        End Function

        Private Function GetParentNodes(n As TreeNode) As TreeNodeCollection
            Dim parent As TreeNode = n.Parent

            If parent Is Nothing Then
                Return Nodes
            Else
                Return parent.Nodes
            End If
        End Function

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)
            Native.SetWindowTheme(Handle, "explorer", Nothing)
        End Sub

        Function AddNode(path As String) As TreeNode
            Dim pathElements = path.SplitNoEmptyAndWhiteSpace("|")
            Dim currentNodeList = Nodes
            Dim currentPath = ""
            Dim ret As TreeNode = Nothing

            For Each iNodeName In pathElements
                If currentPath <> "" Then
                    currentPath += "|"
                End If

                currentPath += iNodeName
                Dim found = False

                For Each iNode As TreeNode In currentNodeList
                    If iNode.Text = iNodeName Then
                        ret = iNode
                        currentNodeList = iNode.Nodes
                        found = True
                    End If
                Next

                If Not found Then
                    ret = New TreeNode
                    ret.Text = iNodeName
                    currentNodeList.Add(ret)
                    currentNodeList = ret.Nodes
                End If
            Next

            Return ret
        End Function

        Function GetNodes() As List(Of TreeNode)
            Dim ret As New List(Of TreeNode)
            AddNodesRecursive(Nodes, ret)
            Return ret
        End Function

        Shared Sub AddNodesRecursive(searchList As TreeNodeCollection, returnList As List(Of TreeNode))
            For Each i As TreeNode In searchList
                returnList.Add(i)
                AddNodesRecursive(i.Nodes, returnList)
            Next
        End Sub
    End Class

    Public Enum TreeNodeExpandMode
        Disabled
        Normal
        InclusiveChilds
    End Enum

    Public Class ToolStripEx
        Inherits ToolStrip

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ParamArray items As ToolStripItem())
            MyBase.New(items)
        End Sub

        <DefaultValue(False), Description("Overdraws a weird grey line visible at the bottom in some configurations.")>
        Property OverdrawLine As Boolean

        <DefaultValue(False), Description("Only one button can be checked at the same time.")>
        Property SingleAutoChecked As Boolean

        <DefaultValue(False), Description("Shows a themed control border.")>
        Property ShowControlBorder As Boolean

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If OverdrawLine Then
                Dim b As New SolidBrush(BackColor)
                e.Graphics.FillRectangle(b, 0, Height - 2, Width, 2)
                b.Dispose()
            End If

            If ShowControlBorder AndAlso VisualStyleInformation.IsEnabledByUser Then
                ControlPaint.DrawBorder(e.Graphics,
                                        ClientRectangle,
                                        VisualStyleInformation.TextControlBorder,
                                        ButtonBorderStyle.Solid)
            End If
        End Sub

        Protected Overrides Sub OnItemClicked(e As ToolStripItemClickedEventArgs)
            If SingleAutoChecked Then
                For Each i In Items.OfType(Of ToolStripButton)()
                    If i Is e.ClickedItem Then
                        i.Checked = True
                    Else
                        i.Checked = False
                    End If
                Next
            End If

            MyBase.OnItemClicked(e)
        End Sub

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim ret = MyBase.CreateParams

                If ShowControlBorder AndAlso Not VisualStyleInformation.IsEnabledByUser Then
                    ret.ExStyle = ret.ExStyle Or &H200 'WS_EX_CLIENTEDGE
                End If

                Return ret
            End Get
        End Property
    End Class

    Public Class LineControl
        Inherits Control

        Public Sub New()
            Margin = New Padding(4, 2, 5, 2)
            Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim textOffset As Integer
            Dim lineHeight = CInt(Height / 2)

            If Text <> "" Then
                Dim textSize = e.Graphics.MeasureString(Text, Font)
                textOffset = CInt(textSize.Width)

                Using b = New SolidBrush(If(Enabled, ForeColor, SystemColors.GrayText))
                    e.Graphics.DrawString(Text, Font, b, 0, CInt((Height - textSize.Height) / 2) - 1)
                End Using
            End If

            If Enabled Then
                e.Graphics.DrawLine(Pens.Silver, textOffset, lineHeight, Width, lineHeight)
                e.Graphics.DrawLine(Pens.White, textOffset, lineHeight + 1, Width, lineHeight + 1)
            Else
                e.Graphics.DrawLine(SystemPens.InactiveBorder, textOffset, lineHeight, Width, lineHeight)
            End If

            MyBase.OnPaint(e)
        End Sub
    End Class

    Public Class CommandLink
        Inherits Button

        Const BS_COMMANDLINK As Integer = &HE

        Const BCM_SETSHIELD As Integer = &H160C
        Const BCM_SETNOTE As Integer = &H1609
        Const BCM_GETNOTE As Integer = &H160A
        Const BCM_GETNOTELENGTH As Integer = &H160B

        Sub New()
            Me.FlatStyle = FlatStyle.System
        End Sub

        Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim r = MyBase.CreateParams
                r.Style = r.Style Or BS_COMMANDLINK
                Return r
            End Get
        End Property

        Private NoteValue As String = ""

        <DefaultValue("")>
        Property Note() As String
            Get
                Return NoteValue
            End Get
            Set(value As String)
                Native.SendMessage(Handle, BCM_SETNOTE, 0, value)
                NoteValue = value
            End Set
        End Property

        Protected Overrides Sub OnCreateControl()
            If Note <> "" Then Text += BR2 + Note
            MyBase.OnCreateControl()
        End Sub
    End Class

    Public Class TextBoxEx
        Inherits TextBox

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property Name() As String
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property TabIndex As Integer
            Get
                Return MyBase.TabIndex
            End Get
            Set(value As Integer)
                MyBase.TabIndex = value
            End Set
        End Property

        Sub SetTextWithoutTextChangedEvent(text As String)
            BlockOnTextChanged = True
            Me.Text = text
            BlockOnTextChanged = False
        End Sub

        Private BlockOnTextChanged As Boolean

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            If Not BlockOnTextChanged Then MyBase.OnTextChanged(e)
        End Sub
    End Class

    Public Class PanelEx
        Inherits Panel

        Private ShowNiceBorderValue As Boolean

        Sub New()
            SetStyle(ControlStyles.ResizeRedraw, True)
        End Sub

        <DefaultValue(False), Description("Nicer border if themes are enabled.")>
        Property ShowNiceBorder() As Boolean
            Get
                Return ShowNiceBorderValue
            End Get
            Set(Value As Boolean)
                ShowNiceBorderValue = Value
                Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If ShowNiceBorder AndAlso VisualStyleInformation.IsEnabledByUser Then
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    VisualStyleInformation.TextControlBorder, ButtonBorderStyle.Solid)
            End If
        End Sub
    End Class

    Public Class CheckBoxEx
        Inherits CheckBox

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property Name() As String
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property TabIndex As Integer
            Get
                Return MyBase.TabIndex
            End Get
            Set(value As Integer)
                MyBase.TabIndex = value
            End Set
        End Property
    End Class

    Public Class RichTextBoxEx
        Inherits RichTextBox

        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Property BlockPaint As Boolean

        Private BorderRect As Native.RECT

        Sub New()
            MyClass.New(True)
        End Sub

        Sub New(createMenu As Boolean)
            If createMenu Then InitMenu()
            If VisualStyleInformation.IsEnabledByUser Then BorderStyle = BorderStyle.None
        End Sub

        Sub InitMenu()
            If DesignHelp.IsDesignMode Then Exit Sub

            Dim cms As New ContextMenuStripEx()
            Dim cutItem = cms.Add("Cut")
            cutItem.SetImage(Symbol.Cut)
            Dim copyItem = cms.Add("Copy", Sub() Clipboard.SetText(SelectedText))
            copyItem.SetImage(Symbol.Copy)
            Dim pasteItem = cms.Add("Paste")
            pasteItem.SetImage(Symbol.Paste)
            cms.Add("Copy Everything", Sub() Clipboard.SetText(Text))

            AddHandler cutItem.Click, Sub()
                                          Clipboard.SetText(SelectedText)
                                          SelectedText = ""
                                      End Sub

            AddHandler pasteItem.Click, Sub()
                                            SelectedText = Clipboard.GetText
                                            ScrollToCaret()
                                        End Sub

            AddHandler cms.Opening, Sub()
                                        cutItem.Visible = SelectionLength > 0 AndAlso Not Me.ReadOnly
                                        copyItem.Visible = SelectionLength > 0
                                        pasteItem.Visible = Clipboard.GetText <> "" AndAlso Not Me.ReadOnly
                                    End Sub

            ContextMenuStrip = cms
        End Sub

        Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
            If e.KeyData = (Keys.Control Or Keys.V) Then
                e.SuppressKeyPress = True
                SelectedText = Clipboard.GetText
            End If

            MyBase.OnKeyDown(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            Const WM_NCPAINT = &H85
            Const WM_NCCALCSIZE = &H83
            Const WM_THEMECHANGED = &H31A

            Select Case m.Msg
                Case 15, 20 'WM_PAINT, WM_ERASEBKGND
                    If BlockPaint Then Exit Sub
                Case Else
            End Select

            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_NCPAINT
                    WmNcpaint(m)
                Case WM_NCCALCSIZE
                    WmNccalcsize(m)
                Case WM_THEMECHANGED
                    UpdateStyles()
            End Select
        End Sub

        Private Sub WmNccalcsize(ByRef m As Message)
            If Not VisualStyleInformation.IsEnabledByUser Then Return

            Dim par As New Native.NCCALCSIZE_PARAMS()
            Dim windowRect As Native.RECT

            If m.WParam <> IntPtr.Zero Then
                par = CType(Marshal.PtrToStructure(m.LParam, GetType(Native.NCCALCSIZE_PARAMS)), Native.NCCALCSIZE_PARAMS)
                windowRect = par.rgrc0
            End If

            Dim clientRect = windowRect

            clientRect.Left += 1
            clientRect.Top += 1
            clientRect.Right -= 1
            clientRect.Bottom -= 1

            BorderRect = New Native.RECT(clientRect.Left - windowRect.Left,
                                         clientRect.Top - windowRect.Top,
                                         windowRect.Right - clientRect.Right,
                                         windowRect.Bottom - clientRect.Bottom)

            If m.WParam = IntPtr.Zero Then
                Marshal.StructureToPtr(clientRect, m.LParam, False)
            Else
                par.rgrc0 = clientRect
                Marshal.StructureToPtr(par, m.LParam, False)
            End If

            Const WVR_HREDRAW = &H100
            Const WVR_VREDRAW = &H200
            Const WVR_REDRAW = (WVR_HREDRAW Or WVR_VREDRAW)

            m.Result = New IntPtr(WVR_REDRAW)
        End Sub

        Private Sub WmNcpaint(ByRef m As Message)
            If Not VisualStyleInformation.IsEnabledByUser Then Return

            Dim r As Native.RECT
            Native.GetWindowRect(Handle, r)

            r.Right -= r.Left
            r.Bottom -= r.Top
            r.Top = 0
            r.Left = 0

            r.Left += BorderRect.Left
            r.Top += BorderRect.Top
            r.Right -= BorderRect.Right
            r.Bottom -= BorderRect.Bottom

            Dim hDC = Native.GetWindowDC(Handle)
            Native.ExcludeClipRect(hDC, r.Left, r.Top, r.Right, r.Bottom)

            Using g = Graphics.FromHdc(hDC)
                g.Clear(Color.CadetBlue)
            End Using

            Native.ReleaseDC(Handle, hDC)
            m.Result = IntPtr.Zero
        End Sub

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)
            AutoWordSelection = False
        End Sub
    End Class

    Public Class TrackBarEx
        Inherits TrackBar

        <DefaultValue(False)>
        Property NoMouseWheelEvent As Boolean

        Shadows Property Value As Integer
            Get
                Return MyBase.Value
            End Get
            Set(value As Integer)
                If value > Maximum Then value = Maximum
                If value < Minimum Then value = Minimum
                If value <> MyBase.Value Then MyBase.Value = value
            End Set
        End Property
    End Class

    Public Class LabelEx
        Inherits Label

        Sub New()
            TextAlign = Drawing.ContentAlignment.MiddleLeft
        End Sub

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property Name() As String
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property TabIndex As Integer
            Get
                Return MyBase.TabIndex
            End Get
            Set(value As Integer)
                MyBase.TabIndex = value
            End Set
        End Property
    End Class

    Public Class PropertyGridEx
        Inherits PropertyGrid

        Private Description As String

        Sub New()
            ToolbarVisible = False 'GridView is not ready when PropertySortChanged happens so Init fails
        End Sub

        Protected Overrides Sub OnSelectedGridItemChanged(e As SelectedGridItemChangedEventArgs)
            MyBase.OnSelectedGridItemChanged(e)
            Description = e.NewSelection.PropertyDescriptor.Description
            SetHelpHeight()
        End Sub

        Protected Overrides Sub OnLayout(e As LayoutEventArgs)
            SetHelpHeight()
            MyBase.OnLayout(e)
        End Sub

        Sub SetHelpHeight()
            If Description <> "" Then
                HelpVisible = True

                Dim lines = CInt(Math.Ceiling(CreateGraphics.MeasureString(
                    Description, Font, Width).Height / Font.Height))

                Dim grid As New Reflector(Me, GetType(PropertyGrid))
                Dim doc = grid.Invoke("doccomment")
                doc.Invoke("Lines", lines + 1)
                doc.Invoke("userSized", True)
                grid.Invoke("OnLayoutInternal", False)
            Else
                HelpVisible = False
            End If
        End Sub

        Sub MoveSplitter(x As Integer)
            Dim grid As New Reflector(Me, GetType(PropertyGrid))
            grid.Invoke("gridView").Invoke("MoveSplitterTo", x)
        End Sub
    End Class

    Public Class ButtonLabel
        Inherits Label

        Private LinkColorNormal As Color
        Private LinkColorHover As Color

        Property LinkColor As Color
            Get
                Return LinkColorNormal
            End Get
            Set(value As Color)
                LinkColorNormal = value
                LinkColorHover = ControlPaint.Dark(LinkColorNormal)
                ForeColor = value
            End Set
        End Property

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            Font = New Font(Font, FontStyle.Bold)
            ForeColor = LinkColorHover
            MyBase.OnMouseEnter(e)
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            Font = New Font(Font, FontStyle.Regular)
            ForeColor = LinkColorNormal
            MyBase.OnMouseLeave(e)
        End Sub
    End Class

    <DefaultEvent("LinkClick")>
    Public Class LinkGroupBox
        Inherits GroupBox

        Public WithEvents Label As New ButtonLabel
        Event LinkClick()

        Sub New()
            Label.Left = 4
            Label.AutoSize = True
            Controls.Add(Label)
        End Sub

        Property Color As Color

        Overrides Property Text() As String
            Get
                Return Label.Text
            End Get
            Set(value As String)
                Label.Text = value
            End Set
        End Property

        Private Sub Label_Click() Handles Label.Click
            ShowContext()
            RaiseEvent LinkClick()
        End Sub

        Private Sub ShowContext()
            If Not Label.ContextMenuStrip Is Nothing Then Label.ContextMenuStrip.Show(Label, 0, 16)
        End Sub
    End Class

    Public Class MenuButton
        Inherits ButtonEx

        Event ValueChangedUser(value As Object)

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Property Items As New List(Of Object)
        Property Menu As New ContextMenuStripEx

        Public Sub New()
            Menu.ShowImageMargin = False
            ShowMenuSymbol = True
            AddHandler Menu.Opening, AddressOf MenuOpening
        End Sub

        Sub MenuOpening(sender As Object, e As CancelEventArgs)
            Menu.MinimumSize = New Size(Width, 0)

            For Each i As ActionMenuItem In Menu.Items
                i.Font = New Font("Segoe UI", 9 * s.UIScaleFactor, If(Not Value Is Nothing AndAlso Value.Equals(i.Tag), FontStyle.Bold, FontStyle.Regular))

                If (Menu.Width - i.Width) > 2 Then
                    i.AutoSize = False
                    i.Width = Menu.Width - 1
                End If
            Next
        End Sub

        Private ValueValue As Object

        <DefaultValue(CStr(Nothing))>
        Property Value As Object
            Get
                Return ValueValue
            End Get
            Set(value As Object)
                If Menu.Items.Count = 0 Then
                    If TypeOf value Is System.Enum Then
                        For Each i In System.Enum.GetValues(value.GetType)
                            Dim text = DispNameAttribute.GetValueForEnum(i)
                            Dim temp = i

                            ActionMenuItem.Add(Menu.Items, text, Sub(o As Object) OnAction(text, o), temp, Nothing).Tag = temp
                        Next
                    End If
                End If

                If Not value Is Nothing Then
                    For Each i In Menu.Items.OfType(Of ActionMenuItem)()
                        If value.Equals(i.Tag) Then Text = i.Text
                    Next
                End If

                If Text = "" AndAlso Not value Is Nothing Then Text = value.ToString

                ValueValue = value
            End Set
        End Property

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                Menu.Show(Me, 0, Height)
            Else
                MyBase.OnMouseDown(e)
            End If
        End Sub

        Protected Overridable Sub OnValueChanged(value As Object)
            RaiseEvent ValueChangedUser(value)
        End Sub

        Sub Add(items As IEnumerable(Of Object))
            For Each i In items
                Add(i.ToString, i, Nothing)
            Next
        End Sub

        Sub Add(ParamArray items As Object())
            For Each i In items
                Add(i.ToString, i, Nothing)
            Next
        End Sub

        Function Add(path As String, obj As Object, Optional tip As String = Nothing) As ActionMenuItem
            Items.Add(obj)
            Dim name = path
            If path.Contains("|") Then name = path.RightLast("|").Trim
            Dim ret = ActionMenuItem.Add(Menu.Items, path, Sub(o As Object) OnAction(name, o), obj, tip)
            ret.Tag = obj
            Return ret
        End Function

        Sub Clear()
            Items.Clear()
            Menu.Items.ClearAndDisplose
        End Sub

        Private Sub OnAction(text As String, value As Object)
            Me.Text = text
            Me.Value = value
            OnValueChanged(value)
        End Sub

        Function GetValue(Of T)() As T
            Return DirectCast(Value, T)
        End Function

        Function GetInt() As Integer
            Return DirectCast(Value, Integer)
        End Function

        Protected Overrides Sub Dispose(disposing As Boolean)
            Menu.Dispose()
            MyBase.Dispose(disposing)
        End Sub
    End Class

    Public Class CommandLineRichTextBox
        Inherits RichTextBoxEx

        Property LastCommandLine As String

        Sub SetText(commandLine As String)
            If commandLine = LastCommandLine Then Exit Sub

            If commandLine = "" Then
                Text = ""
                LastCommandLine = ""
                Exit Sub
            End If

            BlockPaint = True
            Text = commandLine
            SelectAll()
            SelectionColor = ForeColor
            SelectionFont = New Font(Font, FontStyle.Regular)

            If LastCommandLine <> "" Then
                Dim selStart = GetCompareIndex(commandLine, LastCommandLine)
                Dim selEnd = commandLine.Length - GetCompareIndex(ReverseString(commandLine), ReverseString(LastCommandLine))

                If selEnd > selStart AndAlso selEnd - selStart < commandLine.Length - 1 Then
                    While selStart > 0 AndAlso selStart + 1 < commandLine.Length AndAlso
                        Not commandLine(selStart - 1) + commandLine(selStart) = " -"

                        selStart = selStart - 1
                    End While

                    If selEnd - selStart < 25 Then
                        SelectionStart = selStart

                        If selEnd - selStart = commandLine.Length Then
                            SelectionLength = 0
                        Else
                            SelectionLength = selEnd - selStart
                        End If

                        SelectionFont = New Font(Font, FontStyle.Bold)
                    End If
                End If
            End If

            SelectionStart = commandLine.Length
            BlockPaint = False
            Refresh()
            LastCommandLine = commandLine
        End Sub

        Function GetCompareIndex(a As String, b As String) As Integer
            For x = 0 To a.Length - 1
                If x > b.Length - 1 OrElse x > a.Length - 1 OrElse a(x) <> b(x) Then
                    Return x
                End If
            Next

            Return 0
        End Function

        Function ReverseString(value As String) As String
            Dim a = value.ToCharArray
            Array.Reverse(a)
            Return New String(a)
        End Function

        Private Sub CommandLineRichTextBox_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not DesignMode Then Font = New Font("Consolas", 10 * s.UIScaleFactor)
        End Sub

        Sub UpdateHeight()
            Using graphics = CreateGraphics()
                Dim stringSize = graphics.MeasureString(Text, Font, Size.Width)
                Size = New Size(Size.Width, CInt(stringSize.Height) + 1)
            End Using
        End Sub
    End Class

    <ProvideProperty("Expand", GetType(Control))>
    Public Class FlowLayoutPanelEx
        Inherits FlowLayoutPanel

        <DefaultValue(False)>
        Property UseParenWidth As Boolean

        Property AutomaticOffset As Boolean

        Sub New()
            WrapContents = False
        End Sub

        Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
            MyBase.OnLayout(levent)

            If Not WrapContents AndAlso FlowDirection = FlowDirection.LeftToRight Then
                Dim nextPos As Integer

                For Each ctrl As Control In Controls
                    If nextPos = 0 Then nextPos = 3

                    If ctrl.Visible Then
                        nextPos += ctrl.Margin.Left + ctrl.Width + ctrl.Margin.Right

                        Dim expandetControl = TryCast(ctrl, SimpleUI.SimpleUIControl)

                        If Not expandetControl Is Nothing AndAlso expandetControl.Expand Then
                            Dim diff = Aggregate i2 In Controls.OfType(Of Control)() Into Sum(If(i2.Visible, i2.Width + i2.Margin.Left + i2.Margin.Right, 0))

                            Dim hostWidth = Width - 1

                            If ctrl.AutoSize Then
                                nextPos += hostWidth - diff
                            Else
                                ctrl.Width += hostWidth - diff
                                nextPos += hostWidth - diff
                            End If
                        End If
                    End If

                    Dim index = Controls.IndexOf(ctrl)

                    If index < Controls.Count - 1 Then
                        Dim ctrl2 = Controls(index + 1)
                        ctrl2.Left = nextPos
                    End If
                Next
            End If

            'vertical align middles
            If Not WrapContents AndAlso
                (FlowDirection = FlowDirection.LeftToRight OrElse
                FlowDirection = FlowDirection.RightToLeft) Then

                For Each i As Control In Controls
                    Dim offset = 0
                    If TypeOf i Is CheckBox Then offset = 1
                    i.Top = CInt((Height - i.Height) / 2) + offset
                Next
            End If

            Dim labelBlocks = From block In Controls.OfType(Of SimpleUI.LabelBlock)() Where block.Label.Offset = 0

            If labelBlocks.Count > 0 Then
                Dim hMax = Aggregate i In labelBlocks Into Max(TextRenderer.MeasureText(i.Label.Text, i.Label.Font).Width)

                For Each i In labelBlocks
                    i.Label.Offset = hMax / i.Label.Font.Height
                Next
            End If
        End Sub

        Protected Overrides ReadOnly Property DefaultMargin As Padding
            Get
                Return New Padding(0)
            End Get
        End Property

        Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
            Dim ret = MyBase.GetPreferredSize(proposedSize)
            If UseParenWidth Then ret.Width = Parent.Width
            Return ret
        End Function
    End Class

    Public Class ButtonEx
        Inherits Button

        <DefaultValue(False)>
        Property ShowMenuSymbol As Boolean

        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Property ClickAction As Action

        Public Sub New()
            MyBase.UseVisualStyleBackColor = True
        End Sub

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property Name() As String
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property

        <DefaultValue(0), Browsable(False),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property TabIndex As Integer
            Get
                Return MyBase.TabIndex
            End Get
            Set(value As Integer)
                MyBase.TabIndex = value
            End Set
        End Property

        <DefaultValue(True), Browsable(False),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Shadows Property UseVisualStyleBackColor() As Boolean
            Get
                Return True
            End Get
            Set(value As Boolean)
            End Set
        End Property

        Protected Overrides ReadOnly Property DefaultSize As Size
            Get
                Return New Size(250, 70)
            End Get
        End Property

        Protected Overrides Sub OnClick(e As EventArgs)
            If Not ClickAction Is Nothing Then ClickAction.Invoke()
            If Not ContextMenuStrip Is Nothing Then ContextMenuStrip.Show(Me, 0, Height)
            MyBase.OnClick(e)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If ShowMenuSymbol Then
                Dim _text = "6"
                Dim _font = New Font("Marlett", Font.Size)
                Dim textSize = e.Graphics.MeasureString(_text, _font)
                Dim x = If(Text <> "", Width - textSize.Width, Math.Ceiling((Width - textSize.Width) / 2))
                Dim y = Math.Ceiling((Height - textSize.Height) / 2)
                Dim brush = If(Enabled, Brushes.Black, SystemBrushes.GrayText)
                e.Graphics.DrawString(_text, _font, brush, CSng(x), CSng(y))
            End If
        End Sub

        Sub ShowBold()
            Font = New Font(Font, FontStyle.Bold)

            For i = 0 To 20
                Application.DoEvents()
                Thread.Sleep(10)
            Next

            Font = New Font(Font, FontStyle.Regular)
        End Sub
    End Class

    Public Class ListBoxEx
        Inherits ListBox

        <DefaultValue(GetType(Button), Nothing)> Property UpButton As Button
        <DefaultValue(GetType(Button), Nothing)> Property DownButton As Button
        <DefaultValue(GetType(Button), Nothing)> Property RemoveButton As Button
        <DefaultValue(GetType(Button), Nothing)> Property Button1 As Button
        <DefaultValue(GetType(Button), Nothing)> Property Button2 As Button

        Private LastTick As Long
        Private KeyText As String = ""
        Private BlockOnSelectedIndexChanged As Boolean

        Private ColorBorder As Color
        Private ColorTop As Color
        Private ColorBottom As Color

        Public Sub New()
            DrawMode = DrawMode.OwnerDrawFixed
            InitAero()
        End Sub

        Protected Overrides Sub OnFontChanged(e As EventArgs)
            ItemHeight = CInt(Font.Height * 1.4)
            MyBase.OnFontChanged(e)
        End Sub

        Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
            If Items.Count = 0 OrElse e.Index < 0 Then Exit Sub

            Dim g = e.Graphics
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim r = e.Bounds

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                r.Width -= 1
                r.Height -= 1

                Using p As New Pen(ColorBorder)
                    g.DrawRectangle(p, r)
                End Using

                r.Inflate(-1, -1)

                Using b As New SolidBrush(ColorBottom)
                    g.FillRectangle(b, r)
                End Using
            Else
                Using b As New SolidBrush(BackColor)
                    g.FillRectangle(b, r)
                End Using
            End If

            Dim sf As New StringFormat
            sf.FormatFlags = StringFormatFlags.NoWrap
            sf.LineAlignment = StringAlignment.Center

            Dim r2 = e.Bounds
            r2.X = 2
            r2.Width = e.Bounds.Width

            Dim caption As String = Nothing

            If DisplayMember <> "" Then
                Try
                    caption = Items(e.Index).GetType.GetProperty(DisplayMember).GetValue(Items(e.Index), Nothing).ToString
                Catch ex As Exception
                    caption = Items(e.Index).ToString()
                End Try
            Else
                caption = Items(e.Index).ToString()
            End If

            e.Graphics.DrawString(caption, Font, Brushes.Black, r2, sf)
        End Sub

        Sub InitAero()
            Dim argb = CInt(Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", 0))
            If argb = 0 Then argb = Color.Orange.ToArgb
            InitColors(Color.FromArgb(argb))
        End Sub

        Sub InitColors(c As Color)
            Dim border = HSLColor.Convert(c)
            border.Luminosity = 50

            Dim top = border
            Dim bottom = border

            top.Luminosity = 240
            bottom.Luminosity = 220

            ColorBorder = border.ToColor
            ColorTop = top.ToColor
            ColorBottom = bottom.ToColor
        End Sub

        Public Shared Function CreateRoundRectangle(r As Rectangle, radius As Integer) As GraphicsPath
            Dim path As New GraphicsPath()

            Dim l = r.Left
            Dim t = r.Top
            Dim w = r.Width
            Dim h = r.Height
            Dim d = radius << 1

            path.AddArc(l, t, d, d, 180, 90)
            path.AddLine(l + radius, t, l + w - radius, t)
            path.AddArc(l + w - d, t, d, d, 270, 90)
            path.AddLine(l + w, t + radius, l + w, t + h - radius)
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90)
            path.AddLine(l + w - radius, t + h, l + radius, t + h)
            path.AddArc(l, t + h - d, d, d, 90, 90)
            path.AddLine(l, t + h - radius, l, t + radius)
            path.CloseFigure()

            Return path
        End Function

        Sub UpdateSelection()
            If SelectedIndex > -1 Then
                BlockOnSelectedIndexChanged = True
                Items(SelectedIndex) = SelectedItem
                BlockOnSelectedIndexChanged = False

                If Sorted Then
                    Sorted = False
                    Sorted = True
                End If
            End If
        End Sub

        Private SavedSelection As New List(Of Integer)

        Sub SaveSelection()
            SavedSelection.Clear()

            For Each i As Integer In SelectedIndices
                SavedSelection.Add(i)
            Next
        End Sub

        Sub RestoreSelection()
            For Each i In SavedSelection
                SetSelected(i, True)
            Next
        End Sub

        Sub DeleteItem(text As String)
            If FindStringExact(text) > -1 Then Items.RemoveAt(FindStringExact(text))
        End Sub

        Sub RemoveSelection()
            If SelectedIndex > -1 Then
                If SelectionMode = Windows.Forms.SelectionMode.One Then
                    Dim index = SelectedIndex

                    If Items.Count - 1 > SelectedIndex Then
                        SelectedIndex += 1
                    Else
                        SelectedIndex -= 1
                    End If

                    Items.RemoveAt(index)
                    UpdateButtons()
                Else
                    Dim iFirst = SelectedIndex

                    Dim indices(SelectedIndices.Count - 1) As Integer
                    SelectedIndices.CopyTo(indices, 0)

                    SelectedIndex = -1

                    For i = indices.Length - 1 To 0 Step -1
                        Items.RemoveAt(indices(i))
                    Next

                    If iFirst > Items.Count - 1 Then
                        SelectedIndex = Items.Count - 1
                    Else
                        SelectedIndex = iFirst
                    End If
                End If
            End If
        End Sub

        Function CanMoveUp() As Boolean
            Return SelectedIndices.Count > 0 AndAlso SelectedIndices(0) > 0
        End Function

        Function CanMoveDown() As Boolean
            Return SelectedIndices.Count > 0 AndAlso SelectedIndices(SelectedIndices.Count - 1) < Items.Count - 1
        End Function

        Sub MoveSelectionUp()
            If CanMoveUp() Then
                Dim iAbove = SelectedIndices(0) - 1

                If iAbove = -1 Then
                    Exit Sub
                End If

                Dim itemAbove = Items(iAbove)
                Items.RemoveAt(iAbove)
                Dim iLastItem = SelectedIndices(SelectedIndices.Count - 1)
                Items.Insert(iLastItem + 1, itemAbove)
                SetSelected(SelectedIndex, True)
            End If
        End Sub

        Sub MoveSelectionDown()
            If CanMoveDown() Then
                Dim iBelow = SelectedIndices(SelectedIndices.Count - 1) + 1

                If iBelow >= Items.Count Then
                    Exit Sub
                End If

                Dim itemBelow = Items(iBelow)
                Items.RemoveAt(iBelow)
                Dim iAbove = SelectedIndices(0) - 1
                Items.Insert(iAbove + 1, itemBelow)
                SetSelected(SelectedIndex, True)
            End If
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()

            If Not DesignMode Then
                If Not UpButton Is Nothing Then UpButton.AddClickAction(AddressOf MoveSelectionUp)
                If Not DownButton Is Nothing Then DownButton.AddClickAction(AddressOf MoveSelectionDown)
                If Not RemoveButton Is Nothing Then RemoveButton.AddClickAction(AddressOf RemoveSelection)
                UpdateButtons()
            End If
        End Sub

        Protected Overrides Sub OnSelectedIndexChanged(e As EventArgs)
            If Not BlockOnSelectedIndexChanged Then
                MyBase.OnSelectedIndexChanged(e)
                UpdateButtons()
            End If
        End Sub

        Sub UpdateButtons()
            If Not RemoveButton Is Nothing Then RemoveButton.Enabled = Not SelectedItem Is Nothing
            If Not UpButton Is Nothing Then UpButton.Enabled = SelectedIndex > 0
            If Not DownButton Is Nothing Then DownButton.Enabled = SelectedIndex > -1 AndAlso SelectedIndex < Items.Count - 1
            If Not Button1 Is Nothing Then Button1.Enabled = Not SelectedItem Is Nothing
            If Not Button2 Is Nothing Then Button2.Enabled = Not SelectedItem Is Nothing
        End Sub
    End Class

    Public Class NumEdit
        Inherits UserControl

        WithEvents TextBox As New Edit

        Private UpControl As New UpDownButton(True)
        Private DownControl As New UpDownButton(False)
        Private BorderColor As Color = Color.CadetBlue

        Event ValueChanged(numEdit As NumEdit)

        Sub New()
            SetStyle(ControlStyles.Opaque Or ControlStyles.ResizeRedraw, True)

            TextBox.BorderStyle = BorderStyle.None
            TextBox.TextAlign = HorizontalAlignment.Center
            TextBox.Text = "0"

            Controls.Add(TextBox)
            Controls.Add(UpControl)
            Controls.Add(DownControl)

            UpControl.ClickAction = Sub()
                                        Value += Increment
                                        Value = Math.Round(Value, DecimalPlaces)
                                    End Sub

            DownControl.ClickAction = Sub()
                                          Value -= Increment
                                          Value = Math.Round(Value, DecimalPlaces)
                                      End Sub

            AddHandler UpControl.MouseDown, Sub() Focus()
            AddHandler DownControl.MouseDown, Sub() Focus()
            AddHandler TextBox.LostFocus, Sub() UpdateText()
            AddHandler TextBox.GotFocus, Sub() SetColor(Color.CornflowerBlue)
            AddHandler TextBox.LostFocus, Sub() SetColor(Color.CadetBlue)
            AddHandler TextBox.MouseWheel, AddressOf Wheel
        End Sub

        Private TipProvider As TipProvider

        WriteOnly Property Help As String
            Set(value As String)
                If TipProvider Is Nothing Then TipProvider = New TipProvider()
                TipProvider.SetTip(value, TextBox, UpControl, DownControl)
            End Set
        End Property

        Protected Overrides Sub Dispose(disposing As Boolean)
            TipProvider?.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Sub Wheel(sender As Object, e As MouseEventArgs)
            If e.Delta > 0 Then
                Value += Increment
            Else
                Value -= Increment
            End If
        End Sub

        Private Sub SetColor(c As Color)
            BorderColor = c
            Invalidate()
        End Sub

        Protected Overridable Sub OnValueChanged(numEdit As NumEdit)
            RaiseEvent ValueChanged(Me)
        End Sub

        Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
            Dim h = (ClientSize.Height \ 2) - 3
            h -= h Mod 2
            If h > 20 Then h -= 1

            UpControl.Width = CInt(ClientSize.Height * 0.7)
            UpControl.Height = h
            UpControl.Top = 3
            UpControl.Left = ClientSize.Width - UpControl.Width - 3

            DownControl.Width = UpControl.Width
            DownControl.Left = UpControl.Left
            DownControl.Top = ClientSize.Height - h - 3
            DownControl.Height = h

            TextBox.Top = (ClientSize.Height - TextBox.Height) \ 2
            TextBox.Left = 2
            TextBox.Width = DownControl.Left - 3
            TextBox.Height = TextRenderer.MeasureText("gG", TextBox.Font).Height

            MyBase.OnLayout(levent)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim r = ClientRectangle
            r.Inflate(-1, -1)
            e.Graphics.FillRectangle(If(Enabled, Brushes.White, SystemBrushes.Control), r)
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid)
            MyBase.OnPaint(e)
        End Sub

        Protected Overrides ReadOnly Property DefaultSize() As Size
            Get
                Return New Size(250, 70)
            End Get
        End Property

        <Category("Data")>
        <DefaultValue(GetType(Double), "-9000000000")>
        Property Minimum As Double = -9000000000

        <Category("Data")>
        <DefaultValue(GetType(Double), "9000000000")>
        Property Maximum As Double = 9000000000

        <Category("Data")>
        <DefaultValue(GetType(Double), "1")>
        Property Increment As Double = 1

        Private ValueValue As Double

        <Category("Data")>
        <DefaultValue(GetType(Double), "0")>
        Property Value As Double
            Get
                Return ValueValue
            End Get
            Set(value As Double)
                SetValue(value, True)
            End Set
        End Property

        Private DecimalPlacesValue As Integer

        <Category("Data")>
        <DefaultValue(0)>
        Property DecimalPlaces As Integer
            Get
                Return DecimalPlacesValue
            End Get
            Set(value As Integer)
                DecimalPlacesValue = value
                UpdateText()
            End Set
        End Property

        Private Sub SetValue(value As Double, updateText As Boolean)
            If value <> ValueValue Then
                If value > Maximum Then
                    value = Maximum
                ElseIf value < Minimum Then
                    value = Minimum
                End If

                ValueValue = value
                If updateText Then Me.UpdateText()
                OnValueChanged(Me)
            End If
        End Sub

        Sub UpdateText()
            TextBox.SetTextWithoutTextChanged(ValueValue.ToString("F" & DecimalPlaces))
        End Sub

        Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox.KeyDown
            If e.KeyData = Keys.Up Then
                Value += Increment
            ElseIf e.KeyData = Keys.Down Then
                Value -= Increment
            End If
        End Sub

        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            If TextBox.Text.IsDouble Then SetValue(TextBox.Text.ToDouble, False)
        End Sub

        Private Class Edit
            Inherits TextBox

            Private BlockTextChanged As Boolean

            Sub SetTextWithoutTextChanged(val As String)
                BlockTextChanged = True
                Text = val
                BlockTextChanged = False
            End Sub

            Protected Overrides Sub OnTextChanged(e As EventArgs)
                If Not BlockTextChanged Then MyBase.OnTextChanged(e)
            End Sub
        End Class

        Private Class UpDownButton
            Inherits Control

            Private IsUp As Boolean
            Private IsHot As Boolean
            Private IsPressed As Boolean
            Private LastMouseDownTick As Integer

            Property ClickAction As Action

            Sub New(isUp As Boolean)
                Me.IsUp = isUp
                TabStop = False
                SetStyle(ControlStyles.Opaque Or
                         ControlStyles.ResizeRedraw Or
                         ControlStyles.OptimizedDoubleBuffer, True)
            End Sub

            Protected Overrides Sub OnMouseEnter(e As EventArgs)
                IsHot = True
                Invalidate()
                MyBase.OnMouseEnter(e)
            End Sub

            Protected Overrides Sub OnMouseLeave(e As EventArgs)
                IsHot = False
                Invalidate()
                MyBase.OnMouseLeave(e)
            End Sub

            Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
                IsPressed = True
                Invalidate()
                ClickAction.Invoke()
                LastMouseDownTick = Environment.TickCount
                MouseDownClicks(1000, LastMouseDownTick)
                MyBase.OnMouseDown(e)
            End Sub

            Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
                IsPressed = False
                Invalidate()
                MyBase.OnMouseUp(e)
            End Sub

            Protected Overrides Sub OnPaint(e As PaintEventArgs)
                If IsPressed Then
                    e.Graphics.Clear(Color.LightBlue)
                ElseIf IsHot Then
                    e.Graphics.Clear(Color.AliceBlue)
                Else
                    e.Graphics.Clear(SystemColors.Control)
                End If

                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.CadetBlue, ButtonBorderStyle.Solid)

                Dim text = If(IsUp, "5", "6")
                Dim font = New Font("Marlett", Me.Font.Size - 1)
                Dim textSize = e.Graphics.MeasureString(text, font)
                Dim offsetY = 0
                If text = "5" Then offsetY += CInt(textSize.Height * 0.1)
                Dim offsetX = textSize.Width * 0.01
                Dim x = (Width - textSize.Width) / 2
                Dim y = (Height - textSize.Height) / 2 + offsetY
                Dim brush = If(Enabled, Brushes.Black, SystemBrushes.GrayText)
                e.Graphics.DrawString(text, font, brush, CInt(x + offsetX), CInt(y))

                MyBase.OnPaint(e)
            End Sub

            Async Sub MouseDownClicks(sleep As Integer, tick As Integer)
                Await Task.Run(Sub() Thread.Sleep(sleep))

                If IsPressed AndAlso LastMouseDownTick = tick Then
                    ClickAction.Invoke()
                    MouseDownClicks(20, tick)
                End If
            End Sub
        End Class
    End Class

    Public Class TextEdit
        Inherits UserControl

        Public WithEvents TextBox As New TextBoxEx
        Public Shadows Event TextChanged()
        Private BorderColor As Color = Color.CadetBlue

        Sub New()
            SetStyle(ControlStyles.Opaque Or ControlStyles.ResizeRedraw, True)
            TextBox.BorderStyle = BorderStyle.None
            Controls.Add(TextBox)
            BackColor = Color.White
            AddHandler TextBox.GotFocus, Sub() SetColor(Color.CornflowerBlue)
            AddHandler TextBox.LostFocus, Sub() SetColor(Color.CadetBlue)
            AddHandler TextBox.TextChanged, Sub() RaiseEvent TextChanged()
        End Sub

        Private Sub SetColor(c As Color)
            BorderColor = c
            Invalidate()
        End Sub

        Private TextValue As String

        <BrowsableAttribute(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Overrides Property Text As String
            Get
                Return TextBox.Text
            End Get
            Set(value As String)
                TextBox.Text = value
            End Set
        End Property

        Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
            If TextBox.Multiline Then
                TextBox.Top = 2
                TextBox.Left = 2
                TextBox.Width = ClientSize.Width - 4
                TextBox.Height = ClientSize.Height - 4
            Else
                TextBox.Top = (ClientSize.Height - TextBox.Height) \ 2
                TextBox.Left = 2
                TextBox.Width = ClientSize.Width - 4
                TextBox.Height = TextRenderer.MeasureText("gG", TextBox.Font).Height
            End If

            MyBase.OnLayout(levent)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim r = ClientRectangle
            r.Inflate(-1, -1)

            Using brush As New SolidBrush(BackColor)
                e.Graphics.FillRectangle(If(Enabled, brush, SystemBrushes.Control), r)
            End Using

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid)
            MyBase.OnPaint(e)
        End Sub
    End Class

    Public Interface IPage
        Property Node As TreeNode
        Property Path As String
        Property TipProvider As TipProvider
        Property FormSizeScaleFactor As SizeF
    End Interface

    Public Class DataGridViewEx
        Inherits DataGridView

        Function AddTextBoxColumn() As DataGridViewTextBoxColumn
            Dim ret As New DataGridViewTextBoxColumn
            Columns.Add(ret)
            Return ret
        End Function

        Function AddComboBoxColumn() As DataGridViewComboBoxColumn
            Dim ret As New DataGridViewComboBoxColumn
            Columns.Add(ret)
            Return ret
        End Function
    End Class

    Public Class TabControlEx
        Inherits TabControl

        Private DragStartPosition As Point = Point.Empty
        Private TabType As Type

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            DragStartPosition = New Point(e.X, e.Y)
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            If e.Button <> MouseButtons.Left Then Return

            Dim rect = New Rectangle(DragStartPosition, Size.Empty)
            rect.Inflate(SystemInformation.DragSize)

            Dim page = HoverTab()

            If Not page Is Nothing AndAlso Not rect.Contains(e.X, e.Y) Then
                TabType = page.GetType
                DoDragDrop(page, DragDropEffects.All)
            End If

            DragStartPosition = Point.Empty
            MyBase.OnMouseMove(e)
        End Sub

        Protected Overrides Sub OnDragOver(e As DragEventArgs)
            Dim hoverTab = Me.HoverTab()

            If hoverTab Is Nothing Then
                e.Effect = DragDropEffects.None
            Else
                If e.Data.GetDataPresent(TabType) Then
                    e.Effect = DragDropEffects.Move
                    Dim dragTab = DirectCast(e.Data.GetData(TabType), TabPage)

                    If hoverTab Is dragTab Then Return

                    Dim tabRect = GetTabRect(TabPages.IndexOf(hoverTab))
                    tabRect.Inflate(-3, -3)

                    If tabRect.Contains(PointToClient(New Point(e.X, e.Y))) Then
                        SwapTabPages(dragTab, hoverTab)
                        SelectedTab = dragTab
                    End If
                End If
            End If

            MyBase.OnDragOver(e)
        End Sub

        Private Function HoverTab() As TabPage
            For index = 0 To TabCount - 1
                If GetTabRect(index).Contains(PointToClient(Cursor.Position)) Then
                    Return TabPages(index)
                End If
            Next
        End Function

        Private Sub SwapTabPages(ByVal tp1 As TabPage, ByVal tp2 As TabPage)
            Dim index1 = TabPages.IndexOf(tp1)
            Dim index2 = TabPages.IndexOf(tp2)
            TabPages(index1) = tp2
            TabPages(index2) = tp1
        End Sub
    End Class

    Public Class LabelProgressBar
        Inherits Control

        Public Sub New()
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.Selectable, False)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

            ForeColor = Color.Green
            BackColor = SystemColors.Control
        End Sub

        Private _Minimum As Double

        Public Property Minimum() As Double
            Get
                Return _Minimum
            End Get
            Set
                If _Minimum <> Value Then
                    _Minimum = Value
                    Invalidate()
                End If
            End Set
        End Property

        Private _Maximum As Double = 100

        Public Property Maximum() As Double
            Get
                Return _Maximum
            End Get
            Set
                If _Maximum <> Value Then
                    _Maximum = Value
                    Invalidate()
                End If
            End Set
        End Property

        Private _Value As Double

        Public Property Value() As Double
            Get
                Return _Value
            End Get
            Set
                If _Value <> Value Then
                    _Value = Value
                    Invalidate()
                End If
            End Set
        End Property

        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set
                MyBase.Text = Value
                Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Using br = New SolidBrush(ForeColor)
                g.FillRectangle(br, New RectangleF(0, 0, CSng(Width * (Value - Minimum) / Maximum), Height))
                g.DrawString(Text, Font, Brushes.Black, 0, CInt((Height - FontHeight) / 2))
            End Using

            MyBase.OnPaint(e)
        End Sub
    End Class
End Namespace
