Imports System.IO
Imports System.Security.Cryptography.Xml
Imports DeGustan.ConnectorBD
Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient












Public Class Movimientos
    ' Variables del último movimiento
    Private productoNombre As String
    Private tipo As String
    Private motivo As String
    Private referencia As String
    Private usuarioNombre As String
    Private usuarioApellido As String
    Private cantidad As Integer
    Private anterior As Integer
    Private nueva As Integer
    Private fechaMovimiento As Date

    ' Documento de impresión
    Private WithEvents DocMovimiento As New PrintDocument
    Private PrintPreviewDialog1 As New PrintPreviewDialog


    ' Cargar datos del movimiento
    Private Sub CargarMovimiento(Optional movimientoId As Integer = -1)
        Dim connStr As String = "server=localhost;user id=root;password=3312U;database=degustan;port=3306;"
        Using conn As New MySqlConnection(connStr)
            Dim query As String = "
            SELECT 
                m.tipo, m.cantidad, m.cantidad_anterior, m.cantidad_nueva, m.motivo, m.referencia, m.created_at,
                p.nombre AS producto_nombre,
                u.nombre AS usuario_nombre, u.apellido AS usuario_apellido
            FROM movimientos_stock m
            JOIN productos p ON m.producto_id = p.id
            JOIN usuarios u ON m.usuario_id = u.id"

            If movimientoId > 0 Then
                query &= " WHERE m.id = @id"
            Else
                query &= " ORDER BY m.id DESC LIMIT 1"
            End If

            Dim cmd As New MySqlCommand(query, conn)
            If movimientoId > 0 Then
                cmd.Parameters.AddWithValue("@id", movimientoId)
            End If

            Try
                conn.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        productoNombre = reader("producto_nombre").ToString()
                        tipo = reader("tipo").ToString()
                        cantidad = Convert.ToInt32(reader("cantidad"))
                        anterior = Convert.ToInt32(reader("cantidad_anterior"))
                        nueva = Convert.ToInt32(reader("cantidad_nueva"))
                        motivo = reader("motivo").ToString()
                        referencia = reader("referencia").ToString()
                        usuarioNombre = reader("usuario_nombre").ToString()
                        usuarioApellido = reader("usuario_apellido").ToString()
                        fechaMovimiento = Convert.ToDateTime(reader("created_at"))
                    End If
                End Using
            Catch ex As Exception
                MsgBox("Error al obtener datos del movimiento: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Movimientos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializar conexión y cargar datos base
        Try
            Conectar()
        Catch
        End Try

        ' Cargar tipos
        cbType.Items.Clear()
        cbType.Items.Add("entrada")
        cbType.Items.Add("salida")
        cbType.Items.Add("ajuste")

        ' Cargar productos en comboboxes
        LoadProductsIntoCombo(cbProductID)
        LoadProductsIntoCombo(ComboBox1)

        ' Cargar tipos para filtro
        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("") ' vacío = todos
        ComboBox2.Items.Add("entrada")
        ComboBox2.Items.Add("salida")
        ComboBox2.Items.Add("ajuste")

        ' Cargar movimientos
        LoadMovements()
    End Sub

    Private Sub LoadProductsIntoCombo(cmb As ComboBox)
        Try
            conexion.Open()
            Dim query As String = "SELECT id, CONCAT(codigo,' - ',nombre) AS display FROM productos WHERE activo = 1 ORDER BY nombre;"
            Dim cmd As New MySqlCommand(query, conexion)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            conexion.Close()

            cmb.DisplayMember = "display"
            cmb.ValueMember = "id"
            cmb.DataSource = dt
        Catch ex As Exception
            MsgBox("Error cargando productos: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadMovements(Optional ByVal productId As Integer = -1, Optional ByVal tipo As String = "")
        Try
            conexion.Open()
            Dim query As String = "SELECT m.id, p.codigo, p.nombre AS producto, m.tipo, m.cantidad, m.cantidad_anterior, m.cantidad_nueva, m.motivo, u.nombre AS usuario, m.referencia, m.created_at " &
                                  "FROM movimientos_stock m " &
                                  "JOIN productos p ON m.producto_id = p.id " &
                                  "JOIN usuarios u ON m.usuario_id = u.id "

            Dim whereAdded As Boolean = False
            If productId > 0 Then
                query &= " WHERE m.producto_id = @pid"
                whereAdded = True
            End If
            If Not String.IsNullOrEmpty(tipo) Then
                If whereAdded Then
                    query &= " AND m.tipo = @tipo"
                Else
                    query &= " WHERE m.tipo = @tipo"
                    whereAdded = True
                End If
            End If

            query &= " ORDER BY m.created_at DESC;"

            Dim cmd As New MySqlCommand(query, conexion)
            If productId > 0 Then cmd.Parameters.AddWithValue("@pid", productId)
            If Not String.IsNullOrEmpty(tipo) Then cmd.Parameters.AddWithValue("@tipo", tipo)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            ListView1.Items.Clear()
            While reader.Read()
                Dim it As New ListViewItem(reader("id").ToString())
                it.SubItems.Add(reader("codigo").ToString())
                it.SubItems.Add(reader("tipo").ToString())
                it.SubItems.Add(reader("cantidad").ToString())
                it.SubItems.Add(reader("cantidad_anterior").ToString())
                it.SubItems.Add(reader("cantidad_nueva").ToString())
                it.SubItems.Add(reader("motivo").ToString())
                it.SubItems.Add(reader("usuario").ToString())
                it.SubItems.Add(If(IsDBNull(reader("referencia")), "", reader("referencia").ToString()))
                it.SubItems.Add(reader("created_at").ToString())
                ListView1.Items.Add(it)
            End While
            reader.Close()
            conexion.Close()
        Catch ex As Exception
            MsgBox("Error cargando movimientos: " & ex.Message)
        End Try
    End Sub

    Private Function GetCurrentStock(productId As Integer) As Integer
        Try
            conexion.Open()
            Dim q As String = "SELECT stock_actual FROM productos WHERE id = @id LIMIT 1"
            Dim cmd As New MySqlCommand(q, conexion)
            cmd.Parameters.AddWithValue("@id", productId)
            Dim obj = cmd.ExecuteScalar()
            conexion.Close()
            If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
                Return Convert.ToInt32(obj)
            End If
        Catch ex As Exception
            MsgBox("Error obteniendo stock: " & ex.Message)
        End Try
        Return 0
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Validaciones
        If cbProductID.SelectedValue Is Nothing Then
            MsgBox("Seleccione un producto.")
            Return
        End If
        If cbType.SelectedItem Is Nothing Then
            MsgBox("Seleccione tipo de movimiento.")
            Return
        End If
        Dim prodId As Integer = Convert.ToInt32(cbProductID.SelectedValue)
        Dim tipo As String = cbType.SelectedItem.ToString()
        Dim cantidadInput As Integer
        If Not Integer.TryParse(tbNumber.Text.Trim(), cantidadInput) Then
            MsgBox("Ingrese una cantidad válida.")
            Return
        End If
        If cantidadInput < 0 Then
            MsgBox("La cantidad debe ser positiva.")
            Return
        End If
        Dim motivo As String = tbmotivo.Text.Trim()
        If motivo = "" Then
            MsgBox("Ingrese un motivo.")
            Return
        End If
        Dim referencia As String = ""
        If tbReference IsNot Nothing Then referencia = tbReference.Text.Trim()
        If referencia.Length > 100 Then
            MsgBox("La referencia no puede superar 100 caracteres.")
            Return
        End If

        Try
            ' Obtener stock actual
            Dim stockActual As Integer = GetCurrentStock(prodId)
            Dim cantidadAnterior As Integer = stockActual
            Dim cantidadNueva As Integer = stockActual

            If tipo = "entrada" Then
                cantidadNueva = stockActual + cantidadInput
            ElseIf tipo = "salida" Then
                cantidadNueva = stockActual - cantidadInput
                If cantidadNueva < 0 Then
                    MsgBox("No hay stock suficiente para realizar la salida.")
                    Exit Sub
                End If ' evitar negativos
            ElseIf tipo = "ajuste" Then
                ' Para ajuste interpretamos el número ingresado como nuevo stock
                cantidadNueva = cantidadInput
            End If

            ' Insertar movimiento y actualizar stock en una transacción
            conexion.Open()
            Dim trans = conexion.BeginTransaction()
            Try
                Dim ins As String = "INSERT INTO movimientos_stock (producto_id, tipo, cantidad, cantidad_anterior, cantidad_nueva, motivo, usuario_id, referencia, created_at) " &
                                    "VALUES (@pid, @tipo, @cant, @cant_prev, @cant_new, @motivo, @uid, @ref, NOW())"
                Dim cmdIns As New MySqlCommand(ins, conexion, trans)
                cmdIns.Parameters.AddWithValue("@pid", prodId)
                cmdIns.Parameters.AddWithValue("@tipo", tipo)
                cmdIns.Parameters.AddWithValue("@cant", If(tipo = "ajuste", cantidadNueva, cantidadInput))
                cmdIns.Parameters.AddWithValue("@cant_prev", cantidadAnterior)
                cmdIns.Parameters.AddWithValue("@cant_new", cantidadNueva)
                cmdIns.Parameters.AddWithValue("@motivo", motivo)
                cmdIns.Parameters.AddWithValue("@uid", UsuarioActual.Id)
                ' referencia opcional
                If Not String.IsNullOrWhiteSpace(referencia) Then
                    cmdIns.Parameters.AddWithValue("@ref", referencia)
                Else
                    cmdIns.Parameters.AddWithValue("@ref", DBNull.Value)
                End If
                cmdIns.ExecuteNonQuery()

                Dim upd As String = "UPDATE productos SET stock_actual = @stock WHERE id = @id"
                Dim cmdUpd As New MySqlCommand(upd, conexion, trans)
                cmdUpd.Parameters.AddWithValue("@stock", cantidadNueva)
                cmdUpd.Parameters.AddWithValue("@id", prodId)
                cmdUpd.ExecuteNonQuery()

                trans.Commit()
                conexion.Close()

                MsgBox("Movimiento registrado correctamente.")
                ' Refrescar listados
                LoadMovements()
                clearForm()
            Catch ex As Exception
                trans.Rollback()
                conexion.Close()
                MsgBox("Error al registrar movimiento: " & ex.Message)
            End Try
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub clearForm()
        cbProductID.SelectedIndex = -1
        cbType.SelectedIndex = -1
        tbNumber.Text = ""
        tbmotivo.Text = ""
        If tbReference IsNot Nothing Then tbReference.Text = ""
    End Sub


    Private Sub docMovimiento_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printMovimiento.PrintPage
        Try
            ' Fuentes
            Dim fuenteTitulo As New Font("Segoe UI", 16, FontStyle.Bold)
            Dim fuenteSeccion As New Font("Segoe UI", 12, FontStyle.Bold)
            Dim fuenteCampo As New Font("Segoe UI", 10, FontStyle.Bold)
            Dim fuenteValor As New Font("Segoe UI", 10)
            Dim fuentePie As New Font("Segoe UI", 9, FontStyle.Italic)

            ' Layout
            Dim margenIzq As Integer = 60
            Dim margenTop As Integer = 60
            Dim anchoRemito As Integer = 520
            Dim altoRemito As Integer = 400
            Dim rect As New Rectangle(margenIzq, margenTop, anchoRemito, altoRemito)
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rect)
            e.Graphics.DrawRectangle(New Pen(Color.Gray, 2), rect)

            ' Logo institucional
            Dim rutaLogo As String = "C:\Users\User\OneDrive\Desktop\aguscrudvs\DeGustan-master\DeGustan-master\DeGustan\bin\Debug\net8.0-windows\loho.jpg"
            If File.Exists(rutaLogo) Then
                Dim logo As Image = Image.FromFile(rutaLogo)
                e.Graphics.DrawImage(logo, margenIzq + 10, margenTop + 10, 80, 80)
            End If

            ' Título
            e.Graphics.DrawString("REMITO DE MOVIMIENTO DE STOCK", fuenteTitulo, Brushes.Black, margenIzq + 110, margenTop + 30)

            ' Línea divisoria
            e.Graphics.DrawLine(Pens.Gray, margenIzq + 10, margenTop + 100, margenIzq + anchoRemito - 10, margenTop + 100)

            ' Datos del remito
            Dim yBase As Integer = margenTop + 110
            Dim salto As Integer = 24
            Dim xCampo As Integer = margenIzq + 20
            Dim xValor As Integer = margenIzq + 180

            e.Graphics.DrawString("Producto:", fuenteCampo, Brushes.Black, xCampo, yBase)
            e.Graphics.DrawString(productoNombre, fuenteValor, Brushes.Black, xValor, yBase)

            e.Graphics.DrawString("Tipo de movimiento:", fuenteCampo, Brushes.Black, xCampo, yBase + salto)
            e.Graphics.DrawString(tipo, fuenteValor, Brushes.Black, xValor, yBase + salto)

            e.Graphics.DrawString("Cantidad:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 2)
            e.Graphics.DrawString(cantidad.ToString(), fuenteValor, Brushes.Black, xValor, yBase + salto * 2)

            e.Graphics.DrawString("Stock anterior:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 3)
            e.Graphics.DrawString(anterior.ToString(), fuenteValor, Brushes.Black, xValor, yBase + salto * 3)

            e.Graphics.DrawString("Stock nuevo:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 4)
            e.Graphics.DrawString(nueva.ToString(), fuenteValor, Brushes.Black, xValor, yBase + salto * 4)

            e.Graphics.DrawString("Motivo:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 5)
            e.Graphics.DrawString(motivo, fuenteValor, Brushes.Black, xValor, yBase + salto * 5)

            e.Graphics.DrawString("Referencia:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 6)
            e.Graphics.DrawString(referencia, fuenteValor, Brushes.Black, xValor, yBase + salto * 6)

            e.Graphics.DrawString("Fecha:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 7)
            e.Graphics.DrawString(fechaMovimiento.ToString("dd/MM/yyyy HH:mm"), fuenteValor, Brushes.Black, xValor, yBase + salto * 7)

            e.Graphics.DrawString("Usuario responsable:", fuenteCampo, Brushes.Black, xCampo, yBase + salto * 8)
            e.Graphics.DrawString(usuarioNombre & " " & usuarioApellido, fuenteValor, Brushes.Black, xValor, yBase + salto * 8)

            ' Pie
            Dim yPie As Integer = margenTop + altoRemito - 60
            e.Graphics.DrawLine(Pens.Gray, margenIzq + 10, yPie - 10, margenIzq + anchoRemito - 10, yPie - 10)
            e.Graphics.DrawString("Emitido por Degustan Mini", fuentePie, Brushes.Black, xCampo, yPie)
            e.Graphics.DrawString("Sistema de gestión de stock", fuentePie, Brushes.Gray, xCampo, yPie + 20)

        Catch ex As Exception
            MsgBox("Error al imprimir remito: " & ex.Message)
        End Try

    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs)
        CargarMovimiento()
        PrintPreviewDialog1.Document = printMovimiento
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim prodId As Integer = -1
        Dim tipo = ""
        Try
            If ComboBox1.SelectedValue IsNot Nothing Then
                prodId = Convert.ToInt32(ComboBox1.SelectedValue)
            End If
        Catch
        End Try
        Try
            If ComboBox2.SelectedItem IsNot Nothing AndAlso ComboBox2.SelectedItem.ToString <> "" Then
                tipo = ComboBox2.SelectedItem.ToString
            End If
        Catch
        End Try
        LoadMovements(prodId, tipo)
    End Sub

    Private Sub btnPritMovimiento_Click(sender As Object, e As EventArgs) Handles btnPritMovimiento.Click
        Dim idMovimiento As Integer

        If String.IsNullOrWhiteSpace(tbIdmovimiento.Text) Then
            idMovimiento = 0 ' valor por defecto si no se ingresó nada
        ElseIf Integer.TryParse(tbIdmovimiento.Text, idMovimiento) Then
            ' idMovimiento ahora tiene el valor numérico ingresado
        Else
            MsgBox("El ID ingresado no es válido.")
            Exit Sub
        End If

        CargarMovimiento(idMovimiento)
        PrintPreviewDialog1.Document = printMovimiento
        PrintPreviewDialog1.ShowDialog()

    End Sub
End Class