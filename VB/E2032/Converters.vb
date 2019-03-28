Imports System
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace E2032
	Public Class RemoveParenthesesConverter
		Implements IValueConverter

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim str = TryCast(value, String)
			If str Is Nothing Then
				Return Nothing
			End If
			Return str.Replace("(", String.Empty).Replace(")", String.Empty)
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class

	Public Class PrintRowInfoToImageSourceConverter
		Implements IValueConverter

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim category As Category = Nothing
			If System.Enum.TryParse(value.ToString(), category) Then
				Select Case category
					Case E2032.Category.Deferred
						Return "Images/Deffered_32x32.png"
					Case E2032.Category.Normal
						Return "Images/Normal_32x32.png"
					Case E2032.Category.Urgent
						Return "Images/Urgent_32x32.png"
				End Select
			End If
			Return Nothing
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
