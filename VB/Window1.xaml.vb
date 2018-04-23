Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System.Collections
Imports DevExpress.Xpf.Core.Native
Imports System.ComponentModel
Imports System

Namespace Printing_GroupSummaryAlignment

    Partial Public Class Window1
        Inherits Window

        Public Sub New()

            InitializeComponent()

            Dim list As New List(Of TestData)()
            For i As Integer = 0 To 99
                list.Add(New TestData() With {.Number1 = i, .Number2 = Convert.ToInt32((i + 10) / 10), .Text1 = "row " & i, .Text2 = "ROW " & i})
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

    Public Class CollectionConverterDecorator
        Inherits Border
        Implements IWeakEventListener


        Public Property SummaryText() As String
            Get
                Return CStr(GetValue(SummaryTextProperty))
            End Get
            Set(ByVal value As String)
                SetValue(SummaryTextProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SummaryTextProperty As DependencyProperty = DependencyProperty.Register("SummaryText", GetType(String), GetType(CollectionConverterDecorator), New PropertyMetadata(Nothing))

        Public Property Collection() As IList
            Get
                Return CType(GetValue(CollectionProperty), IList)
            End Get
            Set(ByVal value As IList)
                SetValue(CollectionProperty, value)
            End Set
        End Property
        Public Shared ReadOnly CollectionProperty As DependencyProperty = DependencyProperty.Register("Collection",
                                                                              GetType(IList),
                                                                              GetType(CollectionConverterDecorator),
                                                                              New PropertyMetadata(Nothing, AddressOf OnCollectionChanged))

        Private Shared Sub OnCollectionChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, CollectionConverterDecorator).OnCollectionChanged()
        End Sub
        Private bindingList As IBindingList
        Private Sub OnCollectionChanged()
            If bindingList IsNot Nothing Then
                ListChangedEventManager.RemoveListener(bindingList, Me)
            End If
            bindingList = BindingListAdapter.CreateFromList(Collection, ItemPropertyNotificationMode.All)
            If bindingList IsNot Nothing Then
                ListChangedEventManager.AddListener(bindingList, Me)
            End If
        End Sub

#Region "IWeakEventListener Members"

        Private Function ReceiveWeakEvent(ByVal managerType As System.Type, ByVal sender As Object, ByVal e As System.EventArgs) As Boolean Implements IWeakEventListener.ReceiveWeakEvent
            If managerType Is GetType(ListChangedEventManager) Then
                InvalidateMeasure()
                Return True
            End If
            Return False
        End Function

#End Region
        Protected Overrides Function MeasureOverride(ByVal constraint As Size) As Size
            If bindingList IsNot Nothing Then
                SummaryText = GetSummaryText()
            End If
            Return MyBase.MeasureOverride(constraint)
        End Function
        Private Function GetSummaryText() As String
            Dim res As String = String.Empty
            For Each item As GridGroupSummaryData In bindingList
                res &= item.Text
                If (Not item.IsLast) Then
                    res &= ", "
                End If
            Next item
            Return res
        End Function

    End Class
End Namespace
