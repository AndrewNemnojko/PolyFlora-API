
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Interfaces.Repositories
{
    public interface IFlowerRepository
    {
        public Task<Flower> AddAsync(Flower flower);
        public Task<bool> RemoveAsync(Guid id);
        public Task<Flower?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Flower>> GetFlowersByIdsAsync(IEnumerable<Guid> flowersIds);
        public Task<Flower?> GetByNameAsync(string name, CancellationToken ct);
        public Task<IList<Flower>> SearchByNameAsync(string name, CancellationToken ct);
        public Task<IList<Flower>> GetAllAsync(CancellationToken ct);
        public Task<bool> UpdateAsync(Flower flower);
    }
}
