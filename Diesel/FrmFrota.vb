Imports System.Data.SqlClient

Public Class FrmFrota
    Dim Sql As String


    Private Sub FrmFrota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dr As SqlDataReader = Nothing


        Using con As SqlConnection = getconnectionSQL()

            Sql = ""
            Try
                con.Open()

                Sql = "Select Ativa,reserva from Configuracao where Empresa='" & Empresa & "'"
                Dim cmd As SqlCommand = New SqlCommand(Sql, con)
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    TxtFrota.Text = CInt(dr.Item("Ativa"))
                    TxtReserva.Text = CInt(dr.Item("Reserva"))
                End If
            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using con As SqlConnection = getconnectionSQL()

            Dim sql As String = ""
            Try

                con.Open()

                sql = "Update Configuracao set "
                sql += "Ativa=" & TxtFrota.Text & ", Reserva=" & TxtReserva.Text & " where Empresa='" & Empresa & "'"
                ' sql = "Update cliente set nome='" & txtNome.Text & "' where codigo=" & txtnum.Text
                Dim cmd As SqlCommand = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MsgBox(" Gravação Concluida", vbInformation)

            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                con.Close()
            End Try

        End Using

    End Sub
End Class