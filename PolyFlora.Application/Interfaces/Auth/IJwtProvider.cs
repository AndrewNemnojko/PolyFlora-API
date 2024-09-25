
using PolyFlora.Core.Models;
using System.Security.Claims;

namespace PolyFlora.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
        public string GenerateRefreshToken();
        public ClaimsPrincipal? GetTokenPrincipal(string token);
    }
}
