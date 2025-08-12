public class LoginService : ILoginService
{
    private readonly TodoContext _context;
    private readonly PasswordHasher<Usuario> _passwordHasher;

    public LoginService(TodoContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        try
        {
            var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null)
                return false;

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, request.Password);
            return result == PasswordVerificationResult.Success;
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented here)
            return false;
        }
        
    }

    public async Task<bool> RegisterAsync(RegistroRequest request)
    {
        if (request.Password != request.ConfirmarPassword)
            return false;

        if (await _context.Usuarios.AnyAsync(u => u.Email == request.Email || u.NombreUsuario == request.NombreUsuario))
            return false;

        var usuario = new Usuario
        {
            NombreUsuario = request.NombreUsuario,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(null, request.Password)
        };

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task LogoutAsync()
    {
        // Implement logout logic if necessary
    }
}