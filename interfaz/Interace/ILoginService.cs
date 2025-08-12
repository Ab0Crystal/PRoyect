public interface ILoginService
{
    Task<bool> LoginAsync(LoginRequest request);
    Task<bool> RegisterAsync(RegistroRequest request);
    Task LogoutAsync();
}