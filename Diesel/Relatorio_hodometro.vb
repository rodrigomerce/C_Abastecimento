Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Relatorio_hodometro
	Private Sub Relatorio_hodometro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Call Buscar()
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
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
			Dgbgrid.Columns(0).Width = 70
			Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
			Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
			Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray
			Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

			con.Open()
			'Dim command As New SqlCommand("SELECT * FROM Capac_onibus WHERE relatorio=@relatorio and empresa=@empresa ORDER BY prefixo", con)
			Dim command As New SqlCommand("SELECT distinct(prefixo) FROM KM WHERE st_hodo=@status_hodometro AND empresa=@empresa ORDER BY prefixo", con)
			command.Parameters.AddWithValue("@status_hodometro", "1")
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim y As Integer = 0
			While dr.Read()
				Dgbgrid.Rows.Add("")
				Dgbgrid.Rows(y).Cells(0).Value = Trim(dr.Item("prefixo"))
				y = y + 1
			End While
			'Catch ex As Exception
			'	MsgBox(ex.Message)
			'Finally
			con.Close()

			'End Try

		End Using

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
			XcelApp.Cells(1, 4) = Now.ToShortDateString



			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub

End Class