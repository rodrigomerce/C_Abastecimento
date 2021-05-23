Imports System.Data.SqlClient


Public Class Onibus
	Public sql As String
	Private Sub Onibus_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Dim ln_grid As Integer = 0

		ComboBox1.Items.Clear()
		ComboBox1.Items.Add("ATIVO")
		ComboBox1.Items.Add("MANUT/VEND")

		ComboBox2.Items.Clear()
		ComboBox2.Items.Add("TL")
		ComboBox2.Items.Add("VB")
		ComboBox2.Items.Add("GT")
		ComboBox2.Items.Add("TV")

		ComboBox3.Items.Clear()
		ComboBox3.Items.Add("U")
		ComboBox3.Items.Add("I")
		ComboBox3.Items.Add("E")

		ComboBox4.Items.Clear()
		ComboBox4.Items.Add("s")
		ComboBox4.Items.Add("N")

		Call Limpacampos()
		Call Localizar()
		Call Tiptext()
		Button8.Visible = False
	End Sub

	Private Sub Limpacampos()
		TextBox1.Text = ""
		TextBox2.Text = ""
		textbox3.Text = ""
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button7, "Gravar prefixo " & TextBox1.Text)
		ToolTip1.SetToolTip(Me.Button8, "Excluir prefixo " & TextBox1.Text)
	End Sub

	Private Sub Localizar()
		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM Capac_onibus order by prefixo ", con)
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
			Dgbgrid.Columns.Add("capac", "Capac")
			Dgbgrid.Columns.Add("status", "Status")
			Dgbgrid.Columns.Add("emp", "Emp")
			Dgbgrid.Columns.Add("tipo", "Tipo")
			Dgbgrid.Columns.Add("rel", "Rel")
			Dgbgrid.Columns.Add("consumo", "Consumo")

			Dgbgrid.Columns(0).Width = 60
			Dgbgrid.Columns(1).Width = 70
			Dgbgrid.Columns(2).Width = 60
			Dgbgrid.Columns(3).Width = 50
			Dgbgrid.Columns(4).Width = 60
			Dgbgrid.Columns(5).Width = 60
			Dgbgrid.Columns(6).Width = 60

			Dim qt_vis_TL As Integer = 0
			Dim qt_vis_GT As Integer = 0
			Dim qt_vis_VB As Integer = 0
			Dim qt_vis_TV As Integer = 0

			' mais infos sobre dagrid personalização -> https://docs.microsoft.com/pt-br/dotnet/api/system.windows.forms.datagridview.allowusertoresizecolumns?view=netframework-4.8

			' Set the selection background color for all the cells.
			Dgbgrid.DefaultCellStyle.SelectionBackColor = Color.Gray
			Dgbgrid.DefaultCellStyle.SelectionForeColor = Color.Black

			' Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
			' value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
			Dgbgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DarkGray

			' Set the background color for all rows and for alternating rows. 
			' The value for alternating rows overrides the value for all rows. 
			Dgbgrid.RowsDefaultCellStyle.BackColor = Color.White ' background das linhas
			Dgbgrid.RowsDefaultCellStyle.ForeColor = Color.Black ' background das linhas

			'Dgbgrid.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray

			' Set the row and column header styles.
			Dgbgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

			' Set the Format property on the "Last Prepared" column to cause
			' the DateTime to be formatted as "Month, Year".
			'Dgbgrid.Columns("Last Prepared").DefaultCellStyle.Format = "y"
			Dim relatorio As String = ""
			Dim i As Integer = 1

			Dim prefixo As String = ""
			Dim capacidade As Integer = 0
			Dim status As String = ""
			Dim empresa As String = ""
			Dim tipo As String = ""
			Dim consumo As Double = 0

			While dr.Read()
				If Not IsDBNull(dr.Item("relatorio")) Then ' PRIMEIRA LINHA DO BANCO DE DADOS ESTA TODA NULA OU VAZIAL (CONDIÇÃO PARA IGNORAR ESSA LINHA)
					prefixo = dr.Item("prefixo")
					capacidade = dr.Item("capacidade")

					status = "ATIVO"
					If Not dr.Item("status") Then status = "MANUT/VEND"

					empresa = dr.Item("empresa")
					tipo = dr.Item("tipo")
					relatorio = dr.Item("relatorio")
					If dr.Item("relatorio") = "S" Then
						'Dgbgrid.Rows(i).Cells(5).Value = dr.Item("relatorio")
						If dr.Item("empresa") = "TL" Then qt_vis_TL = qt_vis_TL + 1
						If dr.Item("empresa") = "VB" Then qt_vis_VB = qt_vis_VB + 1
						If dr.Item("empresa") = "GT" Then qt_vis_GT = qt_vis_GT + 1
						If dr.Item("empresa") = "TV" Then qt_vis_TV = qt_vis_TV + 1
					End If

					consumo = dr.Item("consumo")
					i = i + 1

					Me.Dgbgrid.Rows.Add(prefixo, capacidade, status, empresa, tipo, relatorio, consumo)
				End If
			End While

			Label12.Text = qt_vis_TL
			Label13.Text = qt_vis_VB
			Label14.Text = qt_vis_GT
			Label15.Text = qt_vis_TV
		End Using
	End Sub

	Private Sub Dgbgrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgbgrid.CellContentClick

		Dim linha As Integer = Dgbgrid.CurrentRow.Index
		Dgbgrid.CurrentCell = Dgbgrid.Rows(linha).Cells(0)

		TextBox1.Text = Dgbgrid.Rows(linha).Cells(0).Value
		TextBox2.Text = Dgbgrid.Rows(linha).Cells(1).Value
		ComboBox1.Text = Dgbgrid.Rows(linha).Cells(2).Value
		ComboBox2.Text = Dgbgrid.Rows(linha).Cells(3).Value
		ComboBox3.Text = Dgbgrid.Rows(linha).Cells(4).Value
		ComboBox4.Text = Dgbgrid.Rows(linha).Cells(5).Value
		textbox3.Text = Dgbgrid.Rows(linha).Cells(6).Value

		Button8.Visible = True
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		Call Gravar()
	End Sub

	Private Sub Gravar()
		If (TextBox1.Text <> "") And (textbox3.Text <> "") Then

			Using con As SqlConnection = getconnectionSQL()

				Try
					con.Open()

					Dim cmd As New SqlCommand("SELECT * From Capac_onibus where prefixo=@prefixo", con)
					cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox1.Text))

					Dim dr As SqlDataReader '= command.ExecuteReader()

					dr = cmd.ExecuteReader()

					If dr.Read() Then
						con.Close()
						con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

						Dim d As String = "000000" & TextBox1.Text
						Dim prefixo As String = ""
						prefixo = d.Substring(6)

						Dim cmd2 As New SqlCommand("UPDATE Capac_onibus Set  prefixo=@prefixo, capacidade=@capacidade, status=@status, empresa=@empresa, tipo=@tipo, relatorio=@relatorio, consumo=@consumo, abastecido=@abastecido WHERE prefixo=@prefixo", con)

						cmd2.Parameters.AddWithValue("@prefixo", prefixo)
						cmd2.Parameters.AddWithValue("@capacidade", TextBox2.Text)
						Dim v_status As Boolean = False
						If ComboBox1.Text = "ATIVO" Then v_status = True
						cmd2.Parameters.AddWithValue("@status", v_status)
						cmd2.Parameters.AddWithValue("@empresa", Trim(ComboBox2.Text))
						cmd2.Parameters.AddWithValue("@tipo", Trim(UCase(ComboBox3.Text)))
						cmd2.Parameters.AddWithValue("@relatorio", UCase(ComboBox4.Text))
						cmd2.Parameters.AddWithValue("@consumo", textbox3.Text)
						cmd2.Parameters.AddWithValue("@abastecido", False)

						cmd2.ExecuteNonQuery()

					Else
						con.Close()

						con.Open()

						Dim cmd3 As SqlCommand
						Sql = ""
						Try

							Dim d As String = "000000" & TextBox1.Text
							Dim prefixo As String = ""
							prefixo = d.Substring(6)

							sql = "INSERT INTO Capac_onibus (prefixo, capacidade, status, empresa, tipo, relatorio, consumo, abastecido) VALUES (@prefixo, @capacidade, @status, @empresa, @tipo, @relatorio, @consumo, @abastecido)"

							cmd3 = New SqlCommand(Sql, con)

							cmd3.Parameters.AddWithValue("@prefixo", prefixo)
							cmd3.Parameters.AddWithValue("@capacidade", TextBox2.Text)
							Dim v_status As Boolean = False
							If ComboBox1.Text = "ATIVO" Then v_status = True
							cmd3.Parameters.AddWithValue("@status", v_status)
							cmd3.Parameters.AddWithValue("@empresa", Trim(ComboBox2.Text))
							cmd3.Parameters.AddWithValue("@tipo", Trim(ComboBox3.Text))
							cmd3.Parameters.AddWithValue("@relatorio", UCase(ComboBox4.Text))
							cmd3.Parameters.AddWithValue("@consumo", textbox3.Text)
							cmd3.Parameters.AddWithValue("@abastecido", False)

							cmd3.ExecuteNonQuery()

						Catch ex As Exception
							MsgBox(ex.Message)
						End Try
					End If

				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
					Call Localizar()

					TextBox1.Text = ""
					TextBox1.Focus()
				End Try

			End Using


		End If
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		Call Deletar
	End Sub

	Private Sub Deletar()

		Dim dr As SqlDataReader = Nothing

		Using con As SqlConnection = getconnectionSQL()
			Try

				Dim d As String = "000000" & TextBox1.Text
				Dim prefixo As String = ""
				prefixo = d.Substring(6)

				con.Open()
				sql = "Delete from capac_onibus where prefixo=@prefixo"
				Dim cmd As SqlCommand = New SqlCommand(sql, con)

				cmd.Parameters.AddWithValue("@prefixo", prefixo)
				dr = cmd.ExecuteReader()
			Catch ex As Exception
				MsgBox(ex.Message)
			Finally
				con.Close()
				MsgBox("Registro excluido com sucesso !")
			End Try
		End Using


	End Sub
End Class