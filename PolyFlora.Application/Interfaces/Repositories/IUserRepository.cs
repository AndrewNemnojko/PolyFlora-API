
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> AddAsync(User user);
        public Task<bool> RemoveAsync(Guid id);
        public Task<User?> GetByIdAsync(Guid id);
        public Task<User?> GetByEmailAsync(string email);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<bool> ChangeUserAsync(User user);
        public Task<bool> ChangeUserPasswordAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
    }
}
