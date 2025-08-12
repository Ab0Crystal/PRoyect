using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoWeb.Models;
using TodoWeb.Datos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace TodoWeb.Controllers
{
    public class CuentaController : Controller
    {
        private readonly TodoContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        private readonly ILoginService _loginService;

        public CuentaController(TodoContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
            _loginService = new LoginService(_context);
        }

        // GET: /Cuenta/Registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // POST: /Cuenta/Registro
        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string email, string password, string confirmarPassword)
        {
            if (password != confirmarPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }
            
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

        // GET: /Cuenta/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Tareas");

            return View();
        }

        // POST: /Cuenta/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBod] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Por favor, complete todos los campos.";
                return View();
            }

            var response = await _loginService.LoginAsync(loginRequest);
            var email = loginRequest.Email;
            var password = loginRequest.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "El correo y la contraseña son obligatorios.";
                return View();
            }
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
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = false,
                });

                return RedirectToAction("Index", "Tareas");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        // POST: /Cuenta/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
