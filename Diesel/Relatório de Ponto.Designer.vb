<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Relatório_de_Ponto
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
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.dtlabel = New System.Windows.Forms.Label()
		Me.Dgbgrid = New System.Windows.Forms.DataGridView()
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.GroupBox1.SuspendLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'GroupBox1
		'
		Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(5, Byte), Integer), CType(CType(5, Byte), Integer))
		Me.GroupBox1.Controls.Add(Me.Button2)
		Me.GroupBox1.Controls.Add(Me.Button1)
		Me.GroupBox1.Controls.Add(Me.TextBox2)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.TextBox1)
		Me.GroupBox1.Controls.Add(Me.dtlabel)
		Me.GroupBox1.Location = New System.Drawing.Point(8, 11)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(284, 75)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.Color.Green
		Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button2.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button2.ForeColor = System.Drawing.Color.White
		Me.Button2.Location = New System.Drawing.Point(203, 41)
		Me.Button2.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button2.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(60, 23)
		Me.Button2.TabIndex = 89
		Me.Button2.Text = "EXCEL"
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.Color.DodgerBlue
		Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button1.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button1.ForeColor = System.Drawing.Color.White
		Me.Button1.Location = New System.Drawing.Point(203, 15)
		Me.Button1.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button1.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(60, 23)
		Me.Button1.TabIndex = 88
		Me.Button1.Text = "BUSCAR"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(93, 42)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(89, 21)
		Me.TextBox2.TabIndex = 83
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Roboto", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.White
		Me.Label1.Location = New System.Drawing.Point(26, 49)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(61, 14)
		Me.Label1.TabIndex = 82
		Me.Label1.Text = "DATA FIM"
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(93, 16)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(89, 21)
		Me.TextBox1.TabIndex = 81
		'
		'dtlabel
		'
		Me.dtlabel.AutoSize = True
		Me.dtlabel.Font = New System.Drawing.Font("Roboto", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.dtlabel.ForeColor = System.Drawing.Color.White
		Me.dtlabel.Location = New System.Drawing.Point(5, 23)
		Me.dtlabel.Name = "dtlabel"
		Me.dtlabel.Size = New System.Drawing.Size(82, 14)
		Me.dtlabel.TabIndex = 80
		Me.dtlabel.Text = "DATA INICIAL"
		'
		'Dgbgrid
		'
		Me.Dgbgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid.Location = New System.Drawing.Point(8, 93)
		Me.Dgbgrid.Name = "Dgbgrid"
		Me.Dgbgrid.Size = New System.Drawing.Size(283, 533)
		Me.Dgbgrid.TabIndex = 1
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(65, 87)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 2
		Me.MonthCalendar1.Visible = False
		'
		'Relatório_de_Ponto
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(300, 638)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.Dgbgrid)
		Me.Controls.Add(Me.GroupBox1)
		Me.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Name = "Relatório_de_Ponto"
		Me.Text = "REGISTROS"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Dgbgrid As DataGridView
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents dtlabel As Label
	Friend WithEvents Button2 As Button
	Friend WithEvents Button1 As Button
	Friend WithEvents MonthCalendar1 As MonthCalendar
	Friend WithEvents ToolTip1 As ToolTip
End Class
