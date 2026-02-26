<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Categorias
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        btnClose = New Button()
        PictureBox7 = New PictureBox()
        GroupBox1 = New GroupBox()
        rtbDesc = New RichTextBox()
        cmbActive = New ComboBox()
        tbName = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        btnDelete = New Button()
        btnNew = New Button()
        btnAdd = New Button()
        GroupBox2 = New GroupBox()
        btnSearch = New Button()
        tbNameQuery = New TextBox()
        Label8 = New Label()
        lvDataGridCat = New ListView()
        colID = New ColumnHeader()
        colName = New ColumnHeader()
        colDescription = New ColumnHeader()
        colActive = New ColumnHeader()
        colCreated = New ColumnHeader()
        colUpdated = New ColumnHeader()
        btnModify = New Button()
        tmrSearch = New Timer(components)
        Label12 = New Label()
        CType(PictureBox7, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnClose
        ' 
        btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClose.Location = New Point(1193, 2)
        btnClose.Margin = New Padding(3, 2, 3, 2)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(35, 30)
        btnClose.TabIndex = 15
        btnClose.Text = "X"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' PictureBox7
        ' 
        PictureBox7.Image = My.Resources.Resources.loguito
        PictureBox7.Location = New Point(163, 48)
        PictureBox7.Margin = New Padding(3, 2, 3, 2)
        PictureBox7.Name = "PictureBox7"
        PictureBox7.Size = New Size(164, 100)
        PictureBox7.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox7.TabIndex = 20
        PictureBox7.TabStop = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Left
        GroupBox1.Controls.Add(rtbDesc)
        GroupBox1.Controls.Add(cmbActive)
        GroupBox1.Controls.Add(tbName)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.ForeColor = SystemColors.Control
        GroupBox1.Location = New Point(60, 272)
        GroupBox1.Margin = New Padding(3, 2, 3, 2)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 2, 3, 2)
        GroupBox1.Size = New Size(360, 210)
        GroupBox1.TabIndex = 19
        GroupBox1.TabStop = False
        GroupBox1.Text = "Datos"
        ' 
        ' rtbDesc
        ' 
        rtbDesc.Location = New Point(150, 51)
        rtbDesc.Name = "rtbDesc"
        rtbDesc.Size = New Size(154, 96)
        rtbDesc.TabIndex = 17
        rtbDesc.Text = ""
        ' 
        ' cmbActive
        ' 
        cmbActive.FormattingEnabled = True
        cmbActive.Location = New Point(150, 157)
        cmbActive.Margin = New Padding(3, 2, 3, 2)
        cmbActive.Name = "cmbActive"
        cmbActive.Size = New Size(154, 23)
        cmbActive.TabIndex = 16
        ' 
        ' tbName
        ' 
        tbName.Location = New Point(150, 20)
        tbName.Margin = New Padding(3, 2, 3, 2)
        tbName.Name = "tbName"
        tbName.Size = New Size(154, 23)
        tbName.TabIndex = 14
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(10, 160)
        Label3.Name = "Label3"
        Label3.Size = New Size(41, 15)
        Label3.TabIndex = 10
        Label3.Text = "Activo"
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label2.AutoSize = True
        Label2.Location = New Point(10, 63)
        Label2.Name = "Label2"
        Label2.Size = New Size(69, 15)
        Label2.TabIndex = 8
        Label2.Text = "Descripción"
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(10, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(51, 15)
        Label1.TabIndex = 6
        Label1.Text = "Nombre"
        ' 
        ' btnDelete
        ' 
        btnDelete.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = SystemColors.Control
        btnDelete.Location = New Point(194, 671)
        btnDelete.Margin = New Padding(3, 2, 3, 2)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(92, 32)
        btnDelete.TabIndex = 18
        btnDelete.Text = "Eliminar"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnNew.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnNew.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnNew.FlatStyle = FlatStyle.Flat
        btnNew.ForeColor = SystemColors.Control
        btnNew.Location = New Point(194, 596)
        btnNew.Margin = New Padding(3, 2, 3, 2)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(92, 32)
        btnNew.TabIndex = 17
        btnNew.Text = "Nuevo"
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' btnAdd
        ' 
        btnAdd.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = SystemColors.Control
        btnAdd.Location = New Point(60, 596)
        btnAdd.Margin = New Padding(3, 2, 3, 2)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(92, 32)
        btnAdd.TabIndex = 16
        btnAdd.Text = "Agregar"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Anchor = AnchorStyles.Top
        GroupBox2.Controls.Add(btnSearch)
        GroupBox2.Controls.Add(tbNameQuery)
        GroupBox2.Controls.Add(Label8)
        GroupBox2.ForeColor = SystemColors.Control
        GroupBox2.Location = New Point(642, 54)
        GroupBox2.Margin = New Padding(3, 2, 3, 2)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 2, 3, 2)
        GroupBox2.Size = New Size(420, 58)
        GroupBox2.TabIndex = 21
        GroupBox2.TabStop = False
        GroupBox2.Text = "Consultas"
        ' 
        ' btnSearch
        ' 
        btnSearch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnSearch.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.ForeColor = SystemColors.Control
        btnSearch.Location = New Point(299, 15)
        btnSearch.Margin = New Padding(3, 2, 3, 2)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(94, 35)
        btnSearch.TabIndex = 8
        btnSearch.Text = "Buscar"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' tbNameQuery
        ' 
        tbNameQuery.Location = New Point(106, 19)
        tbNameQuery.Margin = New Padding(3, 2, 3, 2)
        tbNameQuery.Name = "tbNameQuery"
        tbNameQuery.Size = New Size(154, 23)
        tbNameQuery.TabIndex = 12
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label8.AutoSize = True
        Label8.Location = New Point(8, 22)
        Label8.Name = "Label8"
        Label8.Size = New Size(51, 15)
        Label8.TabIndex = 8
        Label8.Text = "Nombre"
        ' 
        ' lvDataGridCat
        ' 
        lvDataGridCat.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lvDataGridCat.Columns.AddRange(New ColumnHeader() {colID, colName, colDescription, colActive, colCreated, colUpdated})
        lvDataGridCat.FullRowSelect = True
        lvDataGridCat.Location = New Point(537, 197)
        lvDataGridCat.Name = "lvDataGridCat"
        lvDataGridCat.Size = New Size(639, 515)
        lvDataGridCat.TabIndex = 22
        lvDataGridCat.UseCompatibleStateImageBehavior = False
        lvDataGridCat.View = View.Details
        ' 
        ' colID
        ' 
        colID.Text = "id"
        colID.Width = 0
        ' 
        ' colName
        ' 
        colName.Text = "Nombre"
        colName.TextAlign = HorizontalAlignment.Center
        ' 
        ' colDescription
        ' 
        colDescription.Text = "Descripcion"
        colDescription.TextAlign = HorizontalAlignment.Center
        ' 
        ' colActive
        ' 
        colActive.Text = "Activo"
        colActive.TextAlign = HorizontalAlignment.Center
        ' 
        ' colCreated
        ' 
        colCreated.Text = "Creado"
        colCreated.TextAlign = HorizontalAlignment.Center
        ' 
        ' colUpdated
        ' 
        colUpdated.Text = "Actualizado"
        colUpdated.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnModify
        ' 
        btnModify.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnModify.FlatAppearance.BorderColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        btnModify.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(66), CByte(51), CByte(85))
        btnModify.FlatStyle = FlatStyle.Flat
        btnModify.ForeColor = SystemColors.Control
        btnModify.Location = New Point(328, 596)
        btnModify.Margin = New Padding(3, 2, 3, 2)
        btnModify.Name = "btnModify"
        btnModify.Size = New Size(92, 32)
        btnModify.TabIndex = 23
        btnModify.Text = "Modificar"
        btnModify.UseVisualStyleBackColor = True
        ' 
        ' tmrSearch
        ' 
        tmrSearch.Interval = 400
        ' 
        ' Label12
        ' 
        Label12.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 12F)
        Label12.ForeColor = Color.FromArgb(CByte(198), CByte(149), CByte(72))
        Label12.Location = New Point(1086, 9)
        Label12.Name = "Label12"
        Label12.Size = New Size(77, 21)
        Label12.TabIndex = 24
        Label12.Text = "Categoría"
        ' 
        ' Categorias
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(49), CByte(33), CByte(70))
        ClientSize = New Size(1229, 781)
        Controls.Add(Label12)
        Controls.Add(btnModify)
        Controls.Add(lvDataGridCat)
        Controls.Add(GroupBox2)
        Controls.Add(PictureBox7)
        Controls.Add(GroupBox1)
        Controls.Add(btnDelete)
        Controls.Add(btnNew)
        Controls.Add(btnAdd)
        Controls.Add(btnClose)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(3, 2, 3, 2)
        Name = "Categorias"
        Text = "Categorias"
        CType(PictureBox7, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents cmbActive As ComboBox
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents tbName As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tbNameQuery As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents lvDataGridCat As ListView
    Friend WithEvents colID As ColumnHeader
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents colDescription As ColumnHeader
    Friend WithEvents colActive As ColumnHeader
    Friend WithEvents colCreated As ColumnHeader
    Friend WithEvents colUpdated As ColumnHeader
    Friend WithEvents rtbDesc As RichTextBox
    Friend WithEvents btnModify As Button
    Friend WithEvents tmrSearch As Timer
    Friend WithEvents Label12 As Label
End Class
