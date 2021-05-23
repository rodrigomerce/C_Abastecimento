Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class configuracao_sistema
	Dim grid As Object
	Private Sub configuracao_sistema_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Call Menu_Buscar()
	End Sub

	Private Sub Menu_Buscar()
		Call Buscar_Empresas()
		Call Buscar_carros()
	End Sub

	Private Sub Buscar_carros()
		DgbCarros.Rows.Clear()
		DgbCarros.Columns.Clear()
		DgbCarros.Columns.Add("prefixo", "EMPRESA")
		DgbCarros.Columns.Add("capacidade", "CAPACIDADE")
		DgbCarros.Columns.Add("status", "STATUS")
		DgbCarros.Columns.Add("empresa", "EMPRESA")
		DgbCarros.Columns.Add("tipo", "TIPO")
		DgbCarros.Columns.Add("relatorio", "RELATORIO")
		DgbCarros.Columns.Add("abastecido", "ABASTECIDO")
		DgbCarros.Columns.Add("consumo", "CONSUMO")
		DgbCarros.Columns(0).Width = 60
		DgbCarros.Columns(1).Width = 60
		DgbCarros.Columns(2).Width = 60
		DgbCarros.Columns(3).Width = 60
		DgbCarros.Columns(4).Width = 60
		DgbCarros.Columns(5).Width = 60
		DgbCarros.Columns(6).Width = 60
		DgbCarros.Columns(7).Width = 60

		Call Griid(DgbCarros)
		Using con As SqlConnection = getconnectionSQL()
			con.Open()
			Dim sql As String = "SELECT * FROM Capac_onibus"
			Dim command As New SqlCommand(sql, con)
			Dim dr As SqlDataReader = command.ExecuteReader
			Dim ln As Integer = 0
			While dr.Read
				DgbCarros.Rows.Add()
				DgbCarros.Rows(ln).Cells(0).Value = Trim(dr.Item("prefixo"))
				DgbCarros.Rows(ln).Cells(1).Value = Trim(dr.Item("capacidade"))
				DgbCarros.Rows(ln).Cells(2).Value = "NÃO"
				If dr.Item("status") = True Then DgbCarros.Rows(ln).Cells(2).Value = "SIM"
				DgbCarros.Rows(ln).Cells(3).Value = Trim(dr.Item("empresa"))
				DgbCarros.Rows(ln).Cells(4).Value = Trim(dr.Item("tipo"))

				DgbCarros.Rows(ln).Cells(5).Value = "NÃO"
				If Trim(dr.Item("relatorio")) = "S" Then DgbCarros.Rows(ln).Cells(5).Value = "SIM"

				DgbCarros.Rows(ln).Cells(6).Value = "NÃO"
				If dr.Item("abastecido") = True Then DgbCarros.Rows(ln).Cells(6).Value = "SIM"
				DgbCarros.Rows(ln).Cells(7).Value = Trim(dr.Item("consumo"))
				ln = ln + 1
			End While
		End Using
	End Sub

	Private Sub Buscar_Empresas()
		DgbEmpresas.Rows.Clear()
		DgbEmpresas.Columns.Clear()
		DgbEmpresas.Columns.Add("empresa", "EMPRESA")
		DgbEmpresas.Columns.Add("filial", "FILIAL")
		DgbEmpresas.Columns.Add("tanque", "TANQUE")
		DgbEmpresas.Columns.Add("bomba", "BOMBA")
		DgbEmpresas.Columns(0).Width = 50
		DgbEmpresas.Columns(1).Width = 50
		DgbEmpresas.Columns(2).Width = 50
		DgbEmpresas.Columns(3).Width = 50

		Call Griid(DgbEmpresas)
		Using con As SqlConnection = getconnectionSQL()
			con.Open()
			Dim sql As String = "SELECT * FROM configuracao"
			Dim command As New SqlCommand(sql, con)
			Dim dr As SqlDataReader = command.ExecuteReader
			Dim ln As Integer = 0
			While dr.Read
				DgbEmpresas.Rows.Add()
				DgbEmpresas.Rows(ln).Cells(0).Value = dr.Item("empresa")
				DgbEmpresas.Rows(ln).Cells(1).Value = dr.Item("filial")
				DgbEmpresas.Rows(ln).Cells(2).Value = dr.Item("tanque")
				DgbEmpresas.Rows(ln).Cells(3).Value = dr.Item("bomba")
				ln = ln + 1
			End While
		End Using
	End Sub

	Private Sub DgbEmpresas_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgbEmpresas.CurrentCellChanged
		TextBox1.Text = DgbEmpresas.Rows(DgbEmpresas.CurrentRow.Index).Cells(0).Value
		TextBox3.Text = DgbEmpresas.Rows(DgbEmpresas.CurrentRow.Index).Cells(1).Value
		TextBox4.Text = DgbEmpresas.Rows(DgbEmpresas.CurrentRow.Index).Cells(2).Value
		TextBox5.Text = DgbEmpresas.Rows(DgbEmpresas.CurrentRow.Index).Cells(3).Value

	End Sub

	Private Sub DgbCarros_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgbCarros.CurrentCellChanged
		TextBox2.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(0).Value
		TextBox6.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(1).Value
		ComboBox1.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(2).Value
		TextBox7.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(3).Value
		TextBox8.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(4).Value
		ComboBox2.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(5).Value
		TextBox9.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(6).Value
		TextBox10.Text = DgbCarros.Rows(DgbCarros.CurrentRow.Index).Cells(7).Value
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Call Deletar_Empresas()
	End Sub

	Private Sub Deletar_Empresas()

		If MsgBox("DESEJA EXCLUIR ?", vbCritical + vbYesNo) = vbYes Then
			Using con As SqlConnection = getconnectionSQL()
				Try
					con.Open()
					Dim sql As String = ""
					sql = "DELETE FROM configuracao WHERE empresa=@empresa AND filial=@filial
				AND tanque=@tanque AND bomba=@bomba"
					Dim cmd As SqlCommand = New SqlCommand(sql, con)

					cmd.Parameters.AddWithValue("@empresa", TextBox1.Text)
					cmd.Parameters.AddWithValue("@filial", TextBox3.Text)
					cmd.Parameters.AddWithValue("@tanque", TextBox4.Text)
					cmd.Parameters.AddWithValue("@bomba", TextBox5.Text)
					Dim dr As SqlDataReader = cmd.ExecuteReader()

				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
					'Call limpar()
					MsgBox("Registro excluido com sucesso !")
					'	GroupBox3.Visible = False
				End Try
			End Using
		Else
			MsgBox("OPERAÇÃO CANCELADA")
		End If
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Call Gravar_Carros()
	End Sub

	Private Sub Gravar_Carros()
		Dim vBoll As Boolean = False
		Using con As SqlConnection = getconnectionSQL()
			con.Open()
			Dim command As New SqlCommand("SELECT * FROM Capac_onibus WHERE prefixo=@prefixo AMD capacidade=@capacidade
			AND status=@status AND empresa=@empresa AND tipo=@tipo AND relatorio=@relatorio AND abastecido=@abastecido
			AND consumo=@consumo", con)
			command.Parameters.AddWithValue("prefixo", ("000000" & TextBox2.Text).Substring(Len("000000" & TextBox2.Text) - 6, 6))
			command.Parameters.AddWithValue("capacidade", CInt(TextBox6.Text))

			vBoll = (ComboBox1.Text = "SIM")
			command.Parameters.AddWithValue("status", vBoll)
			command.Parameters.AddWithValue("empresa", TextBox7.Text)
			command.Parameters.AddWithValue("tipo", TextBox8.Text)
			command.Parameters.AddWithValue("relatorio", ComboBox2.Text)
			command.Parameters.AddWithValue("abastecido", TextBox9.Text)
			command.Parameters.AddWithValue("consumo", TextBox10.Text)
			Dim dr As SqlDataReader = command.ExecuteReader
			If dr.Read Then
				con.Close()
				con.Open()

				Dim commandU As New SqlCommand("UPDATE Capac_onibus SET prefixo=@prefixo, capacidade=@capacidade, status=@status
				empresa=@empresa, tipo=@tipo, relatorio=@relatorio, abastecido=@abastecido, consumo=@consumo", con)
				'commandU.Parameters.AddWithValue("")
				commandU.ExecuteNonQuery()
			Else

			End If

		End Using

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Gravar_Empresas()
	End Sub

	Private Sub Gravar_Empresas()
		Using con As SqlConnection = getconnectionSQL()
			con.Open()
			Dim sql As String = "SELECT * FROM configuracao WHERE empresa=@empresa AND filial=@filial 
			AND tanque=@tanque AND bomba=@bomba"
			Dim command As New SqlCommand(sql, con)
			command.Parameters.AddWithValue("@empresa", CInt(TextBox1.Text))
			command.Parameters.AddWithValue("@filial", CInt(TextBox3.Text))
			command.Parameters.AddWithValue("@tanque", CInt(TextBox4.Text))
			command.Parameters.AddWithValue("@bomba", CInt(TextBox5.Text))
			Dim dr As SqlDataReader = command.ExecuteReader
			If dr.Read Then
				con.Close()
				con.Open()

				Dim commandU As New SqlCommand("UPDATE configuracao SET empresa=@empresa, filial=@filial,
				tanque=@tanque, bomba=@bomba WHERE empresa=@empresa, filial=@filial, tanque=@tanque, 
				bomba=@bomba", con)
				commandU.Parameters.AddWithValue("@empresa", TextBox1.Text)
				commandU.Parameters.AddWithValue("@filial", TextBox3.Text)
				commandU.Parameters.AddWithValue("@tanque", TextBox4.Text)
				commandU.Parameters.AddWithValue("@bomba", TextBox5.Text)

				commandU.ExecuteNonQuery()
			Else
				con.Close()
				con.Open()

				Dim commandI As New SqlCommand("INSERT INTO configuracao (empresa, filial,
				tanque, bomba) VALUES (@empresa, @filial, @tanque, @bomba)", con)
				commandI.Parameters.AddWithValue("@empresa", TextBox1.Text)
				commandI.Parameters.AddWithValue("@filial", TextBox3.Text)
				commandI.Parameters.AddWithValue("@tanque", TextBox4.Text)
				commandI.Parameters.AddWithValue("@bomba", TextBox5.Text)

				commandI.ExecuteNonQuery()
			End If
		End Using
	End Sub

	Private Sub DgbCarros_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgbCarros.CellContentClick

	End Sub
End Class