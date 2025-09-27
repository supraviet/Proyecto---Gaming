using Proyecto_Gaming.Models;  // Agregar esta línea

public class BibliotecaUsuario
{
    public int IdUsuario { get; set; }
    public int IdJuego { get; set; }
    public string Estado { get; set; } // "Pendiente", "Jugando", "Completado"

    // Navegación a la entidad Juego
    public virtual Juego Juego { get; set; }
}
