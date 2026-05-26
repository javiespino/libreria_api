# API Librería Ensigna

API REST desarrollada con ASP.NET Core 6 y Entity Framework Core para la gestión de una librería online.

## Tecnologías

- ASP.NET Core 6
- Entity Framework Core 6
- SQL Server
- Swagger

## Estructura

- **Controllers** — AuthController, LibrosController, PedidosController
- **Models** — Libro, Usuario, Pedido, PedidoItem
- **DTOs** — Objetos de transferencia de datos
- **Repositories** — Patrón repositorio para acceso a datos
- **Data** — Contexto de Entity Framework
- **Migrations** — Migraciones de la base de datos

## Endpoints principales

### Auth
- `POST /api/Auth/register` — Registro de usuario
- `POST /api/Auth/login` — Inicio de sesión

### Libros
- `GET /api/Libros` — Obtener todos los libros disponibles
- `GET /api/Libros/{id}` — Obtener libro por id
- `POST /api/Libros` — Crear libro (admin)
- `PUT /api/Libros/{id}` — Editar libro (admin)
- `DELETE /api/Libros/{id}` — Borrado lógico de libro (admin)

### Pedidos
- `GET /api/Pedidos` — Obtener todos los pedidos (admin)
- `GET /api/Pedidos/usuario/{usuarioId}` — Pedidos de un usuario
- `POST /api/Pedidos` — Crear pedido
- `PUT /api/Pedidos/{id}/estado` — Cambiar estado del pedido

## Base de datos

El archivo `libreriadb.sql` contiene el script completo para restaurar la base de datos con esquema y datos.

## Configuración

La cadena de conexión se configura en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=LibreriaDB;Trusted_Connection=True;"
}
```

## Autor

Javier Espino Trejo — 2º DAM  
IES Arroyo Harnina, Almendralejo
