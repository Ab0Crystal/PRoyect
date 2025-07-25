using Microsoft.EntityFrameworkCore;
using TodoWeb.Models; 

namespace TodoWeb.Datos
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; } 
    }
}