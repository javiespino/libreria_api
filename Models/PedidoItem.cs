using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace javier_api.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        public int LibroId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; } = null!;

        [ForeignKey("LibroId")]
        public Libro Libro { get; set; } = null!;
    }
}