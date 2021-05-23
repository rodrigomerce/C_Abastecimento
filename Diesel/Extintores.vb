
Public Class Extintores
	'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
	'	Call Ativo(False)

	'	Toolstrip1 = "	Usuário " & Environment.UserName
	'	Toolstrip2 = "v 1.0"

	'	ToolStripStatusLabel2.Text = Toolstrip1
	'	ToolStripStatusLabel3.Text = Toolstrip2

	'End Sub


	'Dim st As Boolean
	'Private Sub Ativo(st)
	'	v_usuario = Trim(UCase(TextBox1.Text))

	'	RegistroToolStripMenuItem.Enabled = st
	'	RelatóriosToolStripMenuItem.Enabled = st
	'	AlteraçõesToolStripMenuItem.Enabled = st
	'	'SairToolStripMenuItem.Enabled = Not st
	'	LogoutToolStripMenuItem.Enabled = st

	'	TextBox1.Visible = Not st
	'	TextBox2.Visible = Not st
	'	TextBox1.Text = ""
	'	TextBox2.Text = ""

	'End Sub

	'Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
	'	If e.KeyCode = 13 Then
	'		Dim senha As Boolean = False
	'		v_administrador = False

	'		senha = senha Or UCase(TextBox1.Text) = "ADM" And TextBox2.Text = "241188"
	'		senha = senha Or UCase(TextBox1.Text) = "MICHAEL" And TextBox2.Text = "2017"
	'		senha = senha Or UCase(TextBox1.Text) = "JONATHAN" And TextBox2.Text = "2017"

	'		v_administrador = v_administrador Or UCase(TextBox1.Text) = "ADM" And TextBox2.Text = "241188"
	'		v_administrador = v_administrador Or UCase(TextBox1.Text) = "JONATHAN" And TextBox2.Text = "2017"

	'		If senha Then
	'			Call Ativo(True)
	'		Else
	'			MsgBox("Senha ou Usuário incorreto !")
	'			TextBox1.Text = ""
	'			TextBox2.Text = ""
	'		End If
	'	End If
	'End Sub

	'Private Sub RegistroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistroToolStripMenuItem.Click
	'	Mapa.Show()
	'End Sub

	'Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
	'	Me.Close()
	'End Sub

	'Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
	'	Call Ativo(False)
	'End Sub

	'Private Sub RelatóriosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RelatóriosToolStripMenuItem.Click
	'	relatorio.Show()
	'End Sub

	'Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged
	'	If sender.text <> "" Then v_empresa = sender.text
	'	Dim s As String = v_empresa
	'	v_empresa = s.Substring(0, 2)

	'End Sub

	'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
	'	ToolStripStatusLabel1.Text = Now()
	'End Sub

	'Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

	'End Sub
End Class

