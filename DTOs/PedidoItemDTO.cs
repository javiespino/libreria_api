namespace javier_api.DTOs
{
    public class PedidoItemDto
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public string LibroTitulo { get; set; } = string.Empty;
        public string LibroAutor { get; set; } = string.Empty;
        public string LibroIsbn { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}