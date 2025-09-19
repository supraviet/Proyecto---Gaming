using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gaming.Models
{
    public class Juego
    {
        public int IdJuego { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; }
        
        [Required(ErrorMessage = "La plataforma es obligatoria")]
        public string Plataforma { get; set; }
        
        public string Imagen { get; set; }
        
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5")]
        public decimal? PuntuacionMedia { get; set; }
    }
}