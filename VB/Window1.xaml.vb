Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports DevExpress.Wpf.Editors
Imports DevExpress.Wpf.Grid

Namespace Printing_GroupSummaryAlignment

    Partial Public Class Window1
        Inherits Window

        Public Sub New()

            InitializeComponent()

            Dim list As New List(Of TestData)()
            For i As Integer = 0 To 99
                list.Add(New TestData() With {.Number1 = i, _
                                              .Number2 = CType((i + 10) / 10, Integer), _
                                              .Text1 = "row " & i, _
                                              .Text2 = "ROW " & i})
            Next i
            DataContext = list
        End Sub

        Private Sub showPreviewButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            view.ShowPrintPreviewDialog(Me)
        End Sub
    End Class

    Public Class TestData
        Private privateNumber1 As Integer
        Public Property Number1() As Integer
            Get
                Return privateNumber1
            End Get
            Set(ByVal value As Integer)
                privateNumber1 = value
            End Set
        End Property

        Private privateNumber2 As Integer
        Public Property Number2() As Integer
            Get
                Return privateNumber2
            End Get
            Set(ByVal value As Integer)
                privateNumber2 = value
            End Set
        End Property

        Private privateText1 As String
        Public Property Text1() As String
            Get
                Return privateText1
            End Get
            Set(ByVal value As String)
                privateText1 = value
            End Set
        End Property

        Private privateText2 As String
        Public Property Text2() As String
            Get
                Return privateText2
            End Get
            Set(ByVal value As String)
                privateText2 = value
            End Set
        End Property
    End Class

    Public Class CustomGroupRowContentControl
        Inherits GroupRowContentControl
        Private Overloads ReadOnly Property GroupRowData() As GroupRowData
            Get
                Return TryCast(RowData.GetRowData(Me), GroupRowData)
            End Get
        End Property

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()

            Dim edit As TextEdit = TryCast(GetTemplateChild("PART_Edit"), TextEdit)
            Dim panel As Panel = TryCast(GetTemplateChild("PART_Panel"), Panel)
            Dim summary As TextEdit = TryCast(GetTemplateChild("PART_Summary"), TextEdit)

            panel.Width = edit.Width

            edit.ClearValue(FrameworkElement.WidthProperty)
            edit.BorderThickness = New Thickness(edit.BorderThickness.Left, _
                edit.BorderThickness.Top, 0, edit.BorderThickness.Bottom)

            summary.Style = View.PrintGroupRowStyle
            summary.BorderThickness = New Thickness(0, edit.BorderThickness.Top, _
                summary.BorderThickness.Right, edit.BorderThickness.Bottom)
            summary.EditValue = GetSummaryText()
        End Sub

        Protected Overrides Function GetGroupRowText() As String
            Return String.Format("{0}: {1}", GroupRowData.GroupValue.Column.HeaderCaption, _
                                 GroupRowData.GroupValue.Value)
        End Function

        Private Function GetSummaryText() As String
            Dim res As String = String.Empty
            For Each item As GridGroupSummaryData In GroupRowData.GroupSummaryData
                res &= item.Text
                If (Not item.IsLast) Then
                    res &= ", "
                End If
            Next item
            Return res
        End Function
    End Class
End Namespace
