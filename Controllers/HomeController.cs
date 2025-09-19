using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gaming.Models;

namespace Proyecto___Gaming.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Acción para la página de Login
        public IActionResult Login()
        {
            return View();
        }

        // Acción para la página de Registro
        public IActionResult Register()
        {
            return View();
        }

        // Acción POST para procesar el login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Aquí iría la lógica de autenticación
            // Por ahora, solo redirigimos al Index si los campos no están vacíos
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Error = "Por favor, complete todos los campos";
            return View();
        }

        // Acción POST para procesar el registro (eliminado RegisterViewModel)
        [HttpPost]
        public IActionResult Register(string username, string password, string confirmPassword)
        {
            // Validación simple (podrías agregar más validaciones)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || password != confirmPassword)
            {
                ViewBag.Error = "Por favor, complete todos los campos correctamente.";
                return View();
            }

            // Lógica de registro aquí, por ejemplo, guardar el usuario en la base de datos

            // Si el registro es exitoso, redirigimos al login
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
