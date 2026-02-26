# DeGustan

DeGustan es una aplicación de escritorio desarrollada en Visual Basic .NET 8 para la gestión de un sistema de ventas e inventario. Proporciona formularios para administrar categorías, productos, proveedores, usuarios y movimientos, con autenticación básica y conexión a base de datos.

## Características

- Gestión de categorías, productos y proveedores.
- Control de usuarios y autenticación.
- Registro de movimientos (entradas, salidas y ventas).
- Interfaz WinForms tradicional con ListView y controles estándar.
- Conexión a base de datos mediante un módulo compartido (`ConnectorBD.vb`).

## Requisitos

- Windows 10/11.
- .NET 8 SDK (el runtime se incluye en la carpeta `bin` para ejecución).
- Visual Studio 2022 o posterior para compilar.
- Base de datos accesible con la estructura esperada; la cadena se almacena en `dataConnect.txt`.
  - Se incluye una base de datos de prueba (`degustan-mini.sql`). Ajustar la ruta en `dataConnect.txt` según corresponda.

## Instalación

1. Clonar o descargar el repositorio.
2. Abrir `DeGustan.sln` en Visual Studio.
3. Restaurar paquetes NuGet si es necesario.
4. Configurar la conexión a la base de datos editando `dataConnect.txt` y copiarlo a la carpeta `bin`.
   - Si se desea usar la base de datos de prueba incluida, actualizar la ruta dentro de `dataConnect.txt`.
5. Compilar y ejecutar (`F5` o Ctrl+F5).
6. El ejecutable se genera en `bin\Debug\net8.0-windows` junto con los recursos necesarios.

## Uso

1. Al iniciar, se mostrará el formulario de login; se requiere un usuario válido en la base de datos (puede utilizarse la conta de prueba incluida para comenzar).
2. Tras el acceso, el menú principal permite navegar a las secciones de categorías, productos, proveedores, usuarios y movimientos.
3. Las operaciones básicas (alta, baja, modificación) se realizan desde los respectivos formularios.

## Estructura del proyecto

- **Formularios principales**:
  - `LoginForm.vb`
  - `Principal.vb`
  - `Categorias.vb`
  - `Productos.vb`
  - `Proveedores.vb`
  - `Usuarios.vb`
  - `Movimientos.vb`

- **Módulos**:
  - `Modulos/ConnectorBD.vb`: lógica de conexión y ejecución de consultas.

- **Configuración**:
  - `My Project`: archivos de aplicación y recursos.
  - `dataConnect.txt`: cadena de conexión (no debe incluirse en el control de versiones si contiene credenciales).

## Problemas conocidos

- **Módulo Categorías**: la aplicación puede cerrar abruptamente cuando se realizan búsquedas muy rápidas. Actualmente, se ejecuta una consulta por cada letra ingresada en el cuadro de búsqueda, lo que puede saturar la base de datos.
  - *Solución propuesta*: almacenar los resultados en memoria y actualizar directamente el `ListView` sin volver a consultar en cada pulsación.

## Contribuir

- Las sugerencias, correcciones y mejoras pueden enviarse mediante issues o pull requests.
- Mantener compatibilidad con .NET 8 y la estructura actual del proyecto.

## Licencia

Este proyecto no tiene una licencia especificada.
