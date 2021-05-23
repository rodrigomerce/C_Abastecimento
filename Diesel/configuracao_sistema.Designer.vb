<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class configuracao_sistema
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
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.DgbEmpresas = New System.Windows.Forms.DataGridView()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TextBox5 = New System.Windows.Forms.TextBox()
		Me.TextBox4 = New System.Windows.Forms.TextBox()
		Me.TextBox3 = New System.Windows.Forms.TextBox()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.DgbCarros = New System.Windows.Forms.DataGridView()
		Me.TextBox9 = New System.Windows.Forms.TextBox()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.TextBox8 = New System.Windows.Forms.TextBox()
		Me.TextBox7 = New System.Windows.Forms.TextBox()
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.TextBox6 = New System.Windows.Forms.TextBox()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.TextBox10 = New System.Windows.Forms.TextBox()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		CType(Me.DgbEmpresas, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TabPage2.SuspendLayout()
		CType(Me.DgbCarros, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Location = New System.Drawing.Point(7, 12)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(640, 507)
		Me.TabControl1.TabIndex = 0
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.Color.Gainsboro
		Me.TabPage1.Controls.Add(Me.Button2)
		Me.TabPage1.Controls.Add(Me.Button1)
		Me.TabPage1.Controls.Add(Me.DgbEmpresas)
		Me.TabPage1.Controls.Add(Me.Label5)
		Me.TabPage1.Controls.Add(Me.Label4)
		Me.TabPage1.Controls.Add(Me.Label3)
		Me.TabPage1.Controls.Add(Me.Label1)
		Me.TabPage1.Controls.Add(Me.TextBox5)
		Me.TabPage1.Controls.Add(Me.TextBox4)
		Me.TabPage1.Controls.Add(Me.TextBox3)
		Me.TabPage1.Controls.Add(Me.TextBox1)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(632, 481)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Empresas"
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(231, 442)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(75, 23)
		Me.Button2.TabIndex = 12
		Me.Button2.Text = "Excluir"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(27, 442)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 11
		Me.Button1.Text = "Gravar"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'DgbEmpresas
		'
		Me.DgbEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DgbEmpresas.Location = New System.Drawing.Point(6, 112)
		Me.DgbEmpresas.Name = "DgbEmpresas"
		Me.DgbEmpresas.Size = New System.Drawing.Size(345, 313)
		Me.DgbEmpresas.TabIndex = 10
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(266, 48)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(40, 13)
		Me.Label5.TabIndex = 9
		Me.Label5.Text = "Bomba"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(178, 48)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(44, 13)
		Me.Label4.TabIndex = 8
		Me.Label4.Text = "Tanque"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(90, 48)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(27, 13)
		Me.Label3.TabIndex = 7
		Me.Label3.Text = "Filial"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 48)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(70, 13)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Cod Empresa"
		'
		'TextBox5
		'
		Me.TextBox5.Location = New System.Drawing.Point(269, 64)
		Me.TextBox5.Name = "TextBox5"
		Me.TextBox5.Size = New System.Drawing.Size(82, 20)
		Me.TextBox5.TabIndex = 4
		'
		'TextBox4
		'
		Me.TextBox4.Location = New System.Drawing.Point(181, 64)
		Me.TextBox4.Name = "TextBox4"
		Me.TextBox4.Size = New System.Drawing.Size(82, 20)
		Me.TextBox4.TabIndex = 3
		'
		'TextBox3
		'
		Me.TextBox3.Location = New System.Drawing.Point(93, 64)
		Me.TextBox3.Name = "TextBox3"
		Me.TextBox3.Size = New System.Drawing.Size(82, 20)
		Me.TextBox3.TabIndex = 2
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(6, 64)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(81, 20)
		Me.TextBox1.TabIndex = 0
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.Color.Gainsboro
		Me.TabPage2.Controls.Add(Me.TextBox10)
		Me.TabPage2.Controls.Add(Me.Button3)
		Me.TabPage2.Controls.Add(Me.Button4)
		Me.TabPage2.Controls.Add(Me.DgbCarros)
		Me.TabPage2.Controls.Add(Me.TextBox9)
		Me.TabPage2.Controls.Add(Me.ComboBox2)
		Me.TabPage2.Controls.Add(Me.TextBox8)
		Me.TabPage2.Controls.Add(Me.TextBox7)
		Me.TabPage2.Controls.Add(Me.ComboBox1)
		Me.TabPage2.Controls.Add(Me.TextBox6)
		Me.TabPage2.Controls.Add(Me.TextBox2)
		Me.TabPage2.Location = New System.Drawing.Point(4, 22)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(632, 481)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Carros/Prefixo"
		'
		'DgbCarros
		'
		Me.DgbCarros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DgbCarros.Location = New System.Drawing.Point(6, 71)
		Me.DgbCarros.Name = "DgbCarros"
		Me.DgbCarros.Size = New System.Drawing.Size(531, 366)
		Me.DgbCarros.TabIndex = 7
		'
		'TextBox9
		'
		Me.TextBox9.Location = New System.Drawing.Point(387, 37)
		Me.TextBox9.Name = "TextBox9"
		Me.TextBox9.Size = New System.Drawing.Size(30, 20)
		Me.TextBox9.TabIndex = 6
		'
		'ComboBox2
		'
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Location = New System.Drawing.Point(313, 36)
		Me.ComboBox2.MaxLength = 1
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(68, 21)
		Me.ComboBox2.TabIndex = 5
		'
		'TextBox8
		'
		Me.TextBox8.Location = New System.Drawing.Point(277, 36)
		Me.TextBox8.MaxLength = 1
		Me.TextBox8.Name = "TextBox8"
		Me.TextBox8.Size = New System.Drawing.Size(30, 20)
		Me.TextBox8.TabIndex = 4
		'
		'TextBox7
		'
		Me.TextBox7.Location = New System.Drawing.Point(231, 36)
		Me.TextBox7.MaxLength = 2
		Me.TextBox7.Name = "TextBox7"
		Me.TextBox7.Size = New System.Drawing.Size(40, 20)
		Me.TextBox7.TabIndex = 3
		'
		'ComboBox1
		'
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(157, 35)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(68, 21)
		Me.ComboBox1.TabIndex = 2
		'
		'TextBox6
		'
		Me.TextBox6.Location = New System.Drawing.Point(81, 35)
		Me.TextBox6.Name = "TextBox6"
		Me.TextBox6.Size = New System.Drawing.Size(69, 20)
		Me.TextBox6.TabIndex = 1
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(6, 35)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(69, 20)
		Me.TextBox2.TabIndex = 0
		'
		'Button3
		'
		Me.Button3.Location = New System.Drawing.Point(313, 452)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(75, 23)
		Me.Button3.TabIndex = 14
		Me.Button3.Text = "Excluir"
		Me.Button3.UseVisualStyleBackColor = True
		'
		'Button4
		'
		Me.Button4.Location = New System.Drawing.Point(109, 452)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(75, 23)
		Me.Button4.TabIndex = 13
		Me.Button4.Text = "Gravar"
		Me.Button4.UseVisualStyleBackColor = True
		'
		'TextBox10
		'
		Me.TextBox10.Location = New System.Drawing.Point(423, 37)
		Me.TextBox10.Name = "TextBox10"
		Me.TextBox10.Size = New System.Drawing.Size(45, 20)
		Me.TextBox10.TabIndex = 15
		'
		'configuracao_sistema
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(654, 531)
		Me.Controls.Add(Me.TabControl1)
		Me.Name = "configuracao_sistema"
		Me.Text = "configuracao_sistema"
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		CType(Me.DgbEmpresas, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		CType(Me.DgbCarros, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TabControl1 As TabControl
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents DgbEmpresas As DataGridView
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents TextBox5 As TextBox
	Friend WithEvents TextBox4 As TextBox
	Friend WithEvents TextBox3 As TextBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents DgbCarros As DataGridView
	Friend WithEvents TextBox9 As TextBox
	Friend WithEvents ComboBox2 As ComboBox
	Friend WithEvents TextBox8 As TextBox
	Friend WithEvents TextBox7 As TextBox
	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents TextBox6 As TextBox
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents Button3 As Button
	Friend WithEvents Button4 As Button
	Friend WithEvents TextBox10 As TextBox
End Class
