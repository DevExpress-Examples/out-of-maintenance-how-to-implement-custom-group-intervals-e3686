Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_CustomGroupIntervals
	Partial Public Class MainPage
		Inherits UserControl
        Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
            Dim assembly As System.Reflection.Assembly = _
                System.Reflection.Assembly.GetExecutingAssembly()
            Dim stream As Stream = assembly.GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub
        Private Sub pivotGridControl1_CustomGroupInterval(ByVal sender As Object, _
                                                          ByVal e As PivotCustomGroupIntervalEventArgs)
            If (Not Object.ReferenceEquals(e.Field, fieldCustomerGroup)) Then
                Return
            End If
            Dim customerName As String = Convert.ToString(e.Value)
            If customerName.Chars(0) < "F"c Then
                e.GroupValue = "A-E"
            ElseIf customerName.Chars(0) > "E"c AndAlso customerName.Chars(0) < "T"c Then
                e.GroupValue = "F-S"
            ElseIf customerName.Chars(0) > "S"c Then
                e.GroupValue = "T-Z"
            End If
        End Sub
	End Class
End Namespace