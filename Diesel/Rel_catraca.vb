Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Rel_catraca

	Dim prefixo(300) As Integer
	Dim catraca(300) As Integer

	Private Sub Rel_catraca_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Call Tiptext()
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Buscar")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()

		Using con As SqlConnection = getconnectionSQL()
			Dgbgrid.Rows.Clear()
			Dgbgrid.Columns.Clear()
			Dgbgrid.AllowUserToAddRows = False
			Dgbgrid.AllowUserToDeleteRows = False
			Dgbgrid.EditMode = DataGridViewEditMode.EditProgrammatically
			Dgbgrid.MultiSelect = False
			Dgbgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
			Dgbgrid.AllowUserToOrderColumns = False
			Dgbgrid.AllowUserToResizeColumns = False
			Dgbgrid.Columns.Add("prefixo", "Prefixo")
			Dgbgrid.Columns.Add("catraca ant.", "Catraca Ant.")
			Dgbgrid.Columns.Add("catraca dia", "catraca Dia")
			Dgbgrid.Columns.Add("dif", "Dif")
			Dgbgrid.Columns(0).Width = 70
			Dgbgrid.Columns(1).Width = 80
			Dgbgrid.Columns(2).Width = 70
			Dgbgrid.Columns(3).Width = 70
			Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
			Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
			Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray
			Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

			con.Open()
			'	Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data and empresa=@empresa ORDER BY prefixo", con)
			Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim y As Integer = 0
			While dr.Read()
				Dgbgrid.Rows.Add("")
				If Trim(dr.Item("registro")) <> "" Then Dgbgrid.Rows(y).Cells(0).Value = Trim(dr.Item("prefixo"))
				If Not IsDBNull(dr.Item("catraca")) Then
					If dr.Item("catraca") <> "" Then Dgbgrid.Rows(y).Cells(2).Value = Format(dr.Item("catraca"), "###,###")
				End If
				y = y + 1
			End While
			'Catch ex As Exception
			'	MsgBox(ex.Message)
			'Finally
			con.Close()

			'End Try

		End Using

		Using con As SqlConnection = getconnectionSQL()


			For i = 0 To 300
				prefixo(i) = 0
				catraca(i) = 0
			Next

			con.Open()
			'	Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem And empresa=@empresa ORDER BY prefixo", con)
			Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem ", con)
			command.Parameters.AddWithValue("@ontem", data_SQL(CDate(TextBox1.Text).AddDays(-1)))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim y As Integer = 0
			Dgbgrid.Rows.Add("")
			While dr.Read()
				Dgbgrid.Rows.Add("")
				prefixo(y) = dr.Item("prefixo")
				If Not IsDBNull(dr.Item("catraca")) Then catraca(y) = dr.Item("catraca")

				y = y + 1
			End While
			'Catch ex As Exception
			'	MsgBox(ex.Message)
			'Finally
			con.Close()

			'End Try



			Dim x As Integer = 0

			While Dgbgrid.Rows(x).Cells(0).Value <> ""
				For i = 0 To 300
					If Dgbgrid.Rows(x).Cells(0).Value = prefixo(i) Then
						If catraca(i) <> 0 Then Dgbgrid.Rows(x).Cells(1).Value = Format(dr.Item("catraca"), "###,###")
					End If

				Next

				x = x + 1
			End While

		End Using
	End Sub



	Private Sub Textbox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Textbox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		TextBox1.Text = MonthCalendar1.SelectionStart
		TextBox2.Text = CDate(TextBox1.Text).AddDays(1)
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Call Relatorio_excel()
	End Sub

	Private Sub Relatorio_excel()

		Dim XcelApp As New Excel.Application

		If Dgbgrid.Rows.Count > 0 Then
			'			Try
			XcelApp.Application.Workbooks.Add(Type.Missing)
			For i As Integer = 1 To Dgbgrid.Columns.Count
				XcelApp.Cells(1, i) = Dgbgrid.Columns(i - 1).HeaderText
			Next

			Dim x As Integer = 0
			Dim y As Integer = 0
			While x < Dgbgrid.Rows.Count

				For j As Integer = 0 To Dgbgrid.Columns.Count - 1
					On Error Resume Next
					XcelApp.Cells(y + 2, j + 1) = Dgbgrid.Rows(x).Cells(j).Value.ToString()
				Next
				y = y + 1
				x = x + 1
			End While
			'



			XcelApp.Cells(1, 3) = "DATA "
			XcelApp.Cells(1, 4) = TextBox1.Text



			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub
End Class