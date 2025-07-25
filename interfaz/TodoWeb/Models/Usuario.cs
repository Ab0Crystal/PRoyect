using System.Collections.Generic;
namespace TodoWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Tarea> Tareas { get; set; }
    }
}
