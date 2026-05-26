using javier_api.Data;
using javier_api.Models;
using Microsoft.EntityFrameworkCore;

namespace javier_api.Repositories
{
    public class Librorepository : ILibroRepository
    {
        private readonly LibreriaContext _context;
        public Librorepository(LibreriaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Libro>> GetAllAsync()
        {
            return await _context.Libros
                .Where(l => l.Disponible)
                .ToListAsync();
        }
        public async Task<bool> EstaEnPedidoAsync(int libroId)
        {
            return await _context.PedidoItems
                .AnyAsync(pi => pi.LibroId == libroId);
        }
        public async Task<Libro?> GetByIdAsync(int id)
        {
            return await _context.Libros.FindAsync(id);
        }
        public async Task AddAsync(Libro libro)
        {
            await _context.Libros.AddAsync(libro);
        }
        public void Update(Libro libro)
        {
            _context.Libros.Update(libro);
        }
        public void Delete(Libro libro)
        {
            _context.Libros.Remove(libro);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Libro?> GetByIsbnAsync(string isbn)
        {
            return await _context.Libros
            .FirstOrDefaultAsync(l => l.Isbn == isbn);
        }
        public async Task<bool> ExistsByIsbnAsync(string isbn)
        {
            return await _context.Libros
            .AnyAsync(l => l.Isbn == isbn);
        }

    }
}
