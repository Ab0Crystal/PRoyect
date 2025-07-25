namespace TodoAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public ICollection<Tarea> Tareas { get; set; }
    }
}
