Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class Mapa

	Dim var_primeira_vez As Boolean

	Dim data_selecionada As Object
	Public sql As String
	Dim lb6 As Boolean
	Dim v_dt(300)
	Dim v_hr(300)
	Dim v_onib As Integer = 120



	Dim v_reg As Integer = 300
	Dim v_carga(179) As String
	Dim arrayradio(3) As RadioButton


	Dim v_pfx(178) As String
	Dim v_consumo(178) As Double


	Dim labelarray(v_onib) As Label

	'##############################################################################################################' KM

	Dim k_prx(300)
	Dim k_cmb(300)
	Dim k_hdm(300)
	Dim k_lo(300)
	Dim k_bmb(300)
	Dim k_dt(300)
	Dim k_hr(300)


	Dim labelarray2(v_reg) As Label

	'################################################################################################# ANALISE DA LEITURA DO KM

	Dim Aprefixo(300) As String
	Dim AKM(300) As Double


	Private Sub Mapa_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		'Call Menus()
	End Sub

	Private Sub Barradestatus()
		ToolStripStatusLabel1.Text = Now()
		With My.Application.Info.Version
			ToolStripStatusLabel2.Text = "Version " & .Major & "." & .Minor & " (Build " & .Build & "." & .Revision & ")"
		End With
		ToolStripStatusLabel3.Text = "Usuário " & Environment.UserName
	End Sub

	Private Sub Formata_radio(v_ind)
		arrayradio(0) = RadioButton1
		arrayradio(1) = RadioButton2
		arrayradio(2) = RadioButton3

		For i = 0 To 2
			arrayradio(i).Checked = False
		Next
		arrayradio(v_ind).Checked = True

		Select Case v_ind
			Case 0
				Empresa = "VB"
				NumEmpresa = "003"
			Case 1
				Empresa = "GT"
				NumEmpresa = "004"
			Case 2
				Empresa = "TV"
				NumEmpresa = "011"
				'Case 3
				'	Empresa = "TL"
				'	NumEmpresa = "002"
		End Select

	End Sub

	Private Sub Configura_Data()
		Dim Y
		Y = DateTime.Now.TimeOfDay.Hours
		Dim dataatual As DateTime = Now
		If Y > 10 Then
			TextBox4.Text = dataatual.ToShortDateString
		Else
			TextBox4.Text = dataatual.AddDays(-1).ToShortDateString
		End If
	End Sub

	Private Sub Mapa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim nomemaquina As String = System.Environment.UserDomainName()
		var_primeira_vez = True

		Call TipText()

		Call Barradestatus() ' CONFIGURA AS INFORMAÇÕES DE VERSÃO E DE DATA NA BARRA DE STATUS
		Call Formata_radio(0) ' FORMATA RADIOBUTTON ONDE É SELECIONADO EMPRESA E NUMEMPRESA PARA OS DEVIDOS QUERYS
		Call Configura_Data() ' CONFIGURA A DATA -1 PARA PEGAR O DIA QUE FOI FEITO O ABASTECIMENTO, NO CASO A NOITE ANTERIOR
		Call Buscar() ' BUSCAR INFORMAÇÕES DE EMPRESA, FILIAL E TANQUE EM CONFIGURAÇÕES
		Call Menus() ' JA ACONTECE EM FORM_ACTIVE
		Call LOG("LOGOU " & TimeOfDay, TextBox4.Text)

	End Sub

	Private Sub TipText()
		'BOTÕES ABASTECIMENTO
		ToolTip1.SetToolTip(Me.Button5, "Relatório de abasteciementos feitos durante o dia")
		ToolTip1.SetToolTip(Me.Button6, "Resumo dos abastecimentos e hodometros por bomba")
		ToolTip1.SetToolTip(Me.Button11, "Validação")
		ToolTip1.SetToolTip(Me.Button12, "Relatório de abastecimento em excel deste painel")
		ToolTip1.SetToolTip(Me.Button13, "Gerar arquivo")
		ToolTip1.SetToolTip(Me.Button14, "Configuração")
		ToolTip1.SetToolTip(Me.Button15, "Ajuda")
		ToolTip1.SetToolTip(Me.Button16, "Apagar base de dados")
		ToolTip1.SetToolTip(Me.Button17, "Relatórios")
		ToolTip1.SetToolTip(Me.Button18, "Relatórios Média")
		ToolTip1.SetToolTip(Me.Button19, "Relatórios Bombas")

		'BOTÕES HODOMETRO
		ToolTip1.SetToolTip(Me.Button23, "Relatório de hodometro em excel deste painel")
		ToolTip1.SetToolTip(Me.Button24, "Relatório Prefeitura")
		ToolTip1.SetToolTip(Me.Button31, "Relatório Ponto")
		ToolTip1.SetToolTip(Me.Button32, "Relatório Hodometro")
		ToolTip1.SetToolTip(Me.Button33, "Relatório Catraca")

		ToolTip1.SetToolTip(Me.Button25, "Validação")
		ToolTip1.SetToolTip(Me.Button26, "Lista mestra")

		'ABASTECIMENTO
		ToolTip1.SetToolTip(Me.Button2, "Excluir abastecimento do prefixo " & TextBox5.Text & " em " & TextBox2.Text)
		ToolTip1.SetToolTip(Me.Button3, "Gravar abastecimento do prefixo " & TextBox5.Text & " em " & TextBox2.Text)

		'HODOMETRO
		ToolTip1.SetToolTip(Me.Button22, "Excluir KM do prefixo " & TextBox5.Text & " em " & TextBox2.Text)
		ToolTip1.SetToolTip(Me.Button21, "Gravar KM do prefixo " & TextBox5.Text & " em " & TextBox2.Text)

		ToolTip1.SetToolTip(Me.Button1, "Fechar")

		'ABASTECIMENTO DIA
		ToolTip1.SetToolTip(Me.Button7, "Excluir abastecimento DIA do prefixo " & TextBox14.Text)
		ToolTip1.SetToolTip(Me.Button8, "Gravar abastecimento DIA do prefixo " & TextBox14.Text)
		ToolTip1.SetToolTip(Me.Button4, "Fechar")

		'RESUMO
		ToolTip1.SetToolTip(Me.Button9, "Gravar resumo diário")
		ToolTip1.SetToolTip(Me.Button10, "Fechar")

		'PLUS
		ToolTip1.SetToolTip(Me.Button35, "Verificar dados antes da importação")


	End Sub

	Private Sub Buscar()
		Using con As SqlConnection = getconnectionSQL()
			con.Open()

			Dim x As Integer = 1
			'	Dim command As New SqlCommand("SELECT * FROM configuracao", con)							'ORIGINAL
			Dim command As New SqlCommand("SELECT * FROM configuracao WHERE empresa=@NumEmpresa", con)
			command.Parameters.AddWithValue("@NumEmpresa", Trim(NumEmpresa))
			Dim dr As SqlDataReader
			dr = command.ExecuteReader()

			While dr.Read()
				LVEMP.Text = dr.Item("empresa")
				LVFIL.Text = dr.Item("filial")
				LVTAN.Text = dr.Item("tanque")
				x = x + 1
			End While

			con.Close()
		End Using
	End Sub

	Private Sub Base_dados()

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim x As Integer = 0

			'	Dim command As New SqlCommand("SELECT * FROM capac_onibus", con)							'ORIGINAL
			Dim command As New SqlCommand("SELECT * FROM capac_onibus WHERE empresa=@empresa ORDER BY prefixo", con)
			command.Parameters.AddWithValue("@empresa", Trim(Empresa))
			Dim dr As SqlDataReader
			dr = command.ExecuteReader()

			While dr.Read()

				v_prefixo(x) = dr.Item("prefixo")
				If Not IsDBNull(dr.Item("capacidade")) Then v_carga(x) = dr.Item("capacidade")

				If Not IsDBNull(dr.Item("empresa")) Then
					If dr.Item("empresa") = "TL" Then v_codEmp(x) = "002"
					If dr.Item("empresa") = "VB" Then v_codEmp(x) = "003"
					If dr.Item("empresa") = "GT" Then v_codEmp(x) = "004"
					If dr.Item("empresa") = "TV" Then v_codEmp(x) = "011"

					x = x + 1
				End If

			End While
			con.Close()

		End Using

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			'	Dim command As New SqlCommand("SELECT * FROM configuracao", con)							'ORIGINAL
			Dim command As New SqlCommand("SELECT * FROM configuracao WHERE empresa=@NumEmpresa", con)
			command.Parameters.AddWithValue("@NumEmpresa", Trim(NumEmpresa))
			Dim dr As SqlDataReader
			dr = command.ExecuteReader()
			Dim x As Integer = 0
			While dr.Read()
				v_empresa = dr.Item("empresa")
				v_filial = dr.Item("filial")
				v_tanque = dr.Item("tanque")
				If x = 0 Then v_bomba1 = dr.Item("bomba")
				v_bomba = dr.Item("bomba")
				v_bombaArray(x) = dr.Item("bomba")
				x = x + 1
			End While

			con.Close()

		End Using

	End Sub

	Private Sub Localizar()
		Using con As SqlConnection = getconnectionSQL()

			Dim labelarray(v_onib) As Label

			labelarray(0) = Label1
			labelarray(1) = Label2
			labelarray(2) = Label3
			labelarray(3) = Label4
			labelarray(4) = Label5
			labelarray(5) = Label6
			labelarray(6) = Label7
			labelarray(7) = Label8
			labelarray(8) = Label9
			labelarray(9) = Label10
			labelarray(10) = Label11
			labelarray(11) = Label12
			labelarray(12) = Label13
			labelarray(13) = Label14
			labelarray(14) = Label15
			labelarray(15) = Label16
			labelarray(16) = Label17
			labelarray(17) = Label18
			labelarray(18) = Label19
			labelarray(19) = Label20
			labelarray(20) = Label21
			labelarray(21) = Label22
			labelarray(22) = Label23
			labelarray(23) = Label24
			labelarray(24) = Label25
			labelarray(25) = Label26
			labelarray(26) = Label27
			labelarray(27) = Label28
			labelarray(28) = Label29
			labelarray(29) = Label30
			labelarray(30) = Label31
			labelarray(31) = Label32
			labelarray(32) = Label33
			labelarray(33) = Label34
			labelarray(34) = Label35
			labelarray(35) = Label36
			labelarray(36) = Label37
			labelarray(37) = Label38
			labelarray(38) = Label39
			labelarray(39) = Label40
			labelarray(40) = Label41
			labelarray(41) = Label42
			labelarray(42) = Label43
			labelarray(43) = Label44
			labelarray(44) = Label45
			labelarray(45) = Label46
			labelarray(46) = Label47
			labelarray(47) = Label48
			labelarray(48) = Label49
			labelarray(49) = Label50
			labelarray(50) = Label51
			labelarray(51) = Label52
			labelarray(52) = Label53
			labelarray(53) = Label54
			labelarray(54) = Label55
			labelarray(55) = Label56
			labelarray(56) = Label57
			labelarray(57) = Label58
			labelarray(58) = Label59
			labelarray(59) = Label60
			labelarray(60) = Label61
			labelarray(61) = Label62
			labelarray(62) = Label63
			labelarray(63) = Label64
			labelarray(64) = Label65
			labelarray(65) = Label66
			labelarray(66) = Label67
			labelarray(67) = Label68
			labelarray(68) = Label69
			labelarray(69) = Label70
			labelarray(70) = Label71
			labelarray(71) = Label72
			labelarray(72) = Label73
			labelarray(73) = Label74
			labelarray(74) = Label75
			labelarray(75) = Label76
			labelarray(76) = Label77
			labelarray(77) = Label78
			labelarray(78) = Label79
			labelarray(79) = Label80
			labelarray(80) = Label81
			labelarray(81) = Label82
			labelarray(82) = Label83
			labelarray(83) = Label84
			labelarray(84) = Label85
			labelarray(85) = Label86
			labelarray(86) = Label87
			labelarray(87) = Label88
			labelarray(88) = Label89
			labelarray(89) = Label90
			labelarray(90) = Label91
			labelarray(91) = Label92
			labelarray(92) = Label93
			labelarray(93) = Label94
			labelarray(94) = Label95
			labelarray(95) = Label96
			labelarray(96) = Label97
			labelarray(97) = Label98
			labelarray(98) = Label99
			labelarray(99) = Label100
			labelarray(100) = Label101
			labelarray(101) = Label102
			labelarray(102) = Label103
			labelarray(103) = Label104
			labelarray(104) = Label105
			labelarray(105) = Label106
			labelarray(106) = Label107
			labelarray(107) = Label108
			labelarray(108) = Label109
			labelarray(109) = Label110
			labelarray(110) = Label111
			labelarray(111) = Label112
			labelarray(112) = Label113
			labelarray(113) = Label114
			labelarray(114) = Label115
			labelarray(115) = Label116
			labelarray(116) = Label117
			labelarray(117) = Label118
			labelarray(118) = Label119
			labelarray(119) = Label120

			For i = 0 To v_onib - 1
				labelarray(i).Visible = False
			Next

			For i = 1 To 178
				v_consumo(i) = 0
			Next
			con.Open()

			'			Dim command As New SqlCommand("SELECT * FROM Capac_onibus order by prefixo", con)					ORIGINAL
			Dim command As New SqlCommand("SELECT * FROM Capac_onibus WHERE empresa=@empresa ORDER BY prefixo", con)
			command.Parameters.AddWithValue("@empresa", Trim(Empresa))
			Dim dr As SqlDataReader '= command.ExecuteReader()

			dr = command.ExecuteReader()

			Dim ind As Integer = 0

			While dr.Read()
				If Trim(dr.Item("prefixo")) <> "" Then
					v_pfx(ind) = dr.Item("prefixo")
					v_consumo(ind) = dr.Item("Consumo") / 10

					If dr.Item("Relatorio") = "S" Then
						labelarray(ind).Text = dr.Item("prefixo")
						labelarray(ind).Visible = True
						labelarray(ind).BackColor = Color.Red
						If dr.Item("status") Then labelarray(ind).BackColor = Color.Firebrick
						ind = ind + 1
					End If
				End If
			End While
		End Using

	End Sub

	Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click, Label2.Click, Label3.Click, Label4.Click, Label5.Click, Label6.Click, Label7.Click, Label8.Click, Label9.Click, Label10.Click _
, Label11.Click, Label12.Click, Label13.Click, Label14.Click, Label15.Click, Label16.Click, Label17.Click, Label18.Click, Label19.Click, Label20.Click, Label21.Click, Label22.Click, Label23.Click, Label24.Click, Label25.Click _
, Label26.Click, Label27.Click, Label28.Click, Label29.Click, Label30.Click, Label31.Click, Label32.Click, Label33.Click, Label34.Click, Label35.Click, Label36.Click, Label37.Click, Label38.Click, Label39.Click, Label40.Click _
, Label41.Click, Label42.Click, Label43.Click, Label44.Click, Label45.Click, Label46.Click, Label47.Click, Label48.Click, Label49.Click, Label50.Click, Label51.Click, Label52.Click, Label53.Click, Label54.Click, Label55.Click _
, Label56.Click, Label57.Click, Label58.Click, Label59.Click, Label60.Click, Label61.Click, Label62.Click, Label63.Click, Label64.Click, Label65.Click, Label66.Click, Label67.Click, Label68.Click, Label69.Click, Label70.Click _
, Label71.Click, Label72.Click, Label73.Click, Label74.Click, Label75.Click, Label76.Click, Label77.Click, Label78.Click, Label79.Click, Label80.Click, Label81.Click, Label82.Click, Label83.Click, Label84.Click, Label85.Click _
, Label86.Click, Label87.Click, Label88.Click, Label89.Click, Label90.Click, Label91.Click, Label92.Click, Label93.Click, Label94.Click, Label95.Click, Label96.Click, Label97.Click, Label98.Click, Label99.Click, Label100.Click _
, Label101.Click, Label102.Click, Label103.Click, Label104.Click, Label105.Click, Label106.Click, Label107.Click, Label108.Click, Label109.Click, Label110.Click, Label111.Click, Label112.Click, Label113.Click, Label114.Click, Label115.Click _
, Label116.Click, Label117.Click, Label118.Click, Label119.Click, Label120.Click

		''Select Case DateTime.Now.TimeOfDay

		''	Case New TimeSpan(20, 0, 0) To New TimeSpan(23, 59, 59)
		''		If var_primeira_vez Then MsgBox("Lançamentos")
		''	Case New TimeSpan(10, 0, 0) To New TimeSpan(19, 59, 59)

		''	Case New TimeSpan(0, 0, 0) To New TimeSpan(9, 59, 59)
		''		'	GoTo DiferenteVB
		''End Select

		'DiferenteVB:
		Call limpar()

		GroupBox3.Visible = True
		GroupBox4.Visible = False

		'Dim labelarray(v_onib) As Label

		labelarray(0) = Label1
		labelarray(1) = Label2
		labelarray(2) = Label3
		labelarray(3) = Label4
		labelarray(4) = Label5
		labelarray(5) = Label6
		labelarray(6) = Label7
		labelarray(7) = Label8
		labelarray(8) = Label9
		labelarray(9) = Label10
		labelarray(10) = Label11
		labelarray(11) = Label12
		labelarray(12) = Label13
		labelarray(13) = Label14
		labelarray(14) = Label15
		labelarray(15) = Label16
		labelarray(16) = Label17
		labelarray(17) = Label18
		labelarray(18) = Label19
		labelarray(19) = Label20
		labelarray(20) = Label21
		labelarray(21) = Label22
		labelarray(22) = Label23
		labelarray(23) = Label24
		labelarray(24) = Label25
		labelarray(25) = Label26
		labelarray(26) = Label27
		labelarray(27) = Label28
		labelarray(28) = Label29
		labelarray(29) = Label30
		labelarray(30) = Label31
		labelarray(31) = Label32
		labelarray(32) = Label33
		labelarray(33) = Label34
		labelarray(34) = Label35
		labelarray(35) = Label36
		labelarray(36) = Label37
		labelarray(37) = Label38
		labelarray(38) = Label39
		labelarray(39) = Label40
		labelarray(40) = Label41
		labelarray(41) = Label42
		labelarray(42) = Label43
		labelarray(43) = Label44
		labelarray(44) = Label45
		labelarray(45) = Label46
		labelarray(46) = Label47
		labelarray(47) = Label48
		labelarray(48) = Label49
		labelarray(49) = Label50
		labelarray(50) = Label51
		labelarray(51) = Label52
		labelarray(52) = Label53
		labelarray(53) = Label54
		labelarray(54) = Label55
		labelarray(55) = Label56
		labelarray(56) = Label57
		labelarray(57) = Label58
		labelarray(58) = Label59
		labelarray(59) = Label60
		labelarray(60) = Label61
		labelarray(61) = Label62
		labelarray(62) = Label63
		labelarray(63) = Label64
		labelarray(64) = Label65
		labelarray(65) = Label66
		labelarray(66) = Label67
		labelarray(67) = Label68
		labelarray(68) = Label69
		labelarray(69) = Label70
		labelarray(70) = Label71
		labelarray(71) = Label72
		labelarray(72) = Label73
		labelarray(73) = Label74
		labelarray(74) = Label75
		labelarray(75) = Label76
		labelarray(76) = Label77
		labelarray(77) = Label78
		labelarray(78) = Label79
		labelarray(79) = Label80
		labelarray(80) = Label81
		labelarray(81) = Label82
		labelarray(82) = Label83
		labelarray(83) = Label84
		labelarray(84) = Label85
		labelarray(85) = Label86
		labelarray(86) = Label87
		labelarray(87) = Label88
		labelarray(88) = Label89
		labelarray(89) = Label90
		labelarray(90) = Label91
		labelarray(91) = Label92
		labelarray(92) = Label93
		labelarray(93) = Label94
		labelarray(94) = Label95
		labelarray(95) = Label96
		labelarray(96) = Label97
		labelarray(97) = Label98
		labelarray(98) = Label99
		labelarray(99) = Label100
		labelarray(100) = Label101
		labelarray(101) = Label102
		labelarray(102) = Label103
		labelarray(103) = Label104
		labelarray(104) = Label105
		labelarray(105) = Label106
		labelarray(106) = Label107
		labelarray(107) = Label108
		labelarray(108) = Label109
		labelarray(109) = Label110
		labelarray(110) = Label111
		labelarray(111) = Label112
		labelarray(112) = Label113
		labelarray(113) = Label114
		labelarray(114) = Label115
		labelarray(115) = Label116
		labelarray(116) = Label117
		labelarray(117) = Label118
		labelarray(118) = Label119
		labelarray(119) = Label120


		Dim indice As Integer = 0

		Dim t As String = sender.name
		Dim tt As Integer = t.Substring(6 - 1)
		indice = tt - 1

		Call Click_informacao_carro_abasteciemto(sender)
		Call Click_informacao_carro_km(sender, indice)

		Call TipText()

	End Sub

	Private Sub Click_informacao_carro_abasteciemto(sender)

		TextBox5.Text = sender.text
		TextBox1.Text = TextBox4.Text

		Dim vx As Integer = sender.text
		Dim vi As Integer = 0
		Dim achou As Boolean = False

		While vi < v_onib And achou = False
			If labelarray(vi).Text = vx Then achou = True
			vi = vi + 1
		End While

		Dim indice As Integer = vi - 1
		Dim lb As String
		lb = sender.text

		Dim v_capacidade As Double = v_carga(indice)
		lbbmb2.Visible = True
		lbbmb2.Text = "[ " & v_bomba1 & " à " & v_bomba & " ]"


		Call acerto_data()

		Dim y As Integer = 0

		While (v_prx(y) <> labelarray(indice).Text) And (y < v_onib)
			y = y + 1
		End While

		If (v_prx(y) = labelarray(indice).Text) Then
			TextBox13.Text = v_cmb(y)

			Dim t As String = (v_cmb(y) - Int(v_cmb(y))) * 10
			Dim tt As String = ""
			tt = t.Substring(0, 1)

			TextBox12.Text = v_bmb(y)
			TextBox6.Text = v_km2(y)
			TextBox8.Text = v_km1(y)


			On Error Resume Next
			Dim v As String = (v_km1(y) - v_km2(y)) / v_cmb(y)
			Dim vv As String = ""
			vv = v.Substring(0, 4)

			If v_cmb(y) > 0 Then
				TextBox15.Text = CDbl(vv)
				If vv = "" Then TextBox15.Text = v
			End If
			TextBox9.Text = v_km1(y) - v_km2(y)

			y = 0
			While (v_pfx(y) <> labelarray(indice).Text) And (y < v_onib)
				y = y + 1
			End While

			If (v_pfx(y) = labelarray(indice).Text) Then
				TextBox11.Text = v_consumo(y)

				Dim xx As Double

				If (v_consumo(y) > 0) And (Not IsDBNull(v_consumo(y))) Then xx = (v_km1(y) - v_km2(y) / v_consumo(y))

				Dim s As String = xx
				Dim ss As String = ""
				ss = s.Substring(0, 6)
				TextBox10.Text = ss
				If ss = "" Then TextBox10.Text = xx

			End If
			TextBox2.Text = CStr(v_dt(y))

			Dim hora = v_hr(y)
			Dim v_hora As String = Convert.ToString(hora)

			TextBox3.Text = v_hora
			TextBox12.Focus()
		End If

	End Sub

	Private Sub acerto_data()
		TextBox2.Text = Date.Now.ToShortDateString
		TextBox3.Text = TimeOfDay
	End Sub

	Private Sub limpar()
		TextBox1.Text = ""
		TextBox3.Text = ""
		TextBox6.Text = ""
		TextBox8.Text = ""
		TextBox9.Text = ""
		TextBox10.Text = ""
		TextBox11.Text = ""
		TextBox12.Text = ""
		TextBox15.Text = ""
		TextBox5.Text = ""
		TextBox13.Text = ""

		'Campos do abastecimento dia

		TextBox14.Text = ""
		TextBox16.Text = ""
		TextBox17.Text = ""

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Call limpar()
		Call Limpar_KM()
		GroupBox3.Visible = False
		Label153.Visible = False
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		'Call Gravar_abastecimento()
		'GroupBox3.Visible = False

	End Sub

	Private Sub Gravar_abastecimento()

		'VERIFICA CAPACIDADE DO TANQUE DO VEICULO
		Dim y As Integer = 0
		While (Trim(v_prefixo(y)) <> Trim(TextBox5.Text)) And (y < v_onib)
			y = y + 1
		End While

		Dim erro As Boolean = False
		erro = erro Or (TextBox5.Text = "")
		erro = erro Or (TextBox12.Text = "")
		erro = erro Or (TextBox13.Text = "")

		If Not erro Then
			'########################################
			If TextBox13.Text <= CInt(v_carga(y)) Then

				TextBox13.Text = TextBox13.Text.Replace(",", ".")

				Using con As SqlConnection = getconnectionSQL()

					Try

						con.Open()

						Dim cmd As New SqlCommand("SELECT * From Abastecimento WHERE data=@data AND prefixo=@prefixo", con)
						cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
						cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
						Dim dr As SqlDataReader '= command.ExecuteReader()
						dr = cmd.ExecuteReader()

						If dr.Read() Then
							con.Close()
							con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

							Dim d As String = TextBox3.Text
							Dim dd As String = ""
							dd = d.Substring(0, 5)

							Dim cmd2 As New SqlCommand("UPDATE Abastecimento Set  Data=@data, Data_abast=@data_abast, hora=@hora, prefixo=@prefixo, bomba=@bomba, Combustivel=@combustivel, Empresa=@empresa WHERE  prefixo=@prefixo", con)
							cmd2.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
							cmd2.Parameters.AddWithValue("@data_abast", data_SQL(TextBox2.Text))
							cmd2.Parameters.AddWithValue("@hora", dd)
							cmd2.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
							cmd2.Parameters.AddWithValue("@bomba", Trim(CInt(TextBox12.Text)))
							cmd2.Parameters.AddWithValue("@combustivel", Trim(TextBox13.Text))
							cmd2.Parameters.AddWithValue("@empresa", Empresa)

							cmd2.ExecuteNonQuery()

							Call LOG("ALT.AB" & TextBox13.Text & Trim(TextBox5.Text), TextBox1.Text) ' LOG
						Else
							con.Close()
							con.Open()

							Dim cmd3 As SqlCommand
							sql = ""
							Try

								Dim d As String = TextBox3.Text
								Dim dd As String = ""
								dd = d.Substring(0, 5)

								sql = "INSERT INTO Abastecimento (Data, Data_abast, hora, prefixo, bomba, Combustivel, empresa) VALUES (@data, @data_abast, @hora, @prefixo, @bomba, @combustivel, @empresa)"
								cmd3 = New SqlCommand(sql, con)
								If TextBox1.Text <> "" Then cmd3.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
								If TextBox2.Text <> "" Then cmd3.Parameters.AddWithValue("@data_abast", data_SQL(TextBox2.Text))
								cmd3.Parameters.AddWithValue("@hora", dd)
								If TextBox5.Text <> "" Then cmd3.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
								If TextBox12.Text <> "" Then cmd3.Parameters.AddWithValue("@bomba", Trim(CInt(TextBox12.Text)))
								If TextBox13.Text <> "" Then cmd3.Parameters.AddWithValue("@combustivel", Trim(TextBox13.Text))
								cmd3.Parameters.AddWithValue("@empresa", Empresa)

								cmd3.ExecuteNonQuery()

								Call LOG("INS.AB" & Trim(TextBox13.Text) & Trim(TextBox5.Text), TextBox1.Text) ' LOG

							Catch ex As Exception
								MsgBox("ERRO AO INSERIR DADOS")
							End Try
						End If
					Catch ex As Exception
						MsgBox("ERRO AO CONSULTAR DADOS")
					Finally
						con.Close()
					End Try
				End Using

				'CAPACIDADE ONIBUS
				Using con As SqlConnection = getconnectionSQL()

					Try

						con.Open()

						Dim cmd As New SqlCommand("SELECT * From Capac_onibus WHERE prefixo=@prefixo", con)
						Dim dr As SqlDataReader '= command.ExecuteReader()
						cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
						dr = cmd.ExecuteReader()

						If dr.Read() Then
							con.Close()
							con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

							Dim cmd2 As New SqlCommand("UPDATE Capac_onibus Set  abastecido='" & True & "' WHERE prefixo=@prefixo", con)
							cmd2.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
							cmd2.ExecuteNonQuery()

							'Call limpar()
							'Call acerto_data()
							GroupBox3.Visible = False
							'MsgBox("Registro alterado com sucesso !")


						Else
							con.Close()
							con.Open()

							Dim cmd3 As SqlCommand
							sql = ""
							Try

								sql = "INSERT INTO Capac_onibus (prefixo, abastecimento) VALUES (@prefixo, @abastecimento )"

								cmd3 = New SqlCommand(sql, con)
								cmd3.Parameters.AddWithValue("@prefixo", Trim(TextBox5.Text))
								cmd3.Parameters.AddWithValue("@abastecimento", True)
								cmd3.ExecuteNonQuery()

								Call limpar()
								Call acerto_data()
								GroupBox3.Visible = False
								'MsgBox("Registro adicionado com sucesso !")

							Catch ex As Exception
								MsgBox(ex.Message)
							End Try
						End If

					Catch ex As Exception
						MsgBox(ex.Message)
					Finally
						con.Close()
						'Call Menus()
					End Try

				End Using

			Else
				MsgBox("Quantidade de DIESEL maior que o suportado pelo carro - GRAVAÇÃO DE DIESEL CANCELADA", vbCritical)
			End If
		Else
			MsgBox("Verifique se preencheu corretamento os campos:Bomba e Combustivel - GRAVAÇÃO DE DIESEL CANCELADA", vbCritical)
		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Dim chave As String
		chave = InputBox("Senha ?")

		If chave = "1992" Then Call Deletar()
		GroupBox3.Visible = False
	End Sub

	Private Sub Deletar()

		Dim resultado As MsgBoxResult
		resultado = MsgBox("Tem certeza que deseja apagar esse registro ?", vbYesNo, "Exclusão de registro")
		If resultado = MsgBoxResult.Yes Then

			Dim erro As Boolean
			Dim dr As SqlDataReader = Nothing

			erro = False
			erro = erro Or (TextBox1.Text = "")
			erro = erro Or (TextBox5.Text = "")

			If Not erro Then

				Using con As SqlConnection = getconnectionSQL()
					Try
						con.Open()
						sql = "Delete from Abastecimento where data=@data and prefixo=@prefixo"
						Dim cmd As SqlCommand = New SqlCommand(sql, con)

						cmd.Parameters.AddWithValue("@data", data_SQL(TextBox1.Text))
						cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox27.Text))
						dr = cmd.ExecuteReader()

						Call LOG("APG.AB" & Trim(TextBox27.Text), TextBox1.Text) ' LOG


					Catch ex As Exception
						MsgBox(ex.Message)
					Finally
						con.Close()
						'Call limpar()
						MsgBox("Registro excluido com sucesso !")
						Call Menus()
						'	GroupBox3.Visible = False
					End Try
				End Using
			End If
		Else
			MsgBox("Exclusão cancelada !")
		End If
	End Sub

	Private Sub TextBox6_Click(sender As Object, e As EventArgs)
		MonthCalendar1.Visible = True
	End Sub

	Private Sub TextBox5_Click(sender As Object, e As EventArgs)
		data_selecionada = sender
		MonthCalendar1.Visible = True
	End Sub

	Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
		TextBox4.Text = MonthCalendar1.SelectionStart
	End Sub

	Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter
		MonthCalendar1.Visible = False
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		ToolStripStatusLabel1.Text = Now()
		'ToolStripStatusLabel1.Text = Now().ToShortTimeString & " " & Now().ToShortDateString

		If DateTime.Now.TimeOfDay > CDate("11:00:00").TimeOfDay And DateTime.Now.TimeOfDay < CDate("11:00:02").TimeOfDay Then
			Call Mapa_Load(e, e)
		End If

	End Sub

	Dim v_prx(300)
	Dim v_cmb(300)
	Dim v_hdm(300)
	Dim v_lo(300)
	Dim v_bmb(300)

	Dim v_km1(300)
	Dim v_km2(300)

	Private Sub COMAND5_CLICK()
		If TextBox4.Text <> "" Then

			Dim labelarray(v_onib) As Label

			labelarray(0) = Label1
			labelarray(1) = Label2
			labelarray(2) = Label3
			labelarray(3) = Label4
			labelarray(4) = Label5
			labelarray(5) = Label6
			labelarray(6) = Label7
			labelarray(7) = Label8
			labelarray(8) = Label9
			labelarray(9) = Label10
			labelarray(10) = Label11
			labelarray(11) = Label12
			labelarray(12) = Label13
			labelarray(13) = Label14
			labelarray(14) = Label15
			labelarray(15) = Label16
			labelarray(16) = Label17
			labelarray(17) = Label18
			labelarray(18) = Label19
			labelarray(19) = Label20
			labelarray(20) = Label21
			labelarray(21) = Label22
			labelarray(22) = Label23
			labelarray(23) = Label24
			labelarray(24) = Label25
			labelarray(25) = Label26
			labelarray(26) = Label27
			labelarray(27) = Label28
			labelarray(28) = Label29
			labelarray(29) = Label30
			labelarray(30) = Label31
			labelarray(31) = Label32
			labelarray(32) = Label33
			labelarray(33) = Label34
			labelarray(34) = Label35
			labelarray(35) = Label36
			labelarray(36) = Label37
			labelarray(37) = Label38
			labelarray(38) = Label39
			labelarray(39) = Label40
			labelarray(40) = Label41
			labelarray(41) = Label42
			labelarray(42) = Label43
			labelarray(43) = Label44
			labelarray(44) = Label45
			labelarray(45) = Label46
			labelarray(46) = Label47
			labelarray(47) = Label48
			labelarray(48) = Label49
			labelarray(49) = Label50
			labelarray(50) = Label51
			labelarray(51) = Label52
			labelarray(52) = Label53
			labelarray(53) = Label54
			labelarray(54) = Label55
			labelarray(55) = Label56
			labelarray(56) = Label57
			labelarray(57) = Label58
			labelarray(58) = Label59
			labelarray(59) = Label60
			labelarray(60) = Label61
			labelarray(61) = Label62
			labelarray(62) = Label63
			labelarray(63) = Label64
			labelarray(64) = Label65
			labelarray(65) = Label66
			labelarray(66) = Label67
			labelarray(67) = Label68
			labelarray(68) = Label69
			labelarray(69) = Label70
			labelarray(70) = Label71
			labelarray(71) = Label72
			labelarray(72) = Label73
			labelarray(73) = Label74
			labelarray(74) = Label75
			labelarray(75) = Label76
			labelarray(76) = Label77
			labelarray(77) = Label78
			labelarray(78) = Label79
			labelarray(79) = Label80
			labelarray(80) = Label81
			labelarray(81) = Label82
			labelarray(82) = Label83
			labelarray(83) = Label84
			labelarray(84) = Label85
			labelarray(85) = Label86
			labelarray(86) = Label87
			labelarray(87) = Label88
			labelarray(88) = Label89
			labelarray(89) = Label90
			labelarray(90) = Label91
			labelarray(91) = Label92
			labelarray(92) = Label93
			labelarray(93) = Label94
			labelarray(94) = Label95
			labelarray(95) = Label96
			labelarray(96) = Label97
			labelarray(97) = Label98
			labelarray(98) = Label99
			labelarray(99) = Label100
			labelarray(100) = Label101
			labelarray(101) = Label102
			labelarray(102) = Label103
			labelarray(103) = Label104
			labelarray(104) = Label105
			labelarray(105) = Label106
			labelarray(106) = Label107
			labelarray(107) = Label108
			labelarray(108) = Label109
			labelarray(109) = Label110
			labelarray(110) = Label111
			labelarray(111) = Label112
			labelarray(112) = Label113
			labelarray(113) = Label114
			labelarray(114) = Label115
			labelarray(115) = Label116
			labelarray(116) = Label117
			labelarray(117) = Label118
			labelarray(118) = Label119
			labelarray(119) = Label120

			For i = 0 To 300
				v_prx(i) = ""
				v_cmb(i) = ""
				v_hdm(i) = ""
				v_lo(i) = ""
				v_bmb(i) = ""
				'                  v_consumo(i) = ""

				v_km1(i) = 0
				v_km2(i) = 0

			Next

			''''Using con As SqlConnection = getconnectionSQL()
			''''	con.Open()

			''''	'Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data", con)
			''''	Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE empresa=@empresa", con)
			''''	command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			''''	command.Parameters.AddWithValue("@empresa", Empresa)
			''''	Dim dr As SqlDataReader '= command.ExecuteReader()

			''''	dr = command.ExecuteReader()
			''''	Dim VY As Integer = 0
			''''	While dr.Read()
			''''		v_prx(VY) = dr.Item("prefixo")
			''''		VY = VY + 1
			''''	End While

			''''End Using

			Using con As SqlConnection = getconnectionSQL()

				con.Open()

				'Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data", con)
				Dim command As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data AND empresa=@empresa", con)
				command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()

				While dr.Read()

					Dim VY As Integer = 0
					Dim encontrou As Boolean = False

					While Not encontrou And (VY < v_onib - 1)
						encontrou = (Trim(dr.Item("prefixo")) = Trim(labelarray(VY).Text))
						VY = VY + 1
					End While

					VY = VY - 1

					If encontrou Then
						labelarray(VY).BackColor = Color.Green
						v_prx(VY) = dr.Item("prefixo")
						v_cmb(VY) = dr.Item("combustivel")
						If IsNothing(dr.Item("combustivel")) Then v_cmb(VY) = 0
						v_bmb(VY) = dr.Item("bomba")
						v_dt(VY) = data_Normal(dr.Item("Data_abast"))
						'v_hr(VY) = Format(dr.Item("Hora"), "HH:MM")
						v_hr(VY) = dr.Item("Hora")
					End If

				End While

			End Using

			Using con As SqlConnection = getconnectionSQL()

				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data", con)
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data AND empresa=@empresa", con)
				command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()

				While dr.Read()
					Dim w As Integer = 0
					Dim encontrou_ant As Boolean = False

					While Not encontrou_ant And (w < 300)
						encontrou_ant = (Trim(dr.Item("prefixo")) = Trim(v_prx(w)))
						If Not encontrou_ant Then w = w + 1
					End While
					v_km1(w) = dr.Item("hodometro")
					If IsNothing(dr.Item("hodometro")) Then v_km1(w) = 0
					w = w - 1
				End While

			End Using

			'PROCURA KM DO DIA ANTERIOR

			Using con As SqlConnection = getconnectionSQL()

				con.Open()

				'	Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data", con)
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem AND empresa=@empresa", con)
				command.Parameters.AddWithValue("@ontem", data_SQL(CDate(TextBox4.Text).AddDays(-1)))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()

				While dr.Read()
					Dim w As Integer = 0
					Dim encontrou_ant As Boolean = False

					While Not encontrou_ant And (w < 300)
						encontrou_ant = (Trim(dr.Item("prefixo")) = Trim(v_prx(w)))
						If Not encontrou_ant Then w = w + 1
					End While
					v_km2(w) = dr.Item("hodometro")
					If IsNothing(dr.Item("hodometro")) Then v_km2(w) = 0
					w = w - 1
				End While

			End Using

			For i = 0 To v_onib - 1
				If labelarray(i).BackColor <> Color.Green Then
					Dim XY As Integer = 0
					While (labelarray(i).Text <> v_prx(XY)) And (XY < 120 + 1)
						XY = XY + 1
					End While

					If v_prx(XY) = Nothing Then
						v_prx(XY) = ""
					End If

					'If (labelarray(i).Text = v_prx(XY)) And (labelarray(i).Text <> "") Then
					'	labelarray(XY).BackColor = Color.Blue
					'	If (v_km1(XY) > 0) And (v_km1(XY) = v_km2(XY)) Then labelarray(XY).BackColor = Color.DarkGray
					'End If
				End If
			Next

			Dim V_VLH As Integer = 0
			Dim V_VRD As Integer = 0
			Dim V_AZL As Integer = 0

			Dim Y As Integer = 0

			While (Y < v_onib)
				If labelarray(Y).Visible = True Then
					If labelarray(Y).BackColor = Color.Green Then V_VRD = V_VRD + 1
					If labelarray(Y).BackColor = Color.Firebrick Then V_VLH = V_VLH + 1
					If labelarray(Y).BackColor = Color.Blue Then V_AZL = V_AZL + 1
				End If
				Y = Y + 1
			End While

			labelverde.Text = V_VRD
			labelvermelha.Text = V_VLH
			labelazul.Text = V_AZL

		Else
			MsgBox("Data inválida !")
		End If
	End Sub

	Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
		MonthCalendar1.Visible = False
	End Sub

	Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
		If e.KeyCode = 13 Then
			data_Select = TextBox4.Text
			Call Menus()
		End If
	End Sub

	Private Sub TextBox4_Click(sender As Object, e As EventArgs) Handles TextBox4.Click
		lb6 = True
		data_selecionada = sender
		MonthCalendar1.Visible = True
	End Sub

	Private Sub Menus()
		GroupBox3.Visible = False
		Call Buscar()

		Call Base_dados()
		Call Localizar()
		Call COMAND5_CLICK()
		Call COMAND5_CLICK_KM()

		Call TipText()

	End Sub

	Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown
		If e.KeyCode = 13 Then
			If (TextBox7.Text = "abast") Or (TextBox7.Text = "241188") Then
				TextBox4.Enabled = True
				GroupBox6.Visible = True
				GroupBox12.Visible = True

				TextBox7.Visible = False
				keylabel.Visible = False

				Call LOG("INS.SENHA.CORRT.A", TextBox4.Text)
			Else
				If (TextBox7.Text <> "auxiliar") Then Call LOG("SENHAINCORRT" & TextBox7.Text, TextBox4.Text)
			End If
			If (TextBox7.Text = "auxiliar") Then
				TextBox4.Enabled = True
				Call LOG("INS.SENHA.CORRT.B", TextBox4.Text)
			End If
		End If
	End Sub

	Private Sub labeltitulo_DoubleClick(sender As Object, e As EventArgs) Handles labeltitulo.DoubleClick
		TextBox7.Visible = True
		keylabel.Visible = True

		TextBox7.Enabled = True
		keylabel.Enabled = True
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		GroupBox3.Visible = False
		GroupBox4.Visible = True
		GroupBox4.Left = 354
		Call Limpar_dia()
		Call Atualiza_diaria()
	End Sub

	Private Sub Atualiza_diaria()

		Dim x As Integer = 0
		Dim tt As Double = 0

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim command As New SqlCommand("SELECT * FROM diaria WHERE data=@data AND empresa=@empresa ORDER BY prefixo ", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
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
			Dgbgrid.Columns.Add("bomba", "Bomba")
			Dgbgrid.Columns.Add("diesel", "Diesel")
			Dgbgrid.Columns.Add("km", "KM")
			Dgbgrid.Columns.Add("hora", "Hora")
			Dgbgrid.Columns.Add("registro", "Registro")

			Dgbgrid.Columns(0).Width = 50
			Dgbgrid.Columns(1).Width = 40
			Dgbgrid.Columns(2).Width = 50
			Dgbgrid.Columns(3).Width = 70
			Dgbgrid.Columns(4).Width = 60
			Dgbgrid.Columns(5).Width = 60
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
			Dim v_registro = "000000"
			While dr.Read()
				If Not IsDBNull(dr.Item("Registro")) Then v_registro = dr.Item("Registro")
				Me.Dgbgrid.Rows.Add(dr.Item("prefixo"), dr.Item("bomba"), Format(dr.Item("diesel"), "#0.0"), Format(dr.Item("hodometro"), "###,###"), dr.Item("hora"), v_registro)
				tt = tt + dr.Item("diesel")
			End While
			Label127.Text = Format(tt, "#0.0")
		End Using

		TextBox33.Text = TimeOfDay

	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		GroupBox4.Visible = False
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

		Dim y As Integer = 0
		Dim d As String = "000000" & TextBox14.Text
		Dim pref As String = d.Substring(d.Length - 6)
		TextBox14.Text = pref
		While (Trim(v_prefixo(y)) <> Trim(pref)) And (y < v_onib)
			y = y + 1
		End While

		Dim x As Integer = 0
		While (v_bombaArray(x) <> CInt(TextBox16.Text)) And (x < 4)
			x = x + 1
		End While

		If (Trim(v_prefixo(y)) = Trim(TextBox14.Text)) And (v_bombaArray(x) = TextBox16.Text) Then
			Call Gravar_diaria()
		Else
			If MsgBox("Carro ou numero da bomba não pertencem a empresa selecionada, continuar gravação ?", vbYesNo) = vbYes Then
				Call Gravar_diaria()
			Else
				MsgBox("Operação cancelada!")
			End If

		End If

	End Sub

	Private Sub Gravar_diaria()

		Dim erro As Boolean = False
		erro = erro Or (TextBox4.Text = "")
		erro = erro Or (TextBox14.Text = "")
		erro = erro Or (TextBox16.Text = "")
		erro = erro Or (TextBox17.Text = "")
		erro = erro Or (TextBox35.Text = "") 'REGISTRO

		If Not erro Then
			Using con As SqlConnection = getconnectionSQL()

				Try
					con.Open()

					sql = "SELECT * From Diaria WHERE data=@data AND prefixo=@prefixo AND empresa=@empresa "
					'Using db = New SqlConnection("")
					Dim cmd = New SqlCommand(sql, con)
					Dim v_data As String = data_SQL(TextBox4.Text)
					'cmd.Parameters.Add("@data", SqlDbType.Date).Value = v_data
					cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
					'cmd.Parameters.Add("@prefixo", SqlDbType.NChar, 6).Value = Trim(TextBox14.Text)
					cmd.Parameters.AddWithValue("@prefixo", (TextBox14.Text))
					cmd.Parameters.AddWithValue("@empresa", Empresa)

					'Dados.Read(cmd.ExecuteReader())

					Dim dr As SqlDataReader = cmd.ExecuteReader()

					If dr.Read() Then
						con.Close()
						con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

						Dim cmd2 As SqlCommand
						sql = ""
						Try
							Dim d As String = TextBox33.Text
							Dim dd As String = ""
							dd = d.Substring(0, 5)

							sql = "UPDATE diaria Set  Data=@data , prefixo=@prefixo, bomba=@bomba, diesel=@diesel, hodometro=@km, hora=@hora, Registro=@registro WHERE prefixo=@prefixo and data=@data and bomba=@bomba and empresa=@empresa"
							cmd2 = New SqlCommand(sql, con)

							cmd2.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
							cmd2.Parameters.AddWithValue("@prefixo", Trim(TextBox14.Text))
							cmd2.Parameters.AddWithValue("@bomba", CInt(TextBox16.Text))
							cmd2.Parameters.AddWithValue("@diesel", CDbl(TextBox17.Text))
							cmd2.Parameters.AddWithValue("@empresa", Empresa)

							cmd2.Parameters.AddWithValue("@km", Trim(CDbl(TextBox34.Text)))
							cmd2.Parameters.AddWithValue("@hora", dd)
							cmd2.Parameters.AddWithValue("@registro", Trim(TextBox35.Text))

							cmd2.ExecuteNonQuery()
						Catch ex As Exception
							MsgBox(ex.Message)
						End Try

					Else
						con.Close()
						con.Open()

						Dim cmd3 As SqlCommand
						sql = ""
						Try

							Dim d As String = TextBox33.Text
							Dim dd As String = ""
							dd = d.Substring(0, 5)

							sql = "INSERT INTO diaria (Data, prefixo, bomba, diesel, empresa, hodometro, hora, registro) VALUES (@data,@prefixo,@bomba,@diesel,@empresa,@km,@hora,@registro)"
							cmd3 = New SqlCommand(sql, con)
							cmd3.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
							cmd3.Parameters.AddWithValue("@prefixo", Trim(TextBox14.Text))
							cmd3.Parameters.AddWithValue("@bomba", CInt(TextBox16.Text))
							cmd3.Parameters.AddWithValue("@diesel", CDbl(TextBox17.Text))
							cmd3.Parameters.AddWithValue("@empresa", Empresa)

							cmd3.Parameters.AddWithValue("@km", Trim(CDbl(TextBox34.Text)))
							cmd3.Parameters.AddWithValue("@hora", dd)
							cmd3.Parameters.AddWithValue("@registro", Trim(TextBox35.Text))

							cmd3.ExecuteNonQuery()
						Catch ex As Exception
							MsgBox(ex.Message)
						End Try
					End If
					Call Limpar_dia()
					Call Atualiza_diaria()
				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
				End Try
			End Using
		End If
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		Call Deletar_dia()
	End Sub

	Private Sub Deletar_dia()
		Dim resp As Boolean
		If TextBox14.Text <> "" Then
			resp = MsgBox("Confirma a ELIMINAÇÃO do Registro", vbCritical + vbOKCancel)
			If resp Then

				Dim dr As SqlDataReader = Nothing
				Using con As SqlConnection = getconnectionSQL()
					Try
						con.Open()
						sql = "Delete from diaria where data=@data and prefixo=@prefixo and diesel=@diesel"
						Dim cmd As SqlCommand = New SqlCommand(sql, con)
						cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
						cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox14.Text))
						cmd.Parameters.AddWithValue("@diesel", CDbl(TextBox17.Text))
						cmd.Parameters.AddWithValue("@empresa", Empresa)

						dr = cmd.ExecuteReader()
					Catch ex As Exception
						MsgBox(ex.Message)
					Finally
						con.Close()
						MsgBox("Registro excluido com sucesso !")
						Call Limpar_dia()

						Call Atualiza_diaria()
						GroupBox3.Visible = False
					End Try
				End Using
			End If
		End If
	End Sub

	Private Sub Limpar_dia()
		TextBox14.Text = ""
		TextBox16.Text = ""
		TextBox17.Text = ""
		TextBox34.Text = ""
		TextBox33.Text = TimeOfDay
		TextBox35.Text = ""
	End Sub

	Private Sub Dgbgrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgbgrid.CellContentClick

		Dim linha As Integer = Dgbgrid.CurrentRow.Index

		Dim prefixo As String = ""
		Dim bomba As Integer
		Dim diesel As Double
		Dim km As Integer
		Dim hora As String
		Dim registro As String

		Dgbgrid.CurrentCell = Dgbgrid.Rows(linha).Cells(0)

		prefixo = Dgbgrid.Rows(linha).Cells(0).Value
		bomba = Dgbgrid.Rows(linha).Cells(1).Value
		diesel = Dgbgrid.Rows(linha).Cells(2).Value
		km = Dgbgrid.Rows(linha).Cells(3).Value
		hora = Convert.ToString(Dgbgrid.Rows(linha).Cells(4).Value)
		registro = Trim(Dgbgrid.Rows(linha).Cells(5).Value)

		TextBox14.Text = Trim(prefixo)
		TextBox16.Text = Trim(bomba)
		TextBox17.Text = diesel
		TextBox33.Text = hora
		TextBox34.Text = km
		TextBox35.Text = registro

	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		'MsgBox("Resumo diário em desenvolvimento")
		GroupBox5.Left = 250
		Call Preenche_resumo()
	End Sub

	Private Sub Preenche_resumo()

		GroupBox3.Visible = False
		GroupBox5.Visible = True

		Dim tt_b(500) As Double
		Dim tt_Q(500) As Double

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim cmd As New SqlCommand("SELECT * FROM Abastecimento WHERE data=@data AND empresa=@empresa ", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			cmd.Parameters.AddWithValue("@empresa", Empresa)

			Dim dr As SqlDataReader

			dr = cmd.ExecuteReader()
			Dim b As Integer = 0
			Dim c As Double = 0
			While dr.Read()
				b = dr.Item("bomba")
				c = dr.Item("combustivel")
				tt_Q(b) = tt_Q(b) + 1
				tt_b(b) = tt_b(b) + c

			End While

			con.Close()

		End Using


		Using con As SqlConnection = getconnectionSQL()

			'HODOMETRO DO DIA ANTERIOR
			T174.Text = 0
			T184.Text = 0


			Dim Ontem As Date = CDate(TextBox4.Text).AddDays(-1)

			Dim X As Integer = 1

			con.Open()

			Dim cmd As New SqlCommand("SELECT * FROM hodometro WHERE data=@data AND empresa=@empresa ", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(Ontem))
			cmd.Parameters.AddWithValue("@empresa", Empresa)

			Dim dr As SqlDataReader
			dr = cmd.ExecuteReader()

			If dr.Read() Then
				T174.Text = Format(dr.Item("HFB1"), "#,##0.0")
				T184.Text = Format(dr.Item("HFB2"), "#,##0.0")

			End If

			con.Close()

		End Using

		'HODOMETRO DO DIA GRAVADO

		Using con As SqlConnection = getconnectionSQL()

			T175.Text = 0
			T185.Text = 0

			T177.Text = 0
			T187.Text = 0


			con.Open()

			Dim cmd As New SqlCommand("SELECT * FROM hodometro WHERE data=@data AND empresa=@empresa", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			cmd.Parameters.AddWithValue("@empresa", Empresa)

			Dim dr As SqlDataReader
			dr = cmd.ExecuteReader()

			If dr.Read() Then
				T175.Text = Format(dr.Item("HIB1"), "#,##0.0")
				T177.Text = Format(dr.Item("HFB1"), "#,##0.0")

				T185.Text = Format(dr.Item("HIB2"), "#,##0.0")
				T187.Text = Format(dr.Item("HFB2"), "#,##0.0")

			End If

			con.Close()


		End Using





		Using con As SqlConnection = getconnectionSQL()

			'	Dim tt_bd(4) As Integer ' NUMERO DAS BOMBAS
			Dim tt_bd(300) As Integer ' NUMERO DAS BOMBAS

			con.Open()
			Dim cmd As New SqlCommand("SELECT * FROM diaria WHERE data=@data AND empresa=@empresa", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			cmd.Parameters.AddWithValue("@empresa", Empresa)

			Dim dr As SqlDataReader
			dr = cmd.ExecuteReader()

			While dr.Read()
				Dim b As Integer = dr.Item("bomba")
				Dim d As Double = dr.Item("diesel")
				tt_bd(b) = tt_bd(b) + d
			End While

			Dim TTBD As Integer = 0
			'T172.Text = Format(tt_bd(1), "#,##0.00")
			'T182.Text = Format(tt_bd(2), "#,##0.00")
			'T192.Text = Format(tt_bd(3), "#,##0.00")
			'T202.Text = Format(tt_bd(4), "#,##0.00")

			T172.Text = Format(tt_bd(203), "#,##0.00")
			T182.Text = Format(tt_bd(204), "#,##0.00")

			For i = 1 To 300
				TTBD = TTBD + tt_bd(i)
			Next

			T212.Text = Format(TTBD, "#,##0.0")
			con.Close()
		End Using

		'QUANTIDADE DE ABASTECIMENTO

		Dim ttQC As Integer = 0

		T17.Text = tt_Q(203)
		T18.Text = tt_Q(204)

		For i = 1 To 500
			ttQC = ttQC + tt_Q(i)
		Next

		T21.Text = ttQC

		Dim ttBB As Double = 0

		T171.Text = Format(tt_b(203), "#,##0.00")
		T181.Text = Format(tt_b(204), "#,##0.00")

		For i = 1 To 300
			ttBB = ttBB + tt_b(i)
		Next
		T211.Text = Format(ttBB, "#,##0.00")


		Call Calculo_tt()
	End Sub

	Private Sub Calculo_tt()
		'HODOMETRO ANTERIOR - FINAL (5)
		T178.Text = 0
		T188.Text = 0

		If (T174.Text <> "") And (T177.Text <> "") Then
			If CDbl((T177.Text) < CDbl(T174.Text)) Then
				T178.Text = Format(100000 - CDbl(T174.Text) + CDbl(T177.Text), "#,##0.00")
			Else
				T178.Text = Format(CDbl(T177.Text) - CDbl(T174.Text), "#,##0.00")
			End If
		End If

		If (T184.Text <> "") And (T187.Text <> "") Then
			If CDbl((T187.Text) < CDbl(T184.Text)) Then
				T188.Text = Format(100000 - CDbl(T184.Text) + CDbl(T187.Text), "#,##0.00")
			Else
				T188.Text = Format(CDbl(T187.Text) - CDbl(T184.Text), "#,##0.00")
			End If
		End If


		T218.Text = Format(CDbl(T178.Text) + CDbl(T188.Text), "#,##0.00")


		'TOTAL ABASTECIMENTO
		If (T172.Text <> "") And (T171.Text <> "") Then T173.Text = Format(CDbl(T172.Text) + CDbl(T171.Text), "#,##0.00")
		If (T182.Text <> "") And (T181.Text <> "") Then T183.Text = Format(CDbl(T182.Text) + CDbl(T181.Text), "#,##0.00")

		T213.Text = Format(CDbl(T173.Text) + CDbl(T183.Text), "#,##0.00")


		'DIFERENÇA ENTRE O ANTERIOR E O INICIAL
		If (T174.Text <> "") And (T175.Text <> "") Then T176.Text = Format(CDbl(T175.Text) - CDbl(T174.Text), "#,##0.00")
		If (T184.Text <> "") And (T185.Text <> "") Then T186.Text = Format(CDbl(T185.Text) - CDbl(T184.Text), "#,##0.00")

		T216.Text = Format(CDbl(T176.Text) + CDbl(T186.Text), "#,##0.00")




		If (T173.Text <> "") And (T178.Text <> "") Then T179.Text = Format(CDbl(T173.Text) - CDbl(T178.Text), "#,##0.00")
		If (T183.Text <> "") And (T188.Text <> "") Then T189.Text = Format(CDbl(T183.Text) - CDbl(T188.Text), "#,##0.00")

		T219.Text = Format(CDbl(T179.Text) + CDbl(T189.Text), "#,##0.00")

	End Sub

	Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
		GroupBox5.Visible = False
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		Call Gravar_resumo
	End Sub

	Private Sub Gravar_resumo()

		Dim erro As Boolean = False
		erro = erro Or (T175.Text = "")
		erro = erro Or (T177.Text = "")
		erro = erro Or (T185.Text = "")
		erro = erro Or (T187.Text = "")


		If Not erro Then
			Using con As SqlConnection = getconnectionSQL()

				Try

					con.Open()

					sql = "SELECT * From hodometro WHERE data=@data"
					Dim cmd As New SqlCommand(sql, con)

					cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
					Dim dr As SqlDataReader '= command.ExecuteReader()

					dr = cmd.ExecuteReader()

					If dr.Read() Then
						con.Close()
						con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

						Dim cmd2 As New SqlCommand("UPDATE hodometro Set  Data=@data, HIB1=@HIB1, HFB1=@HFB1, HIB2=@HIB2, HFB2=@HFB2, HIB3=@HIB3, HFB3=@HFB3, HIB4=@HIB4, HFB4=@HFB4 WHERE  data=@data", con)

						cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
						cmd.Parameters.AddWithValue("@HIB1", T175.Text)
						cmd.Parameters.AddWithValue("@HFB1", T177.Text)
						cmd.Parameters.AddWithValue("@HIB2", T185.Text)
						cmd.Parameters.AddWithValue("@HFB2", T187.Text)


						cmd2.ExecuteNonQuery()

					Else
						con.Close()
						con.Open()

						Dim cmd3 As SqlCommand
						sql = ""
						Try

							sql = "INSERT INTO hodometro (data, HIB1, HFB1, HIB2, HFB2, HIB3, HFB3, HIB4, HFB4) VALUES (@data, @HIB1, @HFB1, @HIB2, @HFB2, @HIB3, @HFB3, @HIB4, @HFB4)"
							cmd3 = New SqlCommand(sql, con)

							cmd3.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
							cmd3.Parameters.AddWithValue("@HIB1", CDbl(T175.Text))
							cmd3.Parameters.AddWithValue("@HFB1", CDbl(T177.Text))
							cmd3.Parameters.AddWithValue("@HIB2", CDbl(T185.Text))
							cmd3.Parameters.AddWithValue("@HFB2", CDbl(T187.Text))


							cmd3.ExecuteNonQuery()
							MsgBox("Gravado com sucesso !")
						Catch ex As Exception
							MsgBox(ex.Message)
						End Try
					End If

				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
				End Try

			End Using
		Else
			MsgBox("Preencha os Campos !")
		End If
	End Sub

	Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
		Correção.Show()
	End Sub

	Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
		Call Relatorio_mapa_abastecimento
	End Sub

	Private Sub Relatorio_mapa_abastecimento()

		Dim XcelApp As New Excel.Application

		XcelApp.Application.Workbooks.Add(Type.Missing)

		Dim labelarray(v_onib) As Label

		labelarray(0) = Label1
		labelarray(1) = Label2
		labelarray(2) = Label3
		labelarray(3) = Label4
		labelarray(4) = Label5
		labelarray(5) = Label6
		labelarray(6) = Label7
		labelarray(7) = Label8
		labelarray(8) = Label9
		labelarray(9) = Label10
		labelarray(10) = Label11
		labelarray(11) = Label12
		labelarray(12) = Label13
		labelarray(13) = Label14
		labelarray(14) = Label15
		labelarray(15) = Label16
		labelarray(16) = Label17
		labelarray(17) = Label18
		labelarray(18) = Label19
		labelarray(19) = Label20
		labelarray(20) = Label21
		labelarray(21) = Label22
		labelarray(22) = Label23
		labelarray(23) = Label24
		labelarray(24) = Label25
		labelarray(25) = Label26
		labelarray(26) = Label27
		labelarray(27) = Label28
		labelarray(28) = Label29
		labelarray(29) = Label30
		labelarray(30) = Label31
		labelarray(31) = Label32
		labelarray(32) = Label33
		labelarray(33) = Label34
		labelarray(34) = Label35
		labelarray(35) = Label36
		labelarray(36) = Label37
		labelarray(37) = Label38
		labelarray(38) = Label39
		labelarray(39) = Label40
		labelarray(40) = Label41
		labelarray(41) = Label42
		labelarray(42) = Label43
		labelarray(43) = Label44
		labelarray(44) = Label45
		labelarray(45) = Label46
		labelarray(46) = Label47
		labelarray(47) = Label48
		labelarray(48) = Label49
		labelarray(49) = Label50
		labelarray(50) = Label51
		labelarray(51) = Label52
		labelarray(52) = Label53
		labelarray(53) = Label54
		labelarray(54) = Label55
		labelarray(55) = Label56
		labelarray(56) = Label57
		labelarray(57) = Label58
		labelarray(58) = Label59
		labelarray(59) = Label60
		labelarray(60) = Label61
		labelarray(61) = Label62
		labelarray(62) = Label63
		labelarray(63) = Label64
		labelarray(64) = Label65
		labelarray(65) = Label66
		labelarray(66) = Label67
		labelarray(67) = Label68
		labelarray(68) = Label69
		labelarray(69) = Label70
		labelarray(70) = Label71
		labelarray(71) = Label72
		labelarray(72) = Label73
		labelarray(73) = Label74
		labelarray(74) = Label75
		labelarray(75) = Label76
		labelarray(76) = Label77
		labelarray(77) = Label78
		labelarray(78) = Label79
		labelarray(79) = Label80
		labelarray(80) = Label81
		labelarray(81) = Label82
		labelarray(82) = Label83
		labelarray(83) = Label84
		labelarray(84) = Label85
		labelarray(85) = Label86
		labelarray(86) = Label87
		labelarray(87) = Label88
		labelarray(88) = Label89
		labelarray(89) = Label90
		labelarray(90) = Label91
		labelarray(91) = Label92
		labelarray(92) = Label93
		labelarray(93) = Label94
		labelarray(94) = Label95
		labelarray(95) = Label96
		labelarray(96) = Label97
		labelarray(97) = Label98
		labelarray(98) = Label99
		labelarray(99) = Label100
		labelarray(100) = Label101
		labelarray(101) = Label102
		labelarray(102) = Label103
		labelarray(103) = Label104
		labelarray(104) = Label105
		labelarray(105) = Label106
		labelarray(106) = Label107
		labelarray(107) = Label108
		labelarray(108) = Label109
		labelarray(109) = Label110
		labelarray(110) = Label111
		labelarray(111) = Label112
		labelarray(112) = Label113
		labelarray(113) = Label114
		labelarray(114) = Label115
		labelarray(115) = Label116
		labelarray(116) = Label117
		labelarray(117) = Label118
		labelarray(118) = Label119
		labelarray(119) = Label120


		XcelApp.Cells(1, 1) = "DATA"
		XcelApp.Cells(1, 3) = " EMPRESA"
		XcelApp.Cells(1, 5) = "FILIAL"
		XcelApp.Cells(1, 7) = "TANQUE"

		XcelApp.Cells(2, 1) = "PREFIXO"
		XcelApp.Cells(2, 2) = "STATUS"
		XcelApp.Cells(2, 3) = "COMBUSTÍVEL"
		XcelApp.Cells(2, 4) = "HODOMETRO"
		XcelApp.Cells(2, 5) = "OLEO"
		XcelApp.Cells(2, 6) = "BOMBA"
		XcelApp.Cells(2, 7) = "DATA"
		XcelApp.Cells(2, 8) = "HORA"

		XcelApp.Cells(1, 2) = TextBox4.Text
		XcelApp.Cells(1, 6) = Label9.Text

		For i = 0 To v_onib - 1

			If (labelarray(i).Visible = True) Then
				XcelApp.Cells(i + 3, 1) = labelarray(i).Text

				If (labelarray(i).BackColor = Color.Firebrick) Then
					XcelApp.Cells(i + 3, 2) = "Falta"
				End If
				If (labelarray(i).BackColor = Color.Green) Then
					XcelApp.Cells(i + 3, 2) = "OK"
					Dim Y As Integer = 0

					While (v_prx(Y) <> labelarray(i).Text) And (Y <= v_reg)
						Y = Y + 1
					End While

					If (v_prx(Y) = labelarray(i).Text) Then
						XcelApp.Cells(i + 3, 3) = v_cmb(Y)
						XcelApp.Cells(i + 3, 4) = v_hdm(Y)
						XcelApp.Cells(i + 3, 5) = v_lo(Y)
						XcelApp.Cells(i + 3, 6) = v_bmb(Y)
						XcelApp.Cells(i + 3, 7) = CStr(v_dt(Y))
						Dim hora As String = Convert.ToString(v_hr(Y))
						XcelApp.Cells(i + 3, 8) = hora
					End If
				End If
				If labelarray(i).BackColor = Color.DarkGray Then XcelApp.Cells(i + 3, 2) = "MANUTENÇÃO"
			End If

		Next

		XcelApp.Columns.AutoFit()
		'
		XcelApp.Visible = True

	End Sub

	Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
		Exportar.Show()
	End Sub

	Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
		Onibus.Show()
	End Sub

	Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
		Importar.Show()
	End Sub

	Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
		Apagar.Show()
	End Sub

	Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
		'Relatorios.Show()
		MsgBox("Relatório em desenvolvimento")
	End Sub

	Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

	End Sub

	Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
		Medias.Show()
	End Sub

	Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
		Rel_Bomba.Show()
	End Sub

	Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
		Call Formata_radio(0)
		Call Menus()
		Call Ajusta_Sistema_Para_Empresa_selecionada(Empresa)
	End Sub

	Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
		Call Formata_radio(1)
		Call Menus()
		Call Ajusta_Sistema_Para_Empresa_selecionada(Empresa)
	End Sub

	Private Sub RadioButton3_Click(sender As Object, e As EventArgs) Handles RadioButton3.Click
		Call Formata_radio(2)
		Call Menus()
		Call Ajusta_Sistema_Para_Empresa_selecionada(Empresa)
	End Sub

	Private Sub Ajusta_Sistema_Para_Empresa_selecionada(xempresa)
		Label139.Text = "BOMBA " & v_bomba1
		Label140.Text = "BOMBA " & v_bomba


	End Sub

	Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
		Call Menus()
	End Sub

	'########################################################################  KM  ######################################################################################################'
	'########################################################################  KM  ######################################################################################################'

	Private Sub COMAND5_CLICK_KM()

		If TextBox4.Text <> "" Then

			Using con As SqlConnection = getconnectionSQL()


				Dim labelarray(v_onib) As Label

				labelarray(0) = Label1
				labelarray(1) = Label2
				labelarray(2) = Label3
				labelarray(3) = Label4
				labelarray(4) = Label5
				labelarray(5) = Label6
				labelarray(6) = Label7
				labelarray(7) = Label8
				labelarray(8) = Label9
				labelarray(9) = Label10
				labelarray(10) = Label11
				labelarray(11) = Label12
				labelarray(12) = Label13
				labelarray(13) = Label14
				labelarray(14) = Label15
				labelarray(15) = Label16
				labelarray(16) = Label17
				labelarray(17) = Label18
				labelarray(18) = Label19
				labelarray(19) = Label20
				labelarray(20) = Label21
				labelarray(21) = Label22
				labelarray(22) = Label23
				labelarray(23) = Label24
				labelarray(24) = Label25
				labelarray(25) = Label26
				labelarray(26) = Label27
				labelarray(27) = Label28
				labelarray(28) = Label29
				labelarray(29) = Label30
				labelarray(30) = Label31
				labelarray(31) = Label32
				labelarray(32) = Label33
				labelarray(33) = Label34
				labelarray(34) = Label35
				labelarray(35) = Label36
				labelarray(36) = Label37
				labelarray(37) = Label38
				labelarray(38) = Label39
				labelarray(39) = Label40
				labelarray(40) = Label41
				labelarray(41) = Label42
				labelarray(42) = Label43
				labelarray(43) = Label44
				labelarray(44) = Label45
				labelarray(45) = Label46
				labelarray(46) = Label47
				labelarray(47) = Label48
				labelarray(48) = Label49
				labelarray(49) = Label50
				labelarray(50) = Label51
				labelarray(51) = Label52
				labelarray(52) = Label53
				labelarray(53) = Label54
				labelarray(54) = Label55
				labelarray(55) = Label56
				labelarray(56) = Label57
				labelarray(57) = Label58
				labelarray(58) = Label59
				labelarray(59) = Label60
				labelarray(60) = Label61
				labelarray(61) = Label62
				labelarray(62) = Label63
				labelarray(63) = Label64
				labelarray(64) = Label65
				labelarray(65) = Label66
				labelarray(66) = Label67
				labelarray(67) = Label68
				labelarray(68) = Label69
				labelarray(69) = Label70
				labelarray(70) = Label71
				labelarray(71) = Label72
				labelarray(72) = Label73
				labelarray(73) = Label74
				labelarray(74) = Label75
				labelarray(75) = Label76
				labelarray(76) = Label77
				labelarray(77) = Label78
				labelarray(78) = Label79
				labelarray(79) = Label80
				labelarray(80) = Label81
				labelarray(81) = Label82
				labelarray(82) = Label83
				labelarray(83) = Label84
				labelarray(84) = Label85
				labelarray(85) = Label86
				labelarray(86) = Label87
				labelarray(87) = Label88
				labelarray(88) = Label89
				labelarray(89) = Label90
				labelarray(90) = Label91
				labelarray(91) = Label92
				labelarray(92) = Label93
				labelarray(93) = Label94
				labelarray(94) = Label95
				labelarray(95) = Label96
				labelarray(96) = Label97
				labelarray(97) = Label98
				labelarray(98) = Label99
				labelarray(99) = Label100
				labelarray(100) = Label101
				labelarray(101) = Label102
				labelarray(102) = Label103
				labelarray(103) = Label104
				labelarray(104) = Label105
				labelarray(105) = Label106
				labelarray(106) = Label107
				labelarray(107) = Label108
				labelarray(108) = Label109
				labelarray(109) = Label110
				labelarray(110) = Label111
				labelarray(111) = Label112
				labelarray(112) = Label113
				labelarray(113) = Label114
				labelarray(114) = Label115
				labelarray(115) = Label116
				labelarray(116) = Label117
				labelarray(117) = Label118
				labelarray(118) = Label119
				labelarray(119) = Label120

				For i = 0 To v_reg

					k_prx(i) = ""
					k_cmb(i) = ""
					k_hdm(i) = ""
					k_lo(i) = ""
					k_bmb(i) = ""

				Next

				con.Open()

				Dim command As New SqlCommand("SELECT * FROM Km WHERE data=@data AND empresa=@empresa", con)
				command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)

				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Call Limpa_arraylabel2()

				For i = 0 To 119                                  'CRIAR NOVAS LABELS PARA MOSTRAR INFORMAÇÕES SOBRE O KM
					If (labelarray(i).Visible = True) Then
						labelarray2(i) = New Label With {
						.Visible = True,
						.Left = labelarray(i).Left,
						.Top = labelarray(i).Bottom + 2,
						.Width = labelarray(i).Width,
						.Height = 2.5,
						.BackColor = Color.Firebrick
						}
					End If
					If Not IsNothing(labelarray2(i)) Then
						If labelarray2(i).Visible = True Then
							GroupBox1.Controls.Add(labelarray2(i))
						End If
					End If
				Next

				While dr.Read()

					Dim VY As Integer = 0
					Dim encontrou As Boolean = False

					While Not encontrou And (VY < v_onib - 1)
						encontrou = (Trim(dr.Item("prefixo")) = Trim(labelarray(VY).Text))
						VY = VY + 1
					End While

					VY = VY - 1

					If encontrou Then
						labelarray2(VY).BackColor = Color.Green
						Refresh()
						k_prx(VY) = dr.Item("prefixo")
						k_hdm(VY) = dr.Item("hodometro")
						If IsNothing(dr.Item("hodometro")) Then k_hdm(VY) = 0
						k_dt(VY) = data_Normal(dr.Item("Data_abast"))
						'v_hr(VY) = Format(dr.Item("Hora"), "HH:MM")
						k_hr(VY) = dr.Item("Hora")
					End If

				End While

				''Dim verde As Integer = 0
				''Dim vermelho As Integer = 0

				''For i = 0 To 119
				''	If labelarray(i).Visible = True Then
				''		If labelarray2(i).Visible Then
				''			If labelarray2(i).BackColor = Color.Firebrick Then vermelho = vermelho + 1
				''			If labelarray2(i).BackColor = Color.Green Then verde = verde + 1
				''		End If
				''	End If
				''Next
				''lbregistrado.Text = verde
				''naoregistrado.Text = vermelho



				Dim V_VLH As Integer = 0
				Dim V_VRD As Integer = 0
				Dim V_AZL As Integer = 0

				Dim Y As Integer = 0

				While (Y < v_onib)
					If labelarray(Y).Visible = True Then
						If labelarray2(Y).Visible Then
							If labelarray2(Y).BackColor = Color.Green Then V_VRD = V_VRD + 1
							If labelarray2(Y).BackColor = Color.Firebrick Then V_VLH = V_VLH + 1
							If labelarray2(Y).BackColor = Color.Blue Then V_AZL = V_AZL + 1
						End If
					End If
					Y = Y + 1
				End While

				lbregistrado.Text = V_VRD
				naoregistrado.Text = V_VLH
				'labelazul.Text = V_AZL

			End Using

		Else
			MsgBox("Data Invalida KM")
		End If

	End Sub

	Private Sub Limpa_arraylabel2()

		For i = 0 To 119
			If Not IsNothing(labelarray2(i)) Then
				labelarray2(i).Dispose()
			End If

		Next

	End Sub

	Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click

		Call Gravar_abastecimento()  ' GRAVA O ABASTECIMENTO, ADICIONADO PARA QUE SEJA GRAVADO HODOMETRO E ABASTECIMENTO EM UNICO BOTÃO

		Dim erro As Boolean = False
		If TextBox26.Text = "" Then
			Call Gravar_hodometro()
		Else

			If CheckBox1.CheckState = 0 Then
				If TextBox21.Text <> "" Then
					If CDbl(TextBox21.Text) < CDbl(TextBox26.Text) Or TextBox21.Text = "" Then ' VERIFICAR QUANDO VEM VAZIO O QUE FAZER
						Label153.Text = "Valor de KM menor que KM anterior - GRAVAÇÃO CANCELADA"
						erro = True
					End If

					If (CDbl(TextBox21.Text) - CDbl(TextBox26.Text)) > 1200 Then
						Label153.Text = "Valor de KM muito alto - GRAVAÇÃO CANCELADA"
						erro = True
					End If
				End If
				If Not erro Then Call Gravar_hodometro()
			Else
				Call Gravar_hodometro()
			End If
		End If
		Call Verifica_Dados_Duplicados()
		Call Menus()
	End Sub

	Private Sub Gravar_hodometro()

		Dim erro As Boolean = False
		erro = erro Or (TextBox27.Text = "")
		erro = erro Or (TextBox21.Text = "")
		'erro = erro Or (TextBox22.Text = "") REGISTRO

		If Not erro Then

			Using con As SqlConnection = getconnectionSQL()

				Try

					con.Open()

					Dim cmd As New SqlCommand("SELECT * FROM KM WHERE data=@data AND prefixo=@prefixo", con)
					cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
					cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox27.Text))
					cmd.Parameters.AddWithValue("@empresa", Empresa)
					Dim dr As SqlDataReader '= command.ExecuteReader()
					dr = cmd.ExecuteReader()

					If dr.Read() Then
						con.Close()
						con.Open()                                                                                                                                                                                                                                                ' empresa='" & ComboBox1.Text & "' , funcionario='" & TextBox4.Text & "', telefone ='" & TextBox5.Text & "', email ='" & TextBox6.Text & "', area ='" & ComboBox2.Text & "', celular ='" & TextBox7.Text & "', status ='" & v_check & "', estacao ='" & TextBox8.Text & "'

						Dim d As String = TextBox20.Text
						Dim dd As String = ""
						dd = d.Substring(0, 5)

						Dim cmd2 As New SqlCommand("UPDATE KM Set  Data=@data, Data_abast=@data_abast, hora=@hora, prefixo=@prefixo, hodometro=@hodometro, registro=@registro, horaMot=@HoraMot, st_hodo=@St_hodometro WHERE data=@data and prefixo=@prefixo", con)

						cmd2.Parameters.AddWithValue("@data", data_SQL(TextBox19.Text))
						cmd2.Parameters.AddWithValue("@data_abast", data_SQL(TextBox18.Text))
						cmd2.Parameters.AddWithValue("@hora", dd)
						cmd2.Parameters.AddWithValue("@prefixo", Trim(TextBox27.Text))
						cmd2.Parameters.AddWithValue("@hodometro", Trim(CDbl(TextBox21.Text)))

						If TextBox22.Text = "" Then TextBox22.Text = 0
						If TextBox23.Text = "" Then TextBox23.Text = "00:00"
						'..CDate(Format(TextBox1.Text, "hh:mm:ss"))
						If TextBox22.Text <> "" Then cmd2.Parameters.AddWithValue("@registro", TextBox22.Text)

						If TextBox23.Text <> "" Then cmd2.Parameters.AddWithValue("@HoraMot", TextBox23.Text)
						cmd2.Parameters.AddWithValue("@St_hodometro", CheckBox1.CheckState)

						cmd2.ExecuteNonQuery()

						Call LOG("ALT.KM" & Trim(TextBox21.Text) & Trim(TextBox27.Text), TextBox19.Text) ' LOG
					Else
						con.Close()
						con.Open()

						Dim cmd3 As SqlCommand
						sql = ""
						Try

							Dim d As String = TextBox20.Text
							Dim dd As String = ""
							dd = d.Substring(0, 5)

							sql = "INSERT INTO KM (Data, Data_abast, hora, prefixo, hodometro, registro, horaMot, st_hodo, empresa) VALUES (@data, @data_abast, @hora, @prefixo, @hodometro, @registro, @HoraMot, @ST_hodometro, @empresa)"
							cmd3 = New SqlCommand(sql, con)
							cmd3.Parameters.AddWithValue("@data", data_SQL(TextBox19.Text))
							cmd3.Parameters.AddWithValue("@data_abast", data_SQL(TextBox18.Text))
							cmd3.Parameters.AddWithValue("@hora", dd)
							cmd3.Parameters.AddWithValue("@prefixo", Trim(TextBox27.Text))
							cmd3.Parameters.AddWithValue("@hodometro", Trim(CDbl(TextBox21.Text)))

							If TextBox22.Text = "" Then TextBox22.Text = 0
							If TextBox23.Text = "" Then TextBox23.Text = "00:00"

							If TextBox22.Text <> "" Then cmd3.Parameters.AddWithValue("@registro", TextBox22.Text)
							If TextBox23.Text <> "" Then cmd3.Parameters.AddWithValue("HoraMot", TextBox23.Text)
							cmd3.Parameters.AddWithValue("@St_hodometro", CheckBox1.CheckState)
							cmd3.Parameters.AddWithValue("@empresa", Empresa)

							cmd3.ExecuteNonQuery()

							Call LOG("INS.KM " & Trim(TextBox21.Text) & Trim(TextBox27.Text), TextBox19.Text)

						Catch ex As Exception
							MsgBox(ex.Message)
						End Try
					End If
				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
					'Call Menus()
				End Try
			End Using
		Else
			MsgBox("Verifique as informações HODOMETRO e REGISTRO")
		End If

	End Sub

	Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
		Dim chave As String
		chave = InputBox("Senha ?")

		If chave = "1992" Then Call deletar2()
		GroupBox3.Visible = False
	End Sub

	Private Sub deletar2()
		If MsgBox("Confirmar a ELIMINAÇÃO do Registro ?", vbCritical + vbYesNo) = vbYes Then

			Using con As SqlConnection = getconnectionSQL()
				Try
					Dim dr As SqlDataReader = Nothing

					con.Open()
					sql = "Delete from KM where data=@data and prefixo=@prefixo"

					Dim cmd As SqlCommand = New SqlCommand(sql, con)

					cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
					cmd.Parameters.AddWithValue("@prefixo", Trim(TextBox27.Text))
					dr = cmd.ExecuteReader()

					Call LOG("APG.KM" & Trim(TextBox27.Text), TextBox19.Text) ' LOG

				Catch ex As Exception
					MsgBox(ex.Message)
				Finally
					con.Close()
					'	Call Limpar_KM()
					MsgBox("Registro excluido com sucesso !")
					Call Menus()
					'GroupBox3.Visible = False
				End Try
			End Using
		End If

	End Sub

	Private Sub Limpar_KM()

		TextBox19.Text = ""
		TextBox18.Text = ""
		TextBox20.Text = ""
		TextBox27.Text = ""
		TextBox26.Text = ""
		TextBox21.Text = ""
		TextBox22.Text = ""
		TextBox23.Text = ""
		TextBox25.Text = ""
		TextBox24.Text = ""

	End Sub

	Private Sub Click_informacao_carro_km(sender, v_indice)

		CheckBox1.CheckState = 0
		Dim v_index As Integer = v_indice

		TextBox19.Text = data_Normal(TextBox4.Text)
		TextBox27.Text = sender.text

		Dim v_capacidade = v_carga(v_index)

		Dim y As Integer = 0

		'ACERTO DE DATA
		TextBox18.Text = Now.ToShortDateString
		TextBox20.Text = Now.ToLongTimeString

		While (v_prx(y) <> labelarray(v_index).Text) And (y <= v_onib)
			y = y + 1
		End While


		If (v_prx(y) = labelarray(v_index).Text) Then
			TextBox21.Text = Format(v_hdm(v_index), "###,###")
			TextBox18.Text = data_Normal(v_dt(v_index))
			TextBox20.Text = Convert.ToString(v_hr(v_index))

		End If


		TextBox21.Focus()

		Dim ontem As String = CDate(TextBox4.Text).AddDays(-1)
		Label157.Text = ontem

		Dim pfx As String = sender.text

		Dim x As Integer = 1

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim cmd As New SqlCommand("SELECT * FROM KM WHERE data=@data AND prefixo=@prefixo", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(ontem))
			cmd.Parameters.AddWithValue("@prefixo", pfx)

			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = cmd.ExecuteReader()

			If dr.Read() Then
				TextBox26.Text = Format(dr.Item("hodometro"), "###,###")
			Else
				TextBox26.Text = ""
			End If
			con.Close()
		End Using

		Using con As SqlConnection = getconnectionSQL()

			con.Open()

			Dim cmd As New SqlCommand("SELECT * FROM KM WHERE data=@data AND prefixo=@prefixo", con)
			cmd.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			cmd.Parameters.AddWithValue("@prefixo", pfx)

			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = cmd.ExecuteReader()

			If dr.Read() Then
				TextBox21.Text = Format(dr.Item("hodometro"), "###,###")
				TextBox22.Text = Trim(dr.Item("registro"))
				TextBox23.Text = Trim(dr.Item("HoraMot"))
				'TextBox25.Text = Trim(dr.Item("registro2"))
				'TextBox24.Text = Trim(dr.Item("HoraMot2"))
			Else
				TextBox21.Text = ""
				TextBox22.Text = ""
				TextBox23.Text = ""
				TextBox25.Text = ""
				TextBox24.Text = ""

			End If
			con.Close()
		End Using



	End Sub

	Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
		Call Relatorio_mapa_KM()
	End Sub

	Private Sub Relatorio_mapa_KM()

		Dim XcelApp As New Excel.Application

		XcelApp.Application.Workbooks.Add(Type.Missing)

		Dim labelarray(v_onib) As Label

		labelarray(0) = Label1
		labelarray(1) = Label2
		labelarray(2) = Label3
		labelarray(3) = Label4
		labelarray(4) = Label5
		labelarray(5) = Label6
		labelarray(6) = Label7
		labelarray(7) = Label8
		labelarray(8) = Label9
		labelarray(9) = Label10
		labelarray(10) = Label11
		labelarray(11) = Label12
		labelarray(12) = Label13
		labelarray(13) = Label14
		labelarray(14) = Label15
		labelarray(15) = Label16
		labelarray(16) = Label17
		labelarray(17) = Label18
		labelarray(18) = Label19
		labelarray(19) = Label20
		labelarray(20) = Label21
		labelarray(21) = Label22
		labelarray(22) = Label23
		labelarray(23) = Label24
		labelarray(24) = Label25
		labelarray(25) = Label26
		labelarray(26) = Label27
		labelarray(27) = Label28
		labelarray(28) = Label29
		labelarray(29) = Label30
		labelarray(30) = Label31
		labelarray(31) = Label32
		labelarray(32) = Label33
		labelarray(33) = Label34
		labelarray(34) = Label35
		labelarray(35) = Label36
		labelarray(36) = Label37
		labelarray(37) = Label38
		labelarray(38) = Label39
		labelarray(39) = Label40
		labelarray(40) = Label41
		labelarray(41) = Label42
		labelarray(42) = Label43
		labelarray(43) = Label44
		labelarray(44) = Label45
		labelarray(45) = Label46
		labelarray(46) = Label47
		labelarray(47) = Label48
		labelarray(48) = Label49
		labelarray(49) = Label50
		labelarray(50) = Label51
		labelarray(51) = Label52
		labelarray(52) = Label53
		labelarray(53) = Label54
		labelarray(54) = Label55
		labelarray(55) = Label56
		labelarray(56) = Label57
		labelarray(57) = Label58
		labelarray(58) = Label59
		labelarray(59) = Label60
		labelarray(60) = Label61
		labelarray(61) = Label62
		labelarray(62) = Label63
		labelarray(63) = Label64
		labelarray(64) = Label65
		labelarray(65) = Label66
		labelarray(66) = Label67
		labelarray(67) = Label68
		labelarray(68) = Label69
		labelarray(69) = Label70
		labelarray(70) = Label71
		labelarray(71) = Label72
		labelarray(72) = Label73
		labelarray(73) = Label74
		labelarray(74) = Label75
		labelarray(75) = Label76
		labelarray(76) = Label77
		labelarray(77) = Label78
		labelarray(78) = Label79
		labelarray(79) = Label80
		labelarray(80) = Label81
		labelarray(81) = Label82
		labelarray(82) = Label83
		labelarray(83) = Label84
		labelarray(84) = Label85
		labelarray(85) = Label86
		labelarray(86) = Label87
		labelarray(87) = Label88
		labelarray(88) = Label89
		labelarray(89) = Label90
		labelarray(90) = Label91
		labelarray(91) = Label92
		labelarray(92) = Label93
		labelarray(93) = Label94
		labelarray(94) = Label95
		labelarray(95) = Label96
		labelarray(96) = Label97
		labelarray(97) = Label98
		labelarray(98) = Label99
		labelarray(99) = Label100
		labelarray(100) = Label101
		labelarray(101) = Label102
		labelarray(102) = Label103
		labelarray(103) = Label104
		labelarray(104) = Label105
		labelarray(105) = Label106
		labelarray(106) = Label107
		labelarray(107) = Label108
		labelarray(108) = Label109
		labelarray(109) = Label110
		labelarray(110) = Label111
		labelarray(111) = Label112
		labelarray(112) = Label113
		labelarray(113) = Label114
		labelarray(114) = Label115
		labelarray(115) = Label116
		labelarray(116) = Label117
		labelarray(117) = Label118
		labelarray(118) = Label119
		labelarray(119) = Label120


		XcelApp.Cells(1, 1) = "DATA   "
		XcelApp.Cells(1, 2) = Now.ToShortDateString
		XcelApp.Cells(1, 3) = "EMPRESA  "
		XcelApp.Cells(1, 4) = Empresa

		XcelApp.Cells(2, 1) = "PREFIXO"
		XcelApp.Cells(2, 2) = "STATUS"
		XcelApp.Cells(2, 3) = "HORA"
		XcelApp.Cells(2, 4) = "KM"


		For i = 0 To v_onib - 1

			If Not IsNothing(labelarray(i)) Then

				If (labelarray(i).Visible = True) Then
					XcelApp.Cells(i + 3, 1) = labelarray(i).Text

					If (labelarray2(i).BackColor = Color.Firebrick) Then
						XcelApp.Cells(i + 3, 2) = "Falta"
					End If
					If (labelarray2(i).BackColor = Color.Green) Then
						XcelApp.Cells(i + 3, 2) = "OK"
						Dim Y As Integer = 0

						While (k_prx(Y) <> labelarray(i).Text) And (Y <= v_reg)
							Y = Y + 1
						End While

						If (k_prx(Y) = labelarray(i).Text) Then
							XcelApp.Cells(i + 3, 3) = Convert.ToString(k_hr(Y))
							XcelApp.Cells(i + 3, 4) = k_hdm(Y)
						End If
					End If
					If labelarray2(i).BackColor = Color.DarkGray Then XcelApp.Cells(i + 3, 2) = "MANUTENÇÃO"
				End If

			End If
		Next

		XcelApp.Columns.AutoFit()
		'
		XcelApp.Visible = True

	End Sub

	Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
		Relatorio_prefeitura.Show()
	End Sub

	'############################ ANALIS DE LEITURA DO KM ( LISTAANTERIOR ) ####################################'

	Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
		GroupBox15.Visible = True
		GroupBox15.Location = New Point(250, 114)

		Call Lista_anterior()
	End Sub

	Private Sub Lista_anterior()

		If TextBox4.Text <> "" Then

			For x = 0 To 300
				Aprefixo(x) = 0
				AKM(x) = 0
			Next
			'	Try
			Using con As SqlConnection = getconnectionSQL()
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
				Dgbgrid2.Columns.Add("hora", "Hora")
				Dgbgrid2.Columns.Add("km", "KM")
				Dgbgrid2.Columns.Add("hod.erro", "Hod. Erro")
				Dgbgrid2.Columns.Add("ant.km", "Ant. KM")
				Dgbgrid2.Columns.Add("dif.km", "Dif. KM")
				Dgbgrid2.Columns(0).Width = 70
				Dgbgrid2.Columns(1).Width = 100
				Dgbgrid2.Columns(2).Width = 90
				Dgbgrid2.Columns(3).Width = 70
				Dgbgrid2.Columns(4).Width = 70
				Dgbgrid2.Columns(5).Width = 70
				Dgbgrid2.Columns(6).Width = 70
				Dgbgrid2.DefaultCellStyle.SelectionBackColor = Color.White
				Dgbgrid2.DefaultCellStyle.SelectionForeColor = Color.Black
				Dgbgrid2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
				Dgbgrid2.RowsDefaultCellStyle.BackColor = Color.LightGray
				Dgbgrid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
				Dgbgrid2.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
				Dgbgrid2.RowHeadersDefaultCellStyle.BackColor = Color.Black

				con.Open()
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@data AND empresa=@empresa order by prefixo", con)
				command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()
				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					Dgbgrid2.Rows.Add("")
					Dgbgrid2.Rows(i).Cells(0).Value = dr.Item("prefixo")
					Dgbgrid2.Rows(i).Cells(1).Value = data_Normal(dr.Item("Data_abast"))
					Dgbgrid2.Rows(i).Cells(2).Value = Convert.ToString(dr.Item("hora"))
					Dgbgrid2.Rows(i).Cells(3).Value = Format(dr.Item("hodometro"), "###,###")
					If dr.Item("st_hodo") <> "" Then Dgbgrid2.Rows(i).Cells(4).Value = dr.Item("st_hodo")
					i = i + 1
				End While
				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'Finally
				con.Close()

				'End Try

			End Using


			'Try
			Using con As SqlConnection = getconnectionSQL()
				con.Open()
				Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem AND empresa=@empresa", con)
				command.Parameters.AddWithValue("@ontem", data_SQL(CDate(TextBox4.Text).AddDays(-1)))
				command.Parameters.AddWithValue("@empresa", Empresa)
				Dim dr As SqlDataReader '= command.ExecuteReader()

				dr = command.ExecuteReader()
				Dim i As Integer = 0
				While dr.Read()
					Aprefixo(i) = dr.Item("prefixo")
					AKM(i) = Format(dr.Item("hodometro"), "###,###")
					i = i + 1
				End While

				Dim v_erros As Integer = 0
				Dim v_zero As Integer = 0
				Dim v_defeitos As Integer = 0

				For y = 0 To Dgbgrid2.RowCount - 1

					Dim x As Integer = 0

					While (x < 300) And (Dgbgrid2.Rows(y).Cells(0).Value <> Aprefixo(x))
						x = x + 1
					End While

					If (Dgbgrid2.Rows(y).Cells(0).Value = Aprefixo(x)) Then
						Dgbgrid2.Rows(y).Cells(5).Value = AKM(x)


						If AKM(x) <> 0 Then Dgbgrid2.Rows(y).Cells(6).Value = CDbl(Dgbgrid2.Rows(y).Cells(3).Value) - CDbl(AKM(x))
						If (Convert.ToString(Dgbgrid2.Rows(y).Cells(6).Value) <> "") Then
							If (Dgbgrid2.Rows(y).Cells(6).Value < 0) Or (Dgbgrid2.Rows(y).Cells(6).Value > 1200) Then
								Dgbgrid2.Rows(y).Cells(6).Style.BackColor = Color.Orange
								v_erros = v_erros + 1
							End If
						End If
					End If

					If (Convert.ToString(Dgbgrid2.Rows(y).Cells(6).Value) <> "") Then
						If (Dgbgrid2.Rows(y).Cells(6).Value >= 0) And (Dgbgrid2.Rows(y).Cells(6).Value <= 5) Then
							Dgbgrid2.Rows(y).Cells(6).Style.BackColor = Color.Yellow
							Dgbgrid2.Rows(y).Cells(6).Style.ForeColor = Color.Black
							v_zero = v_zero + 1
						End If
					End If

					If (Convert.ToString(Dgbgrid2.Rows(y).Cells(4).Value) <> "") Then
						If (Dgbgrid2.Rows(y).Cells(4).Value = 1) Then
							Dgbgrid2.Rows(y).Cells(4).Style.BackColor = Color.MidnightBlue
							v_defeitos = v_defeitos + 1
						End If
					End If

				Next

				TextBox28.Text = v_erros
				TextBox29.Text = v_zero
				TextBox30.Text = v_defeitos
				TextBox31.Text = v_erros + v_zero

				If (lbregistrado.Text <> "") And (lbregistrado.Text <> "0") Then TextBox32.Text = ((v_erros + v_zero) / CDbl(lbregistrado.Text)) * 100

				'Catch ex As Exception
				'	MsgBox(ex.Message)
				'End Try

			End Using
		Else
			MsgBox("Data INVÁLIDA")
		End If
	End Sub

	Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
		GroupBox15.Visible = False
	End Sub

	Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
		Call Relatorio_excel_ANALISE_LEITURADOKM()
	End Sub


	Private Sub Relatorio_excel_ANALISE_LEITURADOKM()

		Dim XcelApp As New Excel.Application

		If Dgbgrid2.Rows.Count > 0 Then
			'			Try
			XcelApp.Application.Workbooks.Add(Type.Missing)
			For i As Integer = 1 To Dgbgrid2.Columns.Count
				XcelApp.Cells(1, i) = Dgbgrid2.Columns(i - 1).HeaderText
			Next

			Dim x As Integer = 0
			Dim y As Integer = 0
			While x < Dgbgrid2.Rows.Count

				For j As Integer = 0 To Dgbgrid2.Columns.Count - 1
					On Error Resume Next
					XcelApp.Cells(y + 2, j + 1) = Dgbgrid2.Rows(x).Cells(j).Value.ToString()
				Next
				y = y + 1
				x = x + 1
			End While
			XcelApp.Cells(y + 4, 2) = "ERRO"
			XcelApp.Cells(y + 4, 3) = "ZERO"
			XcelApp.Cells(y + 4, 4) = "DEFEITO"
			XcelApp.Cells(y + 4, 5) = "TOTAL"
			XcelApp.Cells(y + 4, 6) = "%PROBLEMAS"

			XcelApp.Cells(y + 5, 1) = "HODOMETRO COM"
			XcelApp.Cells(y + 5, 2) = TextBox28.Text
			XcelApp.Cells(y + 5, 3) = TextBox29.Text
			XcelApp.Cells(y + 5, 4) = TextBox30.Text
			XcelApp.Cells(y + 5, 5) = TextBox31.Text
			XcelApp.Cells(y + 5, 6) = TextBox32.Text


			'
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub

	'############################ LISTA MESTRA ( LISTAATUAL ) ####################################'

	Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
		GroupBox16.Visible = True
		GroupBox16.Location = New Point(289, 134)
		Call Lista_atual()
	End Sub

	Private Sub Lista_atual()

		Using con As SqlConnection = getconnectionSQL()

			Dgbgrid3.Rows.Clear()
			Dgbgrid3.Columns.Clear()

			Dgbgrid3.AllowUserToAddRows = False
			Dgbgrid3.AllowUserToDeleteRows = False
			Dgbgrid3.EditMode = DataGridViewEditMode.EditProgrammatically
			Dgbgrid3.MultiSelect = False
			Dgbgrid3.SelectionMode = DataGridViewSelectionMode.FullRowSelect
			Dgbgrid3.AllowUserToOrderColumns = False
			Dgbgrid3.AllowUserToResizeColumns = False

			Dgbgrid3.Columns.Add("prefixo", "Prefixo")
			Dgbgrid3.Columns.Add("km", "KM")

			Dgbgrid3.Columns(0).Width = 80
			Dgbgrid3.Columns(1).Width = 80

			Dgbgrid3.DefaultCellStyle.SelectionBackColor = Color.White
			Dgbgrid3.DefaultCellStyle.SelectionForeColor = Color.Black
			Dgbgrid3.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
			Dgbgrid3.RowsDefaultCellStyle.BackColor = Color.LightGray
			Dgbgrid3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
			Dgbgrid3.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
			Dgbgrid3.RowHeadersDefaultCellStyle.BackColor = Color.Black
			'	Try
			con.Open()
			Dim command As New SqlCommand("SELECT * FROM Capac_onibus WHERE empresa=@empresa ORDER BY prefixo", con)
			command.Parameters.AddWithValue("@data", data_SQL(TextBox4.Text))
			command.Parameters.AddWithValue("@empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim i As Integer = 0
			While dr.Read()
				If Trim(dr.Item("prefixo")) <> "" Then
					Dgbgrid3.Rows.Add("")
					Dgbgrid3.Rows(i).Cells(0).Value = dr.Item("prefixo")
					i = i + 1
				End If
			End While
			'Catch ex As Exception
			'	MsgBox(ex.Message)
			'Finally
			con.Close()

			'End Try
		End Using

		Using con As SqlConnection = getconnectionSQL()

			'	Try

			For x = 0 To 300
				Aprefixo(x) = 0
				AKM(x) = 0
			Next

			con.Open()
			Dim command As New SqlCommand("SELECT * FROM KM WHERE data=@ontem AND empresa=@empresa ORDER BY prefixo", con)
			command.Parameters.AddWithValue("@ontem", data_SQL(CDate(TextBox4.Text).AddDays(-1).ToShortDateString))
			command.Parameters.AddWithValue("empresa", Empresa)
			Dim dr As SqlDataReader '= command.ExecuteReader()
			dr = command.ExecuteReader()
			Dim i As Integer = 0
			While dr.Read()
				Aprefixo(i) = dr.Item("prefixo")
				AKM(i) = Format(dr.Item("hodometro"), "###,###")
				i = i + 1
			End While
			'Catch ex As Exception
			'	MsgBox(ex.Message)
			'Finally
			con.Close()

			'End Try

			For i = 0 To Dgbgrid3.RowCount - 1
				Dim x As Integer = 0
				While (x < 300) And (Dgbgrid3.Rows(i).Cells(0).Value <> Aprefixo(x))
					x = x + 1
				End While
				If (Dgbgrid3.Rows(i).Cells(0).Value = Aprefixo(x)) Then Dgbgrid3.Rows(i).Cells(1).Value = Format(AKM(x), "###,###")
			Next
		End Using

	End Sub

	Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
		Call Relatorio_excel_LISTAMETRA()
	End Sub

	Private Sub Relatorio_excel_LISTAMETRA()

		Dim XcelApp As New Excel.Application

		If Dgbgrid3.Rows.Count > 0 Then
			'			Try
			XcelApp.Application.Workbooks.Add(Type.Missing)
			For i As Integer = 1 To Dgbgrid3.Columns.Count
				XcelApp.Cells(1, i) = Dgbgrid3.Columns(i - 1).HeaderText
			Next

			Dim x As Integer = 0
			Dim y As Integer = 0
			While x < Dgbgrid3.Rows.Count

				For j As Integer = 0 To Dgbgrid3.Columns.Count - 1
					On Error Resume Next
					XcelApp.Cells(y + 2, j + 1) = Dgbgrid3.Rows(x).Cells(j).Value.ToString()
				Next
				y = y + 1
				x = x + 1
			End While
			'
			XcelApp.Columns.AutoFit()
			'
			XcelApp.Visible = True
		End If

	End Sub

	Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
		GroupBox16.Visible = False
	End Sub

	Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
		Relatório_de_Ponto.Show()
	End Sub

	Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
		Relatorio_hodometro.Show()
	End Sub

	Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
		Rel_catraca.Show()
	End Sub

	Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

	End Sub

	Private Sub TextBox12_LostFocus(sender As Object, e As EventArgs) Handles TextBox12.LostFocus
		If TextBox12.Text <> "" Then
			Select Case Trim(TextBox12.Text)
				Case v_bomba

				Case v_bomba1

				Case Else
					'	MsgBox("A BOMBA " & TextBox12.Text & " NÃO ESTA ASSOCIADA A EMPRESA " & Empresa & " !", vbCritical, "ATENÇÃO")
			End Select
		End If
	End Sub

	Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

	End Sub

	Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
		verificacao.Show()
	End Sub

	Private Sub Mapa_Closed(sender As Object, e As EventArgs) Handles Me.Closed
		Call LOG("FECHOU " & TimeOfDay, TextBox4.Text)
	End Sub

	Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

	End Sub

	Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

	End Sub

	Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

	End Sub

	Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

	End Sub
End Class