using PolyFlora.Application.Interfaces.Auth;
using PolyFlora.Core.Models;
using System.Security.Claims;

namespace PolyFlora.Infrastructure.Security
{
    public class JwtProvider : IJwtProvider
    {
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal? GetTokenPrincipal(string token)
        {
            throw new NotImplementedException();
        }
    }
}
