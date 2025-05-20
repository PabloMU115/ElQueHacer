using Microsoft.AspNetCore.Mvc;
using ElQueHacer.Data;
using ElQueHacer.Models;

namespace ElQueHacer.Controllers
{
    public class TareasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TareasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //---------------------------- Tareas ------------------------------

        [HttpGet]
        public JsonResult listarTareas()
        {
            string usuarioId = Request.Cookies["usuario_id"];
            var tareas = (from tarea in _context.Tarea
                          where tarea.IdUsuario == usuarioId
                          select new
                          {
                              id = tarea.IdTarea,
                              titulo = tarea.Titulo,
                              descripcion = tarea.Descripcion,
                              completada = tarea.Completada
                          }).ToList();

            return Json(new { data = tareas });
        }

        [HttpPost]
        public IActionResult CrearTarea([FromBody] Tarea nuevaTarea)
        {
                _context.Tarea.Add(nuevaTarea);
                _context.SaveChanges();
                return Json(new { result = 1, message = "Tarea creada correctamente." });
        }

        [HttpDelete]
        public IActionResult DeleteTarea([FromBody] Tarea deleteTarea)
        {
            var tarea = _context.Tarea.FirstOrDefault(u => u.IdTarea == deleteTarea.IdTarea);

            if (tarea == null)
            {
                return NotFound(new { message = "Tarea no encontrada." });
            }

            _context.Tarea.Remove(tarea);
            _context.SaveChanges();

            return Json(new { result = true, message = "Usuario eliminado correctamente." });
        }

        [HttpPut]
        public IActionResult UpdateTarea([FromBody] Tarea updateTarea)
        {
            var tarea = _context.Tarea.FirstOrDefault(u => u.IdTarea == updateTarea.IdTarea);

            if (tarea == null)
            {
                return NotFound(new { message = "Tarea no encontrada." });
            }

            tarea.IdTarea = updateTarea.IdTarea;
            tarea.Titulo = updateTarea.Titulo;
            tarea.Descripcion = updateTarea.Descripcion;
            tarea.Completada = updateTarea.Completada;
            tarea.IdUsuario = updateTarea.IdUsuario;

            _context.Tarea.Update(tarea);
            _context.SaveChanges();

            return Json(new { result = true, message = "Usuario editada correctamente." });
        }


    }
}
