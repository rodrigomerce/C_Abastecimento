<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Relatorio_hodometro
	Inherits System.Windows.Forms.Form

	'Descartar substituições de formulário para limpar a lista de componentes.
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

	'Exigido pelo Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
	'Pode ser modificado usando o Windows Form Designer.  
	'Não o modifique usando o editor de códigos.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.Dgbgrid = New System.Windows.Forms.DataGridView()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Dgbgrid
		'
		Me.Dgbgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid.Location = New System.Drawing.Point(8, 8)
		Me.Dgbgrid.Name = "Dgbgrid"
		Me.Dgbgrid.Size = New System.Drawing.Size(184, 587)
		Me.Dgbgrid.TabIndex = 0
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.Color.Green
		Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button2.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button2.ForeColor = System.Drawing.Color.White
		Me.Button2.Location = New System.Drawing.Point(201, 8)
		Me.Button2.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(47, 23)
		Me.Button2.TabIndex = 90
		Me.Button2.Text = "EXCEL"
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Relatorio_hodometro
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.WhiteSmoke
		Me.ClientSize = New System.Drawing.Size(252, 603)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Dgbgrid)
		Me.Name = "Relatorio_hodometro"
		Me.Text = "HODOMETRO DEFEITO"
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Dgbgrid As DataGridView
	Friend WithEvents Button2 As Button
	Friend WithEvents ToolTip1 As ToolTip
End Class
