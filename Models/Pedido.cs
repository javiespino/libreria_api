using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace javier_api.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; } = "Pendiente";

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; } = null!;

        public ICollection<PedidoItem> Items { get; set; } = new List<PedidoItem>();
    }
}