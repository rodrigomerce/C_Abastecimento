Imports System.Data.SqlClient

Imports Microsoft.Office.Interop

Public Class Exportar

	Dim prefixo(300)
	Dim km(300)
	Dim combustiv(300)

	Dim data_selecionada As String

	Private Sub Exportar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		TextBox1.Text = data_Select
		GroupBox2.Visible = False
		Call Tiptext()
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button1, "Buscar dados")
		ToolTip1.SetToolTip(Me.Button2, "Relatório Excel")
		ToolTip1.SetToolTip(Me.Button3, "Validar")


	End Sub

	Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
		MonthCalendar1.Visible = True
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
		If e.KeyCode = 13 Then
			data_Select = TextBox1.Text

			Call Buscar()
		End If
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Buscar()
	End Sub

	Private Sub Buscar()

		data_Select = TextBox1.Text
		Dim n_reg As Integer = 300
		For i = 0 To n_reg
			prefixo(i) = 0
			combustiv(i) = 0
		Next

		If TextBox1.Text <> "" Then
			Dim X As Integer = 1

			Using con As SqlConnection = getconnectionSQL()

				con.Open()

				'Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data order by prefixo ", con)			'ORIGINAL
				Dim command As New SqlCommand("SELECT * FROM Abastecimento where data=@data and empresa=@empresa order by prefixo ", con)
				command.Parameters.AddWithValue("@data", data_SQL(data_Select))
				command.Parameters.AddWithValue("@empresa", Empresa)

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
				Dgbgrid.Columns.Add("data_abast", "Data Abast")
				Dgbgrid.Columns.Add("hora", "Hora")
				Dgbgrid.Columns.Add("bb", "Bomba")
				Dgbgrid.Columns.Add("combustivel", "Combustivel")
				Dgbgrid.Columns.Add("hodometro", "Hodometro")
				Dgbgrid.Columns.Add("emp", "Emp")

				Dgbgrid.Columns(0).Width = 60
				Dgbgrid.Columns(1).Width = 90
				Dgbgrid.Columns(2).Width = 90
				Dgbgrid.Columns(3).Width = 80
				Dgbgrid.Columns(4).Width = 40
				Dgbgrid.Columns(5).Width = 60
				Dgbgrid.Columns(6).Width = 90
				Dgbgrid.Columns(7).Width = 50

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
					Me.Dgbgrid.Rows.Add(dr.Item("prefixo"), Trim(data_Select), data_normal(dr.Item("Data_abast")), dr.Item("hora"), dr.Item("bomba"), Format(dr.Item("combustivel"), "#.00"))
				End While

			End Using



			'KM ******************************************************************************************************************************************************

			For i = 0 To n_reg
				prefixo(i) = 0
				km(i) = 0
			Next


			Using con As SqlConnection = getconnectionSQL()
				Dim ontem As Date = CDate(data_Select).AddDays(-1)
				X = 1
				con.Open()

				Dim command As New SqlCommand("SELECT * FROM KM where data=@data", con)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				command.Parameters.AddWithValue("@data", data_SQL(data_Select))
				dr = command.ExecuteReader()

				While dr.Read()
					prefixo(X) = dr.Item("prefixo")
					km(X) = Format(dr.Item("hodometro"), "#.00")
					X = X + 1
				End While
				Label3.Text = X - 1
				For Y = 0 To Dgbgrid.RowCount - 1
					X = 1
					While (X < n_reg) And (Dgbgrid.Rows(Y).Cells(0).Value <> prefixo(X))
						X = X + 1
					End While

					If (Dgbgrid.Rows(Y).Cells(0).Value) = prefixo(X) Then
						Dgbgrid.Rows(Y).Cells(6).Value = km(X)
					End If
				Next

				For Y = 0 To Dgbgrid.RowCount - 1
					X = 0
					While (X < n_reg) And (Dgbgrid.Rows(Y).Cells(0).Value <> v_prefixo(X))
						X = X + 1
					End While

					If (Dgbgrid.Rows(Y).Cells(0).Value = v_prefixo(X)) Then
						Dgbgrid.Rows(Y).Cells(7).Value = v_codEmp(X)
					End If
				Next


			End Using

		Else
			MsgBox("DATA INVALIDA")
		End If

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

		GroupBox2.Visible = True

		Dgbgrid2.Rows.Clear()
		Dgbgrid2.Columns.Clear()

		Dgbgrid2.AllowUserToAddRows = False
		Dgbgrid2.AllowUserToDeleteRows = False
		Dgbgrid2.EditMode = DataGridViewEditMode.EditProgrammatically
		Dgbgrid2.MultiSelect = False
		Dgbgrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
		Dgbgrid2.AllowUserToOrderColumns = False
		Dgbgrid2.AllowUserToResizeColumns = False

		Dgbgrid2.Columns.Add("prefixo", "Prefixo")
		Dgbgrid2.Columns.Add("data", "Data")
		Dgbgrid2.Columns.Add("data_abast", "Data Abast")
		Dgbgrid2.Columns.Add("hora", "Hora")
		Dgbgrid2.Columns.Add("bb", "Bomba")
		Dgbgrid2.Columns.Add("combustivel", "Combustivel")
		Dgbgrid2.Columns.Add("hodometro", "Hodometro")
		Dgbgrid2.Columns.Add("emp", "Emp")

		Dgbgrid2.Columns(0).Width = 60
		Dgbgrid2.Columns(1).Width = 90
		Dgbgrid2.Columns(2).Width = 90
		Dgbgrid2.Columns(3).Width = 80
		Dgbgrid2.Columns(4).Width = 40
		Dgbgrid2.Columns(5).Width = 60
		Dgbgrid2.Columns(6).Width = 90
		Dgbgrid2.Columns(7).Width = 50

		Dgbgrid2.DefaultCellStyle.SelectionBackColor = Color.White
		Dgbgrid2.DefaultCellStyle.SelectionForeColor = Color.Black
		Dgbgrid2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
		Dgbgrid2.RowsDefaultCellStyle.BackColor = Color.LightGray
		Dgbgrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
		Dgbgrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
		Dgbgrid2.RowHeadersDefaultCellStyle.BackColor = Color.Black

	End Sub

	Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
		GroupBox2.Visible = False
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Call Validar_erros_transferencia()
	End Sub

	Private Sub Validar_erros_transferencia()

		'Define os objetos Excel
		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook
		Dim xlWorkSheet As Excel.Worksheet

		'Inclui um Novo Workbook
		xlWorkBook = xlApp.Workbooks.Add
		'Exibe o Excel
		xlApp.Visible = True

		'Abre um Workbook existente. (Ajuste o caminho para o seu arquivo Excel)
		xlWorkBook = xlApp.Workbooks.Open("\\hercules.dominio\Arquivos\Manutencao\Arquivos Globus\Relatorios Globus\" & Trim(TextBox2.Text) & ".txt")

		'Exibe o Excel
		xlApp.Visible = True

		'Define a planiliha na qual desejamos inserir o texto
		'	xlWorkSheet = xlWorkBook.Sheets("ABAST")

		'		With xlWorkSheet

		Dim data As Date = TextBox1.Text
			Dim dia As String = ""
			Dim mes As String = ""
			Dim ano As String = ""



			dia = data.DayOfWeek
			mes = data.Month
			ano = data.Year

			Dim ly As Integer = 0
			Dgbgrid2.Rows.Add()
			For w = 0 To Dgbgrid.RowCount
				Dim fim As Boolean = False
				Dim ln As Integer = 1
				Dim x As String = xlApp.Cells(1, ln)
				Dim xx As String = x.Substring(0, 11)


				While (xx <> "Total comb") And Not fim
					Dim z As String = xlApp.Cells(1, ln)
					Dim zz As String = x.Substring(0, 1)
					If zz = "T" Then
						Dim k As String = xlApp.Cells(1, ln)
						Dim kk As String = x.Substring(0, 5)
						fim = (Dgbgrid.Rows(w).Cells(0).Value = kk)
					Else
						Dim q As String = xlApp.Cells(1, ln)
						Dim qq As String = x.Substring(0, 7)
						fim = ("00" & Dgbgrid.Rows(w).Cells(0).Value = qq)
					End If
					ln = ln + 1
				End While

				If Not fim Then
					For i = 0 To 8
						Dgbgrid2.Rows(ly).Cells(i).Value = xlApp.Cells(w, i)
					Next
					ly = ly + 1
					Dgbgrid2.Rows.Add()
				End If
			Next
		'		End With
		MsgBox("Concluído !")
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		TextBox1.Text = MonthCalendar1.SelectionStart
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Call Exportaar()
		Call LOG("GR.ARQ.TXT.P.EXP", Now.ToShortDateString)
	End Sub

	Private Sub Exportaar()

		'Define os objetos Excel
		Dim xlApp As New Excel.Application
		Dim xlWorkBook As Excel.Workbook
		Dim xlWorkSheet As Excel.Worksheet

		'Inclui um Novo Workbook
		xlWorkBook = xlApp.Workbooks.Add
		'Exibe o Excel
		'xlApp.Visible = True

		'Abre um Workbook existente. (Ajuste o caminho para o seu arquivo Excel)
		xlWorkBook = xlApp.Workbooks.Open("\\hercules.dominio\Arquivos\Manutencao\Arquivos Globus\Abast.txt")

		'Exibe o Excel
		xlApp.Visible = True


		'Define a planiliha na qual desejamos inserir o texto
		xlWorkSheet = xlWorkBook.Sheets("ABAST")

		With xlWorkSheet

			Dim data As Date = TextBox1.Text
			Dim dia As String = ""
			Dim mes As String = ""
			Dim ano As String = ""

			dia = data.DayOfWeek
			mes = data.Month
			ano = data.Year

			Dim ln As Integer = 1

			For i = 0 To Dgbgrid.RowCount - 1

				If Dgbgrid.Rows(i).Cells(0).Value <> "" And Dgbgrid.Rows(i).Cells(0).Value <> "" Then

					Dim Registro As String = ""
					Registro = Dgbgrid.Rows(i).Cells(7).Value & v_filial

					Dim v As String = Dgbgrid.Rows(i).Cells(0).Value
					Dim vv As String = ""
					vv = v.Substring(0, 1)

					If vv = "T" Then

						Dim x As String = Dgbgrid.Rows(i).Cells(0).Value & "        "
						Dim xx As String = ""
						xx = x.Substring(0, 7)

						Registro = Registro & xx
					Else

						Dim gri As String = Dgbgrid.Rows(i).Cells(0).Value
						Dim k As String = "0000000" & Dgbgrid.Rows(i).Cells(0).Value
						Dim kk As String = ""
						kk = k.Substring((k.Length) - 7)

						Registro = Registro & kk

					End If


					Dim hora = Dgbgrid.Rows(i).Cells(3).Value
					Dim v_hora As String = Convert.ToString(hora)

					'	v_hora = v

					Registro = Trim(Registro) & Trim(Dgbgrid.Rows(i).Cells(1).Value) & CDate(v_hora).ToShortTimeString

					Dim z As String = "0000000000" & Dgbgrid.Rows(i).Cells(5).Value
					Dim zz As String = ""
					zz = z.Substring(z.Length - 10)

					Dim v_xl As String = zz

					Dim q As String = v_xl
					Dim qq As String = ""
					qq = q.Substring(0, 7)

					Dim v_intx = qq

					Dim q2 As String = v_xl
					Dim qq2 As String = ""
					qq2 = q2.Substring(q2.Length - 2)

					Dim v_dec = qq2

					Dim V_M = v_intx & "." & v_dec


					Registro = Registro & V_M ' LITROS

					Dim h As String = "00000000" & Convert.ToString(CInt(Dgbgrid.Rows(i).Cells(6).Value))
					Dim hh As String = ""
					hh = h.Substring(h.Length - 8)

					Registro = Registro & hh ' HODOMETRO

					Dim t As String = "000" & Dgbgrid.Rows(i).Cells(4).Value
					Dim tt As String = ""
					tt = t.Substring(t.Length - 3)

					Registro = Registro & v_tanque & tt

					Registro = Registro & "0000"
					.Range("A" & ln).Value = Registro
					ln = ln + 1


				End If


			Next

		End With

	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

	End Sub
End Class