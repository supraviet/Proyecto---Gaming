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

        // DbSet para Usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        // DbSet para BibliotecaUsuario (para almacenar juegos en la biblioteca de cada usuario)
        public DbSet<BibliotecaUsuario> BibliotecaUsuario { get; set; }

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

            // Configuración para la tabla Usuarios
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios"); // Nombre de la tabla en la base de datos
                entity.HasKey(u => u.id_usuario); // Clave primaria

                entity.Property(u => u.id_usuario)
                    .HasColumnName("id_usuario")
                    .ValueGeneratedOnAdd(); // SERIAL en PostgreSQL

                entity.Property(u => u.nombre_usuario)
                    .HasColumnName("nombre_usuario")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.nombre_real)
                    .HasColumnName("nombre_real")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.correo_electronico)
                    .HasColumnName("correo_electronico")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.contraseña)
                    .HasColumnName("contraseña")
                    .IsRequired();

                entity.Property(u => u.fecha_nacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .IsRequired();

                entity.Property(u => u.biografia)
                    .HasColumnName("biografia")
                    .HasMaxLength(500);

                entity.Property(u => u.pais)
                    .HasColumnName("pais")
                    .HasMaxLength(100);

                entity.Property(u => u.foto_perfil)
                    .HasColumnName("foto_perfil")
                    .HasMaxLength(500);

                entity.Property(u => u.plataforma_preferida)
                    .HasColumnName("plataforma_preferida")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.fecha_registro)
                    .HasColumnName("fecha_registro")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(u => u.estado)
                    .HasColumnName("estado")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configuración para la tabla BibliotecaUsuario
            modelBuilder.Entity<BibliotecaUsuario>(entity =>
            {
                entity.HasKey(bu => new { bu.IdUsuario, bu.IdJuego }); // Clave primaria compuesta

                entity.ToTable("BibliotecaUsuario");

                entity.Property(bu => bu.Estado)
                    .HasColumnName("estado")
                    .IsRequired()
                    .HasMaxLength(50);

                // Configuración de las relaciones
                entity.HasOne(bu => bu.Usuario)
                    .WithMany(u => u.BibliotecaUsuarios)
                    .HasForeignKey(bu => bu.IdUsuario);

                entity.HasOne(bu => bu.Juego)
                    .WithMany(j => j.BibliotecaUsuarios)
                    .HasForeignKey(bu => bu.IdJuego);
            });
        }
    }
}
