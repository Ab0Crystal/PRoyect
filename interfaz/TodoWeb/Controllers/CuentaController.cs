using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoWeb.Models;
using TodoWeb.Datos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace TodoWeb.Controllers
{
    public class CuentaController : Controller
    {
        private readonly TodoContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public CuentaController(TodoContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        // Mostrar formulario registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // Procesar registro
        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string email, string password, string confirmarPassword)
        {
            if (password != confirmarPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            // Validar si ya existe el email o usuario
            if (_context.Usuarios.Any(u => u.Email == email || u.NombreUsuario == nombreUsuario))
            {
                ViewBag.Error = "El correo o usuario ya existe.";
                return View();
            }

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Email = email
            };

            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, password);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // Mostrar formulario login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Procesar login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, password);

            if (resultado == PasswordVerificationResult.Success)
            {
                // Aquí podrías crear sesión o cookie para mantener al usuario logueado (más adelante si quieres)
                return RedirectToAction("Index", "Tareas"); // O la página principal de tu app
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }
        // Cerrar sesión
        [HttpPost]
        public IActionResult Logout()
        {
            // Aquí podrías eliminar la sesión o cookie del usuario
            return RedirectToAction("Index", "Home"); // O la página principal de tu app
        }
    }
}
