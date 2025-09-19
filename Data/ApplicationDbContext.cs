using Microsoft.EntityFrameworkCore;
using Proyecto_Gaming.Models;

namespace Proyecto_Gaming.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet para la tabla Juegos
        public DbSet<Juego> Juegos { get; set; }

        // Si tienes otras tablas, agrégales aquí:
        // public DbSet<Usuario> Usuarios { get; set; }
        // public DbSet<Reseña> Reseñas { get; set; }
        // etc...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración específica para la tabla Juegos
            modelBuilder.Entity<Juego>(entity =>
            {
                entity.ToTable("Juegos"); // Nombre de la tabla en la base de datos
                entity.HasKey(j => j.IdJuego); // Clave primaria
                
                entity.Property(j => j.IdJuego)
                    .HasColumnName("id_juego")
                    .ValueGeneratedOnAdd(); // SERIAL en PostgreSQL
                
                entity.Property(j => j.Nombre)
                    .HasColumnName("nombre")
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(j => j.Categoria)
                    .HasColumnName("categoria")
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(j => j.Plataforma)
                    .HasColumnName("plataforma")
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(j => j.Imagen)
                    .HasColumnName("imagen")
                    .HasMaxLength(500);
                
                entity.Property(j => j.PuntuacionMedia)
                    .HasColumnName("puntuacion_media")
                    .HasColumnType("decimal(2,1)")
                    .HasPrecision(2, 1);
            });

            // Aquí puedes agregar configuraciones para otras tablas...
        }
    }
}