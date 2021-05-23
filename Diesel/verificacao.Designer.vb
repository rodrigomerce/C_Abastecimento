<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class verificacao
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
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Dgbgrid1 = New System.Windows.Forms.DataGridView()
		Me.Dgbgrid2 = New System.Windows.Forms.DataGridView()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		CType(Me.Dgbgrid1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Dgbgrid2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(14, 27)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(86, 20)
		Me.TextBox1.TabIndex = 0
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(106, 27)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(86, 20)
		Me.TextBox2.TabIndex = 1
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(198, 25)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 2
		Me.Button1.Text = "Buscar"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Dgbgrid1
		'
		Me.Dgbgrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid1.Location = New System.Drawing.Point(13, 83)
		Me.Dgbgrid1.Name = "Dgbgrid1"
		Me.Dgbgrid1.Size = New System.Drawing.Size(471, 563)
		Me.Dgbgrid1.TabIndex = 3
		'
		'Dgbgrid2
		'
		Me.Dgbgrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid2.Location = New System.Drawing.Point(490, 83)
		Me.Dgbgrid2.Name = "Dgbgrid2"
		Me.Dgbgrid2.Size = New System.Drawing.Size(471, 563)
		Me.Dgbgrid2.TabIndex = 4
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.White
		Me.Label1.Location = New System.Drawing.Point(12, 64)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(168, 16)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Verificação Hodometro"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.Color.White
		Me.Label2.Location = New System.Drawing.Point(487, 64)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(194, 16)
		Me.Label2.TabIndex = 6
		Me.Label2.Text = "Verificação Abastecimento"
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(285, 3)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 7
		Me.MonthCalendar1.Visible = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.ForeColor = System.Drawing.Color.White
		Me.Label3.Location = New System.Drawing.Point(15, 13)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(72, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "Data Inicial"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.Color.White
		Me.Label4.Location = New System.Drawing.Point(106, 13)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(62, 13)
		Me.Label4.TabIndex = 9
		Me.Label4.Text = "Data final"
		'
		'verificacao
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(975, 657)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Dgbgrid2)
		Me.Controls.Add(Me.Dgbgrid1)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.TextBox2)
		Me.Controls.Add(Me.TextBox1)
		Me.Name = "verificacao"
		Me.Text = "VERIFICAÇÃO"
		CType(Me.Dgbgrid1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Dgbgrid2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Dgbgrid1 As DataGridView
	Friend WithEvents Dgbgrid2 As DataGridView
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents MonthCalendar1 As MonthCalendar
	Friend WithEvents Label3 As Label
	Friend WithEvents Label4 As Label
End Class
