using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace javier_api.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string Isbn { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Autor { get; set; } = string.Empty;

        [StringLength(150)]
        public string Editorial { get; set; } = string.Empty;

        [StringLength(50)]
        public string Formato { get; set; } = string.Empty;

        [StringLength(50)]
        public string Edicion { get; set; } = string.Empty;

        [Required]
        [Range(0, 9999.99)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [StringLength(500)]
        public string ImagenUrl { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [StringLength(2000)]
        public string Sinopsis { get; set; } = string.Empty;
        public bool Disponible { get; set; } = true;
    }
}