using System.ComponentModel.DataAnnotations;

namespace TodoWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required] 
        [StringLength(20, ErrorMessage = "El nombre de usuario no puede exceder los 20 caracteres.")]
        public string NombreUsuario { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
