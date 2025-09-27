using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Proyecto_Gaming.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string? nombre_real { get; set; }  // Permitir NULL para nombre_real
        public string correo_electronico { get; set; }
        public string contraseña { get; set; }
        public DateTime fecha_nacimiento { get; set; }

        public string? biografia { get; set; }  // Permitir NULL para biografía
        public string? pais { get; set; }  // Permitir NULL para pais
        public string? foto_perfil { get; set; }  // Permitir NULL para foto_perfil
        public string? plataforma_preferida { get; set; }  // Permitir NULL para plataforma_preferida
        
        public DateTime fecha_registro { get; set; } = DateTime.UtcNow;  // Fecha de registro en UTC
        public string? estado { get; set; }  // Permitir NULL para estado

        // Relación uno a muchos con BibliotecaUsuario
        public ICollection<BibliotecaUsuario> BibliotecaUsuarios { get; set; }
    }
}
