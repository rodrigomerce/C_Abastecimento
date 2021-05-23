Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Medias

	Dim prefixoAB(1000)
	Dim combustivelAB(1000)

	Dim prefixoKM(1000)
	Dim KM(1000)

	Dim prefixoKMA(1000)
	Dim KMA(1000)
	Dim UltimaLinhaGrid As Integer = 0

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()
		If Textbox1.Text <> "" Then

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
				Dgbgrid.Columns.Add("km dia ant", "Km Dia ant.")
				Dgbgrid.Columns.Add("km dia", "Km Dia")
				Dgbgrid.Columns.Add("dif.km", "Dif.Km")
				Dgbgrid.Columns.Add("combustivel", "Combustivel")
				Dgbgrid.Columns.Add("media", "Média")


				Dgbgrid.Columns(0).Width = 70
				Dgbgrid.Columns(1).Width = 70
				Dgbgrid.Columns(2).Width = 70
				Dgbgrid.Columns(3).Width = 70
				Dgbgrid.Columns(4).Width = 70
				Dgbgrid.Columns(5).Width = 70


				Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
				Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black

				Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

				Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray

				Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
				Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
				Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data ORDER BY prefixo", con)
				Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data AND empresa=@empresa ORDER BY prefixo", con)

				command.Parameters.AddWithValue("@data", data_SQL(Textbox1.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					prefixoAB(i) = dr.Item("prefixo")
					combustivelAB(i) = Format(dr.Item("combustivel"), "#.00")
					'Dgbgrid.rows(10)
					Dgbgrid.rows.add("")
					Dgbgrid.Rows(i).Cells(0).Value = dr.Item("prefixo")
					Dgbgrid.Rows(i).Cells(4).Value = dr.Item("combustivel")

					i = i + 1
				End While
				UltimaLinhaGrid = i
				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()
			End Using

			'		Refresh()
			' KM* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
			Dim n_reg As Integer = 300

			Using con As SqlConnection = getconnectionSQL()

				For x = 0 To n_reg
					prefixoAB(x) = 0
					KM(x) = 0
				Next

				con.Open()

				'		Dim command As New SqlCommand("SELECT * FROM KM where data=@data", con)
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data AND empresa=@empresa", con)

				command.Parameters.AddWithValue("@data", data_SQL(Textbox1.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					prefixoKM(i) = dr.Item("prefixo")
					KM(i) = Format(dr.Item("hodometro"), "###,###")
					i = i + 1
				End While
				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))

				For y = 1 To UltimaLinhaGrid - 1
					Dim x As Integer = 1

					While (x < n_reg) And (Dgbgrid.Rows(y).Cells(0).Value <> prefixoKM(x))
						x = x + 1
					End While
					If (Dgbgrid.Rows(y).Cells(0).Value = prefixoKM(x)) Then
						Dgbgrid.Rows(y).Cells(2).Value = KM(x)
					End If

				Next

				Refresh()
				con.Close()
			End Using

			' KM DO DIA ANTERIOR * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

			Using con As SqlConnection = getconnectionSQL()

				For k = 0 To n_reg
					prefixoKMA(k) = 0
					KMA(k) = 0
				Next

				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM KM where data=@dataOntem", con)
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem AND empresa=@empresa", con)

				command.Parameters.AddWithValue("@ontem", CDate(data_SQL(Textbox1.Text)).AddDays(-1))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					prefixoKMA(i) = dr.Item("prefixo")
					KMA(i) = Format(dr.Item("hodometro"), "###,###")
					i = i + 1
				End While
				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()
			End Using

			For y = 1 To UltimaLinhaGrid - 1
				Dim x As Integer = 1

				While (x < n_reg) And (Dgbgrid.Rows(y).Cells(0).Value <> prefixoKMA(x))
					x = x + 1
				End While
				If (Dgbgrid.Rows(y).Cells(0).Value = prefixoKMA(x)) Then
					Dgbgrid.Rows(y).Cells(1).Value = KMA(x)
					If (Dgbgrid.Rows(y).Cells(2).Value <> "") And (Dgbgrid.Rows(y).Cells(1).Value <> "") Then
						Dgbgrid.Rows(y).Cells(3).Value = CDbl(Dgbgrid.Rows(y).Cells(2).Value - KMA(x))
						If CDbl(Dgbgrid.Rows(y).Cells(4).Value) > 0 Then Dgbgrid.Rows(y).Cells(5).Value = Format(CDbl(Dgbgrid.Rows(y).Cells(3).Value) / CDbl(Dgbgrid.Rows(y).Cells(4).Value), "#.##")
					End If
				End If
			Next

		End If
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
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub

	Private Sub Medias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Call Tiptext()
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Buscar")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
	End Sub

	Private Sub Textbox1_TextChanged(sender As Object, e As EventArgs) Handles Textbox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Textbox1_Click(sender As Object, e As EventArgs) Handles Textbox1.Click
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		Textbox1.Text = MonthCalendar1.SelectionStart
	End Sub
End Class