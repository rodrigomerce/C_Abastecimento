<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmREL_Disponivel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgdDados = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BttExcel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.dgdDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgdDados
        '
        Me.dgdDados.AllowUserToAddRows = False
        Me.dgdDados.AllowUserToDeleteRows = False
        Me.dgdDados.AllowUserToOrderColumns = True
        Me.dgdDados.BackgroundColor = System.Drawing.Color.White
        Me.dgdDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdDados.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgdDados.Location = New System.Drawing.Point(55, 35)
        Me.dgdDados.Name = "dgdDados"
        Me.dgdDados.ReadOnly = True
        Me.dgdDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgdDados.Size = New System.Drawing.Size(382, 498)
        Me.dgdDados.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(466, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Eliminar"
        '
        'BttExcel
        '
        Me.BttExcel.Image = Global.Soltura.My.Resources.Resources.excel
        Me.BttExcel.Location = New System.Drawing.Point(469, 129)
        Me.BttExcel.Name = "BttExcel"
        Me.BttExcel.Size = New System.Drawing.Size(45, 46)
        Me.BttExcel.TabIndex = 13
        Me.BttExcel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Location = New System.Drawing.Point(466, 178)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Excel"
        '
        'Button2
        '
        Me.Button2.Image = Global.Soltura.My.Resources.Resources.excluir2
        Me.Button2.Location = New System.Drawing.Point(469, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(46, 49)
        Me.Button2.TabIndex = 11
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FrmREL_Disponivel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 574)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BttExcel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.dgdDados)
        Me.Name = "FrmREL_Disponivel"
        Me.Text = "FrmREL_Disponivel"
        CType(Me.dgdDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgdDados As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents BttExcel As Button
    Friend WithEvents Label1 As Label
End Class
