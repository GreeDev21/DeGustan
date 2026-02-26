Imports System.Drawing.Printing
Imports System.IO
Imports MySql.Data.MySqlClient
'Imports Org.BouncyCastle.Utilities

Public Class Productos
    Private Pagina As Integer = 1
    Private printList As New List(Of String)()
    Private WithEvents printDoc As New PrintDocument()

    Private Function ImagenABytes(img As Image) As Byte()
        Using ms As New MemoryStream()
            img.Save(ms, Imaging.ImageFormat.Jpeg)
            Return ms.ToArray()
        End Using
    End Function
    Private Function BytesAImagen(bytes As Byte()) As Image
        Using ms As New MemoryStream(bytes)
            Return Image.FromStream(ms)
        End Using
    End Function

    Private Sub AjustarColumnas()
        ' Verificamos que haya columnas
        If lvProducts.Columns.Count = 0 Then Exit Sub

        ' Defino la columna excluida
        Dim colExcluded As Integer = 0 ' Valor del indice de la columna a excluir
        lvProducts.Columns(colExcluded).Width = 0 ' Le doy un ancho de 0 para ocultarla

        ' Obtenemos el ancho total del ListView
        Dim anchoTotal As Integer = lvProducts.ClientSize.Width
        Dim anchoRest As Integer = anchoTotal - lvProducts.Columns(colExcluded).Width

        ' Calculo cuantas columnas quedan para ajustar
        Dim numColsAjustar As Integer = lvProducts.Columns.Count - 1

        If numColsAjustar <= 0 Then Exit Sub

        ' Calculamos el ancho de cada columna
        Dim anchoColumna As Integer = anchoTotal \ numColsAjustar

        ' Ajustamos cada columna
        For i As Integer = 0 To lvProducts.Columns.Count - 1
            If i <> colExcluded Then
                lvProducts.Columns(i).Width = anchoColumna
            End If
        Next
    End Sub

    Sub clearForm()
        tbCodigo.Text = ""
        tbName.Text = ""
        rtbDescription.Text = ""
        cbCategory.SelectedIndex = -1
        cbSupplie.SelectedIndex = -1
        tbSell.Text = ""
        tbBuy.Text = ""
        tbMin.Text = ""
        tbCurrent.Text = ""

        tbQueryProduct.Text = ""
        tbQueryCategory.Text = ""
    End Sub
    Sub cargarCategoria(order As String) ' sub rutina para cargar las categorias en el combobox
        Try
            conexion.Open()

            Dim query = "SELECT nombre, id FROM categorias order by " & order
            Dim cmd = New MySqlCommand(query, conexion)
            Dim reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    cbCategory.Items.Add(reader("nombre").ToString())   ' Añado los nombres de las categorias al combobox
                End While
            End If

            reader.Close()
            conexion.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub cargarProveedor(order As String) ' sub rutina para cargar los proveedores en el combobox
        Try
            conexion.Open()

            Dim query = "SELECT nombre FROM proveedores order by " & order
            Dim cmd = New MySqlCommand(query, conexion)
            Dim reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    cbSupplie.Items.Add(reader("nombre").ToString())    ' Añado los nombres de los proveedores al combobox
                End While
            End If

            reader.Close()
            conexion.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cargaGrilla()
        Try
            conexion.Open()

            Dim query = "
            SELECT 
                p.id,
                p.codigo,
                p.nombre AS producto,
                p.descripcion,
                p.precio_compra,
                p.precio_venta,
                p.stock_actual,
                p.stock_minimo,
                p.activo,
                c.nombre AS categoria,
                pr.nombre AS proveedor,
                p.created_at,
                p.update_at,
                p.imagen
            FROM productos p
            LEFT JOIN categorias c ON p.categoria_id = c.id
            LEFT JOIN proveedores pr ON p.proveedor_id = pr.id
            ORDER BY p.activo DESC, p.id ASC;"

            Dim cmd = New MySqlCommand(query, conexion)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            lvProducts.Items.Clear()

            If reader.HasRows Then
                While reader.Read()
                    Dim item As New ListViewItem(reader("id").ToString())
                    item.SubItems.Add(reader("codigo").ToString())
                    item.SubItems.Add(reader("producto").ToString())
                    item.SubItems.Add(reader("descripcion").ToString())
                    item.SubItems.Add(reader("precio_compra").ToString())
                    item.SubItems.Add(reader("precio_venta").ToString())
                    item.SubItems.Add(reader("stock_actual").ToString())
                    item.SubItems.Add(reader("stock_minimo").ToString())
                    item.SubItems.Add(reader("categoria").ToString())
                    item.SubItems.Add(reader("proveedor").ToString())
                    item.SubItems.Add(reader("created_at").ToString())
                    item.SubItems.Add(reader("update_at").ToString())

                    ' Guardar imagen como Base64 si existe, para uso interno (no visual)
                    If Not IsDBNull(reader("imagen")) Then
                        Dim bytes() As Byte = CType(reader("imagen"), Byte())
                        Dim base64Img As String = Convert.ToBase64String(bytes)
                        item.Tag = bytes
                    Else
                        item.Tag = Nothing
                    End If

                    Dim isActive As Boolean = False
                    If Not IsDBNull(reader("activo")) Then
                        Try
                            isActive = Convert.ToBoolean(reader("activo"))
                        Catch
                            isActive = (reader("activo").ToString() = "1")
                        End Try
                    End If
                    item.SubItems.Add(If(isActive, "Sí", "No"))

                    lvProducts.Items.Add(item)
                End While
            End If

            reader.Close()
            conexion.Close()
        Catch ex As Exception
            MsgBox("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    Private Sub busquedaAsistida(ByVal buscar As String)
        If buscar = "" Then
            lvProducts.Items.Clear()
            Exit Sub
        End If

        Try
            conexion.Open()

            Dim query As String = "SELECT 
                                        p.id,
                                        p.codigo,
                                        p.nombre AS producto,
                                        p.descripcion,
                                        p.precio_compra,
                                        p.precio_venta,
                                        p.stock_actual,
                                        p.stock_minimo,
                                        p.activo,
                                        c.nombre AS categoria,
                                        pr.nombre AS proveedor,
                                        p.created_at,
                                        p.update_at
                                    FROM productos p
                                    LEFT JOIN categorias c ON p.categoria_id = c.id
                                    LEFT JOIN proveedores pr ON p.proveedor_id = pr.id
                                    WHERE 
                                        (@nombre IS NULL OR p.nombre LIKE @nombre)
                                        AND (@categoria IS NULL OR c.nombre LIKE @categoria)
                                    ORDER BY p.nombre;"

            Dim cmd As New MySqlCommand(query, conexion)

            If String.IsNullOrWhiteSpace(tbQueryProduct.Text) Then
                cmd.Parameters.AddWithValue("@nombre", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@nombre", "%" & buscar & "%")
            End If

            If String.IsNullOrWhiteSpace(tbQueryCategory.Text) Then
                cmd.Parameters.AddWithValue("@categoria", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@categoria", "%" & buscar & "%")
            End If

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            lvProducts.Items.Clear()

            While reader.Read()
                Dim item As New ListViewItem(reader("id").ToString())
                item.SubItems.Add(reader("codigo").ToString())
                item.SubItems.Add(reader("producto").ToString())
                item.SubItems.Add(reader("descripcion").ToString())
                item.SubItems.Add(reader("precio_compra").ToString())
                item.SubItems.Add(reader("precio_venta").ToString())
                item.SubItems.Add(reader("stock_actual").ToString())
                item.SubItems.Add(reader("stock_minimo").ToString())
                item.SubItems.Add(reader("categoria").ToString())
                item.SubItems.Add(reader("proveedor").ToString())
                item.SubItems.Add(reader("created_at").ToString())
                item.SubItems.Add(reader("update_at").ToString())
                item.SubItems.Add(If(reader("activo") = True, "Sí", "No"))
                lvProducts.Items.Add(item)
            End While

            reader.Close()
            conexion.Close()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conectar()
        cargarCategoria("id")
        cargarProveedor("id")

        cargaGrilla()

        ' Si el rol es Ventas, deshabilitar edición y botones CRUD
        Try
            If UsuarioActual.Rol = "ventas" Then
                ' Deshabilitar botones de CRUD
                btnAdd.Enabled = False
                btnModify.Enabled = False
                btnDelete.Enabled = False
                btnNew.Enabled = False
                btnAdd.Visible = False
                btnModify.Visible = False
                btnDelete.Visible = False
                btnNew.Visible = False
                btnChangeImg.Enabled = False
                btnChangeImg.Visible = False

                ' Poner campos en solo lectura / no editables
                tbCodigo.ReadOnly = True
                tbName.ReadOnly = True
                rtbDescription.ReadOnly = True
                cbCategory.Enabled = False
                cbSupplie.Enabled = False
                tbSell.ReadOnly = True
                tbBuy.ReadOnly = True
                tbMin.ReadOnly = True
                tbCurrent.ReadOnly = True
            End If
        Catch
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Bloquear acción si el rol es Ventas
        If UsuarioActual.Rol = "Ventas" Then
            MsgBox("No tiene permiso para crear productos.")
            Return
        End If

        If tbCodigo.Text.Trim() = "" Or tbName.Text.Trim() = "" Or tbSell.Text.Trim() = "" Or tbBuy.Text.Trim() = "" Or
       tbMin.Text.Trim() = "" Or tbCurrent.Text.Trim() = "" Or cbCategory.SelectedIndex = -1 Or cbSupplie.SelectedIndex = -1 Then
            MsgBox("Por favor, complete todos los campos.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            conexion.Open()

            Dim query As String = "
            INSERT INTO productos 
                (codigo, nombre, descripcion, precio_compra, precio_venta, stock_actual, stock_minimo, 
                 categoria_id, proveedor_id, imagen, created_at, update_at)
            VALUES 
                (@codigo, @nombre, @descripcion, @precio_compra, @precio_venta, @stock_actual, @stock_minimo,
                 (SELECT id FROM categorias WHERE nombre = @categoria LIMIT 1),
                 (SELECT id FROM proveedores WHERE nombre = @proveedor LIMIT 1),
                 @imagen, NOW(), NOW())"

            Dim cmd As New MySqlCommand(query, conexion)

            cmd.Parameters.AddWithValue("@codigo", tbCodigo.Text.Trim())
            cmd.Parameters.AddWithValue("@nombre", tbName.Text.Trim())
            cmd.Parameters.AddWithValue("@descripcion", rtbDescription.Text.Trim())
            cmd.Parameters.AddWithValue("@precio_venta", Convert.ToDecimal(tbSell.Text.Trim()))
            cmd.Parameters.AddWithValue("@precio_compra", Convert.ToDecimal(tbBuy.Text.Trim()))
            cmd.Parameters.AddWithValue("@stock_minimo", Convert.ToInt32(tbMin.Text.Trim()))
            cmd.Parameters.AddWithValue("@stock_actual", Convert.ToInt32(tbCurrent.Text.Trim()))
            cmd.Parameters.AddWithValue("@categoria", cbCategory.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@proveedor", cbSupplie.SelectedItem.ToString())

            ' Convertir imagen antes de agregar el parámetro
            Dim imagenBytes As Byte() = Nothing
            If pbImgProd.Image IsNot Nothing Then
                imagenBytes = ImagenABytes(pbImgProd.Image)
            End If

            ' Usar MySqlParameter con tipo LongBlob
            Dim paramImg As New MySqlParameter("@imagen", MySqlDbType.LongBlob)
            paramImg.Value = If(imagenBytes IsNot Nothing, CType(imagenBytes, Object), DBNull.Value)
            cmd.Parameters.Add(paramImg)

            cmd.ExecuteNonQuery()

            clearForm()
            conexion.Close()
            cargaGrilla()
        Catch ex As Exception
            MsgBox("Error al agregar producto: " & ex.Message)
        End Try
    End Sub

    Private Sub lvProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvProducts.SelectedIndexChanged
        If lvProducts.SelectedItems.Count = 0 Then
            clearForm()
            Exit Sub
        End If

        Dim item As ListViewItem = lvProducts.SelectedItems(0)

        tbCodigo.Text = item.SubItems(1).Text
        tbName.Text = item.SubItems(2).Text
        rtbDescription.Text = item.SubItems(3).Text
        tbBuy.Text = item.SubItems(4).Text
        tbSell.Text = item.SubItems(5).Text
        tbCurrent.Text = item.SubItems(6).Text
        tbMin.Text = item.SubItems(7).Text

        ' Selecciona la categoría en el combobox
        Dim categoria As String = item.SubItems(8).Text
        Dim idxCategoria As Integer = cbCategory.FindStringExact(categoria)
        cbCategory.SelectedIndex = idxCategoria

        ' Selecciona el proveedor en el combobox
        Dim proveedor As String = item.SubItems(9).Text
        Dim idxProveedor As Integer = cbSupplie.FindStringExact(proveedor)
        cbSupplie.SelectedIndex = idxProveedor

        ' Cargar imagen desde Tag si existe
        Try
            If item.Tag IsNot Nothing Then
                Dim bytes() As Byte = CType(item.Tag, Byte())
                Using ms As New MemoryStream(bytes)
                    pbImgProd.Image = Image.FromStream(ms)
                End Using
            Else
                pbImgProd.Image = Nothing
            End If
        Catch ex As Exception
            MsgBox("No se pudo cargar la imagen del producto: " & ex.Message)
        End Try
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        ' Bloquear acción si el rol es Ventas
        If UsuarioActual.Rol = "Ventas" Then
            MsgBox("No tiene permiso para modificar productos.")
            Return
        End If

        If lvProducts.SelectedItems.Count = 0 Then
            MsgBox("Por favor, seleccione un producto para modificar.", MsgBoxStyle.Exclamation, "Selección requerida")
            Exit Sub
        End If

        Dim selectedId As Integer = Convert.ToInt32(lvProducts.SelectedItems(0).Text)

        If tbCodigo.Text.Trim() = "" Or tbName.Text.Trim() = "" Or tbSell.Text.Trim() = "" Or tbBuy.Text.Trim() = "" Or
       tbMin.Text.Trim() = "" Or tbCurrent.Text.Trim() = "" Or cbCategory.SelectedIndex = -1 Or cbSupplie.SelectedIndex = -1 Then
            MsgBox("Por favor, complete todos los campos.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim query As String = "
        UPDATE productos SET 
            codigo = @codigo,
            nombre = @nombre,
            descripcion = @descripcion,
            precio_compra = @precio_compra,
            precio_venta = @precio_venta,
            stock_actual = @stock_actual,
            stock_minimo = @stock_minimo,
            categoria_id = (SELECT id FROM categorias WHERE nombre = @categoria LIMIT 1),
            proveedor_id = (SELECT id FROM proveedores WHERE nombre = @proveedor LIMIT 1),
            imagen = @imagen,
            update_at = NOW()
        WHERE id = @id"

        Try
            If conexion.State = ConnectionState.Closed Then conexion.Open()

            Using cmd As New MySqlCommand(query, conexion)
                cmd.Parameters.AddWithValue("@codigo", tbCodigo.Text.Trim())
                cmd.Parameters.AddWithValue("@nombre", tbName.Text.Trim())
                cmd.Parameters.AddWithValue("@descripcion", rtbDescription.Text.Trim())
                cmd.Parameters.AddWithValue("@precio_compra", Convert.ToDecimal(tbBuy.Text.Trim()))
                cmd.Parameters.AddWithValue("@precio_venta", Convert.ToDecimal(tbSell.Text.Trim()))
                cmd.Parameters.AddWithValue("@stock_actual", Convert.ToInt32(tbCurrent.Text.Trim()))
                cmd.Parameters.AddWithValue("@stock_minimo", Convert.ToInt32(tbMin.Text.Trim()))
                cmd.Parameters.AddWithValue("@categoria", cbCategory.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@proveedor", cbSupplie.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@id", selectedId)

                ' Convertir imagen del PictureBox a bytes (clonando para evitar problemas GDI+)
                Dim imagenBytes As Byte() = Nothing
                If pbImgProd.Image IsNot Nothing Then
                    Using bmp As New Bitmap(pbImgProd.Image)
                        Using ms As New MemoryStream()
                            bmp.Save(ms, Imaging.ImageFormat.Png) ' PNG conserva transparencia y evita negros
                            imagenBytes = ms.ToArray()
                        End Using
                    End Using
                End If

                Dim paramImg As New MySqlParameter("@imagen", MySqlDbType.LongBlob)
                paramImg.Value = If(imagenBytes IsNot Nothing, CType(imagenBytes, Object), DBNull.Value)
                cmd.Parameters.Add(paramImg)

                cmd.ExecuteNonQuery()
            End Using

            cargaGrilla()
            clearForm()
        Catch ex As Exception
            MsgBox("Error al modificar el producto: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                conexion.Close()
            End If
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        clearForm()
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCategory.SelectedIndexChanged

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Bloquear acción si el rol es Ventas
        If UsuarioActual.Rol = "Ventas" Then
            MsgBox("No tiene permiso para eliminar productos.")
            Return
        End If

        If lvProducts.SelectedItems.Count = 0 Then ' verifico que haya una fila seleccionada
            MsgBox("Por favor, seleccione un producto para eliminar.", MsgBoxStyle.Exclamation, "Selección requerida")
            Exit Sub
        End If
        Dim selectedId As Integer = Convert.ToInt32(lvProducts.SelectedItems(0).Text)
        Try

            conexion.Open()

            Dim query As String = "UPDATE productos SET activo = 0, update_at = NOW() WHERE id = @id"

            Dim cmd As New MySqlCommand(query, conexion)

            cmd.Parameters.AddWithValue("@id", selectedId)

            cmd.ExecuteNonQuery()

            conexion.Close()
            cargaGrilla()
            clearForm()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Productos_shown(sender As Object, e As EventArgs) Handles Me.Shown
        AjustarColumnas()
    End Sub

    Private Sub tbQueryProduct_TextChanged(sender As Object, e As EventArgs) Handles tbQueryProduct.TextChanged
        If tbQueryProduct.Text = "" Then
            cargaGrilla()

        Else
            busquedaAsistida(tbQueryProduct.Text)
        End If

    End Sub

    Private Sub tbQueryCategory_TextChanged(sender As Object, e As EventArgs) Handles tbQueryCategory.TextChanged
        If tbQueryCategory.Text = "" Then
            cargaGrilla()

        Else
            busquedaAsistida(tbQueryCategory.Text)
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            printList.Clear()
            Conectar()
            conexion.Open()
            ' Simplified query: only productos table and required fields
            Dim query As String = "SELECT codigo, nombre, stock_actual, precio_compra FROM productos WHERE stock_actual < stock_minimo AND activo = 1 ORDER BY stock_actual ASC;"
            Dim cmd As New MySqlCommand(query, conexion)
            Dim rdr As MySqlDataReader = cmd.ExecuteReader()


            ''Dim dt As New DataTable()
            ''dt.Load(rdr)
            ''MsgBox("Total de registros: " & dt.Rows.Count)




            If rdr.HasRows Then
                While rdr.Read()
                    Dim line As String = String.Format("{0}|{1}|{2}|{3}", rdr("codigo").ToString(), rdr("nombre").ToString(), rdr("stock_actual").ToString(), rdr("precio_compra").ToString())
                    printList.Add(line)
                End While
            End If
            rdr.Close()
            conexion.Close()

            If printList.Count = 0 Then
                MsgBox("No hay productos con stock mínimo.")
                Return
            End If

            Pagina = 1
            printDoc.DefaultPageSettings.Landscape = False
            Dim preview As New PrintPreviewDialog()
            preview.Document = printDoc
            preview.Width = 900
            preview.Height = 700
            preview.ShowDialog()
        Catch ex As Exception
            MsgBox("Error preparando impresión: " & ex.Message)
        End Try
    End Sub

    Private Sub printDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim fTitle As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fNormal As New Font("Segoe UI", 9)
        Dim headerBrush = Brushes.White
        Dim bodyBrush = Brushes.Black

        Dim x As Integer = e.MarginBounds.Left
        Dim y As Integer = e.MarginBounds.Top - 20

        ' Fondo oscuro header area
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(38, 22, 61)), e.MarginBounds.Left, y, e.MarginBounds.Width, 120)

        ' Logo: intento cargar Resources\loho.jpg desde la carpeta Resources; si no existe uso pbImgProd.Image
        Dim logoPath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "Resources", "loho.jpg")
        Dim logoImg As Image = Nothing
        Dim loadedFromFile As Boolean = False
        If IO.File.Exists(logoPath) Then
            Try
                logoImg = Image.FromFile(logoPath)
                loadedFromFile = True
            Catch
                logoImg = Nothing
                loadedFromFile = False
            End Try
        End If

        If logoImg Is Nothing AndAlso pbImgProd IsNot Nothing AndAlso pbImgProd.Image IsNot Nothing Then
            logoImg = pbImgProd.Image
            loadedFromFile = False
        End If

        If logoImg IsNot Nothing Then
            e.Graphics.DrawImage(logoImg, x, y + 10, 120, 60)
            If loadedFromFile Then
                logoImg.Dispose()
            End If
        End If

        ' Título
        e.Graphics.DrawString("Listado de productos con stock mínimo", fTitle, headerBrush, x + 140, y + 10)
        e.Graphics.DrawString("Empresa: DeGustan", fHeader, headerBrush, x + 140, y + 40)
        e.Graphics.DrawString("Fecha: " & DateTime.Now.ToString("g"), fHeader, headerBrush, x + 140, y + 60)

        ' Ajustar posición para la tabla
        Dim headerTop As Integer = y + 120
        Dim lineHeight As Integer = 22

        ' Definir columnas usando el ancho imprimible
        Dim pageWidth As Integer = e.MarginBounds.Width
        Dim codeWidth As Integer = 120
        Dim stockWidth As Integer = 80
        Dim priceWidth As Integer = 120
        Dim nameWidth As Integer = Math.Max(100, pageWidth - (codeWidth + stockWidth + priceWidth))

        Dim col0 As Integer = x
        Dim col1 As Integer = col0 + codeWidth
        Dim col2 As Integer = col1 + nameWidth
        Dim col3 As Integer = col2 + stockWidth
        Dim col4 As Integer = col3 + priceWidth

        ' Encabezado de tabla: fondo y texto
        Dim hdrRect As New Rectangle(x, headerTop, pageWidth, lineHeight)
        e.Graphics.FillRectangle(Brushes.DimGray, hdrRect)
        e.Graphics.DrawRectangle(Pens.Black, hdrRect)

        ' Encabezados por columna
        e.Graphics.DrawString("Código", fHeader, headerBrush, New RectangleF(col0 + 4, headerTop + 2, codeWidth, lineHeight))
        e.Graphics.DrawString("Nombre", fHeader, headerBrush, New RectangleF(col1 + 4, headerTop + 2, nameWidth, lineHeight))
        e.Graphics.DrawString("Stock", fHeader, headerBrush, New RectangleF(col2 + 4, headerTop + 2, stockWidth, lineHeight))
        e.Graphics.DrawString("Precio Compra", fHeader, headerBrush, New RectangleF(col3 + 4, headerTop + 2, priceWidth, lineHeight))

        ' Preparar paginación basada en área utilizable debajo del encabezado
        Dim startY As Integer = headerTop + lineHeight + 4
        Dim availableHeight As Integer = e.MarginBounds.Bottom - startY
        Dim itemsPerPage As Integer = Math.Max(1, availableHeight \ lineHeight)
        Dim startIndex As Integer = (Pagina - 1) * itemsPerPage
        Dim i As Integer = 0

        ' Dibujar filas con celdas/bordes
        For idx As Integer = startIndex To printList.Count - 1
            If i >= itemsPerPage Then
                Pagina += 1
                e.HasMorePages = True
                Return
            End If

            Dim rowY As Integer = startY + i * lineHeight
            Dim parts = printList(idx).Split("|"c)

            ' Dibujar rectángulos de celda
            e.Graphics.DrawRectangle(Pens.Gray, col0, rowY, codeWidth, lineHeight)
            e.Graphics.DrawRectangle(Pens.Gray, col1, rowY, nameWidth, lineHeight)
            e.Graphics.DrawRectangle(Pens.Gray, col2, rowY, stockWidth, lineHeight)
            e.Graphics.DrawRectangle(Pens.Gray, col3, rowY, priceWidth, lineHeight)

            ' Escribir texto dentro de cada celda (pequeño padding)
            e.Graphics.DrawString(parts(0).Trim(), fNormal, bodyBrush, New RectangleF(col0 + 4, rowY + 2, codeWidth - 8, lineHeight))
            e.Graphics.DrawString(parts(1).Trim(), fNormal, bodyBrush, New RectangleF(col1 + 4, rowY + 2, nameWidth - 8, lineHeight))
            e.Graphics.DrawString(parts(2).Trim(), fNormal, bodyBrush, New RectangleF(col2 + 4, rowY + 2, stockWidth - 8, lineHeight))
            e.Graphics.DrawString(parts(3).Trim(), fNormal, bodyBrush, New RectangleF(col3 + 4, rowY + 2, priceWidth - 8, lineHeight))

            i += 1
        Next

        e.HasMorePages = False
    End Sub

    Private Sub btnChangeImg_Click(sender As Object, e As EventArgs) Handles btnChangeImg.Click
        Dim ofd As New OpenFileDialog()
        ofd.Title = "Seleccionar imagen del producto"
        ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim img As Image = Image.FromFile(ofd.FileName)
                pbImgProd.Image = img
            Catch ex As Exception
                MsgBox("No se pudo cargar la imagen: " & ex.Message)
            End Try
        End If
    End Sub
End Class