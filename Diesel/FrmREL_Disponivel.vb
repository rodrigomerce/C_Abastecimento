
Imports System.Data.SqlClient
Public Class FrmREL_Disponivel
    Dim SQL As String

    Private Sub FrmREL_Disponivel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pcarregaSQL()
    End Sub

    Private Sub pcarregaSQL()

        Using con As SqlConnection = getconnectionSQL()
            Try
                con.Open()

                SQL = " Select datas,manutencao,frota from Disponibilidade" & Empresa
                Dim cmd As SqlCommand = New SqlCommand(Sql, con)
                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                dgdDados.DataSource = dt
                dgdDados.Columns(0).HeaderText = "Data"
                dgdDados.Columns(0).Width = 100
                dgdDados.Columns(1).HeaderText = "Retidos"
                dgdDados.Columns(1).Width = 100
                dgdDados.Columns(2).HeaderText = "Frota"
                dgdDados.Columns(2).Width = 100
            Catch ex As Exception
                MsgBox("Erro na coneção " & ex.ToString)
            Finally
                con.Close()
            End Try


        End Using


    End Sub

    Private Sub BttExcel_Click(sender As Object, e As EventArgs) Handles BttExcel.Click
        Dim exc As Object = CreateObject("excel.application")
        Dim i

        exc.Visible = True
        Call ConexaoRelatorios()
        exc.workbooks.Open(Local_excel & "Relatorios.xls")
        exc.Range("A1:X5000").Select
        exc.Selection.ClearContents

        exc.Range("A1").Select


        exc.Range("A1").Value = "RELATÓRIO DE CONCLUIDAS"
        '        dgdDados.CurrentRow.Cells(0).Value
        exc.Range("A" & 1).Value = dgdDados.Columns(0).HeaderText
        exc.Range("B" & 1).Value = dgdDados.Columns(1).HeaderText
        exc.Range("C" & 1).Value = dgdDados.Columns(2).HeaderText

        For i = 0 To dgdDados.RowCount - 1
            exc.Range("A" & i + 2).Value = dgdDados.Rows(i).Cells(0).Value
            exc.Range("B" & i + 2).Value = dgdDados.Rows(i).Cells(1).Value
            exc.Range("C" & i + 2).Value = dgdDados.Rows(i).Cells(2).Value
        Next

    End Sub
End Class