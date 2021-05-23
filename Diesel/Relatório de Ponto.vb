Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Relatório_de_Ponto
	Dim txtselectdt As Object
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()

		If (TextBox2.Text <> "") And (TextBox2.Text <> "") Then
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
				Dgbgrid.Columns.Add("registro", "Registro")
				Dgbgrid.Columns.Add("hora", "Hora")
				Dgbgrid.Columns.Add("carro", "Carro")
				Dgbgrid.Columns(0).Width = 70
				Dgbgrid.Columns(1).Width = 80
				Dgbgrid.Columns(2).Width = 70
				Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
				Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black
				Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
				Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray
				Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
				Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
				Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

				con.Open()
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data>=@datainic AND data<=@datafim AND empresa=@empresa ORDER BY prefixo", con)
				command.Parameters.AddWithValue("@datainic", data_SQL(TextBox1.Text))
				command.Parameters.AddWithValue("@datafim", data_SQL(TextBox2.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()
				Dim y As Integer = 0
				While dr.Read()
					If (Trim(dr.Item("registro")) <> "") And (Trim(dr.Item("registro")) <> 0) Then
						Dgbgrid.Rows.Add("")

						Dgbgrid.Rows(y).Cells(0).Value = Trim(dr.Item("registro"))
						If Trim(dr.Item("horaMot")) <> "" Then Dgbgrid.Rows(y).Cells(1).Value = dr.Item("horaMot")
						If Trim(dr.Item("prefixo")) <> "" Then Dgbgrid.Rows(y).Cells(2).Value = dr.Item("prefixo")
						y = y + 1

						If Not IsDBNull(dr.Item("registro2")) Then
							If Trim(dr.Item("registro2")) <> "" Then
								Dgbgrid.Rows.Add("")
								Dgbgrid.Rows(y).Cells(0).Value = Trim(dr.Item("registro2"))
								If Trim(dr.Item("horaMot2")) <> "" Then Dgbgrid.Rows(y).Cells(1).Value = dr.Item("horaMot2")
								If Trim(dr.Item("prefixo")) <> "" Then Dgbgrid.Rows(y).Cells(2).Value = Trim(dr.Item("prefixo"))
								Dgbgrid.BackgroundColor = Color.LightBlue

								y = y + 1
							End If
						End If
					End If
				End While
				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'Finally
				con.Close()

				'End Try

			End Using
		Else
			MsgBox("Data INVÁLIDA")
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



			XcelApp.Cells(1, 6) = "DATA "
			XcelApp.Cells(1, 7) = TextBox1.Text
			XcelApp.Cells(1, 8) = "à"
			XcelApp.Cells(1, 9) = TextBox2.Text


			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub


	Private Sub Textbox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Textbox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
		txtselectdt = sender
		MonthCalendar1.Visible = True
	End Sub

	Private Sub Textbox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Textbox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
		txtselectdt = sender
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		txtselectdt.Text = MonthCalendar1.SelectionStart
	End Sub

	Private Sub Relatório_de_Ponto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Buscar")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
	End Sub
End Class