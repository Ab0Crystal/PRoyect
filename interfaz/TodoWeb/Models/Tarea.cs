using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWeb.Models
{
    public enum Prioridad
    {
        Baja = 1,
        Media = 2,
        Alta = 3
    }

    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public Prioridad Prioridad { get; set; } // Enum ahora

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool Completado { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
