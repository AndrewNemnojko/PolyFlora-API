
using Microsoft.EntityFrameworkCore;
using PolyFlora.Application.DTOs.Common;
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Core.Enums;
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

        public async Task<IEnumerable<Flower>> GetAllAsync( CancellationToken ct)
        {
            var result = await _context.Flowers             
               .Include(i => i.Image)
               .Include(x => x.FlowerParent)
               .Include(c => c.FlowerChildrens)
               .AsNoTracking().ToListAsync(ct);
            return result;
        }

        public async Task<Flower?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _context.Flowers
                .Include(f => f.CultureDetails)
                .Include(x => x.Image)
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
                .FirstOrDefaultAsync(f => f.Id == id, ct);
            return result;
        }

        public async Task<Flower?> GetByIdWithCultureAsync(Guid id, LanguageCode lang, CancellationToken ct)
        {
            var result = await _context.Flowers
                .Include(f => f.CultureDetails.Where(c => c.LanguageCode == lang))
                .Include (x => x.Image)
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
                .FirstOrDefaultAsync(f => f.Id == id, ct);
            return result;
        }

        public async Task<Flower?> GetByNameWithCultureAsync(string name, LanguageCode lang, CancellationToken ct)
        {
            var result = await _context.Flowers
                .Include(f => f.CultureDetails.Where(c => c.LanguageCode == lang))
                .Include(x => x.Image)
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
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

        public async Task<IEnumerable<Flower>> GetFlowersWithPaginationAsync(int pageNumber, int pageSize, LanguageCode lang, CancellationToken ct)
        {
            return await _context.Flowers
                .Include(f => f.CultureDetails.Where(c => c.LanguageCode == lang))
                .Include(i => i.Image)              
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(CancellationToken ct)
        {
            return await _context.Flowers.CountAsync();
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var existingFlower = await _context.Flowers
                .Include(x => x.Image)
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
                .FirstOrDefaultAsync(f => f.Id == id);
            if(existingFlower == null)
            {
                return false;
            }
            _context.Flowers.Remove(existingFlower);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Flower>> SearchByNameAsync(string name, LanguageCode lang, CancellationToken ct)
        {
            var result = await _context.Flowers
                .Include(x => x.Image)
                .Include(x => x.FlowerParent)
                .Include(c => c.FlowerChildrens)
                .Where(f => f.CultureDetails
                    .Any(c => c.LanguageCode == lang && c.Name.Contains(name)))
                .ToListAsync(ct);
            return result; 
        }

        public async Task<Flower?> UpdateAsync(Flower flower)
        {
            var existingFlower = await _context.Flowers
                .Include(l => l.CultureDetails)
                .Include(i => i.Image)
                .Include(p => p.FlowerParent)
                .Include(c =>  c.FlowerChildrens)
                .FirstOrDefaultAsync(x => x.Id == flower.Id);
            if (existingFlower == null)
            {
                return null; 
            }
            foreach(var culture in flower.CultureDetails)
            {
                var existingCulture = existingFlower.CultureDetails
                    .FirstOrDefault(c => c.LanguageCode == culture.LanguageCode);

                if (existingCulture != null)
                {                  
                    existingCulture.Name = culture.Name;
                    existingCulture.Description = culture.Description;
                }
                else
                {                   
                    existingFlower.CultureDetails.Add(culture);
                }
            }
            
            existingFlower.TName = flower.TName;
            existingFlower.Price = flower.Price;           
            existingFlower.InStock = flower.InStock;
            
            existingFlower.Image = flower.Image != null 
                ? flower.Image : existingFlower.Image;

            existingFlower.FlowerParent = flower.FlowerParent != null 
                ? flower.FlowerParent : existingFlower.FlowerParent; 

            existingFlower.FlowerChildrens = flower.FlowerChildrens != null 
                ? flower.FlowerChildrens : existingFlower.FlowerChildrens;
        
            var result = await _context.SaveChangesAsync();
           
            return existingFlower;
        }
    }
}
