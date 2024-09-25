
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Core.Interfaces;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Services
{
    public class FlowerService
    {
        private readonly ICacheService _cacheService;
        private readonly IFlowerRepository _flowerRepository;
        public FlowerService(ICacheService cacheService, IFlowerRepository flowerRepository)
        {
            _cacheService = cacheService;
            _flowerRepository = flowerRepository;
        }

        public async Task<Flower?> GetFlowerByIdAsync(Guid id, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<Flower>($"flower-id-{id}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            var dbModel = await _flowerRepository.GetByIdAsync(id, ct);
            if(dbModel != null && cache)
            {
                await _cacheService.SetAsync<Flower>($"flower-id-{id}", dbModel);               
            }
            return dbModel;
        }
        public async Task<Flower?> GetFlowerByNameAsync(string tname, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<Flower>($"flower-tname-{tname}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            var dbModel = await _flowerRepository.GetByNameAsync(tname, ct);
            if (dbModel != null && cache)
            {
                await _cacheService.SetAsync<Flower>($"flower-tname-{tname}", dbModel);
            }
            return dbModel;
        }
    }
}
