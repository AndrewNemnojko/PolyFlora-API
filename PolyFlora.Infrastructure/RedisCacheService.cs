
using Microsoft.Extensions.Configuration;
using PolyFlora.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace PolyFlora.Infrastructure
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;
        private readonly int EXPIRE_TIME;

        public RedisCacheService(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _database = redis.GetDatabase();
            _configuration = configuration;
            this.EXPIRE_TIME = int.Parse(_configuration
                .GetSection("Redis:ExpireTimeM").Value!);
        }
        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var jsonValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonValue, TimeSpan.FromMinutes(EXPIRE_TIME));
        }
    }
}
