
using PolyFlora.Core.Enums;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Interfaces.Repositories
{
    public interface IFlowerRepository
    {
        public Task<Flower> AddAsync(Flower flower);
        public Task<bool> RemoveAsync(Guid id);
        public Task<Flower?> GetByIdWithCultureAsync(Guid id, LanguageCode lang , CancellationToken ct);
        public Task<Flower?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Flower>> GetFlowersByIdsAsync(IEnumerable<Guid> flowersIds);
        public Task<Flower?> GetByNameWithCultureAsync(string name, LanguageCode lang, CancellationToken ct);
        public Task<IEnumerable<Flower>> SearchByNameAsync(string name, LanguageCode lang, CancellationToken ct);
        public Task<IEnumerable<Flower>> GetAllAsync(CancellationToken ct);
        public Task<IEnumerable<Flower>> GetFlowersWithPaginationAsync(int pageNumber,  int pageSize, LanguageCode lang, CancellationToken ct);
        public Task<int> GetTotalCountAsync(CancellationToken ct);
        public Task<Flower?> UpdateAsync(Flower flower);
    }
}
