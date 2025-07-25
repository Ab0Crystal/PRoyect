using System;

namespace TodoWeb.Models
{
    public class Tarea
    {
        public int Id { get; set; }

[Required   (ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; } 
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; } 
        public bool Completado { get; set; } 

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
