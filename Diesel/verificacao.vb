Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class verificacao
	Dim txtselectdt As Object
	Dim Prefixo_abast(1000)
	Dim data_abast(1000)
	Dim Prefixo_KM(1000)
	Dim data_km(1000)

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim erro As Boolean = False
		erro = erro Or (TextBox1.Text = "")
		erro = erro Or (TextBox2.Text = "")
		If Not erro Then
			For i = 0 To 1000
				Prefixo_abast(i) = ""
				data_abast(i) = ""
				Prefixo_KM(i) = ""
				data_km(i) = ""
			Next

			Call Busca_Abastecimento()
			Call Busca_KM()
			Call Compara()
		Else
			MsgBox("Verifique as datas", vbCritical, "Alerta")
		End If
	End Sub

	Private Sub Busca_Abastecimento()
		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data>=@data AND data <=@data2 AND empresa=@empresa order by data,prefixo", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
			command.Parameters.AddWithValue("@data2", data_SQL(TextBox2.Text))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()

			dr = command.ExecuteReader()
			Dim x As Integer = 0
			While dr.Read()
				Prefixo_abast(x) = Trim(dr.Item("prefixo"))
				data_abast(x) = data_Normal(dr.Item("data"))
				x = x + 1
			End While

		End Using
	End Sub

	Private Sub Busca_KM()
		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM KM WHERE data>=@data AND data <=@data2 AND empresa=@empresa order by data,prefixo", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
			command.Parameters.AddWithValue("@data2", data_SQL(TextBox2.Text))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim w As Integer = 0

			While dr.Read()
				Prefixo_KM(w) = Trim(dr.Item("prefixo"))
				data_km(w) = data_Normal(dr.Item("data"))
				w = w + 1
			End While

		End Using
	End Sub

	Private Sub Compara()
		' VERIFICA ABASTECIMENTOS SEM HODOMETRO
		Dgbgrid1.Rows.Clear()
		Dgbgrid1.Columns.Clear()

		Dgbgrid1.AllowUserToAddRows = False
		Dgbgrid1.AllowUserToDeleteRows = False
		Dgbgrid1.EditMode = DataGridViewEditMode.EditProgrammatically
		Dgbgrid1.MultiSelect = False
		Dgbgrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
		Dgbgrid1.AllowUserToOrderColumns = False
		Dgbgrid1.AllowUserToResizeColumns = False

		Dgbgrid1.Columns.Add("prefixoabast", "PrefixoAbast")
		Dgbgrid1.Columns.Add("dataabast", "DataAbast")
		Dgbgrid1.Columns.Add("prefixokm", "PrefixoKM")
		Dgbgrid1.Columns.Add("datakm", "DataKM")

		Dim y As Integer = 1
		For i = 0 To 1000
			If Prefixo_abast(i) <> "" Then
				Dgbgrid1.Rows.Add()
				Dgbgrid1.Rows(i).Cells(0).Value = Prefixo_abast(i)
				Dgbgrid1.Rows(i).Cells(1).Value = data_abast(i)
			End If

			For y = 0 To 1000
				If (Prefixo_abast(i) = Prefixo_KM(y)) And (data_abast(i) = data_km(y)) And (Prefixo_abast(i) <> "") Then
					Dgbgrid1.Rows(i).Cells(2).Value = Prefixo_KM(y)
					Dgbgrid1.Rows(i).Cells(3).Value = data_km(y)
					'y = y + 1
				End If
			Next
		Next

		' VERIFICA HODOMETROS SEM ABASTECIMENTOS
		Dgbgrid2.Rows.Clear()
		Dgbgrid2.Columns.Clear()

		Dgbgrid2.AllowUserToAddRows = False
		Dgbgrid2.AllowUserToDeleteRows = False
		Dgbgrid2.EditMode = DataGridViewEditMode.EditProgrammatically
		Dgbgrid2.MultiSelect = False
		Dgbgrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
		Dgbgrid2.AllowUserToOrderColumns = False
		Dgbgrid2.AllowUserToResizeColumns = False

		Dgbgrid2.Columns.Add("prefixokm", "PrefixoKM")
		Dgbgrid2.Columns.Add("datakm", "DataKM")
		Dgbgrid2.Columns.Add("prefixoabast", "PrefixoAbast")
		Dgbgrid2.Columns.Add("dataabast", "DataAbast")


		For i = 0 To 1000
			If Prefixo_KM(i) <> "" Then
				Dgbgrid2.Rows.Add()
				Dgbgrid2.Rows(i).Cells(0).Value = Prefixo_KM(i)
				Dgbgrid2.Rows(i).Cells(1).Value = data_km(i)
			End If

			For y = 0 To 1000
				If (Prefixo_KM(i) = Prefixo_abast(y)) And (data_km(i) = data_abast(y)) And (Prefixo_KM(i) <> "") Then
					Dgbgrid2.Rows(i).Cells(2).Value = Prefixo_abast(y)
					Dgbgrid2.Rows(i).Cells(3).Value = data_abast(y)
					'y = y + 1
				End If
			Next
		Next

		'PINTA CAMPOS ALERTA

		For linha As Integer = 0 To Dgbgrid1.Rows.Count - 1
			If Dgbgrid1.Rows(linha).Cells(3).Value = "" Then
				Dgbgrid1.Rows(linha).Cells(0).Style.BackColor = Color.Firebrick
				Dgbgrid1.Rows(linha).Cells(1).Style.BackColor = Color.Firebrick
				Dgbgrid1.Rows(linha).Cells(2).Style.BackColor = Color.Firebrick
				Dgbgrid1.Rows(linha).Cells(3).Style.BackColor = Color.Firebrick
			End If
		Next


		For linha As Integer = 0 To Dgbgrid2.Rows.Count - 1
			If Dgbgrid2.Rows(linha).Cells(3).Value = "" Then
				Dgbgrid2.Rows(linha).Cells(0).Style.BackColor = Color.Firebrick
				Dgbgrid2.Rows(linha).Cells(1).Style.BackColor = Color.Firebrick
				Dgbgrid2.Rows(linha).Cells(2).Style.BackColor = Color.Firebrick
				Dgbgrid2.Rows(linha).Cells(3).Style.BackColor = Color.Firebrick
			End If
		Next

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

End Class