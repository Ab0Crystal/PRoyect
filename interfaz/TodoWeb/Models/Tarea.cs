using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoWeb.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required   (ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; } 
        public string Descripcion { get; set; }

[Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public int Prioridad { get; set; } // 1: Nueva Tarea, 2: Tarea Atrasada, 3: Tarea Urgente
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool Completado { get; set; } 

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
