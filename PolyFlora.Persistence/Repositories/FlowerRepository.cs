
using Microsoft.EntityFrameworkCore;
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Core.Models;

namespace PolyFlora.Persistence.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly ApplicationDbContext _context;
        public FlowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Flower> AddAsync(Flower flower)
        {
            var result = await _context.Flowers
                .AddAsync(flower);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IList<Flower>> GetAllAsync(CancellationToken ct)
        {
            var result = await _context.Flowers
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
                .AsNoTracking().ToListAsync(ct);

            return result;
        }

        public async Task<Flower?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _context.Flowers
                //.Include(x => x.FlowerParent)
                //.Include(c => c.FlowerChildrens)
                .FirstOrDefaultAsync(f => f.Id == id, ct);
            return result;
        }

        public async Task<Flower?> GetByNameAsync(string name, CancellationToken ct)
        {
            var result = await _context.Flowers
                //.Include(x => x.FlowerParent)
                //.Include(c => c.FlowerChildrens)
                .FirstOrDefaultAsync(f => f.TName == name, ct);
            return result;
        }

        public async Task<IEnumerable<Flower>> GetFlowersByIdsAsync(IEnumerable<Guid> flowersIds)
        {
            var result = await _context.Flowers
                .Where(f => flowersIds.Contains(f.Id))
                .ToListAsync();
            return result;
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Flower>> SearchByNameAsync(string name, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Flower flower)
        {
            throw new NotImplementedException();
        }
    }
}
