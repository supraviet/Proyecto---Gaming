using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using Proyecto_Gaming.Models;
using Proyecto_Gaming.Data;

namespace Proyecto_Gaming.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public BibliotecaController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Biblioteca
        public async Task<IActionResult> Index(string categoria, string plataforma, string busqueda)
        {
            var juegos = new List<Juego>();
            var categorias = new List<string>();
            var plataformas = new List<string>();

            await using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Construir query base para juegos
                var query = "SELECT id_juego, nombre, categoria, plataforma, imagen, puntuacion_media FROM Juegos WHERE 1=1";
                var parameters = new List<NpgsqlParameter>();

                if (!string.IsNullOrEmpty(categoria) && categoria != "Todas")
                {
                    query += " AND categoria = @Categoria";
                    parameters.Add(new NpgsqlParameter("@Categoria", categoria));
                }

                if (!string.IsNullOrEmpty(plataforma) && plataforma != "Todas")
                {
                    query += " AND plataforma = @Plataforma";
                    parameters.Add(new NpgsqlParameter("@Plataforma", plataforma));
                }

                if (!string.IsNullOrEmpty(busqueda))
                {
                    query += " AND nombre LIKE @Busqueda";
                    parameters.Add(new NpgsqlParameter("@Busqueda", $"%{busqueda}%"));
                }

                // Ejecutar query para juegos
                await using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var juego = new Juego
                            {
                                IdJuego = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Categoria = reader.GetString(2),
                                Plataforma = reader.GetString(3),
                                Imagen = reader.IsDBNull(4) ? null : reader.GetString(4),
                                PuntuacionMedia = reader.IsDBNull(5) ? null : reader.GetDecimal(5)
                            };
                            juegos.Add(juego);
                        }
                    }
                }

                // Obtener categorías únicas
                await using (var command = new NpgsqlCommand("SELECT DISTINCT categoria FROM Juegos ORDER BY categoria", connection))
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categorias.Add(reader.GetString(0));
                    }
                }

                // Obtener plataformas únicas
                await using (var command = new NpgsqlCommand("SELECT DISTINCT plataforma FROM Juegos ORDER BY plataforma", connection))
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        plataformas.Add(reader.GetString(0));
                    }
                }
            }

            // Pasar datos a la vista
            ViewBag.Categorias = categorias;
            ViewBag.Plataformas = plataformas;
            ViewBag.CategoriaSeleccionada = categoria;
            ViewBag.PlataformaSeleccionada = plataforma;
            ViewBag.Busqueda = busqueda;

            return View(juegos);
        }

        // GET: Biblioteca/Detalles/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Juego juego = null;

            await using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT id_juego, nombre, categoria, plataforma, imagen, puntuacion_media FROM Juegos WHERE id_juego = @Id";
                
                await using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            juego = new Juego
                            {
                                IdJuego = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Categoria = reader.GetString(2),
                                Plataforma = reader.GetString(3),
                                Imagen = reader.IsDBNull(4) ? null : reader.GetString(4),
                                PuntuacionMedia = reader.IsDBNull(5) ? null : reader.GetDecimal(5)
                            };
                        }
                    }
                }
            }

            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }
    }
}