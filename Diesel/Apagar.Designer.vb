<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Apagar
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
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
		Me.Button8 = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'GroupBox1
		'
		Me.GroupBox1.BackColor = System.Drawing.Color.Firebrick
		Me.GroupBox1.Controls.Add(Me.TextBox2)
		Me.GroupBox1.Controls.Add(Me.TextBox1)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Location = New System.Drawing.Point(0, 58)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(497, 72)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(363, 30)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(100, 20)
		Me.TextBox2.TabIndex = 3
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(101, 30)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(100, 20)
		Me.TextBox1.TabIndex = 2
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Roboto Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(293, 30)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(64, 15)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "DATA FIM"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Roboto Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(13, 30)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(81, 15)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "DATA INICIO"
		'
		'RichTextBox1
		'
		Me.RichTextBox1.BackColor = System.Drawing.Color.Silver
		Me.RichTextBox1.Font = New System.Drawing.Font("Roboto Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RichTextBox1.ForeColor = System.Drawing.Color.Black
		Me.RichTextBox1.Location = New System.Drawing.Point(0, 12)
		Me.RichTextBox1.Name = "RichTextBox1"
		Me.RichTextBox1.Size = New System.Drawing.Size(497, 43)
		Me.RichTextBox1.TabIndex = 3
		Me.RichTextBox1.Text = "ESTA OPERAÇÃO ELIMINA OS REGISTROS ENTRE AS DATAS ESPECIFICADAS(INCLUSIVE)"
		'
		'Button8
		'
		Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button8.ForeColor = System.Drawing.Color.White
		Me.Button8.Location = New System.Drawing.Point(419, 139)
		Me.Button8.Name = "Button8"
		Me.Button8.Size = New System.Drawing.Size(65, 23)
		Me.Button8.TabIndex = 36
		Me.Button8.Text = "EXCLUIR"
		Me.Button8.UseVisualStyleBackColor = False
		'
		'Apagar
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(496, 168)
		Me.Controls.Add(Me.Button8)
		Me.Controls.Add(Me.RichTextBox1)
		Me.Controls.Add(Me.GroupBox1)
		Me.Name = "Apagar"
		Me.Text = "APAGAR PERÍODO DE ABASTECIMENTO"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents RichTextBox1 As RichTextBox
	Friend WithEvents Button8 As Button
	Friend WithEvents ToolTip1 As ToolTip
End Class
