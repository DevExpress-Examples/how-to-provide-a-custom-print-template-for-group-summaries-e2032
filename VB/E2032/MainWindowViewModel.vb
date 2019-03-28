Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace E2032

	Public Class MainWindowViewModel
		Private r As Random
		Protected Sub New()
			r = New Random()
			Items = New ObservableCollection(Of Item)()
			For i As Integer = 0 To 49
				Items.Add(Item.Create(i, "Item " & i, r.Next(0, 50), CType(r.Next(0, 3), Category)))
			Next i
		End Sub

		Public Overridable Property Items() As ObservableCollection(Of Item)
	End Class
	Public Class Item
		Protected Sub New()
		End Sub
		Public Shared Function Create(ByVal id As Integer, ByVal name As String, ByVal value As Double, ByVal category As Category) As Item
			Return ViewModelSource.Create(Function() New Item() With {
				.ID = id,
				.Name = name,
				.Value = value,
				.Category = category
			})
		End Function
		Public Overridable Property ID() As Integer

		Public Overridable Property Name() As String

		Public Overridable Property Value() As Double

		Public Overridable Property Category() As Category
	End Class

	Public Enum Category
		Deferred
		Normal
		Urgent
	End Enum
End Namespace
