<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Movimientos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnClose = New Button()
        btnAdd = New Button()
        btnDelete = New Button()
        GroupBox1 = New GroupBox()
        tbmotivo = New TextBox()
        Label4 = New Label()
        tbNumber = New TextBox()
        Label3 = New Label()
        cbType = New ComboBox()
        Label2 = New Label()
        cbProductID = New ComboBox()
        Label1 = New Label()
        PictureBox7 = New PictureBox()
        btnSearch = New Button()
        GroupBox2 = New GroupBox()
        ComboBox2 = New ComboBox()
        Label6 = New Label()
        ComboBox1 = New ComboBox()
        Label5 = New Label()
        Label12 = New Label()
        ListView1 = New ListView()
        colID = New ColumnHeader()
        colProdID = New ColumnHeader()
        colType = New ColumnHeader()
        colNumber = New ColumnHeader()
        colNumberPrev = New ColumnHeader()
        colNumberNew = New ColumnHeader()
        colMotive = New ColumnHeader()
        colUser = New ColumnHeader()
        colReference = New ColumnHeader()
        colCreated_at = New ColumnHeader()
        printMovimiento = New Printing.PrintDocument()
        tbIdmovimiento = New TextBox()
        GroupBox3 = New GroupBox()
        btnPritMovimiento = New Button()
        Label8 = New Label()
        GroupBox1.SuspendLayout()
        CType(PictureBox7, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnClose
        ' 
        btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClose.Location = New Point(1193, 0)
        btnClose.Margin = New Padding(3, 2, 3, 2)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(35, 30)
        btnClose.TabIndex = 0
        btnClose.Text = "X"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnAdd
        ' 
        btnAdd.Anchor = AnchorStyles.Left
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = SystemColors.Control
        btnAdd.Location = New Point(72, 572)
        btnAdd.Margin = New Padding(3, 2, 3, 2)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(92, 32)
        btnAdd.TabIndex = 1
        btnAdd.Text = "Agregar"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Anchor = AnchorStyles.Left
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = SystemColors.Control
        btnDelete.Location = New Point(340, 572)
        btnDelete.Margin = New Padding(3, 2, 3, 2)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(92, 32)
        btnDelete.TabIndex = 3
        btnDelete.Text = "Eliminar"
        btnDelete.UseVisualStyleBackColor = True
        btnDelete.Visible = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Left
        GroupBox1.Controls.Add(tbmotivo)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(tbNumber)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(cbType)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(cbProductID)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.ForeColor = SystemColors.Control
        GroupBox1.Location = New Point(56, 254)
        GroupBox1.Margin = New Padding(3, 2, 3, 2)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 2, 3, 2)
        GroupBox1.Size = New Size(365, 271)
        GroupBox1.TabIndex = 4
        GroupBox1.TabStop = False
        GroupBox1.Text = "Datos"
        ' 
        ' tbmotivo
        ' 
        tbmotivo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        tbmotivo.Location = New Point(150, 144)
        tbmotivo.Margin = New Padding(3, 2, 3, 2)
        tbmotivo.Multiline = True
        tbmotivo.Name = "tbmotivo"
        tbmotivo.Size = New Size(154, 62)
        tbmotivo.TabIndex = 13
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label4.AutoSize = True
        Label4.Location = New Point(10, 146)
        Label4.Name = "Label4"
        Label4.Size = New Size(45, 15)
        Label4.TabIndex = 12
        Label4.Text = "Motivo"
        ' 
        ' tbNumber
        ' 
        tbNumber.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        tbNumber.Location = New Point(150, 100)
        tbNumber.Margin = New Padding(3, 2, 3, 2)
        tbNumber.Name = "tbNumber"
        tbNumber.Size = New Size(154, 23)
        tbNumber.TabIndex = 11
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(10, 104)
        Label3.Name = "Label3"
        Label3.Size = New Size(55, 15)
        Label3.TabIndex = 10
        Label3.Text = "Cantidad"
        ' 
        ' cbType
        ' 
        cbType.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        cbType.FormattingEnabled = True
        cbType.Location = New Point(150, 62)
        cbType.Margin = New Padding(3, 2, 3, 2)
        cbType.Name = "cbType"
        cbType.Size = New Size(154, 23)
        cbType.TabIndex = 9
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label2.AutoSize = True
        Label2.Location = New Point(10, 63)
        Label2.Name = "Label2"
        Label2.Size = New Size(31, 15)
        Label2.TabIndex = 8
        Label2.Text = "Tipo"
        ' 
        ' cbProductID
        ' 
        cbProductID.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        cbProductID.FormattingEnabled = True
        cbProductID.Location = New Point(150, 20)
        cbProductID.Margin = New Padding(3, 2, 3, 2)
        cbProductID.Name = "cbProductID"
        cbProductID.Size = New Size(154, 23)
        cbProductID.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(10, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 15)
        Label1.TabIndex = 6
        Label1.Text = "Producto"
        ' 
        ' PictureBox7
        ' 
        PictureBox7.Image = My.Resources.Resources.loguito
        PictureBox7.Location = New Point(72, 55)
        PictureBox7.Margin = New Padding(3, 2, 3, 2)
        PictureBox7.Name = "PictureBox7"
        PictureBox7.Size = New Size(164, 100)
        PictureBox7.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox7.TabIndex = 7
        PictureBox7.TabStop = False
        ' 
        ' btnSearch
        ' 
        btnSearch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnSearch.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.ForeColor = SystemColors.Control
        btnSearch.Location = New Point(318, 34)
        btnSearch.Margin = New Padding(3, 2, 3, 2)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(92, 32)
        btnSearch.TabIndex = 8
        btnSearch.Text = "Buscar"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Anchor = AnchorStyles.Top
        GroupBox2.Controls.Add(ComboBox2)
        GroupBox2.Controls.Add(btnSearch)
        GroupBox2.Controls.Add(Label6)
        GroupBox2.Controls.Add(ComboBox1)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.ForeColor = SystemColors.Control
        GroupBox2.Location = New Point(732, 55)
        GroupBox2.Margin = New Padding(3, 2, 3, 2)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 2, 3, 2)
        GroupBox2.Size = New Size(420, 100)
        GroupBox2.TabIndex = 9
        GroupBox2.TabStop = False
        GroupBox2.Text = "Consultas"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(149, 62)
        ComboBox2.Margin = New Padding(3, 2, 3, 2)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(154, 23)
        ComboBox2.TabIndex = 11
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label6.AutoSize = True
        Label6.Location = New Point(8, 63)
        Label6.Name = "Label6"
        Label6.Size = New Size(31, 15)
        Label6.TabIndex = 10
        Label6.Text = "Tipo"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(149, 20)
        ComboBox1.Margin = New Padding(3, 2, 3, 2)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(154, 23)
        ComboBox1.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(8, 22)
        Label5.Name = "Label5"
        Label5.Size = New Size(56, 15)
        Label5.TabIndex = 8
        Label5.Text = "Producto"
        ' 
        ' Label12
        ' 
        Label12.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 12F)
        Label12.ForeColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        Label12.Location = New Point(1088, 3)
        Label12.Name = "Label12"
        Label12.Size = New Size(101, 21)
        Label12.TabIndex = 17
        Label12.Text = "Movimientos"
        ' 
        ' ListView1
        ' 
        ListView1.Columns.AddRange(New ColumnHeader() {colID, colProdID, colType, colNumber, colNumberPrev, colNumberNew, colMotive, colUser, colReference, colCreated_at})
        ListView1.Location = New Point(576, 200)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(613, 524)
        ListView1.TabIndex = 18
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' colID
        ' 
        colID.Text = "ID"
        ' 
        ' colProdID
        ' 
        colProdID.Text = "Producto"
        ' 
        ' colType
        ' 
        colType.Text = "Tipo"
        ' 
        ' colNumber
        ' 
        colNumber.Text = "Cantidad"
        ' 
        ' colNumberPrev
        ' 
        colNumberPrev.Text = "Cantidad anterior"
        ' 
        ' colNumberNew
        ' 
        colNumberNew.Text = "Cantidad nueva"
        ' 
        ' colMotive
        ' 
        colMotive.Text = "Motivo"
        ' 
        ' colUser
        ' 
        colUser.Text = "Usuario"
        ' 
        ' colReference
        ' 
        colReference.Text = "Referencia"
        ' 
        ' colCreated_at
        ' 
        colCreated_at.Text = "Creado"
        ' 
        ' printMovimiento
        ' 
        ' 
        ' tbIdmovimiento
        ' 
        tbIdmovimiento.Location = New Point(162, 22)
        tbIdmovimiento.Margin = New Padding(3, 2, 3, 2)
        tbIdmovimiento.Name = "tbIdmovimiento"
        tbIdmovimiento.Size = New Size(137, 23)
        tbIdmovimiento.TabIndex = 19
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Anchor = AnchorStyles.Top
        GroupBox3.Controls.Add(btnPritMovimiento)
        GroupBox3.Controls.Add(tbIdmovimiento)
        GroupBox3.Controls.Add(Label8)
        GroupBox3.ForeColor = SystemColors.Control
        GroupBox3.Location = New Point(301, 55)
        GroupBox3.Margin = New Padding(3, 2, 3, 2)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(3, 2, 3, 2)
        GroupBox3.Size = New Size(333, 100)
        GroupBox3.TabIndex = 12
        GroupBox3.TabStop = False
        GroupBox3.Text = "Imprimir"
        ' 
        ' btnPritMovimiento
        ' 
        btnPritMovimiento.Anchor = AnchorStyles.Left
        btnPritMovimiento.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnPritMovimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnPritMovimiento.FlatStyle = FlatStyle.Flat
        btnPritMovimiento.ForeColor = SystemColors.Control
        btnPritMovimiento.Location = New Point(162, 55)
        btnPritMovimiento.Margin = New Padding(3, 2, 3, 2)
        btnPritMovimiento.Name = "btnPritMovimiento"
        btnPritMovimiento.Size = New Size(136, 32)
        btnPritMovimiento.TabIndex = 20
        btnPritMovimiento.Text = "Comprobante"
        btnPritMovimiento.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label8.AutoSize = True
        Label8.Location = New Point(8, 22)
        Label8.Name = "Label8"
        Label8.Size = New Size(86, 15)
        Label8.TabIndex = 8
        Label8.Text = "ID Movimiento"
        ' 
        ' Movimientos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(49), CByte(33), CByte(70))
        ClientSize = New Size(1229, 662)
        Controls.Add(GroupBox3)
        Controls.Add(ListView1)
        Controls.Add(Label12)
        Controls.Add(GroupBox2)
        Controls.Add(PictureBox7)
        Controls.Add(GroupBox1)
        Controls.Add(btnDelete)
        Controls.Add(btnAdd)
        Controls.Add(btnClose)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(3, 2, 3, 2)
        Name = "Movimientos"
        Text = "prueba"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(PictureBox7, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tbNumber As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbType As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbProductID As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbmotivo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelRef As Label
    Friend WithEvents tbReference As TextBox
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents colID As ColumnHeader
    Friend WithEvents colProdID As ColumnHeader
    Friend WithEvents colType As ColumnHeader
    Friend WithEvents colNumber As ColumnHeader
    Friend WithEvents colNumberPrev As ColumnHeader
    Friend WithEvents colNumberNew As ColumnHeader
    Friend WithEvents colMotive As ColumnHeader
    Friend WithEvents colUser As ColumnHeader
    Friend WithEvents colReference As ColumnHeader
    Friend WithEvents colCreated_at As ColumnHeader
    Friend WithEvents printMovimiento As Printing.PrintDocument
    Friend WithEvents tbIdmovimiento As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnPritMovimiento As Button
    Friend WithEvents Label8 As Label
    ''Friend WithEvents tbReference As TextBox
End Class
