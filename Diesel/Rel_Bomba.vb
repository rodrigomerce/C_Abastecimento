Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Rel_Bomba
	Dim prefixo(300)
	Dim km(300)
	Dim combustiv(300)

	Private Sub Rel_Bomba_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Textbox1.Text = CDate(Now).AddDays(-1).ToShortDateString
		ComboBox1.Items.Clear()
		Select Case Empresa
			Case "TL"
				ComboBox1.Items.Add("1")
				ComboBox1.Items.Add("2")
				ComboBox1.Items.Add("3")
				ComboBox1.Items.Add("4")
			Case "VB"
				ComboBox1.Items.Add("203")
				ComboBox1.Items.Add("204")
			Case "GT"
				ComboBox1.Items.Add("101")
			Case "TV"
				ComboBox1.Items.Add("101")
				ComboBox1.Items.Add("203")
				ComboBox1.Items.Add("204")
		End Select
		ComboBox1.Items.Add("TODAS")

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
		Dim tt_combustivel As Double = 0
		If (Textbox1.Text <> "") And (ComboBox1.Text <> "") Then

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
				Dgbgrid.Columns.Add("data_abast", "Data_abast")
				Dgbgrid.Columns.Add("hora", "Hora")
				Dgbgrid.Columns.Add("bb", "BB")
				Dgbgrid.Columns.Add("combustivel", "Combustivel")
				Dgbgrid.Columns.Add("hodometro", "Hodometro")
				Dgbgrid.Columns.Add("oleo", "Oleo")
				Dgbgrid.Columns.Add("ant. comb.", "Ant. Comb.")
				Dgbgrid.Columns.Add("ant hod", "Ant Hod")
				Dgbgrid.Columns.Add("dif km", "Dif KM")


				Dgbgrid.Columns(0).Width = 60
				Dgbgrid.Columns(1).Width = 70
				Dgbgrid.Columns(2).Width = 70
				Dgbgrid.Columns(3).Width = 30
				Dgbgrid.Columns(4).Width = 70
				Dgbgrid.Columns(5).Width = 70
				Dgbgrid.Columns(6).Width = 35
				Dgbgrid.Columns(7).Width = 65
				Dgbgrid.Columns(8).Width = 65
				Dgbgrid.Columns(9).Width = 65


				Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
				Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black

				Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

				Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray

				Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
				Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
				Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data AND bomba=@bomba ORDER BY prefixo", con)
				Dim sql As String = "SELECT * FROM Abastecimento WHERE empresa=@empresa AND data=@data AND bomba=@bomba ORDER BY prefixo"
				If ComboBox1.Text = "TODAS" Then
					sql = "SELECT * FROM Abastecimento WHERE empresa=@empresa AND data=@data ORDER BY prefixo"
				End If

				Dim command As New SqlCommand(sql, con)

				command.Parameters.AddWithValue("@data", data_SQL(Textbox1.Text))
				command.Parameters.AddWithValue("@bomba", Trim(ComboBox1.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Dim n_reg As Integer = 300
				Dim i As Integer = 0
				Label6.Text = 0
				Label7.Text = 0
				While dr.Read()

					Dgbgrid.Rows.Add("")
					Dgbgrid.Rows(i).Cells(0).Value = dr.Item("prefixo")
					Dgbgrid.Rows(i).Cells(1).Value = data_Normal(dr.Item("Data_abast"))
					Dgbgrid.Rows(i).Cells(2).Value = dr.Item("hora")
					Dgbgrid.Rows(i).Cells(3).Value = dr.Item("bomba")
					Dgbgrid.Rows(i).Cells(4).Value = Format(dr.Item("combustivel"), "#.00")
					tt_combustivel = tt_combustivel + dr.Item("combustivel")

					Select Case Trim(dr.Item("bomba"))
						Case "203"
							Label6.Text = Format(Label6.Text + dr.Item("combustivel"), "#.00")
						Case "204"
							Label7.Text = Format(Label7.Text + dr.Item("combustivel"), "#.00")
					End Select

					i = i + 1
				End While
				Refresh()
				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()


				'COMBUSTIVEL DO DIA ANTERIOR * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 


				con.Open()

				'		Dim cmd As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data", con)
				Dim cmd As New SqlCommand("SELECT * FROM Abastecimento WHERE empresa=@empresa AND data=@data", con)

				cmd.Parameters.AddWithValue("@data", CDate(data_SQL(Textbox1.Text)).AddDays(-1))
				cmd.Parameters.AddWithValue("@bomba", Trim(ComboBox1.Text))
				cmd.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr2 As SqlDataReader '= command.ExecuteReader()

				dr2 = cmd.ExecuteReader()
				Dim k As Integer = 0

				For i = 0 To n_reg
					prefixo(i) = 0
					combustiv(i) = 0
				Next

				While dr2.Read()
					prefixo(k) = dr2.Item("prefixo")
					combustiv(k) = Format(dr2.Item("combustivel"), "#.00")
					k = k + 1
				End While

				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()

				For y = 0 To Dgbgrid.RowCount - 1
					Dim x As Integer = 1

					While (x < n_reg) And (Dgbgrid.Rows(y).Cells(0).Value <> prefixo(x))
						x = x + 1
					End While
					If (Dgbgrid.Rows(y).Cells(0).Value = prefixo(x)) Then
						Dgbgrid.Rows(y).Cells(7).Value = combustiv(x)
					End If

				Next



				'KM * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

				con.Open()

				'	Dim cmd2 As New SqlCommand("SELECT * FROM KM WHERE data=@data", con)
				Dim cmd2 As New SqlCommand("SELECT * FROM KM WHERE empresa=@empresa AND data=@data", con)

				cmd2.Parameters.AddWithValue("@data", data_SQL(Textbox1.Text))
				cmd2.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr3 As SqlDataReader '= command.ExecuteReader()

				dr3 = cmd2.ExecuteReader()
				Dim Q As Integer = 0

				For i = 0 To n_reg
					prefixo(i) = 0
					km(i) = 0
				Next

				While dr3.Read()
					prefixo(Q) = dr3.Item("prefixo")
					km(Q) = Format(dr3.Item("hodometro"), "###,###")
					Q = Q + 1
				End While

				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()

				For y = 0 To Dgbgrid.RowCount - 1
					Dim x As Integer = 0
					While (x < n_reg) And (Dgbgrid.Rows(y).Cells(0).Value <> prefixo(x))
						x = x + 1
					End While
					If (Dgbgrid.Rows(y).Cells(0).Value = prefixo(x)) Then
						Dgbgrid.Rows(y).Cells(5).Value = km(x)
					End If
				Next

				'KM DO DIA ANTERIOR * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

				con.Open()

				'	Dim cmd3 As New SqlCommand("SELECT * FROM KM WHERE data=@data", con)
				Dim cmd3 As New SqlCommand("SELECT * FROM KM WHERE empresa=@empresa AND data=@data", con)

				cmd3.Parameters.AddWithValue("@data", CDate(data_SQL(Textbox1.Text)).AddDays(-1))
				cmd3.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr4 As SqlDataReader '= command.ExecuteReader()

				dr4 = cmd3.ExecuteReader()
				Dim p As Integer = 0

				For i = 0 To n_reg
					prefixo(i) = 0
					km(i) = 0
				Next

				While dr4.Read()
					prefixo(p) = dr4.Item("prefixo")
					km(p) = Format(dr4.Item("hodometro"), "###,###")
					p = p + 1
				End While

				'	Me.Dgbgrid.Rows.Add(grid(4), grid(5), grid(6), grid(7))
				con.Close()

				For y = 0 To Dgbgrid.RowCount - 1
					Dim x As Integer = 1
					While (x < n_reg) And (Dgbgrid.Rows(y).Cells(0).Value <> prefixo(x))
						x = x + 1
					End While
					If (Dgbgrid.Rows(y).Cells(0).Value = prefixo(x)) Then
						Dgbgrid.Rows(y).Cells(8).Value = km(x)
						If Dgbgrid.Rows(y).Cells(5).Value <> 0 Then Dgbgrid.Rows(y).Cells(9).Value = Dgbgrid.Rows(y).Cells(5).Value - km(x)
					End If
				Next
				Label3.Text = Format(tt_combustivel, "#,###.0")

			End Using
		Else
			MsgBox("Data INVÁLIDA!")

		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Call Relatorioexcel
	End Sub

	Private Sub Relatorioexcel()

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

			XcelApp.Cells(x + 5, 4) = Label4.Text
			XcelApp.Cells(x + 5, 5) = Label6.Text

			XcelApp.Cells(x + 5, 6) = Label5.Text
			XcelApp.Cells(x + 5, 7) = Label7.Text
			'
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If


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