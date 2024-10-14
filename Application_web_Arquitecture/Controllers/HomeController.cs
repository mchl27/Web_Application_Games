using Application_web_Arquitecture.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application_web_Arquitecture.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbWebApplicationContext _context;

        public HomeController(DbWebApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string contrasena)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Username == nombreUsuario && u.Contraseña == contrasena);

            if (usuario != null)
            {
                // Iniciar sesión exitosa.
                return RedirectToAction("Index", "Main");
            }
            else
            {
                // Nombre de usuario o contraseña incorrectos.
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
