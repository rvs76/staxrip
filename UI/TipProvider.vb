Imports System.ComponentModel
Imports System.Drawing.Design

Namespace UI
    <ProvideProperty("TipText", GetType(Control))>
    Public Class TipProvider
        Inherits Component
        Implements IExtenderProvider

        Private ToolTip As New ToolTip
        Private TipTitles As New Dictionary(Of Control, String)
        Private ShortHelp As New Dictionary(Of Control, String)
        Private TipTexts As New Dictionary(Of Control, String)
        Private CreatedAdded As Boolean
        Private CleanUpAdded As Boolean

        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Property TipsFunc As Func(Of StringPairList)

        Sub New(Optional component As IContainer = Nothing)
            If Not component Is Nothing Then component.Add(Me)
            ToolTip.AutomaticDelay = 1000
            ToolTip.AutoPopDelay = 10000
            ToolTip.InitialDelay = 1000
            ToolTip.ReshowDelay = 1000
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            ToolTip.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Function CanExtend(obj As Object) As Boolean Implements IExtenderProvider.CanExtend
            Return TypeOf obj Is Control
        End Function

        <Category("TipProvider")>
        <DefaultValue("")>
        <Editor(GetType(StringEditor), GetType(UITypeEditor))>
        Function GetTipText(c As Control) As String
            If TipTexts.ContainsKey(c) Then
                Return TipTexts(c)
            End If

            Return ""
        End Function

        Sub SetTipText(c As Control, value As String)
            TipTexts(c) = value
            Init(value, c)
        End Sub

        Sub SetTip(tipText As String,
                   tipTitle As String,
                   c As Control)

            TipTitles(c) = tipTitle
            SetTipText(c, tipText)
        End Sub

        Sub SetTip(tipText As String, ParamArray controls As Control())
            If tipText = "" Then Exit Sub

            Dim title As String

            For Each i In controls
                If TypeOf i Is Label OrElse TypeOf i Is CheckBox Then
                    title = FormatName(i.Text)
                End If
            Next

            For Each i In controls
                TipTexts(i) = tipText
                If title <> "" Then TipTitles(i) = title
                Init(tipText, i)
            Next
        End Sub

        Sub Init(tipText As String, control As Control)
            If Not DesignMode Then
                AddHandler control.MouseDown, AddressOf TipMouseDown
                tipText = tipText.TrimEnd("."c)

                If tipText.Length > 80 Then
                    If HasContextMenu(control) Then
                        tipText = Nothing
                    Else
                        tipText = "Right-click for help"
                    End If
                ElseIf HelpDocument.MustConvert(tipText) Then
                    tipText = HelpDocument.ConvertMarkup(tipText, True)
                End If

                If tipText <> "" Then AddHandler control.HandleCreated, Sub() ToolTip.SetToolTip(control, tipText)
            End If
        End Sub

        Private Sub TipMouseDown(sender As Object, e As MouseEventArgs)
            If e.Button = MouseButtons.Right AndAlso
                Not HasContextMenu(DirectCast(sender, Control)) Then

                ShowHelp(DirectCast(sender, Control))
            End If
        End Sub

        Private Sub ShowHelp(c As Control)
            Dim t = GetTip(c)
            g.ShowHelp(t.Name, t.Value)
        End Sub

        Private Function GetTip(c As Control) As StringPair
            Dim ret As New StringPair
            If TipTitles.ContainsKey(c) Then ret.Name = FormatName(TipTitles(c))
            If TipTexts.ContainsKey(c) Then ret.Value = TipTexts(c)
            Return ret
        End Function

        Private Function HasContextMenu(c As Control) As Boolean
            If TypeOf c Is TextBox Then Return True
            If TypeOf c Is RichTextBox Then Return True
            If TypeOf c Is NumericUpDown Then Return True

            If TypeOf c Is ComboBox AndAlso
                DirectCast(c, ComboBox).DropDownStyle = ComboBoxStyle.DropDown Then

                Return True
            End If

            If Not c.ContextMenuStrip Is Nothing Then Return True
        End Function

        Private Function FormatName(value As String) As String
            If value.Contains(" ") Then value = value.Trim
            If value.Contains("&") AndAlso Not value.Contains(" & ") Then value = value.Replace("&", "")
            If value.EndsWith("...") Then value = value.TrimEnd("."c)
            If value.EndsWith(":") Then value = value.TrimEnd(":"c)
            Return value
        End Function

        Function GetTips() As StringPairList
            Dim ret As New StringPairList
            Dim temp As New List(Of String) 'to ignore double tips

            For Each i In TipTexts.Keys
                If Not i.IsDisposed Then 'controls in background tabs are not visible!
                    Dim t As New StringPair
                    t.Value = TipTexts(i)

                    If TipTitles.ContainsKey(i) Then
                        t.Name = FormatName(TipTitles(i))
                    Else
                        t.Name = FormatName(i.Text)
                    End If

                    If Not temp.Contains(t.Name) Then
                        temp.Add(t.Name)
                        t.Name = FormatName(t.Name)
                        ret.Add(t)
                    End If
                End If
            Next

            If Not TipsFunc Is Nothing Then
                ret.AddRange(TipsFunc.Invoke)
            End If

            ret.Sort()

            Return ret
        End Function
    End Class
End Namespace