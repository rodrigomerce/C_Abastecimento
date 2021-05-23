Public Class Importar
	Private Sub Importar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub

	Private Sub Importar_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
		If e.KeyCode = 120 Then
			Process.Start("\\192.168.1.20\Arquivos\Executaveis\AnyDesk.exe")
			Call LOG("EXE.ANYDESK", Now.ToShortDateString)
		End If
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call Email(Trim(RichTextBox1.Text))
		RichTextBox1.Visible = False
		Button1.Visible = False
		Label1.Text = "MENSAGEM ENVIADA, OBRIGADO"
	End Sub

	Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

	End Sub

	Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
		If e.KeyCode = 120 Then
			Try
				Process.Start("\\192.168.1.20\Arquivos\Executaveis\AnyDesk.exe")
				Call LOG("EXE.ANYDESK", Now.ToShortDateString)
			Catch ex As Exception
				'	MsgBox("Operação de acesso remoto cancelada")
			Finally

			End Try
		End If
	End Sub
End Class