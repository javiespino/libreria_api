using javier_api.Models;

namespace javier_api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<bool> ExistsAsync(string username, string email);
    }
}