Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Relatorio_prefeitura

	Dim txtselectdt As Object

	Dim Rprefixo(300) As Integer
	Dim RKM(300) As Double

	Private Sub Relatorio_prefeitura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Textbox1.Text = data_Select
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Buscar")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()
		If Textbox1.Text <> "" And TextBox2.Text <> "" Then
			'try
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
				Dgbgrid.Columns.Add("1km", "1º KM")
				Dgbgrid.Columns.Add("2km", "2º KM")
				Dgbgrid.Columns.Add("dif.km", "Dif.KM")
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
				Dim command As New SqlCommand("SELECT * FROM Capac_onibus WHERE relatorio=@relatorio and empresa=@empresa ORDER BY prefixo", con)
				command.Parameters.AddWithValue("@relatorio", "S")
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					Dgbgrid.Rows.Add("")
					Dgbgrid.Rows(i).Cells(0).Value = dr.Item("prefixo")
					i = i + 1
				End While
				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'Finally
				con.Close()

				'End Try

			End Using

			'KM 1º DIA

			Using con As SqlConnection = getconnectionSQL()

				For y = 0 To 300
					Rprefixo(y) = 0
					RKM(y) = 0
				Next

				Dim x As Integer = 1

				con.Open()
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data and empresa=@empresa ORDER BY prefixo", con)
				command.Parameters.AddWithValue("@data", data_SQL(Textbox1.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					Rprefixo(i) = dr.Item("prefixo")
					RKM(i) = dr.Item("hodometro")
					i = i + 1
				End While
				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'Finally
				con.Close()

				'End Try


				For y = 0 To Dgbgrid.RowCount - 1
					Dim z As Integer = 1

					While (z < 300) And (Dgbgrid.Rows(y).Cells(0).Value <> Rprefixo(z))
						z = z + 1
					End While
					If (Dgbgrid.Rows(y).Cells(0).Value = Rprefixo(z)) Then
						Dgbgrid.Rows(y).Cells(1).Value = Format(RKM(z), "###,###")
					End If
				Next

			End Using

			'KM DA DATA FIM
			Using con As SqlConnection = getconnectionSQL()

				For y = 0 To 300
					Rprefixo(y) = 0
					RKM(y) = 0
				Next

				Dim x As Integer = 1

				con.Open()
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data and empresa=@empresa", con)

				command.Parameters.AddWithValue("@data", data_SQL(TextBox2.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					Rprefixo(i) = dr.Item("prefixo")
					RKM(i) = dr.Item("hodometro")
					i = i + 1
				End While
				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'Finally
				con.Close()

				'End Try

				For y = 0 To Dgbgrid.RowCount - 1
					Dim z As Integer = 1
					While (z < 300) And (Dgbgrid.Rows(y).Cells(0).Value <> Rprefixo(z))
						z = z + 1
					End While

					If (Dgbgrid.Rows(y).Cells(0).Value = Rprefixo(z)) Then
						Dgbgrid.Rows(y).Cells(2).Value = Format(RKM(z), "###,###")
						If (Dgbgrid.Rows(y).Cells(2).Value <> "") And (Dgbgrid.Rows(y).Cells(1).Value <> "") Then
							Dgbgrid.Rows(y).Cells(3).Value = CDbl(Dgbgrid.Rows(y).Cells(2).Value) - CDbl(Dgbgrid.Rows(y).Cells(1).Value)
						End If
					End If

				Next

			End Using

		Else
			MsgBox("Data INVÁLIDA")
		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Call RelatorioPrefeitura_excel()
	End Sub

	Private Sub RelatorioPrefeitura_excel()

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
			XcelApp.Cells(1, 7) = Textbox1.Text
			XcelApp.Cells(1, 8) = "à"
			XcelApp.Cells(1, 9) = TextBox2.Text

			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub

	Private Sub Textbox1_TextChanged(sender As Object, e As EventArgs) Handles Textbox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Textbox1_Click(sender As Object, e As EventArgs) Handles Textbox1.Click
		txtselectdt = sender
		MonthCalendar1.Visible = True
	End Sub



	Private Sub textbox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub textbox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
		txtselectdt = sender
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		txtselectdt.Text = MonthCalendar1.SelectionStart
	End Sub
End Class