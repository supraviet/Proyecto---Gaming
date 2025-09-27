using Proyecto_Gaming.Models;  // Agregar esta línea
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gaming.Models
{
    public class BibliotecaUsuario
    {
        [Key]  // Define la propiedad como clave primaria
        public int Id { get; set; }  // Clave primaria

        public int IdUsuario { get; set; }
        public int IdJuego { get; set; }
        public string Estado { get; set; }

        // Definir las relaciones entre las tablas (si es necesario)
        public Usuario Usuario { get; set; }
        public Juego Juego { get; set; }
    }
}
