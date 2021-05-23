Imports System.Data.SqlClient

Imports Microsoft.Office.Interop
Public Class Correção
	Public sql As String

	Private Sub Correção_Load(sender As Object, e As EventArgs) Handles MyBase.Load


		TextBox1.Text = data_Select
		MonthCalendar1.Visible = False

		Button5.Visible = False
		GroupBox4.Visible = False


		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM Configuracao where empresa=@NumEmpresa", con)
			command.Parameters.AddWithValue("@NumEmpresa", Trim(NumEmpresa))
			Dim dr As SqlDataReader '= command.ExecuteReader()


			dr = command.ExecuteReader()
			ComboBox1.Items.Clear()
			While dr.Read()
				ComboBox1.Items.Add(dr.Item("bomba"))
			End While

		End Using

		'ComboBox1.Items.Clear()
		'ComboBox1.Items.Add("1")
		'ComboBox1.Items.Add("2")
		'ComboBox1.Items.Add("3")
		'ComboBox1.Items.Add("4")
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Procurar dados em " & TextBox1.Text)
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")

		ToolTip1.SetToolTip(Me.Button3, "Relatório Excel dos erros")
		ToolTip1.SetToolTip(Me.Button4, "Procura erros")

		ToolTip1.SetToolTip(Me.Button5, "Excluir prefixo " & Label12.Text & " na data " & TextBox1.Text)
		ToolTip1.SetToolTip(Me.Button6, "Gravar prefixo " & Label12.Text & " na data " & TextBox1.Text)

		ToolTip1.SetToolTip(Me.Button7, "Fechar cálculo")
		ToolTip1.SetToolTip(Me.Button8, "Calcular")


	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()
		Dim X As Integer = 0
		If TextBox1.Text <> "" Then
			Using con As SqlConnection = getconnectionSQL()

				con.Open()

				'Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data order by prefixo ", con)
				'	Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data and bomba=@bomba order by prefixo ", con)
				Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data order by prefixo ", con)
				command.Parameters.AddWithValue("@data", data_SQL(data_Select))
				'command.Parameters.AddWithValue("@bomba", ComboBox1.Text)
				Dim dr As SqlDataReader '= command.ExecuteReader()


				dr = command.ExecuteReader()

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
				Dgbgrid.Columns.Add("data", "Data")
				Dgbgrid.Columns.Add("hora", "Hora")
				Dgbgrid.Columns.Add("bomba", "Bomba")
				Dgbgrid.Columns.Add("combustivel", "Combustivel")
				Dgbgrid.Columns.Add("hodometro", "Hodometro")
				Dgbgrid.Columns.Add("oleo", "Oleo")
				Dgbgrid.Columns.Add("ant.combustivel", "Ant.Combustivel")
				Dgbgrid.Columns.Add("ant.hodometro", "Ant.Hodometro")
				Dgbgrid.Columns.Add("dif.km", "Dif.KM")

				Dgbgrid.Columns(0).Width = 60
				Dgbgrid.Columns(1).Width = 70
				Dgbgrid.Columns(2).Width = 60
				Dgbgrid.Columns(3).Width = 50
				Dgbgrid.Columns(4).Width = 70
				Dgbgrid.Columns(5).Width = 70
				Dgbgrid.Columns(6).Width = 50
				Dgbgrid.Columns(7).Width = 90
				Dgbgrid.Columns(8).Width = 90
				Dgbgrid.Columns(9).Width = 70

				' mais infos sobre dagrid personalização -> https://docs.microsoft.com/pt-br/dotnet/api/system.windows.forms.datagridview.allowusertoresizecolumns?view=netframework-4.8

				' Set the selection background color for all the cells.
				Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.White
				Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black

				' Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
				' value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
				Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty

				' Set the background color for all rows and for alternating rows. 
				' The value for alternating rows overrides the value for all rows. 
				Dgbgrid.RowsDefaultCellStyle.BackColor = Color.LightGray
				'Dgbgrid.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray

				' Set the row and column header styles.
				Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
				Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
				Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

				' Set the Format property on the "Last Prepared" column to cause
				' the DateTime to be formatted as "Month, Year".
				'Dgbgrid.Columns("Last Prepared").DefaultCellStyle.Format = "y"

				While dr.Read()
					Me.Dgbgrid.Rows.Add(dr.Item("prefixo"), CDate(dr.Item("Data_abast")).ToShortDateString, dr.Item("hora"), dr.Item("bomba"), dr.Item("combustivel"))
				End While

			End Using

			'COMBUSTIVEL DO DIA ANTERIOR

			Using con As SqlConnection = getconnectionSQL()
				Dim ontem As Date = CDate(data_Select).AddDays(-1)
				X = 1
				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data", con)
				Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data and bomba=@bomba ", con)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				command.Parameters.AddWithValue("@data", data_SQL(ontem))
				command.Parameters.AddWithValue("@bomba", ComboBox1.Text)
				dr = command.ExecuteReader()

				Dim prefixo(300) As String
				Dim combustiv(300) As Double
				While dr.Read()
					prefixo(X) = dr.Item("prefixo")
					combustiv(X) = Format(dr.Item("combustivel"), "#.00")
					X = X + 1
				End While


				For Y = 0 To Dgbgrid.RowCount - 1
					X = 1

					While (X < 300) And (Dgbgrid.Rows(Y).Cells(0).Value <> prefixo(X))
						X = X + 1
					End While

					If (Dgbgrid.Rows(Y).Cells(0).Value = prefixo(X)) Then
						Dgbgrid.Rows(Y).Cells(7).Value = Format(combustiv(X), "#.00")
					End If

				Next


			End Using

			'KM * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

			Using con As SqlConnection = getconnectionSQL()
				Dim ontem As Date = CDate(data_Select).AddDays(-1)
				X = 1
				con.Open()

				Dim command As New SqlCommand("SELECT * FROM KM where data=@data", con)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				command.Parameters.AddWithValue("@data", data_SQL(data_Select))
				dr = command.ExecuteReader()

				Dim prefixo(300) As String
				Dim km(300) As Integer
				While dr.Read()
					prefixo(X) = dr.Item("prefixo")
					km(X) = Format(dr.Item("hodometro"), "###,###")
					X = X + 1
				End While


				For Y = 0 To Dgbgrid.RowCount - 1
					X = 1

					While (X < 300) And (Dgbgrid.Rows(Y).Cells(0).Value <> prefixo(X))
						X = X + 1
					End While

					If (Dgbgrid.Rows(Y).Cells(0).Value = prefixo(X)) Then
						Dgbgrid.Rows(Y).Cells(5).Value = km(X)
					End If

				Next


			End Using

			'KM DO DIA ANTERIOR * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

			Using con As SqlConnection = getconnectionSQL()
				Dim ontem As Date = CDate(data_Select).AddDays(-1)
				X = 1
				con.Open()

				Dim command As New SqlCommand("SELECT * FROM KM where data=@data", con)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				command.Parameters.AddWithValue("@data", data_SQL(ontem))
				dr = command.ExecuteReader()

				Dim prefixo(300) As String
				Dim km(300) As Integer
				While dr.Read()
					prefixo(X) = dr.Item("prefixo")
					km(X) = Format(dr.Item("hodometro"), "###,###")
					X = X + 1
				End While


				For Y = 0 To Dgbgrid.RowCount - 1
					X = 1

					While (X < 300) And (Dgbgrid.Rows(Y).Cells(0).Value <> prefixo(X))
						X = X + 1
					End While

					If (Dgbgrid.Rows(Y).Cells(0).Value = prefixo(X)) Then
						Dgbgrid.Rows(Y).Cells(8).Value = km(X)
						If Dgbgrid.Rows(Y).Cells(5).Value <> 0 Then Dgbgrid.Rows(Y).Cells(9).Value = Dgbgrid.Rows(Y).Cells(5).Value - km(X)
					End If

				Next


			End Using

		Else
			MsgBox("Data invalida !")
		End If

		Dim tt_corr As Integer = 0

		For I = 0 To Dgbgrid.RowCount - 1
			If (CStr(Dgbgrid.Rows(I).Cells(9).Value) = Nothing) Or (Dgbgrid.Rows(I).Cells(9).Value = 0) Then
				Dgbgrid.Rows(I).Cells(9).Style.BackColor = Color.YellowGreen
				tt_corr = tt_corr + 1
			End If
		Next

		Label9.Text = tt_corr
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Call Validar()
	End Sub

	Private Sub Validar()

		Dim v_erros As Integer = 0

		For I = 0 To Dgbgrid.RowCount - 1
			If (CStr(Dgbgrid.Rows(I).Cells(9).Value) <> "") Then
				If (CDbl(Dgbgrid.Rows(I).Cells(9).Value) < CDbl(TextBox2.Text)) And (CDbl(Dgbgrid.Rows(I).Cells(4).Value) > CDbl(TextBox3.Text)) Then
					Dgbgrid.Rows(I).Cells(9).Style.BackColor = Color.Orange
					v_erros = v_erros + 1
				End If
			End If
		Next

		MsgBox("Falhas = " & v_erros)
	End Sub

	Private Sub Dgbgrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgbgrid.CellContentClick

		Dim linha As Integer = Dgbgrid.CurrentRow.Index

		Dgbgrid.CurrentCell = Dgbgrid.Rows(linha).Cells(0)

		Label12.Text = Dgbgrid.Rows(linha).Cells(0).Value

		TextBox6.Text = Dgbgrid.Rows(linha).Cells(5).Value
		TextBox4.Text = Dgbgrid.Rows(linha).Cells(4).Value
		ComboBox1.Text = Dgbgrid.Rows(linha).Cells(3).Value

		Button5.Visible = True


	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

		Dim XcelApp As New Excel.Application

		If Dgbgrid.Rows.Count > 0 Then
			'			Try
			XcelApp.Application.Workbooks.Add(Type.Missing)
			For i As Integer = 1 To Dgbgrid.Columns.Count
				XcelApp.Cells(1, i) = Dgbgrid.Columns(i - 1).HeaderText
			Next
			'
			For i As Integer = 0 To Dgbgrid.Rows.Count - 2
				For j As Integer = 0 To Dgbgrid.Columns.Count - 1
					On Error Resume Next
					XcelApp.Cells(i + 2, j + 1) = Dgbgrid.Rows(i).Cells(j).Value.ToString()
				Next
			Next
			'
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
			'		Catch ex As Exception
			'MessageBox.Show("Erro : " + ex.Message)
			'XcelApp.Quit()
			'		End Try
		End If

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

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
				If (Dgbgrid.Rows(x).Cells(9).Style.BackColor = Color.YellowGreen) Then
					For j As Integer = 0 To Dgbgrid.Columns.Count - 1
						On Error Resume Next

						XcelApp.Cells(y + 2, j + 1) = Dgbgrid.Rows(x).Cells(j).Value.ToString()

					Next
					y = y + 1
				End If
				x = x + 1
			End While
			'
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
			'		Catch ex As Exception
			'MessageBox.Show("Erro : " + ex.Message)
			'XcelApp.Quit()
			'		End Try
		End If

	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		Call Deletar()
	End Sub

	Private Sub Deletar()

		If MsgBox("Excluir registro atual ?", vbYesNo = vbYes) Then

			Dim dr As SqlDataReader = Nothing

			Using con As SqlConnection = getconnectionSQL()
				Try
					con.Open()
					sql = "Delete from Abastecimento where data='" & data_SQL(TextBox1.Text) & "' and prefixo ='" & Label12.Text & "'"
					Dim cmd As SqlCommand = New SqlCommand(Sql, con)
					dr = cmd.ExecuteReader()
					Call LOG("DEL.AB" & Trim(Label12.Text) & TextBox1.Text, Now.ToShortDateString)
				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
					MsgBox("Registro excluido com sucesso !")
				End Try
			End Using
		Else
			MsgBox("Operação cancelada !")
		End If

	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		TextBox7.Text = ""
		TextBox8.Text = ""
		GroupBox4.Visible = False
	End Sub

	Dim v_result As Double

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		If TextBox7.Text <> "" And TextBox8.Text <> "" Then
			v_result = TextBox7.Text + (TextBox4.Text * TextBox8.Text)
			TextBox6.Text = v_result
		Else
			MsgBox("Preencher os campos")
		End If
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		Call Gravar
	End Sub

	Private Sub Gravar()

		If (TextBox6.Text <> "") And (TextBox4.Text <> "") And (ComboBox1.Text <> "") Then

			Using con As SqlConnection = getconnectionSQL()

				Try
					'ABASTECIMENTO
					con.Open()

					Dim cmd As New SqlCommand("SELECT * From Abastecimento where data=@data and prefixo=@prefixo", con)

					cmd.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
					cmd.Parameters.AddWithValue("@prefixo", Trim(Label12.Text))

					Dim dr As SqlDataReader '= command.ExecuteReader()

					dr = cmd.ExecuteReader()

					If dr.Read() Then
						con.Close()
						con.Open()
						' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'
						Dim cmd2 As New SqlCommand("UPDATE Abastecimento Set bomba=@bomba, combustivel=@combustivel WHERE data=@data and prefixo=@prefixo", con)

						cmd2.Parameters.AddWithValue("@bomba", Trim(ComboBox1.Text))
						cmd2.Parameters.AddWithValue("@combustivel", CDbl(TextBox4.Text))
						cmd2.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
						cmd2.Parameters.AddWithValue("@prefixo", Trim(Label12.Text))

						cmd2.ExecuteNonQuery()
					End If

					con.Close()

					'KM
					con.Open()

					Dim cmd3 As New SqlCommand("SELECT * From KM where data=@data and prefixo=@prefixo", con)

					cmd3.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
					cmd3.Parameters.AddWithValue("@prefixo", Trim(Label12.Text))

					Dim dr3 As SqlDataReader '= command.ExecuteReader()

					dr3 = cmd3.ExecuteReader()

					If Not dr3.Read() Then
						con.Close()
						con.Open()

						'ADICIONA
						Dim cmd4 As SqlCommand
						sql = ""
						Try

							'							sql = "INSERT INTO KM (Data, Data_abast, hora, prefixo) VALUES (@data, @data_abast, @hora, @prefixo) WHERE data=@data and prefixo=@prefixo"
							sql = "INSERT INTO KM (Data, Data_abast, hora, prefixo) VALUES (@data, @data_abast, @hora, @prefixo)"

							cmd4 = New SqlCommand(sql, con)

							cmd4.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
							cmd4.Parameters.AddWithValue("@data_abast", Trim(TextBox1.Text))
							cmd4.Parameters.AddWithValue("@hora", "00:00:00")
							cmd4.Parameters.AddWithValue("@prefixo", Trim(Label12.Text))

							cmd4.ExecuteNonQuery()

						Catch ex As Exception
							MsgBox(ex.Message)
						End Try


					End If
					'ATUALIZA
					con.Close()
					con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

					Dim cmd5 As New SqlCommand("UPDATE KM Set hodometro=@hodometro WHERE data=@data and prefixo=@prefixo", con)

					cmd5.Parameters.AddWithValue("@hodometro", Trim(TextBox6.Text))
					cmd5.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
					cmd5.Parameters.AddWithValue("@prefixo", Trim(Label12.Text))

					cmd5.ExecuteNonQuery()

				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
				End Try
			End Using
		Else
			MsgBox("Preencha os campos")
		End If
		Call Buscar()
		Button5.Visible = False
	End Sub

	Private Sub TextBox6_Click(sender As Object, e As EventArgs) Handles TextBox6.Click
		GroupBox4.Visible = True
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox1.KeyDown
		MonthCalendar1.Visible = False
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
		If e.KeyCode = 13 Then
			data_Select = TextBox1.Text
		End If
	End Sub

	Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		TextBox1.Text = MonthCalendar1.SelectionStart
		data_Select = TextBox1.Text
	End Sub

End Class