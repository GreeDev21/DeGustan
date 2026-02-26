Imports System.IO
Imports MySql.Data.MySqlClient

Public Class Usuarios
    Dim imagenBytes As Byte()

    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conectar()
        cargarUsers("username")
        CargarRoles()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Cargar usuarios en el DataGridView
    Private Sub cargarUsers(orden As String)
        Try
            Dim columnasValidas As String() = {
                "id", "username", "nombre", "apellido", "email",
                "activo", "ultimo_login", "created_at", "updated_at"
            }

            If Not columnasValidas.Contains(orden) Then
                MsgBox("Columna de orden inválida: " & orden)
                Exit Sub
            End If

            conexion.Open()
            Dim query As String = "SELECT * FROM usuarios ORDER BY " & orden
            Dim cmd As New MySqlCommand(query, conexion)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            dtgridUsers.Columns.Clear()
            dtgridUsers.Rows.Clear()

            dtgridUsers.Columns.Add("id", "ID")
            dtgridUsers.Columns.Add("username", "Usuario")
            dtgridUsers.Columns.Add("nombre", "Nombre")
            dtgridUsers.Columns.Add("apellido", "Apellido")
            dtgridUsers.Columns.Add("email", "Email")
            dtgridUsers.Columns.Add("activo", "Activo")
            dtgridUsers.Columns.Add("ultimo_login", "Último login")
            dtgridUsers.Columns.Add("created_at", "Creado")
            dtgridUsers.Columns.Add("updated_at", "Actualizado")

            If reader.HasRows Then
                Do While reader.Read()
                    dtgridUsers.Rows.Add(
                        reader("id").ToString(),
                        reader("username").ToString(),
                        reader("nombre").ToString(),
                        reader("apellido").ToString(),
                        reader("email").ToString(),
                        If(Convert.ToBoolean(reader("activo")), "Sí", "No"),
                        If(IsDBNull(reader("ultimo_login")), "", reader("ultimo_login").ToString()),
                        reader("created_at").ToString(),
                        reader("updated_at").ToString()
                    )
                Loop
            End If

            reader.Close()
            conexion.Close()
        Catch ex As Exception
            MsgBox("Error al cargar usuarios: " & ex.Message)
            conexion.Close()
        End Try
    End Sub

    ' Cargar roles en el ComboBox
    Sub CargarRoles()
        cbrol.Items.Clear()
        Dim cmd As New MySqlCommand("SELECT id, nombre FROM roles", conexion)
        Try
            conexion.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                cbrol.Items.Add(New With {.Text = reader("nombre").ToString(), .Value = reader("id")})
            End While
            conexion.Close()
            cbrol.DisplayMember = "Text"
            cbrol.ValueMember = "Value"
        Catch ex As Exception
            MsgBox("Error al cargar roles: " & ex.Message)
            conexion.Close()
        End Try
    End Sub

    ' Seleccionar imagen desde archivo
    Private Sub SeleccionarImagen()
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Imágenes|*.jpg;*.png;*.bmp"
        If ofd.ShowDialog() = DialogResult.OK Then
            pictureUser.Image = Image.FromFile(ofd.FileName)
            imagenBytes = File.ReadAllBytes(ofd.FileName)
        End If
    End Sub

    ' Cargar imagen desde la base de datos
    Sub CargarImagenUsuario(id As Integer)
        Dim cmd As New MySqlCommand("SELECT imagen FROM usuarios WHERE id = @id", conexion)
        cmd.Parameters.AddWithValue("@id", id)
        Try
            conexion.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() AndAlso Not IsDBNull(reader("imagen")) Then
                Dim bytes As Byte() = CType(reader("imagen"), Byte())
                Dim ms As New MemoryStream(bytes)
                pictureUser.Image = Image.FromStream(ms)
                imagenBytes = bytes
            Else
                pictureUser.Image = Nothing
                imagenBytes = Nothing
            End If
            reader.Close()
            conexion.Close()
        Catch ex As Exception
            MsgBox("Error al cargar imagen: " & ex.Message)
            conexion.Close()
        End Try
    End Sub

    ' Guardar nuevo usuario
    Sub GuardarUsuario()
        Dim cmd As New MySqlCommand("INSERT INTO usuarios (username, password, email, nombre, apellido, rol_id, activo, imagen) VALUES (@user, @pass, @email, @nombre, @apellido, @rol, @activo, @imagen)", conexion)
        cmd.Parameters.AddWithValue("@user", tbUsername.Text)
        cmd.Parameters.AddWithValue("@pass", tbPassword.Text)
        cmd.Parameters.AddWithValue("@email", tbEmail.Text)
        cmd.Parameters.AddWithValue("@nombre", tbname.Text)
        cmd.Parameters.AddWithValue("@apellido", tbapellido.Text)
        cmd.Parameters.AddWithValue("@rol", CType(cbrol.SelectedItem, Object).Value)
        cmd.Parameters.AddWithValue("@activo", If(cbactivo.Checked, 1, 0))
        cmd.Parameters.AddWithValue("@imagen", imagenBytes)

        Try
            conexion.Open()
            cmd.ExecuteNonQuery()
            conexion.Close()
            MessageBox.Show("Usuario guardado correctamente.")
            cargarUsers("username")
        Catch ex As Exception
            MsgBox("Error al guardar usuario: " & ex.Message)
            conexion.Close()
        End Try
    End Sub

    ' Botón para agregar usuario
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If tbUsername.Text = "" Or tbname.Text = "" Or tbapellido.Text = "" Or cbrol.SelectedIndex = -1 Then
            MessageBox.Show("Faltan datos obligatorios.")
            Exit Sub
        End If
        GuardarUsuario()
    End Sub

    ' Botón para cargar imagen
    Private Sub btnCargarImagen_Click(sender As Object, e As EventArgs) Handles btnCargarImagen.Click
        SeleccionarImagen()
    End Sub

    ' Al hacer clic en una fila del DataGridView, completar el formulario
    Private Sub dtgridUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtgridUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim fila As DataGridViewRow = dtgridUsers.Rows(e.RowIndex)
            tbUsername.Text = fila.Cells("username").Value.ToString()
            tbname.Text = fila.Cells("nombre").Value.ToString()
            tbapellido.Text = fila.Cells("apellido").Value.ToString()
            tbEmail.Text = fila.Cells("email").Value.ToString()
            dtgridUsers.Rows(e.RowIndex).Selected = True


            'Dim rol As Integer = Convert.ToInt32(fila.Cells("rol_id").Value)

            'If (rol = 1) Then
            'cbrol.Text = "admin"
            'End If


            cbactivo.Checked = (fila.Cells("activo").Value.ToString() = "Sí")
            ' Cargar imagen desde la BD
            Dim idUsuario As Integer = Convert.ToInt32(fila.Cells("id").Value)
            CargarImagenUsuario(idUsuario)

            ' Seleccionar el rol en el ComboBox
            For Each item In cbrol.Items
                If CType(item, Object).Text = fila.Cells("username").Value.ToString() Then
                    cbrol.SelectedItem = item
                    Exit For
                End If
            Next
        End If
    End Sub



    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click

        ' Validar selección
        If dtgridUsers.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccioná un usuario de la lista para modificar.")
            Exit Sub
        End If

        ' Validar campos
        If tbUsername.Text = "" Or tbname.Text = "" Or tbapellido.Text = "" Or cbrol.SelectedIndex = -1 Then
            MessageBox.Show("Faltan datos obligatorios.")
            Exit Sub
        End If

        ' Obtener ID del usuario seleccionado
        Dim idUsuario As Integer = Convert.ToInt32(dtgridUsers.SelectedRows(0).Cells("id").Value)

        Dim cmd As MySqlCommand

        If tbPassword.Text = "" Then
            cmd = New MySqlCommand("UPDATE usuarios SET username=@user, email=@email, nombre=@nombre, apellido=@apellido, rol_id=@rol, activo=@activo, imagen=@imagen WHERE id=@id", conexion)
        Else
            cmd = New MySqlCommand("UPDATE usuarios SET username=@user, password=@pass, email=@email, nombre=@nombre, apellido=@apellido, rol_id=@rol, activo=@activo, imagen=@imagen WHERE id=@id", conexion)
            cmd.Parameters.AddWithValue("@pass", tbPassword.Text)
        End If

        ' Parámetros comunes
        cmd.Parameters.AddWithValue("@user", tbUsername.Text)
        cmd.Parameters.AddWithValue("@email", tbEmail.Text)
        cmd.Parameters.AddWithValue("@nombre", tbname.Text)
        cmd.Parameters.AddWithValue("@apellido", tbapellido.Text)
        cmd.Parameters.AddWithValue("@rol", CType(cbrol.SelectedItem, Object).Value)
        cmd.Parameters.AddWithValue("@activo", If(cbactivo.Checked, 1, 0))
        cmd.Parameters.AddWithValue("@imagen", imagenBytes)
        cmd.Parameters.AddWithValue("@id", idUsuario)

        ' Ejecutar
        Try
            conexion.Open()
            cmd.ExecuteNonQuery()
            conexion.Close()
            MessageBox.Show("Usuario modificado correctamente.")
            cargarUsers("username")
        Catch ex As Exception
            MsgBox("Error al modificar usuario: " & ex.Message)
            conexion.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Validar selección
        If dtgridUsers.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccioná un usuario de la lista para eliminar o desactivar.")
            Exit Sub
        End If

        ' Obtener ID del usuario
        Dim idUsuario As Integer = Convert.ToInt32(dtgridUsers.SelectedRows(0).Cells("id").Value)
        Dim nombreUsuario As String = dtgridUsers.SelectedRows(0).Cells("username").Value.ToString()

        ' Preguntar qué hacer
        Dim respuesta As DialogResult = MessageBox.Show(
        $"¿Querés desactivar al usuario '{nombreUsuario}' (pasarlo a inactivo) o eliminarlo definitivamente?" & vbCrLf &
        "Sí = Desactivar / No = Eliminar / Cancelar = Nada",
        "Confirmación de eliminación",
        MessageBoxButtons.YesNoCancel,
        MessageBoxIcon.Question
    )

        If respuesta = DialogResult.Yes Then
            ' Desactivar usuario
            Dim cmd As New MySqlCommand("UPDATE usuarios SET activo = 0 WHERE id = @id", conexion)
            cmd.Parameters.AddWithValue("@id", idUsuario)
            Try
                conexion.Open()
                cmd.ExecuteNonQuery()
                conexion.Close()
                MessageBox.Show("Usuario desactivado correctamente.")
                cargarUsers("username")
            Catch ex As Exception
                MsgBox("Error al desactivar usuario: " & ex.Message)
                conexion.Close()
            End Try

        ElseIf respuesta = DialogResult.No Then
            ' Eliminar usuario
            Dim cmd As New MySqlCommand("DELETE FROM usuarios WHERE id = @id", conexion)
            cmd.Parameters.AddWithValue("@id", idUsuario)
            Try
                conexion.Open()
                cmd.ExecuteNonQuery()
                conexion.Close()
                MessageBox.Show("Usuario eliminado definitivamente.")
                cargarUsers("username")
            Catch ex As Exception
                MsgBox("Error al eliminar usuario: " & ex.Message)
                conexion.Close()
            End Try

        Else
            ' Cancelado
            MessageBox.Show("Operación cancelada.")
        End If
    End Sub
    Private Sub PrintDocUser_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocUser.PrintPage
        Try
            ' Fuentes institucionales
            Dim fuenteTitulo As New Font("Segoe UI", 14, FontStyle.Bold)
            Dim fuenteNormal As New Font("Segoe UI", 10)
            Dim fuenteCampo As New Font("Segoe UI", 11, FontStyle.Bold)

            ' Marco con borde gris
            Dim rect As New Rectangle(50, 50, 400, 250)
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rect)
            e.Graphics.DrawRectangle(New Pen(Color.Gray, 2), rect)



            Dim rutaLogo As String = Path.Combine(Application.StartupPath, "Resources\loho.jpg")

            If File.Exists(rutaLogo) Then
                Try
                    Dim logo As Image = Image.FromFile(rutaLogo)
                    e.Graphics.DrawImage(logo, 60, 60, 60, 60)
                Catch ex As Exception
                    MsgBox("Error al cargar logo: " & ex.Message)
                End Try
            Else
                MsgBox("Logo no encontrado en: " & rutaLogo)
            End If

            ' Logo institucional
            'Dim rutaLogo As String = Path.Combine(Application.StartupPath, "Resources\loho.jpg")
            'If File.Exists(rutaLogo) Then
            'Dim logo As Image = Image.FromFile(rutaLogo)
            'e.Graphics.DrawImage(logo, 60, 60, 60, 60)
            'End If

            ' Imagen del usuario
            If pictureUser.Image IsNot Nothing Then
                e.Graphics.DrawImage(pictureUser.Image, 330, 75, 80, 80)
            End If

            ' Título centrado
            e.Graphics.DrawString("CREDENCIAL DE USUARIO", fuenteTitulo, Brushes.DarkSlateBlue, 120, 45)

            ' Datos
            Dim xCampo As Integer = 70
            Dim xValor As Integer = 160
            Dim yBase As Integer = 140
            Dim salto As Integer = 22

            e.Graphics.DrawString("Usuario:", fuenteCampo, Brushes.Black, xCampo, yBase)
            e.Graphics.DrawString(tbUsername.Text, fuenteNormal, Brushes.Black, xValor, yBase)

            e.Graphics.DrawString("Nombre:", fuenteCampo, Brushes.Black, xCampo, yBase + salto)
            e.Graphics.DrawString(tbname.Text & " " & tbapellido.Text, fuenteNormal, Brushes.Black, xValor, yBase + salto)

            e.Graphics.DrawString("Email:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 2)
            e.Graphics.DrawString(tbEmail.Text, fuenteNormal, Brushes.Black, xValor, yBase + salto * 2)

            e.Graphics.DrawString("Rol:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 3)
            If cbrol.SelectedItem IsNot Nothing Then
                e.Graphics.DrawString(cbrol.Text.ToString(), fuenteNormal, Brushes.Black, xValor, yBase + salto * 3)
            End If

            e.Graphics.DrawString("Estado:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 4)
            e.Graphics.DrawString(If(cbactivo.Checked, "Activo", "Inactivo"), fuenteNormal, Brushes.Black, xValor, yBase + salto * 4)

            ' Pie institucional
            e.Graphics.DrawString("Emitido el: " & Date.Now.ToShortDateString, fuenteNormal, Brushes.Gray, xCampo, yBase + salto * 5)
            e.Graphics.DrawString("DeGustan", fuenteNormal, Brushes.DarkSlateGray, xValor, yBase + salto * 6)

        Catch ex As Exception
            MsgBox("Error al imprimir credencial: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCredencial_Click(sender As Object, e As EventArgs) Handles btnCredencial.Click
        PrintDocUser.Print()

    End Sub


End Class