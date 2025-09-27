using Microsoft.AspNetCore.Mvc;
using Proyecto_Gaming.Data;
using Proyecto_Gaming.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción GET para mostrar el formulario de login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Acción POST para procesar el login
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Buscar el usuario en la base de datos usando el correo electrónico
        var user = _context.Usuarios.FirstOrDefault(u => u.correo_electronico == email);
        
        if (user != null && user.contraseña == password)  // Comparar la contraseña en texto plano
        {
            // Login correcto, guardamos el ID del usuario en la sesión
            HttpContext.Session.SetString("UserId", user.id_usuario.ToString());

            // Pasar el UserId a ViewData para que esté disponible en la vista Layout.cshtml
            ViewData["UserId"] = user.id_usuario;

            // Redirigir al catálogo de la biblioteca
            return RedirectToAction("Index", "Biblioteca");  // Redirigir a la vista del catálogo
        }
        else
        {
            // Si el correo o la contraseña son incorrectos, mostramos un mensaje de error
            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }
    }

    // Método para logout
    public IActionResult Logout()
    {
        // Limpiar la sesión del usuario
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");  // Redirigir al inicio
    }

    // Vista de registro
    public IActionResult Register()
    {
        return View();
    }

    // Método POST para registro
    [HttpPost]
    public IActionResult Register(string name, string dob, string email, string password, string biografia, string pais, string foto_perfil, string plataforma_preferida)
    {
        // Verificar si el correo electrónico ya está registrado
        var existingUser = _context.Usuarios.FirstOrDefault(u => u.correo_electronico == email);
        if (existingUser != null)
        {
            ViewBag.Error = "El correo ya está registrado.";
            return View();
        }

        // Convertir la fecha de nacimiento a DateTime y luego a UTC
        DateTime fechaNacimiento = DateTime.Parse(dob);
        if (fechaNacimiento.Kind == DateTimeKind.Unspecified)
        {
            fechaNacimiento = DateTime.SpecifyKind(fechaNacimiento, DateTimeKind.Utc);  // Asegurar que la fecha esté en UTC
        }

        // Asignar valores por defecto si no se proporciona alguno de estos campos
        string biografiaFinal = string.IsNullOrEmpty(biografia) ? "Sin biografía" : biografia;
        string paisFinal = string.IsNullOrEmpty(pais) ? "Desconocido" : pais;
        string fotoPerfilFinal = string.IsNullOrEmpty(foto_perfil) ? "Sin foto" : foto_perfil;
        string plataformaPreferidaFinal = string.IsNullOrEmpty(plataforma_preferida) ? "Ninguna" : plataforma_preferida;

        // Crear el nuevo usuario
        var newUser = new Usuario
        {
            nombre_usuario = name,
            nombre_real = name,  // Asignar nombre real (o lo que prefieras)
            fecha_nacimiento = fechaNacimiento,  // Usar fecha en UTC
            correo_electronico = email,
            contraseña = password,  // Guardar la contraseña en texto plano
            biografia = biografiaFinal,  // Asignar biografía
            pais = paisFinal,  // Asignar el valor para el país
            foto_perfil = fotoPerfilFinal,  // Asignar el valor para la foto
            plataforma_preferida = plataformaPreferidaFinal,  // Asignar la plataforma preferida
            fecha_registro = DateTime.UtcNow,  // Fecha de registro en UTC
            estado = "activo"  // Estado predeterminado (puedes modificarlo si lo deseas)
        };

        // Agregar el usuario a la base de datos y guardar los cambios
        _context.Usuarios.Add(newUser);
        _context.SaveChanges();

        // Redirigir al login después de registrar
        return RedirectToAction("Login");
    }

    // Vista de invitado
    public IActionResult Guest()
    {
        // Lógica para continuar como invitado
        return RedirectToAction("Index", "Home");
    }
}
