Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.Mime



Module kmdlAcesso

	Public Toolstrip1 As String
	Public Toolstrip2 As String


	Public Data1 As Date
	Public butt(200) As Button
	'Public Empresa As String = "TL"
	Public Desc_Empresa As String = "TRANSLITORAL"
	Public Local_excel As String
	'Public data_Sql
	'	Public v_empresa As String = "VB"
	Public v_administrador As Boolean
	Public v_usuario As String

	Public data_Select As String

	Public v_codEmp(300) As String
	Public v_prefixo(300) As String
	Public v_bombaArray(300) As Integer

	Public v_empresa
	Public v_filial
	Public v_tanque
	Public v_bomba
	Public v_bomba1


	Public Empresa
	Public NumEmpresa


	' Dim butt() As Button = New Button()


	Public Function getconnectionSQL() As SqlConnection
		Dim ligaçao As String
		Dim v_server = "servidor"
		Dim v_dbase = "Abastecimento"
		Dim v_uid = "user"
		Dim v_pwd = "senha"

		ligaçao = $"Data Source={v_server};Initial Catalog={ v_dbase };Persist Security Info=True;User ID={v_uid};password={ v_pwd }"

		Return New SqlConnection(ligaçao)


	End Function

	'Public Function data_Normal(X)
	'    If X <> "" Then data_Normal = Format(CDate(X), "dd/mm/yyyy")
	'End Function

	'Public Sub data_SQL(X_data As Date)
	'    Dim v_ano, v_mes, v_dia

	'    If X_data <> "" Then

	'        v_ano = X_data.Year
	'        v_mes = X_data.Month
	'        v_dia = X_data.Day
	'        data_p_Sql = v_ano & "-" & v_mes & "-" & v_dia
	'    Else
	'        data_p_Sql = ""
	'    End If
	'End Sub


	Public Sub ConexaoRelatorios()
		'Local_excel = "\\hercules.dominio\arquivos\Executaveis\Plantão\Relatorios\"
		Local_excel = "\\192.168.1.20\arquivos\Executaveis\Plantão\Relatorios\"

	End Sub


	Public Function data_Normal(X)
		data_Normal = Format(CDate(X), "dd/MM/yyyy")
	End Function
	Public Function data_SQL(X)
		data_SQL = Format(CDate(X), "yyyy/MM/dd")
	End Function

	'ENVIO OUTLOOK
	Public Sub Envio_email(tipo, destino, assunto, mensagem)
		On Error Resume Next

		Dim OutlookApp As Object
		Dim OutlookMail As Object

		OutlookApp = CreateObject("Outlook.Application")
		OutlookMail = OutlookApp.CreateItem(0)

		Select Case tipo
			Case "sistema"
				With OutlookMail
					.To = destino
					.CC = ""
					.BCC = "rasilva@translitoral.com.br"
					.Subject = assunto
					.Body = mensagem
					'.send ' Envio de email automatico
					.Display ' para mostrar o email sem envio
				End With
		End Select

		OutlookMail = Nothing
		OutlookApp = Nothing
	End Sub

	''ENVIO GMAIL
	'Public Function EnviaEmailJa(destino, assunto, mensagem) As Boolean
	'	Dim oMail As New MailMessage
	'	oMail.From = New MailAddress("seu email")
	'	oMail.To.Add(New MailAddress(destino))
	'	oMail.IsBodyHtml = False
	'	oMail.Subject = "teste envio mail"
	'	oMail.Body = "teste de envio usando SMTP"


	'	Dim oSMTP As New SmtpClient("servidor smtp", porta)
	'	oSMTP.Credentials = New System.Net.NetworkCredential("seu nome", "########")
	'	oSMTP.Send(oMail)
	'	Return True

	'End Function


	Dim path As String
	Dim mail As New MailMessage()

	Public Sub Email(texto)

		Dim aplicacaonome As String = Application.ProductName

		Dim SmtpServer As New SmtpClient()
		SmtpServer.Credentials = New Net.NetworkCredential("vbnetvb6vba@gmail.com",
								"b0b3sp0nja")
		SmtpServer.Port = 587
		SmtpServer.Host = "smtp.gmail.com"
		SmtpServer.EnableSsl = True
		mail = New MailMessage()
		'Dim addr() As String = TextBox1.Text.Split(",")
		Try
			mail.From = New MailAddress("vbnetvb6vba@gmail.com", aplicacaonome,
						System.Text.Encoding.UTF8)

			mail.To.Add("rasilva@translitoral.com.br")
			mail.Subject = aplicacaonome
			'mail.Body = TextBox4.Text
			'If ListBox1.Items.Count <> 0 Then
			'	For i = 0 To ListBox1.Items.Count - 1
			'		mail.Attachments.Add(New Attachment(ListBox1.Items.Item(i)))
			'	Next
			'End If
			'	Dim logo As New LinkedResource(path)
			'		logo.ContentId = "Logo"
			Dim htmlview As String
			htmlview = "<html><body><table border=2><tr width=100%><td>
        <img src=https://static.wixstatic.com/media/5bb3df_92c6421c057b46969d39dc618fb5d26c~mv2.png/v1/fill/w_674,h_270,al_c/5bb3df_92c6421c057b46969d39dc618fb5d26c~mv2.png height='42' width='42' alt=companyname /></td>
        <td>Rodrigo Mercê ©  2020</td></tr></table>
                        <hr/></body></html>"
			Dim alternateView1 As AlternateView =
		AlternateView.CreateAlternateViewFromString(htmlview +
			  texto, Nothing, MediaTypeNames.Text.Html)
			'	alternateView1.LinkedResources.Add(logo)
			mail.AlternateViews.Add(alternateView1)
			mail.IsBodyHtml = True
			mail.DeliveryNotificationOptions =
				DeliveryNotificationOptions.OnFailure
			mail.ReplyTo = New MailAddress("rodrigomeerce@gmail.com")
			SmtpServer.Send(mail)
		Catch ex As Exception
			MsgBox(ex.ToString())
		End Try

	End Sub

	Public Sub LOG(acao, dataselect)
		'Dim IP As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName) ' OBTEM I IP DA MAQUINA
		'Dim ipmaquina As String = IP.AddressList.GetValue(3).ToString

		Dim nomemaquina As String = System.Environment.UserName() ' OBTEM O NOME DO USUARIO

		Dim d As String = TimeOfDay ' HORA ATUAL
		Dim hora As String = ""
		hora = d.Substring(0, 5)

		Dim data As Date = Now.ToShortDateString ' DATA ATUAL


		Using con As SqlConnection = getconnectionSQL()

			Try
				con.Open()
				Dim cmd3 As SqlCommand
				Dim Sql As String = ""
				Try

					Sql = "INSERT INTO LOG_abastecimento (maquina, data, hora, acao, empresa, dataselect) VALUES (@maquina, @data, @hora, @acao, @empresa, @dataselect)"
					cmd3 = New SqlCommand(Sql, con)
					cmd3.Parameters.AddWithValue("@maquina", nomemaquina)
					cmd3.Parameters.AddWithValue("@data", data_SQL(data))
					cmd3.Parameters.AddWithValue("@hora", hora)
					cmd3.Parameters.AddWithValue("@acao", Trim(acao))
					cmd3.Parameters.AddWithValue("@empresa", Empresa)
					'cmd3.Parameters.AddWithValue("@ip", ipmaquina)
					cmd3.Parameters.AddWithValue("@dataselect", data_SQL(dataselect))

					cmd3.ExecuteNonQuery()

				Catch ex As Exception
					MsgBox(ex.Message)
				End Try
			Catch ex As Exception

			End Try
		End Using

	End Sub

	Public Sub Verifica_Dados_Duplicados()


	End Sub

	Public Sub Griid(dgrid)

		dgrid.AllowUserToAddRows = False
		dgrid.AllowUserToDeleteRows = False
		dgrid.EditMode = DataGridViewEditMode.EditProgrammatically
		dgrid.MultiSelect = False
		dgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
		dgrid.AllowUserToOrderColumns = False
		dgrid.AllowUserToResizeColumns = False


		dgrid.DefaultCellStyle.SelectionBackColor = Color.LightGray
		dgrid.DefaultCellStyle.SelectionForeColor = Color.Black
		dgrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
		dgrid.RowsDefaultCellStyle.BackColor = Color.White
		dgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
		dgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.White
		dgrid.RowHeadersDefaultCellStyle.BackColor = Color.Black

		dgrid.EnableHeadersVisualStyles = False

		dgrid.ColumnHeadersHeightSizeMode = False
		dgrid.EnableHeadersVisualStyles = False
		dgrid.ColumnHeadersHeight = 30

		dgrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

		dgrid.Columns(0).HeaderCell.Style.BackColor = Color.White
		dgrid.Columns(0).HeaderCell.Style.ForeColor = Color.Black

		dgrid.DefaultCellStyle.Font = New Font("Calibri", 7)
		dgrid.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 7, FontStyle.Bold)

		dgrid.RowHeadersVisible = False

	End Sub


End Module
