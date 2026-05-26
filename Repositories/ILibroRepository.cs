using javier_api.Models;

public interface ILibroRepository
{
    Task<IEnumerable<Libro>> GetAllAsync();
    Task<Libro?> GetByIdAsync(int id);
    Task<Libro?> GetByIsbnAsync(string isbn);
    Task<bool> ExistsByIsbnAsync(string isbn);
    Task<bool> EstaEnPedidoAsync(int libroId);
    Task AddAsync(Libro libro);
    void Update(Libro libro);
    void Delete(Libro libro);
    Task SaveAsync();
}