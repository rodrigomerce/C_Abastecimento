Imports System.Data.SqlClient

Imports Microsoft.Office.Interop
Public Class Relatorios

	Dim consumo(4)
	Dim QtA(4)


	Dim Fxh(24)
	Dim FXcons(24)
	Dim FxQt(24)



	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()

		For i = 0 To 4
			consumo(i) = 0
			QtA(i) = 0
		Next

		For i = 0 To 24
			Fxh(i) = 0
			FXcons(i) = 0
			FxQt(i) = 0
		Next


		Using con As SqlConnection = getconnectionSQL()

			Dim labelarray(12) As Label

			labelarray(0) = Label1
			labelarray(1) = Label2
			labelarray(2) = Label3
			labelarray(3) = Label4
			labelarray(4) = Label5
			labelarray(5) = Label6
			labelarray(6) = Label7
			labelarray(7) = Label8
			labelarray(8) = Label9
			labelarray(9) = Label10
			labelarray(10) = Label11
			labelarray(11) = Label12

			Dgbgrid.Rows.Clear()
			Dgbgrid.Columns.Clear()

			Dgbgrid.AllowUserToAddRows = False
			Dgbgrid.AllowUserToDeleteRows = False
			Dgbgrid.EditMode = DataGridViewEditMode.EditProgrammatically
			Dgbgrid.MultiSelect = False
			Dgbgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
			Dgbgrid.AllowUserToOrderColumns = False
			Dgbgrid.AllowUserToResizeColumns = False

			Dgbgrid.Columns.Add("bomba", "Bomba")
			Dgbgrid.Columns.Add("consumo", "Consumo")
			Dgbgrid.Columns.Add("qt.abast", "Qt.Abast")
			Dgbgrid.Columns.Add("med.abast", "Med.Abast")

			Dgbgrid.Columns(0).Width = 45
			Dgbgrid.Columns(1).Width = 60
			Dgbgrid.Columns(2).Width = 60
			Dgbgrid.Columns(3).Width = 60

			Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
			Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black

			Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

			Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray

			Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

			For i = 1 To 4
				consumo(i) = 0
				QtA(i) = 0
			Next

			con.Open()

			'Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data", con)
			Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data AND empresa=@empresa", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()

			Dim y As Integer = 0

			While dr.Read()
				consumo(dr.Item("bomba")) = consumo(dr.Item("bomba")) + dr.Item("combustivel")
				QtA(dr.Item("bomba")) = QtA(dr.Item("bomba")) + 1

			End While

			Dim ttotal As Double = 0
			Dim ttqta As Double = 0
			Dim linha As Integer = 0

			Dim grid(10)

			For x = 1 To 4

				grid(0) = x
				grid(1) = Format(consumo(x), "#,###.0")
				grid(2) = Format(QtA(x), "#,###")
				grid(3) = ""
				If QtA(x) > 0 Then grid(3) = Format(consumo(x) / QtA(x), "#,###.0")


				Me.Dgbgrid.Rows.Add(grid(0), grid(1), grid(2), grid(3))

				labelarray((x - 1) * 3 + 0).Text = Format(consumo(x), "#,###.0")
				labelarray((x - 1) * 3 + 1).Text = Format(QtA(x), "#,###")

				If QtA(x) > 0 Then labelarray((x - 1) * 3 + 2).Text = Format(consumo(x) / QtA(x), "#,###.0")

				ttotal = ttotal + consumo(x)
				ttqta = ttqta + QtA(x)

			Next

			grid(4) = "TOTAL"
			grid(5) = Format(ttotal, "#,###.0")
			grid(6) = Format(ttqta, "#,###")
			If ttqta Then grid(7) = Format(ttotal / ttqta, "#,##.0")

			Me.Dgbgrid.Rows.Add("", "", "", "")

			Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))

		End Using

		' RELATÓRIO FAIXA HORARIA * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *


		Using con As SqlConnection = getconnectionSQL()

			Dgbgrid2.Rows.Clear()
			Dgbgrid2.Columns.Clear()

			Dgbgrid2.AllowUserToAddRows = False
			Dgbgrid2.AllowUserToDeleteRows = False
			Dgbgrid2.EditMode = DataGridViewEditMode.EditProgrammatically
			Dgbgrid2.MultiSelect = False
			Dgbgrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
			Dgbgrid2.AllowUserToOrderColumns = False
			Dgbgrid2.AllowUserToResizeColumns = False


			Dgbgrid2.Columns.Add("bomba", "Bomba")
			Dgbgrid2.Columns.Add("consumo", "Consumo")
			Dgbgrid2.Columns.Add("qt.abast", "Qt.Abast")
			Dgbgrid2.Columns.Add("med.abast", "Med.Abast")


			Dgbgrid2.Columns(0).Width = 45
			Dgbgrid2.Columns(1).Width = 60
			Dgbgrid2.Columns(2).Width = 60
			Dgbgrid2.Columns(3).Width = 60


			Dgbgrid2.DefaultCellStyle.SelectionBackColor = Color.White
			Dgbgrid2.DefaultCellStyle.SelectionForeColor = Color.Black

			Dgbgrid2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

			Dgbgrid2.RowsDefaultCellStyle.BackColor = Color.LightGray

			Dgbgrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid2.RowHeadersDefaultCellStyle.BackColor = Color.Black

			For i = 1 To 24
				FXcons(i) = 0
				FxQt(i) = 0
			Next

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data", con)

			command.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
			Dim dr As SqlDataReader '= command.ExecuteReader()

			dr = command.ExecuteReader()
			Dim hr
			Dim h
			Dim m
			While dr.Read()
				h = dr.Item("hora").hours
				m = dr.Item("hora").Totalminutes

				hr = CInt(h)

				FXcons(hr) = FXcons(hr) + dr.Item("combustivel")
				FxQt(hr) = FxQt(hr) + 1

			End While

			Dim ttotal As Double = 0
			Dim ttqta As Double = 0
			Dim w As Integer = 1
			Dim grid(10)

			For i = 0 To 23

				If FXcons(i) > 0 Then
					grid(1) = i
					grid(2) = Format(FXcons(i), "#,###.0")
					grid(3) = Format(FxQt(i), "#,###")
					If FxQt(i) > 0 Then
						grid(4) = Format(FXcons(i) / FxQt(i), "#,###.0")
					Else
						grid(4) = 0
					End If

					Me.Dgbgrid2.Rows.Add(grid(1), grid(2), grid(3), grid(4))

					w = w + 1
					ttotal = ttotal + FXcons(i)
					ttqta = ttqta + FxQt(i)
				End If

				grid(4) = "TOTAL"
				grid(5) = Format(ttotal, "#,###.0")
				grid(6) = Format(ttqta, "#,###")

				If ttqta > 0 Then
					grid(7) = Format(ttotal / ttqta, "#,###.0")
				Else
					grid(7) = 0
				End If


			Next i
			Me.Dgbgrid2.Rows.Add("", "", "", "")

			Me.Dgbgrid2.Rows.Add(grid(4), grid(5), grid(6), grid(7))
		End Using



	End Sub


	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
		If e.KeyCode = 13 Then Call Buscar()
	End Sub

	Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		TextBox1.Text = MonthCalendar1.SelectionStart
	End Sub

	Private Sub Relatorios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Relatório de abasteciementos feitos durante o dia")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
	End Sub
End Class