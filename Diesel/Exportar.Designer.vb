<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Exportar
	Inherits System.Windows.Forms.Form

	'Descartar substituições de formulário para limpar a lista de componentes.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Dgbgrid = New System.Windows.Forms.DataGridView()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Button10 = New System.Windows.Forms.Button()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.Button5 = New System.Windows.Forms.Button()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Dgbgrid2 = New System.Windows.Forms.DataGridView()
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.GroupBox1.SuspendLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox2.SuspendLayout()
		CType(Me.Dgbgrid2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Button3)
		Me.GroupBox1.Controls.Add(Me.Button2)
		Me.GroupBox1.Controls.Add(Me.Button1)
		Me.GroupBox1.Controls.Add(Me.TextBox1)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.ForeColor = System.Drawing.Color.White
		Me.GroupBox1.Location = New System.Drawing.Point(12, 22)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(395, 55)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "SELEÇÃO"
		'
		'Button3
		'
		Me.Button3.BackColor = System.Drawing.Color.DarkOrange
		Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button3.Location = New System.Drawing.Point(316, 20)
		Me.Button3.MaximumSize = New System.Drawing.Size(69, 23)
		Me.Button3.MinimumSize = New System.Drawing.Size(69, 23)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(69, 23)
		Me.Button3.TabIndex = 6
		Me.Button3.Text = "VALIDAR"
		Me.Button3.UseVisualStyleBackColor = False
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.Color.Green
		Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button2.Location = New System.Drawing.Point(250, 20)
		Me.Button2.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button2.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(60, 23)
		Me.Button2.TabIndex = 5
		Me.Button2.Text = "EXCEL"
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.Color.DodgerBlue
		Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button1.Location = New System.Drawing.Point(184, 20)
		Me.Button1.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button1.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(60, 23)
		Me.Button1.TabIndex = 4
		Me.Button1.Text = "BUSCAR"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(52, 20)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(100, 23)
		Me.TextBox1.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(7, 24)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(39, 15)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "DATA"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.ForeColor = System.Drawing.Color.White
		Me.Label2.Location = New System.Drawing.Point(457, 62)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(118, 15)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "TOTAL REGISTROS"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.BackColor = System.Drawing.Color.White
		Me.Label3.Location = New System.Drawing.Point(581, 62)
		Me.Label3.MaximumSize = New System.Drawing.Size(65, 15)
		Me.Label3.MinimumSize = New System.Drawing.Size(65, 15)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(65, 15)
		Me.Label3.TabIndex = 2
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'Dgbgrid
		'
		Me.Dgbgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid.Location = New System.Drawing.Point(12, 83)
		Me.Dgbgrid.Name = "Dgbgrid"
		Me.Dgbgrid.Size = New System.Drawing.Size(636, 608)
		Me.Dgbgrid.TabIndex = 3
		'
		'GroupBox2
		'
		Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
		Me.GroupBox2.Controls.Add(Me.Button10)
		Me.GroupBox2.Controls.Add(Me.Button4)
		Me.GroupBox2.Controls.Add(Me.Button5)
		Me.GroupBox2.Controls.Add(Me.TextBox2)
		Me.GroupBox2.Controls.Add(Me.Label5)
		Me.GroupBox2.Controls.Add(Me.Label4)
		Me.GroupBox2.Controls.Add(Me.Dgbgrid2)
		Me.GroupBox2.Location = New System.Drawing.Point(34, 245)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(635, 597)
		Me.GroupBox2.TabIndex = 4
		Me.GroupBox2.TabStop = False
		'
		'Button10
		'
		Me.Button10.BackColor = System.Drawing.Color.DimGray
		Me.Button10.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button10.ForeColor = System.Drawing.Color.White
		Me.Button10.Location = New System.Drawing.Point(561, 29)
		Me.Button10.Name = "Button10"
		Me.Button10.Size = New System.Drawing.Size(65, 23)
		Me.Button10.TabIndex = 103
		Me.Button10.Text = "FECHAR"
		Me.Button10.UseVisualStyleBackColor = False
		'
		'Button4
		'
		Me.Button4.BackColor = System.Drawing.Color.DarkOrange
		Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button4.ForeColor = System.Drawing.Color.White
		Me.Button4.Location = New System.Drawing.Point(420, 29)
		Me.Button4.MaximumSize = New System.Drawing.Size(69, 23)
		Me.Button4.MinimumSize = New System.Drawing.Size(69, 23)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(69, 23)
		Me.Button4.TabIndex = 8
		Me.Button4.Text = "VALIDAR"
		Me.Button4.UseVisualStyleBackColor = False
		'
		'Button5
		'
		Me.Button5.BackColor = System.Drawing.Color.Green
		Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button5.ForeColor = System.Drawing.Color.White
		Me.Button5.Location = New System.Drawing.Point(495, 29)
		Me.Button5.MaximumSize = New System.Drawing.Size(60, 23)
		Me.Button5.MinimumSize = New System.Drawing.Size(60, 23)
		Me.Button5.Name = "Button5"
		Me.Button5.Size = New System.Drawing.Size(60, 23)
		Me.Button5.TabIndex = 7
		Me.Button5.Text = "EXCEL"
		Me.Button5.UseVisualStyleBackColor = False
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(308, 29)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(100, 23)
		Me.TextBox2.TabIndex = 3
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.ForeColor = System.Drawing.Color.White
		Me.Label5.Location = New System.Drawing.Point(237, 33)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(64, 15)
		Me.Label5.TabIndex = 2
		Me.Label5.Text = "ARQUIVO"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.Color.Goldenrod
		Me.Label4.Location = New System.Drawing.Point(7, 31)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(212, 19)
		Me.Label4.TabIndex = 1
		Me.Label4.Text = "ERROS DE TRANSFERÊNCIA"
		'
		'Dgbgrid2
		'
		Me.Dgbgrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgbgrid2.Location = New System.Drawing.Point(7, 90)
		Me.Dgbgrid2.Name = "Dgbgrid2"
		Me.Dgbgrid2.Size = New System.Drawing.Size(620, 499)
		Me.Dgbgrid2.TabIndex = 0
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(419, 42)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 5
		Me.MonthCalendar1.Visible = False
		'
		'Exportar
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(660, 709)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.Dgbgrid)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Name = "Exportar"
		Me.Text = "EXPORTAR"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		CType(Me.Dgbgrid, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		CType(Me.Dgbgrid2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Button3 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents Button1 As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Dgbgrid As DataGridView
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents MonthCalendar1 As MonthCalendar
	Friend WithEvents Button4 As Button
	Friend WithEvents Button5 As Button
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Dgbgrid2 As DataGridView
	Friend WithEvents Button10 As Button
	Friend WithEvents ToolTip1 As ToolTip
End Class
