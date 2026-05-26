namespace javier_api.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;

        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<PedidoItemDto> Items { get; set; } = new();
    }
}