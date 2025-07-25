using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de la base de datos para usar SQL Server
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para controlar las solicitudes HTTP
builder.Services.AddControllers();

var app = builder.Build();

// Configuración de las rutas y middleware de la aplicación
app.MapControllers();

// Ejecutar la aplicación
app.Run();
