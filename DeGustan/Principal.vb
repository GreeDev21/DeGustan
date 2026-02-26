Imports MySql.Data.MySqlClient

Public Class Principal
    Private minWitdh As Integer = 85
    Private maxWidth As Integer = 300
    Private expand As Boolean = False
    Private currentForm As Form = Nothing
    Private originalButtonTexts As New Dictionary(Of Button, String)()

    Private Sub OpenChildForm(childForm As Form)
        If currentForm IsNot Nothing Then currentForm.Close()
        currentForm = childForm
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        PanelContainer.Controls.Add(childForm)
        PanelContainer.Tag = childForm
        childForm.BringToFront()
        childForm.Show()
    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not UsuarioActual.IsLoggedIn Then
            MessageBox.Show("Sesión no iniciada. Volviendo al login.")
            Me.Close()
            Dim lf As New LoginForm()
            lf.Show()
            Return
        End If

        Try
            lblUser.Text = String.Format("{0} ({1})", UsuarioActual.Nombre, UsuarioActual.Rol)
        Catch
        End Try

        Panel1.Width = If(UsuarioActual.SidebarExpanded, maxWidth, minWitdh)
        expand = UsuarioActual.SidebarExpanded

        originalButtonTexts.Clear()
        For Each ctrl As Control In Panel1.Controls
            If TypeOf ctrl Is Button Then
                Dim b As Button = DirectCast(ctrl, Button)
                If b Is btnLogOut Then Continue For
                originalButtonTexts(b) = b.Text
            End If
        Next

        btnUsers.Visible = UsuarioActual.HasRole("Root")
        btnUsers.Enabled = UsuarioActual.HasRole("Root")
        btnMovement.Visible = UsuarioActual.HasAnyRole("Root", "Ventas")
        btnMovement.Enabled = UsuarioActual.HasAnyRole("Root", "Ventas")
        btnProducts.Visible = UsuarioActual.HasAnyRole("Root", "Deposito", "Ventas")
        btnProducts.Enabled = UsuarioActual.HasAnyRole("Root", "Deposito", "Ventas")
        btnSuppliers.Visible = UsuarioActual.HasAnyRole("Root", "Deposito")
        btnSuppliers.Enabled = UsuarioActual.HasAnyRole("Root", "Deposito")
        btnCategorys.Visible = UsuarioActual.HasAnyRole("Root", "Deposito", "Ventas")
        btnCategorys.Enabled = UsuarioActual.HasAnyRole("Root", "Deposito", "Ventas")


    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        If Not UsuarioActual.HasRole("Root") Then
            MsgBox("No tiene permiso para acceder a Usuarios. Consulte con el administrador.")
            Return
        End If
        OpenChildForm(New Usuarios())
    End Sub

    Private Sub btnMovement_Click(sender As Object, e As EventArgs) Handles btnMovement.Click
        If Not UsuarioActual.HasAnyRole("Root", "Ventas") Then
            MsgBox("No tiene permiso para acceder a Movimientos.")
            Return
        End If
        OpenChildForm(New Movimientos())
    End Sub

    Private Sub btnProducts_Click(sender As Object, e As EventArgs) Handles btnProducts.Click
        If UsuarioActual.HasAnyRole("Ventas", "Root", "Deposito") Then
            OpenChildForm(New Productos())
        Else
            MsgBox("No tiene permiso para acceder a Productos.")
        End If
    End Sub

    Private Sub btnSuppliers_Click(sender As Object, e As EventArgs) Handles btnSuppliers.Click
        If UsuarioActual.HasAnyRole("Root", "Deposito") Then
            OpenChildForm(New proveedores())
        Else
            MsgBox("No tiene permiso para acceder a Proveedores.")
        End If
    End Sub

    Private Sub btnCategorys_Click(sender As Object, e As EventArgs) Handles btnCategorys.Click
        If UsuarioActual.HasAnyRole("Root", "Deposito", "Ventas") Then
            OpenChildForm(New Categorias())
        Else
            MsgBox("No tiene permiso para acceder a Categorías.")
        End If
    End Sub

    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        UsuarioActual.Id = 0
        UsuarioActual.Nombre = ""
        UsuarioActual.Rol = ""
        UsuarioActual.IsLoggedIn = False
        UsuarioActual.SidebarExpanded = True

        Dim lf As New LoginForm()
        lf.Show()
        Me.Close()
    End Sub

End Class