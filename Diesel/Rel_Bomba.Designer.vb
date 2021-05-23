<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rel_Bomba
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Textbox1 = New System.Windows.Forms.TextBox()
		Me.dtlabel = New System.Windows.Forms.Label()
		Me.Dgbgrid = New System.Windows.Forms.DataGridView()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.GroupBox1.SuspendLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'GroupBox1
		'
		Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.GroupBox1.Controls.Add(Me.ComboBox1)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.Button2)
		Me.GroupBox1.Controls.Add(Me.Button1)
		Me.GroupBox1.Controls.Add(Me.Textbox1)
		Me.GroupBox1.Controls.Add(Me.dtlabel)
		Me.GroupBox1.Font = New System.Drawing.Font("Roboto Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox1.ForeColor = System.Drawing.Color.White
		Me.GroupBox1.Location = New System.Drawing.Point(7, 2)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(392, 53)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "SELEÇÃO"
		'
		'ComboBox1
		'
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(189, 19)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(56, 23)
		Me.ComboBox1.TabIndex = 89
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Roboto", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.White
		Me.Label1.Location = New System.Drawing.Point(151, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(36, 14)
		Me.Label1.TabIndex = 88
		Me.Label1.Text = "DATA"
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.Color.Green
		Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button2.ForeColor = System.Drawing.Color.White
		Me.Button2.Location = New System.Drawing.Point(327, 19)
		Me.Button2.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button2.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(60, 23)
		Me.Button2.TabIndex = 87
		Me.Button2.Text = "EXCEL"
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.Color.DodgerBlue
		Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button1.ForeColor = System.Drawing.Color.White
		Me.Button1.Location = New System.Drawing.Point(264, 19)
		Me.Button1.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button1.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(60, 23)
		Me.Button1.TabIndex = 86
		Me.Button1.Text = "BUSCAR"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'Textbox1
		'
		Me.Textbox1.Location = New System.Drawing.Point(43, 19)
		Me.Textbox1.Name = "Textbox1"
		Me.Textbox1.Size = New System.Drawing.Size(89, 23)
		Me.Textbox1.TabIndex = 85
		'
		'dtlabel
		'
		Me.dtlabel.AutoSize = True
		Me.dtlabel.Font = New System.Drawing.Font("Roboto", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.dtlabel.ForeColor = System.Drawing.Color.White
		Me.dtlabel.Location = New System.Drawing.Point(6, 23)
		Me.dtlabel.Name = "dtlabel"
		Me.dtlabel.Size = New System.Drawing.Size(36, 14)
		Me.dtlabel.TabIndex = 84
		Me.dtlabel.Text = "DATA"
		'
		'Dgbgrid
		'
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Roboto", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.Dgbgrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.Dgbgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid.Location = New System.Drawing.Point(7, 61)
		Me.Dgbgrid.Name = "Dgbgrid"
		Me.Dgbgrid.Size = New System.Drawing.Size(674, 616)
		Me.Dgbgrid.TabIndex = 1
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Roboto", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.Color.White
		Me.Label2.Location = New System.Drawing.Point(454, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(149, 14)
		Me.Label2.TabIndex = 85
		Me.Label2.Text = "QT LITROS ABASTECIDOS"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.BackColor = System.Drawing.Color.White
		Me.Label3.Font = New System.Drawing.Font("Roboto Black", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.ForeColor = System.Drawing.Color.Black
		Me.Label3.Location = New System.Drawing.Point(607, 22)
		Me.Label3.MaximumSize = New System.Drawing.Size(70, 16)
		Me.Label3.MinimumSize = New System.Drawing.Size(70, 16)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(70, 16)
		Me.Label3.TabIndex = 86
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(454, 47)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 87
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Roboto Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.Color.White
		Me.Label4.Location = New System.Drawing.Point(298, 688)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(96, 19)
		Me.Label4.TabIndex = 88
		Me.Label4.Text = "BOMBA 201"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Roboto Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.ForeColor = System.Drawing.Color.White
		Me.Label5.Location = New System.Drawing.Point(510, 688)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(96, 19)
		Me.Label5.TabIndex = 89
		Me.Label5.Text = "BOMBA 202"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.BackColor = System.Drawing.Color.White
		Me.Label6.Font = New System.Drawing.Font("Roboto Black", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Label6.ForeColor = System.Drawing.Color.Black
		Me.Label6.Location = New System.Drawing.Point(394, 690)
		Me.Label6.MaximumSize = New System.Drawing.Size(70, 16)
		Me.Label6.MinimumSize = New System.Drawing.Size(70, 16)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(70, 16)
		Me.Label6.TabIndex = 90
		Me.Label6.Text = "BOMBA 201"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.BackColor = System.Drawing.Color.White
		Me.Label7.Font = New System.Drawing.Font("Roboto Black", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Label7.ForeColor = System.Drawing.Color.Black
		Me.Label7.Location = New System.Drawing.Point(607, 690)
		Me.Label7.MaximumSize = New System.Drawing.Size(70, 16)
		Me.Label7.MinimumSize = New System.Drawing.Size(70, 16)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(70, 16)
		Me.Label7.TabIndex = 91
		Me.Label7.Text = "BOMBA 201"
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Rel_Bomba
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(688, 715)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Dgbgrid)
		Me.Controls.Add(Me.GroupBox1)
		Me.Name = "Rel_Bomba"
		Me.Text = "RELATÓRIO POR BOMBAS"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Button2 As Button
	Friend WithEvents Button1 As Button
	Friend WithEvents Textbox1 As TextBox
	Friend WithEvents dtlabel As Label
	Friend WithEvents Dgbgrid As DataGridView
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents MonthCalendar1 As MonthCalendar
	Friend WithEvents Label4 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents Label7 As Label
End Class
