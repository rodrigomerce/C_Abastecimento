Imports System.Data.SqlClient
Public Class FrmPrincipal
    Dim v_Pref(200) As String
    Dim v_st(200) As String
    Dim SQL As String
    Dim v_atualização As Integer
    Dim v_barra As Integer = 90  ' cada 30 = 5 minutos
    Dim V_Frota_Ativa As Integer
    Dim V_frota_Reserva As Integer
    Dim V_Max_manut As Integer

    Dim ttA As Integer
    Dim ttB As Integer
    Dim ttC As Integer
    Dim ttD As Integer
    Dim ttE As Integer
    Dim ttF As Integer
    Dim ttG As Integer
    Dim V_total As Integer



    Private Sub FrotaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrotaToolStripMenuItem.Click
        FrmConfiguracao.ShowDialog()

    End Sub
    Private Sub FrotaOperacionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrotaOperacionalToolStripMenuItem.Click
        FrmFrota.ShowDialog()
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        End
    End Sub
    'Private Sub InicioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InicioToolStripMenuItem.Click
    '    Dim V_RESP
    '    Dim V_TT_MAN


    '    V_RESP = MsgBox("DESEJA INICIALIZAR A SOLTURA DA FROTA", vbCritical + vbYesNo, "ALERTA")

    '    If V_RESP = vbYes Then
    '        Using con As SqlConnection = getconnectionSQL()

    '            Dim sql As String = ""
    '            Try

    '                con.Open()

    '                sql = "Update carros set Status='G' where status<>'D' "
    '                Dim cmd As SqlCommand = New SqlCommand(sql, con)
    '                cmd.ExecuteNonQuery()
    '            Catch ex As Exception
    '                MsgBox(ex.Message)

    '            Finally
    '                con.Close()
    '            End Try

    '        End Using

    '        Atualiza_botoes()
    '    Else
    '        MsgBox(" Operação Cancelada", vbInformation, "ALERTA")
    '    End If

    '    V_TT_MAN = ttC + ttD
    '    Grava_max_manut(V_TT_MAN)
    '    V_Max_manut = V_TT_MAN
    'End Sub
    Private Sub FrmPrincipal_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        '   Private Sub FrmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        butt = New Button() {Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button10,
    Button11, Button12, Button13, Button14, Button15, Button16, Button17, Button18, Button19, Button20,
    Button21, Button22, Button23, Button24, Button25, Button26, Button27, Button28, Button29, Button30,
    Button31, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40,
    Button41, Button42, Button43, Button44, Button45, Button46, Button47, Button48, Button49, Button50,
    Button51, Button52, Button53, Button54, Button55, Button56, Button57, Button58, Button59, Button60,
    Button61, Button62, Button63, Button64, Button65, Button66, Button67, Button68, Button69, Button70,
    Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80,
    Button81, Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button90,
    Button91, Button92, Button93, Button94, Button95, Button96, Button97, Button98, Button99, Button100,
    Button101, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110,
    Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120,
    Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129, Button130,
    Button131, Button132, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140,
    Button141, Button142, Button143, Button144, Button145, Button146, Button147, Button148, Button149, Button150,
    Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160,
    Button161, Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170,
    Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button178, Button179, Button180,
    Button181, Button182, Button183, Button184, Button185, Button186, Button187, Button188, Button189, Button190,
    Button191, Button192, Button193, Button194, Button195, Button196, Button197, Button198, Button199, Button200}

        Label7.BackColor = BttOper.BackColor
        Label7.ForeColor = BttOper.ForeColor
        Label7.Text = "F"
        Label17.Text = "CARRO EM OPERAÇÂO"

        Frota()

        Atualiza_botoes()
        v_atualização = 0
        ProgressBar1.Maximum = v_barra + 1
        Timer1.Enabled = True
    End Sub
    Private Sub calculos()
        ttA = 0
        ttB = 0
        ttC = 0
        ttD = 0
        ttE = 0
        ttF = 0
        ttG = 0
        V_total = 0

        ' atualizar_dados()

        For i As Integer = 0 To 199
            'Calcula os totais de cada modalidade
            If v_Pref(i) <> "" Then
                Select Case v_st(i)
                    Case "A"
                        ttA = ttA + 1
                    Case "B"
                        ttB = ttB + 1
                    Case "C"
                        ttC = ttC + 1
                    Case "D"
                        ttD = ttD + 1
                    Case "E"
                        ttE = ttE + 1
                    Case "F"
                        ttF = ttF + 1
                    Case "G"
                        ttG = ttG + 1
                End Select
            End If

        Next


        Label8.Text = ttA
        Label9.Text = ttB
        Label10.Text = ttC
        Label11.Text = ttD
        Label12.Text = ttE
        Label13.Text = ttF
        Label1.Text = ttG

        V_total = ttA + ttB + ttC + ttD + ttE + ttF + ttG
        Label3.Text = Int(((ttC + ttD) / V_total) * 100)
        Label5.Text = Int(((ttE + ttF) / V_Frota_Ativa) * 100)
        Label6.Text = Int((ttG / (V_total - ttC - ttD) * 100))
        Label21.Text = Int(ttC + ttD)

        If (ttC + ttD) > V_Max_manut Then
            Grava_max_manut(ttC + ttD)
        End If


    End Sub
    Private Sub Atualiza_botoes()

        atualizar_dados()

        For i As Integer = 0 To 199
            atualizar_nome(butt(i), v_Pref(i))
            atualizar_cor(butt(i), v_st(i))
        Next
        calculos()

    End Sub

    Private Sub Frota()
        Dim dr As SqlDataReader = Nothing


        Using con As SqlConnection = getconnectionSQL()

            SQL = ""
            Try

                con.Open()

                SQL = "Select * from Configuracao where Empresa='" & Empresa & "'"
                Dim cmd As SqlCommand = New SqlCommand(SQL, con)
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    V_Frota_Ativa = CInt(dr.Item("Ativa"))
                    V_frota_Reserva = CInt(dr.Item("Reserva"))
                    V_Max_manut = CInt(dr.Item("Max_manutencao"))
                    Label18.Text = V_Max_manut

                End If
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using

    End Sub

    Private Sub Grava_max_manut(Qt_carros As Integer)
        Using con As SqlConnection = getconnectionSQL()

            Dim sql As String = ""
            Try

                con.Open()

                sql = "Update Configuracao set "
                sql += "Max_Manutencao=" & Qt_carros & " where Empresa='" & Empresa & "'"
                ' sql = "Update cliente set nome='" & txtNome.Text & "' where codigo=" & txtnum.Text
                Dim cmd As SqlCommand = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                V_Max_manut = Qt_carros
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using
        Label18.Text = Qt_carros

    End Sub
    Private Sub atualizar_dados()
        Dim dr As SqlDataReader = Nothing
        Dim k As Int16
        Dim y As Integer

        For y = 0 To 199
            v_Pref(y) = ""
            v_st(y) = ""
        Next

        Using con As SqlConnection = getconnectionSQL()

            SQL = ""
            Try

                con.Open()

                SQL = "Select prefixo,status from carros" & Empresa & " order by prefixo"
                Dim cmd As SqlCommand = New SqlCommand(SQL, con)
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    k = 0
                    While dr.Read()
                        v_Pref(k) = dr.Item("prefixo")
                        v_st(k) = dr.Item("status")
                        k = k + 1
                    End While
                End If
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using

    End Sub
    Private Sub bttInicio_Click(sender As Object, e As EventArgs) Handles bttInicio.Click
        Dim V_RESP
        Dim V_TT_MAN


        V_RESP = MsgBox("DESEJA INICIALIZAR A SOLTURA DA FROTA", vbCritical + vbYesNo, "ALERTA")

        If V_RESP = vbYes Then
            Timer1.Enabled = False

            Registro_Disponibilidade()


            Using con As SqlConnection = getconnectionSQL()

                Dim sql As String = ""
                Try

                    con.Open()

                    sql = "Update carros" & Empresa & " set Status='H' where status<>'D' "
                    Dim cmd As SqlCommand = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)

                Finally
                    con.Close()
                End Try

            End Using

            Atualiza_botoes()
        Else
            MsgBox(" Operação Cancelada", vbInformation, "ALERTA")
        End If

        V_TT_MAN = ttC + ttD
        Grava_max_manut(V_TT_MAN)
        V_Max_manut = V_TT_MAN

        Timer1.Enabled = True

    End Sub
    Private Sub Registro_Disponibilidade()
        Dim v_dt As DateTime
        Dim v_result As String
        Dim cmd As SqlCommand



        Using con As SqlConnection = getconnectionSQL()

            SQL = ""
            Try

                con.Open()
                v_dt = Date.Now ' data de hoje
                v_dt = v_dt.AddDays(-1) ' Retira um dia
                'data_SQL(v_dt)
                v_result = CStr(v_dt.Year) & "-" & CStr(v_dt.Month) & "-" & CStr(v_dt.Day)
                SQL = "INSERT INTO Disponibilidade" & Empresa & " (datas,Manutencao,Frota) VALUES  ('" & v_result & "'," & V_Max_manut & "," & V_Frota_Ativa & ")"

                cmd = New SqlCommand(SQL, con)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using


    End Sub
    Private Sub atualizar_nome(btt As Button, v_nome As String)
        btt.Visible = False
        If v_nome <> "" Then
            btt.Text = v_nome
            btt.Visible = True
        End If

    End Sub
    Private Sub atualizar_cor(btt As Button, cor As String)

        Select Case cor
            Case "A"
                btt.BackColor = Color.LightGray
                btt.ForeColor = Color.Black
            Case "B"
                btt.BackColor = Color.Lime
                btt.ForeColor = Color.Black
            Case "C"
                btt.BackColor = Color.Yellow
                btt.ForeColor = Color.Black
            Case "D"
                btt.BackColor = Color.Red
                btt.ForeColor = Color.Yellow
            Case "E"
                btt.BackColor = Color.Aqua
                btt.ForeColor = Color.Black
            Case "F"
                btt.BackColor = Color.Blue
                btt.ForeColor = Color.Yellow
            Case "G"
                btt.BackColor = Color.BlueViolet
                btt.ForeColor = Color.Yellow
            Case "H"
                btt.BackColor = Color.DimGray
                btt.ForeColor = Color.Yellow
        End Select


    End Sub



    Private Sub BttEscala_Click(sender As Object, e As EventArgs) Handles BttEscala.Click
        Label7.BackColor = BttEscala.BackColor
        Label7.ForeColor = BttEscala.ForeColor
        Label7.Text = "A"
        Label17.Text = "CARRO ESCALADO"

    End Sub
    Private Sub BttDisponivel_Click(sender As Object, e As EventArgs) Handles BttDisponivel.Click
        Label7.BackColor = BttDisponivel.BackColor
        Label7.ForeColor = BttDisponivel.ForeColor
        Label7.Text = "B"
        Label17.Text = "CARRO DISPONÍVEL - Reserva"

    End Sub

    Private Sub BttManRap_Click(sender As Object, e As EventArgs) Handles BttManRap.Click
        Label7.BackColor = BttManRap.BackColor
        Label7.ForeColor = BttManRap.ForeColor
        Label7.Text = "C"
        Label17.Text = "CARRO EM MANUTENÇÃO - Para reparos Simples ou Revisão"

    End Sub

    Private Sub Bttretido_Click(sender As Object, e As EventArgs) Handles Bttretido.Click
        Label7.BackColor = Bttretido.BackColor
        Label7.ForeColor = Bttretido.ForeColor
        Label7.Text = "D"
        Label17.Text = "CARRO EM MANUTENÇÃO - Retido por mais de 24 Horas"

    End Sub

    Private Sub BttOperRes_Click(sender As Object, e As EventArgs) Handles BttOperRes.Click
        Label7.BackColor = BttOperRes.BackColor
        Label7.ForeColor = BttOperRes.ForeColor
        Label7.Text = "E"
        Label17.Text = "CARRO EM OPERAÇÃO COM RESTRIÇÔES"

    End Sub

    Private Sub BttOper_Click(sender As Object, e As EventArgs) Handles BttOper.Click
        Label7.BackColor = BttOper.BackColor
        Label7.ForeColor = BttOper.ForeColor
        Label7.Text = "F"
        Label17.Text = "CARRO EM OPERAÇÂO"

    End Sub
    Private Sub BttTermino_Click(sender As Object, e As EventArgs) Handles BttTermino.Click
        Label7.BackColor = BttTermino.BackColor
        Label7.ForeColor = BttTermino.ForeColor
        Label7.Text = "G"
        Label17.Text = "TERMINO DA OPERAÇÃO"

    End Sub
    Private Sub Gravar(btt As Button, cor As String)
        Using con As SqlConnection = getconnectionSQL()

            Dim sql As String = ""
            Try

                con.Open()

                sql = "Update carros" & Empresa & " Set Status='" & cor & "' where prefixo = " & btt.Text
                Dim cmd As SqlCommand = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using



    End Sub
    Private Sub Botao(bt As Button, nom As String)
        Dim y As Integer
        Gravar(bt, nom)
        atualizar_cor(bt, Label7.Text)

        y = 0
        While (v_Pref(y) <> bt.Text) And (y < 200)
            y += 1
        End While
        If (v_Pref(y) = bt.Text) Then
            v_st(y) = Label7.Text
        End If

        calculos()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Botao(Button1, Label7.Text)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Botao(Button2, Label7.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Botao(Button3, Label7.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Botao(Button4, Label7.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Botao(Button5, Label7.Text)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Botao(Button6, Label7.Text)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Botao(Button7, Label7.Text)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Botao(Button8, Label7.Text)
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Botao(Button9, Label7.Text)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Botao(Button10, Label7.Text)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Botao(Button11, Label7.Text)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Botao(Button12, Label7.Text)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Botao(Button13, Label7.Text)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Botao(Button14, Label7.Text)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Botao(Button15, Label7.Text)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Botao(Button16, Label7.Text)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Botao(Button17, Label7.Text)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Botao(Button18, Label7.Text)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Botao(Button19, Label7.Text)
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Botao(Button20, Label7.Text)
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Botao(Button21, Label7.Text)
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Botao(Button22, Label7.Text)
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Botao(Button23, Label7.Text)
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Botao(Button24, Label7.Text)
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Botao(Button25, Label7.Text)
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Botao(Button26, Label7.Text)
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Botao(Button27, Label7.Text)
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Botao(Button28, Label7.Text)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Botao(Button29, Label7.Text)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Botao(Button30, Label7.Text)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Botao(Button31, Label7.Text)
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Botao(Button32, Label7.Text)
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Botao(Button33, Label7.Text)
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Botao(Button34, Label7.Text)
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Botao(Button35, Label7.Text)
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Botao(Button36, Label7.Text)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Botao(Button37, Label7.Text)
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        Botao(Button38, Label7.Text)
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Botao(Button39, Label7.Text)
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        Botao(Button40, Label7.Text)
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        Botao(Button41, Label7.Text)
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Botao(Button42, Label7.Text)
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        Botao(Button43, Label7.Text)
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Botao(Button44, Label7.Text)
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Botao(Button45, Label7.Text)
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Botao(Button46, Label7.Text)
    End Sub

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Botao(Button47, Label7.Text)
    End Sub

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        Botao(Button48, Label7.Text)
    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        Botao(Button49, Label7.Text)
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Botao(Button50, Label7.Text)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Botao(Button51, Label7.Text)
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        Botao(Button52, Label7.Text)
    End Sub

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        Botao(Button53, Label7.Text)
    End Sub

    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        Botao(Button54, Label7.Text)
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        Botao(Button55, Label7.Text)
    End Sub

    Private Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click
        Botao(Button56, Label7.Text)
    End Sub

    Private Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        Botao(Button57, Label7.Text)
    End Sub

    Private Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        Botao(Button58, Label7.Text)
    End Sub

    Private Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        Botao(Button59, Label7.Text)
    End Sub

    Private Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        Botao(Button60, Label7.Text)
    End Sub

    Private Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Botao(Button61, Label7.Text)
    End Sub

    Private Sub Button62_Click(sender As Object, e As EventArgs) Handles Button62.Click
        Botao(Button62, Label7.Text)
    End Sub

    Private Sub Button63_Click(sender As Object, e As EventArgs) Handles Button63.Click
        Botao(Button63, Label7.Text)
    End Sub

    Private Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        Botao(Button64, Label7.Text)
    End Sub

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        Botao(Button65, Label7.Text)
    End Sub

    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        Botao(Button66, Label7.Text)
    End Sub

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        Botao(Button67, Label7.Text)
    End Sub

    Private Sub Button68_Click(sender As Object, e As EventArgs) Handles Button68.Click
        Botao(Button68, Label7.Text)
    End Sub

    Private Sub Button69_Click(sender As Object, e As EventArgs) Handles Button69.Click
        Botao(Button69, Label7.Text)
    End Sub

    Private Sub Button70_Click(sender As Object, e As EventArgs) Handles Button70.Click
        Botao(Button70, Label7.Text)
    End Sub

    Private Sub Button71_Click(sender As Object, e As EventArgs) Handles Button71.Click
        Botao(Button71, Label7.Text)
    End Sub

    Private Sub Button72_Click(sender As Object, e As EventArgs) Handles Button72.Click
        Botao(Button72, Label7.Text)
    End Sub

    Private Sub Button73_Click(sender As Object, e As EventArgs) Handles Button73.Click
        Botao(Button73, Label7.Text)
    End Sub

    Private Sub Button74_Click(sender As Object, e As EventArgs) Handles Button74.Click
        Botao(Button74, Label7.Text)
    End Sub

    Private Sub Button75_Click(sender As Object, e As EventArgs) Handles Button75.Click
        Botao(Button75, Label7.Text)
    End Sub

    Private Sub Button76_Click(sender As Object, e As EventArgs) Handles Button76.Click
        Botao(Button76, Label7.Text)
    End Sub

    Private Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click
        Botao(Button77, Label7.Text)
    End Sub

    Private Sub Button78_Click(sender As Object, e As EventArgs) Handles Button78.Click
        Botao(Button78, Label7.Text)
    End Sub

    Private Sub Button79_Click(sender As Object, e As EventArgs) Handles Button79.Click
        Botao(Button79, Label7.Text)
    End Sub

    Private Sub Button80_Click(sender As Object, e As EventArgs) Handles Button80.Click
        Botao(Button80, Label7.Text)
    End Sub

    Private Sub Button81_Click(sender As Object, e As EventArgs) Handles Button81.Click
        Botao(Button81, Label7.Text)
    End Sub

    Private Sub Button82_Click(sender As Object, e As EventArgs) Handles Button82.Click
        Botao(Button82, Label7.Text)
    End Sub

    Private Sub Button83_Click(sender As Object, e As EventArgs) Handles Button83.Click
        Botao(Button83, Label7.Text)
    End Sub

    Private Sub Button84_Click(sender As Object, e As EventArgs) Handles Button84.Click
        Botao(Button84, Label7.Text)
    End Sub

    Private Sub Button85_Click(sender As Object, e As EventArgs) Handles Button85.Click
        Botao(Button85, Label7.Text)
    End Sub

    Private Sub Button86_Click(sender As Object, e As EventArgs) Handles Button86.Click
        Botao(Button86, Label7.Text)
    End Sub

    Private Sub Button87_Click(sender As Object, e As EventArgs) Handles Button87.Click
        Botao(Button87, Label7.Text)
    End Sub

    Private Sub Button88_Click(sender As Object, e As EventArgs) Handles Button88.Click
        Botao(Button88, Label7.Text)
    End Sub

    Private Sub Button89_Click(sender As Object, e As EventArgs) Handles Button89.Click
        Botao(Button89, Label7.Text)
    End Sub

    Private Sub Button90_Click(sender As Object, e As EventArgs) Handles Button90.Click
        Botao(Button90, Label7.Text)
    End Sub

    Private Sub Button91_Click(sender As Object, e As EventArgs) Handles Button91.Click
        Botao(Button91, Label7.Text)
    End Sub

    Private Sub Button92_Click(sender As Object, e As EventArgs) Handles Button92.Click
        Botao(Button92, Label7.Text)
    End Sub

    Private Sub Button93_Click(sender As Object, e As EventArgs) Handles Button93.Click
        Botao(Button93, Label7.Text)
    End Sub

    Private Sub Button94_Click(sender As Object, e As EventArgs) Handles Button94.Click
        Botao(Button94, Label7.Text)
    End Sub

    Private Sub Button95_Click(sender As Object, e As EventArgs) Handles Button95.Click
        Botao(Button95, Label7.Text)
    End Sub

    Private Sub Button96_Click(sender As Object, e As EventArgs) Handles Button96.Click
        Botao(Button96, Label7.Text)
    End Sub

    Private Sub Button97_Click(sender As Object, e As EventArgs) Handles Button97.Click
        Botao(Button97, Label7.Text)
    End Sub

    Private Sub Button98_Click(sender As Object, e As EventArgs) Handles Button98.Click
        Botao(Button98, Label7.Text)
    End Sub

    Private Sub Button99_Click(sender As Object, e As EventArgs) Handles Button99.Click
        Botao(Button99, Label7.Text)
    End Sub

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click
        Botao(Button100, Label7.Text)
    End Sub

    Private Sub Button101_Click(sender As Object, e As EventArgs) Handles Button101.Click
        Botao(Button101, Label7.Text)
    End Sub

    Private Sub Button102_Click(sender As Object, e As EventArgs) Handles Button102.Click
        Botao(Button102, Label7.Text)
    End Sub

    Private Sub Button103_Click(sender As Object, e As EventArgs) Handles Button103.Click
        Botao(Button103, Label7.Text)
    End Sub

    Private Sub Button104_Click(sender As Object, e As EventArgs) Handles Button104.Click
        Botao(Button104, Label7.Text)
    End Sub

    Private Sub Button105_Click(sender As Object, e As EventArgs) Handles Button105.Click
        Botao(Button105, Label7.Text)
    End Sub

    Private Sub Button106_Click(sender As Object, e As EventArgs) Handles Button106.Click
        Botao(Button106, Label7.Text)
    End Sub

    Private Sub Button107_Click(sender As Object, e As EventArgs) Handles Button107.Click
        Botao(Button107, Label7.Text)
    End Sub

    Private Sub Button108_Click(sender As Object, e As EventArgs) Handles Button108.Click
        Botao(Button108, Label7.Text)
    End Sub

    Private Sub Button109_Click(sender As Object, e As EventArgs) Handles Button109.Click
        Botao(Button109, Label7.Text)
    End Sub

    Private Sub Button110_Click(sender As Object, e As EventArgs) Handles Button110.Click
        Botao(Button110, Label7.Text)
    End Sub

    Private Sub Button111_Click(sender As Object, e As EventArgs) Handles Button111.Click
        Botao(Button111, Label7.Text)
    End Sub

    Private Sub Button112_Click(sender As Object, e As EventArgs) Handles Button112.Click
        Botao(Button112, Label7.Text)
    End Sub

    Private Sub Button113_Click(sender As Object, e As EventArgs) Handles Button113.Click
        Botao(Button113, Label7.Text)
    End Sub

    Private Sub Button114_Click(sender As Object, e As EventArgs) Handles Button114.Click
        Botao(Button114, Label7.Text)
    End Sub

    Private Sub Button115_Click(sender As Object, e As EventArgs) Handles Button115.Click
        Botao(Button115, Label7.Text)
    End Sub

    Private Sub Button116_Click(sender As Object, e As EventArgs) Handles Button116.Click
        Botao(Button116, Label7.Text)
    End Sub

    Private Sub Button117_Click(sender As Object, e As EventArgs) Handles Button117.Click
        Botao(Button117, Label7.Text)
    End Sub

    Private Sub Button118_Click(sender As Object, e As EventArgs) Handles Button118.Click
        Botao(Button118, Label7.Text)
    End Sub

    Private Sub Button119_Click(sender As Object, e As EventArgs) Handles Button119.Click
        Botao(Button119, Label7.Text)
    End Sub

    Private Sub Button120_Click(sender As Object, e As EventArgs) Handles Button120.Click
        Botao(Button120, Label7.Text)
    End Sub

    Private Sub Button121_Click(sender As Object, e As EventArgs) Handles Button121.Click
        Botao(Button121, Label7.Text)
    End Sub

    Private Sub Button122_Click(sender As Object, e As EventArgs) Handles Button122.Click
        Botao(Button122, Label7.Text)
    End Sub

    Private Sub Button123_Click(sender As Object, e As EventArgs) Handles Button123.Click
        Botao(Button123, Label7.Text)
    End Sub

    Private Sub Button124_Click(sender As Object, e As EventArgs) Handles Button124.Click
        Botao(Button124, Label7.Text)
    End Sub

    Private Sub Button125_Click(sender As Object, e As EventArgs) Handles Button125.Click
        Botao(Button125, Label7.Text)
    End Sub

    Private Sub Button126_Click(sender As Object, e As EventArgs) Handles Button126.Click
        Botao(Button126, Label7.Text)
    End Sub

    Private Sub Button127_Click(sender As Object, e As EventArgs) Handles Button127.Click
        Botao(Button127, Label7.Text)
    End Sub

    Private Sub Button128_Click(sender As Object, e As EventArgs) Handles Button128.Click
        Botao(Button128, Label7.Text)
    End Sub

    Private Sub Button129_Click(sender As Object, e As EventArgs) Handles Button129.Click
        Botao(Button129, Label7.Text)
    End Sub

    Private Sub Button130_Click(sender As Object, e As EventArgs) Handles Button130.Click
        Botao(Button130, Label7.Text)
    End Sub

    Private Sub Button131_Click(sender As Object, e As EventArgs) Handles Button131.Click
        Botao(Button131, Label7.Text)
    End Sub

    Private Sub Button132_Click(sender As Object, e As EventArgs) Handles Button132.Click
        Botao(Button132, Label7.Text)
    End Sub

    Private Sub Button133_Click(sender As Object, e As EventArgs) Handles Button133.Click
        Botao(Button133, Label7.Text)
    End Sub

    Private Sub Button134_Click(sender As Object, e As EventArgs) Handles Button134.Click
        Botao(Button134, Label7.Text)
    End Sub

    Private Sub Button135_Click(sender As Object, e As EventArgs) Handles Button135.Click
        Botao(Button135, Label7.Text)
    End Sub

    Private Sub Button136_Click(sender As Object, e As EventArgs) Handles Button136.Click
        Botao(Button136, Label7.Text)
    End Sub

    Private Sub Button137_Click(sender As Object, e As EventArgs) Handles Button137.Click
        Botao(Button137, Label7.Text)
    End Sub

    Private Sub Button138_Click(sender As Object, e As EventArgs) Handles Button138.Click
        Botao(Button138, Label7.Text)
    End Sub

    Private Sub Button139_Click(sender As Object, e As EventArgs) Handles Button139.Click
        Botao(Button139, Label7.Text)
    End Sub

    Private Sub Button140_Click(sender As Object, e As EventArgs) Handles Button140.Click
        Botao(Button140, Label7.Text)
    End Sub

    Private Sub Button141_Click(sender As Object, e As EventArgs) Handles Button141.Click
        Botao(Button141, Label7.Text)
    End Sub

    Private Sub Button142_Click(sender As Object, e As EventArgs) Handles Button142.Click
        Botao(Button142, Label7.Text)
    End Sub

    Private Sub Button143_Click(sender As Object, e As EventArgs) Handles Button143.Click
        Botao(Button143, Label7.Text)
    End Sub

    Private Sub Button144_Click(sender As Object, e As EventArgs) Handles Button144.Click
        Botao(Button144, Label7.Text)
    End Sub

    Private Sub Button145_Click(sender As Object, e As EventArgs) Handles Button145.Click
        Botao(Button145, Label7.Text)
    End Sub

    Private Sub Button146_Click(sender As Object, e As EventArgs) Handles Button146.Click
        Botao(Button146, Label7.Text)
    End Sub

    Private Sub Button147_Click(sender As Object, e As EventArgs) Handles Button147.Click
        Botao(Button147, Label7.Text)
    End Sub

    Private Sub Button148_Click(sender As Object, e As EventArgs) Handles Button148.Click
        Botao(Button148, Label7.Text)
    End Sub

    Private Sub Button149_Click(sender As Object, e As EventArgs) Handles Button149.Click
        Botao(Button149, Label7.Text)
    End Sub

    Private Sub Button150_Click(sender As Object, e As EventArgs) Handles Button150.Click
        Botao(Button150, Label7.Text)
    End Sub

    Private Sub Button151_Click(sender As Object, e As EventArgs) Handles Button151.Click
        Botao(Button151, Label7.Text)
    End Sub

    Private Sub Button152_Click(sender As Object, e As EventArgs) Handles Button152.Click
        Botao(Button152, Label7.Text)
    End Sub

    Private Sub Button153_Click(sender As Object, e As EventArgs) Handles Button153.Click
        Botao(Button153, Label7.Text)
    End Sub

    Private Sub Button154_Click(sender As Object, e As EventArgs) Handles Button154.Click
        Botao(Button154, Label7.Text)
    End Sub

    Private Sub Button155_Click(sender As Object, e As EventArgs) Handles Button155.Click
        Botao(Button155, Label7.Text)
    End Sub

    Private Sub Button156_Click(sender As Object, e As EventArgs) Handles Button156.Click
        Botao(Button156, Label7.Text)
    End Sub

    Private Sub Button157_Click(sender As Object, e As EventArgs) Handles Button157.Click
        Botao(Button157, Label7.Text)
    End Sub

    Private Sub Button158_Click(sender As Object, e As EventArgs) Handles Button158.Click
        Botao(Button158, Label7.Text)
    End Sub

    Private Sub Button159_Click(sender As Object, e As EventArgs) Handles Button159.Click
        Botao(Button159, Label7.Text)
    End Sub

    Private Sub Button160_Click(sender As Object, e As EventArgs) Handles Button160.Click
        Botao(Button160, Label7.Text)
    End Sub

    Private Sub Button161_Click(sender As Object, e As EventArgs) Handles Button161.Click
        Botao(Button161, Label7.Text)
    End Sub

    Private Sub Button162_Click(sender As Object, e As EventArgs) Handles Button162.Click
        Botao(Button162, Label7.Text)
    End Sub

    Private Sub Button163_Click(sender As Object, e As EventArgs) Handles Button163.Click
        Botao(Button163, Label7.Text)
    End Sub

    Private Sub Button164_Click(sender As Object, e As EventArgs) Handles Button164.Click
        Botao(Button164, Label7.Text)
    End Sub

    Private Sub Button165_Click(sender As Object, e As EventArgs) Handles Button165.Click
        Botao(Button165, Label7.Text)
    End Sub

    Private Sub Button166_Click(sender As Object, e As EventArgs) Handles Button166.Click
        Botao(Button166, Label7.Text)
    End Sub

    Private Sub Button167_Click(sender As Object, e As EventArgs) Handles Button167.Click
        Botao(Button167, Label7.Text)
    End Sub

    Private Sub Button168_Click(sender As Object, e As EventArgs) Handles Button168.Click
        Botao(Button168, Label7.Text)
    End Sub

    Private Sub Button169_Click(sender As Object, e As EventArgs) Handles Button169.Click
        Botao(Button169, Label7.Text)
    End Sub

    Private Sub Button170_Click(sender As Object, e As EventArgs) Handles Button170.Click
        Botao(Button170, Label7.Text)
    End Sub

    Private Sub Button171_Click(sender As Object, e As EventArgs) Handles Button171.Click
        Botao(Button171, Label7.Text)
    End Sub

    Private Sub Button172_Click(sender As Object, e As EventArgs) Handles Button172.Click
        Botao(Button172, Label7.Text)
    End Sub

    Private Sub Button173_Click(sender As Object, e As EventArgs) Handles Button173.Click
        Botao(Button173, Label7.Text)
    End Sub

    Private Sub Button174_Click(sender As Object, e As EventArgs) Handles Button174.Click
        Botao(Button174, Label7.Text)
    End Sub

    Private Sub Button175_Click(sender As Object, e As EventArgs) Handles Button175.Click
        Botao(Button175, Label7.Text)
    End Sub

    Private Sub Button176_Click(sender As Object, e As EventArgs) Handles Button176.Click
        Botao(Button176, Label7.Text)
    End Sub

    Private Sub Button177_Click(sender As Object, e As EventArgs) Handles Button177.Click
        Botao(Button177, Label7.Text)
    End Sub

    Private Sub Button178_Click(sender As Object, e As EventArgs) Handles Button178.Click
        Botao(Button178, Label7.Text)
    End Sub

    Private Sub Button179_Click(sender As Object, e As EventArgs) Handles Button179.Click
        Botao(Button179, Label7.Text)
    End Sub

    Private Sub Button180_Click(sender As Object, e As EventArgs) Handles Button180.Click
        Botao(Button180, Label7.Text)
    End Sub

    Private Sub Button181_Click(sender As Object, e As EventArgs) Handles Button181.Click
        Botao(Button181, Label7.Text)
    End Sub

    Private Sub Button182_Click(sender As Object, e As EventArgs) Handles Button182.Click
        Botao(Button182, Label7.Text)
    End Sub

    Private Sub Button183_Click(sender As Object, e As EventArgs) Handles Button183.Click
        Botao(Button183, Label7.Text)
    End Sub

    Private Sub Button184_Click(sender As Object, e As EventArgs) Handles Button184.Click
        Botao(Button184, Label7.Text)
    End Sub

    Private Sub Button185_Click(sender As Object, e As EventArgs) Handles Button185.Click
        Botao(Button185, Label7.Text)
    End Sub

    Private Sub Button186_Click(sender As Object, e As EventArgs) Handles Button186.Click
        Botao(Button186, Label7.Text)
    End Sub

    Private Sub Button187_Click(sender As Object, e As EventArgs) Handles Button187.Click
        Botao(Button187, Label7.Text)
    End Sub

    Private Sub Button188_Click(sender As Object, e As EventArgs) Handles Button188.Click
        Botao(Button188, Label7.Text)
    End Sub

    Private Sub Button189_Click(sender As Object, e As EventArgs) Handles Button189.Click
        Botao(Button189, Label7.Text)
    End Sub

    Private Sub Button190_Click(sender As Object, e As EventArgs) Handles Button190.Click
        Botao(Button190, Label7.Text)
    End Sub

    Private Sub Button191_Click(sender As Object, e As EventArgs) Handles Button191.Click
        Botao(Button191, Label7.Text)
    End Sub

    Private Sub Button192_Click(sender As Object, e As EventArgs) Handles Button192.Click
        Botao(Button192, Label7.Text)
    End Sub

    Private Sub Button193_Click(sender As Object, e As EventArgs) Handles Button193.Click
        Botao(Button193, Label7.Text)
    End Sub

    Private Sub Button194_Click(sender As Object, e As EventArgs) Handles Button194.Click
        Botao(Button194, Label7.Text)
    End Sub

    Private Sub Button195_Click(sender As Object, e As EventArgs) Handles Button195.Click
        Botao(Button195, Label7.Text)
    End Sub

    Private Sub Button196_Click(sender As Object, e As EventArgs) Handles Button196.Click
        Botao(Button196, Label7.Text)
    End Sub

    Private Sub Button197_Click(sender As Object, e As EventArgs) Handles Button197.Click
        Botao(Button197, Label7.Text)
    End Sub

    Private Sub Button198_Click(sender As Object, e As EventArgs) Handles Button198.Click
        Botao(Button198, Label7.Text)
    End Sub

    Private Sub Button199_Click(sender As Object, e As EventArgs) Handles Button199.Click
        Botao(Button199, Label7.Text)
    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click
        Botao(Button200, Label7.Text)
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        v_atualização = v_atualização + 1
        ProgressBar1.Value = v_atualização
        If v_atualização >= v_barra Then ' 10 minutos 10s*6*10
            Atualiza_botoes()
            v_atualização = 0
            ProgressBar1.Value = v_atualização
            ProgressBar1.Refresh()
        End If

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click
        Atualiza_botoes()
        v_atualização = 0
        ProgressBar1.Value = v_atualização
        ProgressBar1.Refresh()

    End Sub

    Private Sub DisponibilidadeMensalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisponibilidadeMensalToolStripMenuItem.Click
        FrmREL_Disponivel.ShowDialog()
    End Sub


End Class
