using PolyFlora.Application.Interfaces.Auth;

namespace PolyFlora.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            throw new NotImplementedException();
        }

        public bool Verify(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
