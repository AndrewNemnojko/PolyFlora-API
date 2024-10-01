
namespace PolyFlora.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public bool IsLogedIn { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
