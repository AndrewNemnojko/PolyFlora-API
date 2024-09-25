
using PolyFlora.Core.Interfaces;

namespace PolyFlora.Infrastructure
{
    public class RedisCacheService : ICacheService
    {
        public Task<bool> ExistsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
