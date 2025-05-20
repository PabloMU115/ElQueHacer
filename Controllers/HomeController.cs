using System.Diagnostics;
using ElQueHacer.Data;
using ElQueHacer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElQueHacer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("usuario_id"))
            {
                // Crea un nuevo usuario
                var nuevoUsuario = new Usuarios
                {
                    IdUsuario = Guid.NewGuid().ToString(),
                    Nombre = "Visitante_" + DateTime.Now.Ticks
                };

                _context.Usuario.Add(nuevoUsuario);
                _context.SaveChanges();

                // Guarda la cookie con el ID del usuario para futuras visitas
                Response.Cookies.Append("usuario_id", nuevoUsuario.IdUsuario, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = true,
                    IsEssential = true
                });
            }

            var usuarioId = Request.Cookies["usuario_id"];
            ViewBag.UsuarioId = usuarioId;
            return View();
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
