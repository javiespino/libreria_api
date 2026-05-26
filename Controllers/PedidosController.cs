using javier_api.Data;
using javier_api.DTOs;
using javier_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace javier_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly LibreriaContext _context;

        public PedidosController(LibreriaContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Pedidos  (ADMIN - ver todos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAllPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario) // <--- FUNDAMENTAL
                .Include(p => p.Items)
                    .ThenInclude(i => i.Libro)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            var result = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                // Mapeo de datos del usuario
                UsuarioNombre = p.Usuario?.Username ?? "Desconocido",
                UsuarioEmail = p.Usuario?.Email ?? "Sin Email",

                FechaPedido = p.FechaPedido,
                Total = p.Total,
                Estado = p.Estado,
                Items = p.Items.Select(i => new PedidoItemDto
                {
                    Id = i.Id,
                    LibroId = i.LibroId,
                    LibroTitulo = i.Libro.Titulo,
                    LibroAutor = i.Libro.Autor,
                    LibroIsbn = i.Libro.Isbn,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario
                }).ToList()
            });

            return Ok(result);
        }

        // ✅ GET: api/Pedidos/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidosByUsuario(int usuarioId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario) // <--- AÑADIDO
                .Include(p => p.Items)
                    .ThenInclude(i => i.Libro)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            var result = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                UsuarioNombre = p.Usuario?.Username ?? "Desconocido",
                UsuarioEmail = p.Usuario?.Email ?? "Sin Email",
                FechaPedido = p.FechaPedido,
                Total = p.Total,
                Estado = p.Estado,
                Items = p.Items.Select(i => new PedidoItemDto
                {
                    Id = i.Id,
                    LibroId = i.LibroId,
                    LibroTitulo = i.Libro.Titulo,
                    LibroAutor = i.Libro.Autor,
                    LibroIsbn = i.Libro.Isbn,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario
                }).ToList()
            });

            return Ok(result);
        }

        // ✅ POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult> CreatePedido(CrearPedidoDto dto)
        {
            var pedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Total = dto.Total,
                FechaPedido = DateTime.Now,
                Estado = "Pendiente",
                Items = dto.Items.Select(i => new PedidoItem
                {
                    LibroId = i.LibroId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario
                }).ToList()
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidosByUsuario),
                new { usuarioId = pedido.UsuarioId },
                pedido.Id);
        }

        // ✅ PUT: api/Pedidos/5/estado  (ADMIN - cambiar estado)
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound();

            pedido.Estado = nuevoEstado;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}