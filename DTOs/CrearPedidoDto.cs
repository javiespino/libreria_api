namespace javier_api.DTOs
{
    public class CrearPedidoItemDto
    {
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class CrearPedidoDto
    {
        public int UsuarioId { get; set; }
        public decimal Total { get; set; }
        public List<CrearPedidoItemDto> Items { get; set; } = new();
    }
}