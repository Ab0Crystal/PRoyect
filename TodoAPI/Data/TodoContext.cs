using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;  // Asegúrate de que esta ruta sea correcta según tu estructura

namespace TodoAPI.Data
{
    public class TodoContext : DbContext
    {
        // Constructor que recibe las opciones de configuración de la base de datos
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        // DbSets que corresponden a las tablas en la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
