using System.ComponentModel.DataAnnotations;

namespace ElQueHacer.Models
{
    public class Usuarios
    {
        [Key]
        public String IdUsuario { get; set; }
        public string Nombre { get; set; }

        public ICollection<Tarea> Tareas{ get; set; }
    }
}
