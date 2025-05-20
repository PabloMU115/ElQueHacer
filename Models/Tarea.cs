using System.ComponentModel.DataAnnotations;

namespace ElQueHacer.Models
{
    public class Tarea
    {
        [Key]
        public string IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Completada { get; set; }

        // Identificador anónimo de sesión
        public string IdUsuario { get; set; }

        public Usuarios Usuario { get; set; }
    }

}
