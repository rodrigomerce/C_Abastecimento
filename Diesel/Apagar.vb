Imports System.Data.SqlClient
Public Class Apagar
	Private Sub Apagar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Call Buscar()
	End Sub

	Private Sub Tiptext()
		ToolTip1.SetToolTip(Me.Button8, "EXCLUIR")

	End Sub

	Private Sub Buscar()

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM Abastecimento ORDER BY data", con)
			Dim dr As SqlDataReader '= command.ExecuteReader()


			dr = command.ExecuteReader()
			'	 CDate(data_Select).AddDays(-1)
			Dim data As Date = "01/01/1992"
			If dr.Read() Then
				data = dr.Item("data")
				TextBox1.Text = dr.Item("data")
				If CDate(data).AddDays(+8) < CDate(Now.ToShortDateString).AddDays(-3) Then
					TextBox2.Text = CDate(data).AddDays(+8)
				Else
					TextBox2.Text = CDate(TextBox1.Text)
				End If
			End If
		End Using

	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		Call ApagarAb()
		Call ApagarKm()
	End Sub

	Private Sub ApagarAb()
		If MsgBox("Confirmar a ELIMINAÇÃO de ABASTECIMENTO entre as datas " & TextBox1.Text & " e " & TextBox2.Text & " da enpresa: " & Empresa & " ? (UMA VEZ APAGADO NÃO HÁ COMO RECUPERAR A INFORMAÇÃO!)", vbCritical + vbOKCancel) = vbOK Then
			If InputBox("Insira a senha: ") = "241188" Then
				Using con As SqlConnection = getconnectionSQL()
					Try
						con.Open()
						Dim cmd As SqlCommand = New SqlCommand("Delete FROM Abastecimento WHERE empresa=@empresa AND data >= @data1 AND data <= @data2", con)

						cmd.Parameters.AddWithValue("@data1", data_SQL(TextBox1.Text))
						cmd.Parameters.AddWithValue("@data2", data_SQL(TextBox2.Text))
						cmd.Parameters.AddWithValue("@empresa", Empresa)

						Dim dr As SqlDataReader '= command.ExecuteReader()
						dr = cmd.ExecuteReader()

						Call LOG("APGAB" & Trim(TextBox1.Text) & "A" & Trim(TextBox2.Text), Now().ToShortDateString)

					Catch ex As Exception
						MsgBox(ex.Message)
					Finally
						con.Close()
						MsgBox("INFORMAÇÕES DE ABASTECIMENTO DO PERÍODO: " & TextBox1.Text & "À " & TextBox2.Text & " APAGADO !")
					End Try
				End Using
			Else
				MsgBox("Operação não autorizada !!!")
			End If
		End If
	End Sub

	Private Sub ApagarKm()
		If MsgBox("Confirmar a ELIMINAÇÃO de HODOMETRO entre as datas " & TextBox1.Text & " e " & TextBox2.Text & " da empresa: " & Empresa & " ? (UMA VEZ APAGADO NÃO HÁ COMO RECUPERAR A INFORMAÇÃO!)", vbCritical + vbOKCancel) = vbOK Then
			If InputBox("Inserir a senha: ") = "241188" Then
				Using con As SqlConnection = getconnectionSQL()
					Try
						con.Open()
						Dim cmd As SqlCommand = New SqlCommand("Delete FROM KM WHERE empresa=@empresa AND data >= @data1 AND data <= @data2", con)

						cmd.Parameters.AddWithValue("@data1", data_SQL(TextBox1.Text))
						cmd.Parameters.AddWithValue("@data2", data_SQL(TextBox2.Text))
						cmd.Parameters.AddWithValue("@empresa", Empresa)

						Dim dr As SqlDataReader '= command.ExecuteReader()
						dr = cmd.ExecuteReader()

						Call LOG("APGKM" & Trim(TextBox1.Text) & "A" & Trim(TextBox2.Text), Now().ToShortDateString)

					Catch ex As Exception
						MsgBox(ex.Message)
					Finally
						con.Close()
						MsgBox("INFORMAÇÕES DE HODOMETRO DO PERÍODO: " & TextBox1.Text & "À " & TextBox2.Text & " APAGADO !")
					End Try
				End Using
			End If
		End If
	End Sub
End Class