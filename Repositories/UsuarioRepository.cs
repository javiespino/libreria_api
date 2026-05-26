using javier_api.Data;
using javier_api.Models;
using Microsoft.EntityFrameworkCore;

namespace javier_api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibreriaContext _context;

        public UsuarioRepository(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByUsernameAsync(string username) =>
            await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<Usuario?> GetByEmailAsync(string email) =>
            await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> ExistsAsync(string username, string email) =>
            await _context.Usuarios.AnyAsync(u => u.Username == username || u.Email == email);
    }
}