
namespace PolyFlora.Application.DTOs.Auth
{
    public record AuthResponse
    (
        string Token,
        string RefreshToken
    );
}
