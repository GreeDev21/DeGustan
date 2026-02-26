Imports MySql.Data.MySqlClient

Public Class Categorias
    ' Subrutina para limpiar los campos del formulario
    Private Sub clearForm()
        tbName.Text = ""
        rtbDesc.Text = ""
        cmbActive.SelectedIndex = 0
    End Sub

    Private Sub cargarCategorias(orden As String)
        ' Subrutina para cargar las categorías en el ListView
        Try
            conexion.Open()

            Dim query = "SELECT * FROM categorias order by " & orden
            Dim cmd = New MySqlCommand(query, conexion)
            Dim reader = cmd.ExecuteReader()

            If reader.HasRows Then
                lvDataGridCat.Items.Clear()

                Dim lvItem As New ListViewItem

                Do While reader.Read()
                    lvItem = lvDataGridCat.Items.Add(reader("id"))
                    lvItem.SubItems.Add(reader("nombre"))
                    lvItem.SubItems.Add(reader("descripcion"))
                    lvItem.SubItems.Add(If(reader("activo") = True, "Sí", "No"))
                    lvItem.SubItems.Add(reader("created_at"))
                    lvItem.SubItems.Add(reader("updated_at"))
                Loop
            End If

            reader.Close()
            conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AjustarColumnas()
        ' Verificamos que haya columnas
        If lvDataGridCat.Columns.Count = 0 Then Exit Sub

        ' Defino la columna excluida
        Dim colExcluded As Integer = 0 ' Valor del indice de la columna a excluir
        lvDataGridCat.Columns(colExcluded).Width = 0 ' Le doy un ancho de 0 para ocultarla

        ' Obtenemos el ancho total del ListView
        Dim anchoTotal As Integer = lvDataGridCat.ClientSize.Width
        Dim anchoRest As Integer = anchoTotal - lvDataGridCat.Columns(colExcluded).Width

        ' Calculo cuantas columnas quedan para ajustar
        Dim numColsAjustar As Integer = lvDataGridCat.Columns.Count - 1

        If numColsAjustar <= 0 Then Exit Sub

        ' Calculamos el ancho de cada columna
        Dim anchoColumna As Integer = anchoTotal \ numColsAjustar

        ' Ajustamos cada columna
        For i As Integer = 0 To lvDataGridCat.Columns.Count - 1
            If i <> colExcluded Then
                lvDataGridCat.Columns(i).Width = anchoColumna
            End If
        Next
    End Sub

    Private Sub CargarSugerencias(ByVal buscar As String)
        'Private Sub busquedaAsistida(ByVal buscar As String)
        If buscar = "" Then
            lvDataGridCat.Items.Clear()
            Exit Sub
        End If

        Try
            conexion.Open()

            Dim query = "SELECT * FROM categorias WHERE nombre LIKE @nombre ORDER BY id"
            Dim cmd = New MySqlCommand(query, conexion) ' Uso Using para asegurar el cierre del comando
            cmd.Parameters.AddWithValue("@nombre", "%" & buscar & "%")
            Dim reader = cmd.ExecuteReader() ' Uso Using para asegurar el cierre del reader
            If reader.HasRows Then
                lvDataGridCat.Items.Clear()
                Dim lvItem As New ListViewItem
                Do While reader.Read()
                    lvItem = lvDataGridCat.Items.Add(reader("id"))
                    lvItem.SubItems.Add(reader("nombre"))
                    lvItem.SubItems.Add(reader("descripcion"))
                    lvItem.SubItems.Add(If(reader("activo") = True, "Sí", "No"))
                    lvItem.SubItems.Add(reader("created_at"))
                    lvItem.SubItems.Add(reader("updated_at"))
                Loop
            Else
                MsgBox("No se encontraron categorías con ese nombre.", MsgBoxStyle.Information, "Sin resultados")
                cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
            End If
            reader.Close()
            conexion.Close()
            clearForm()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Categorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Llamo a la subrutina para conectar a la base de datos
        Conectar()

        ' Configuro el textbox para busqueda auto asistida
        tbNameQuery.AutoCompleteMode = AutoCompleteMode.Suggest
        tbNameQuery.AutoCompleteSource = AutoCompleteSource.CustomSource

        ' Configuro el ComboBox para el estado activo/inactivo
        cmbActive.Items.Add("Activo")
        cmbActive.Items.Add("Inactivo")
        cmbActive.SelectedIndex = 0 ' Selecciono "Activo" por defect

        cargarCategorias("id")  ' Cargo las categorías ordenadas por ID

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        clearForm()
        cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
        tbNameQuery.Text = ""


    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If tbName.Text.Trim() = "" Or rtbDesc.Text.Trim() = "" Then
            MsgBox("Por favor, complete todos los campos.", MsgBoxStyle.Exclamation, "Campos incompletos")
            Exit Sub
        End If


        Try
            conexion.Open()
            Dim nombre As String = tbName.Text.Trim()
            Dim descripcion As String = rtbDesc.Text.Trim()
            Dim activo As Boolean
            If cmbActive.SelectedIndex = 0 Then
                activo = True
            Else
                activo = False
            End If

            Dim cmd As New MySqlCommand
            cmd.Connection = conexion
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO categorias (nombre, descripcion, activo, created_at, updated_at) VALUES (@nombre, @descripcion, @activo, NOW(), NOW())"
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@descripcion", descripcion)
            cmd.Parameters.AddWithValue("@activo", activo)

            cmd.ExecuteNonQuery()

            clearForm()
            conexion.Close()
            cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
            'CargarSugerencias()
        Catch ex As Exception
            MsgBox("Error al conectar a la base de datos: " & ex.Message, MsgBoxStyle.Critical, "Error de conexión")
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lvDataGridCat.SelectedItems.Count = 0 Then ' verifico que haya una fila seleccionada
            MsgBox("Por favor, seleccione una categoría para eliminar.", MsgBoxStyle.Exclamation, "Selección requerida")
            Exit Sub
        End If

        Dim selectedId As Integer = Convert.ToInt32(lvDataGridCat.SelectedItems(0).Text) ' convierto el ID seleccionado a Integer
        Dim confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar la categoría seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            Try
                conexion.Open()
                Dim cmd As New MySqlCommand
                cmd.Connection = conexion
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "DELETE FROM categorias WHERE id = @id"
                cmd.Parameters.AddWithValue("@id", selectedId)
                cmd.ExecuteNonQuery()
                conexion.Close()
                clearForm()
                cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
                'CargarSugerencias()
            Catch ex As Exception
                MsgBox("Error al conectar a la base de datos: " & ex.Message, MsgBoxStyle.Critical, "Error de conexión")
            End Try
        End If

    End Sub

    Private Sub lvDataGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvDataGridCat.SelectedIndexChanged
        ' Muestro los datos de la fila seleccionada en los campos del formulario
        tbName.Text = lvDataGridCat.Items(lvDataGridCat.FocusedItem.Index).SubItems(1).Text
        rtbDesc.Text = lvDataGridCat.Items(lvDataGridCat.FocusedItem.Index).SubItems(2).Text
        If lvDataGridCat.Items(lvDataGridCat.FocusedItem.Index).SubItems(3).Text = "Sí" Then
            cmbActive.SelectedIndex = 0
        Else
            cmbActive.SelectedIndex = 1
        End If
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        If lvDataGridCat.SelectedItems.Count = 0 Then ' verifico que haya una fila seleccionada
            MsgBox("Por favor, seleccione una categoría para eliminar.", MsgBoxStyle.Exclamation, "Selección requerida")
            Exit Sub
        End If

        Dim selectedId As Integer = Convert.ToInt32(lvDataGridCat.SelectedItems(0).Text) ' convierto el ID seleccionado a Integer

        If tbName.Text.Trim() = "" Or rtbDesc.Text.Trim() = "" Then
            MsgBox("Por favor, complete todos los campos.", MsgBoxStyle.Exclamation, "Campos incompletos")
            Exit Sub
        End If

        Try
            conexion.Open()
            Dim nombre As String = tbName.Text.Trim()
            Dim descripcion As String = rtbDesc.Text.Trim()
            Dim activo As Boolean
            If cmbActive.SelectedIndex = 0 Then
                activo = True
            Else
                activo = False
            End If
            Dim cmd As New MySqlCommand
            cmd.Connection = conexion
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE categorias SET nombre = @nombre, descripcion = @descripcion, activo = @activo, updated_at = NOW() WHERE id = @id"
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@descripcion", descripcion)
            cmd.Parameters.AddWithValue("@activo", activo)
            cmd.Parameters.AddWithValue("@id", selectedId)
            cmd.ExecuteNonQuery()
            clearForm()
            conexion.Close()
            cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
            'CargarSugerencias()
        Catch ex As Exception
            MsgBox("Error al conectar a la base de datos: " & ex.Message, MsgBoxStyle.Critical, "Error de conexión")
        End Try

    End Sub
    Private Sub tbNameQuery_TextChanged(sender As Object, e As EventArgs) Handles tbNameQuery.TextChanged
        If tbNameQuery.Text.Trim() = "" Then
            lvDataGridCat.Items.Clear()
            cargarCategorias("id")  ' Recargo las categorías ordenadas por ID
            Exit Sub

        End If
        CargarSugerencias(tbNameQuery.Text)
    End Sub

    Private Sub Categorias_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        AjustarColumnas()
    End Sub
End Class