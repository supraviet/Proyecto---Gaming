using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto___Gaming.Models;

namespace Proyecto___Gaming.Controllers;

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

    // Acción POST para procesar el registro
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Aquí iría la lógica de registro
            // Por ahora, solo redirigimos al Login si el modelo es válido
            return RedirectToAction("Login", "Home");
        }
        
        // Si hay errores de validación, mostramos la vista de registro nuevamente
        return View(model);
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