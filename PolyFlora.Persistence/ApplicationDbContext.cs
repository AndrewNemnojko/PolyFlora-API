
using Microsoft.EntityFrameworkCore;
using PolyFlora.Core.Models;

namespace PolyFlora.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Flower> Flowers { get; set; }
    }
}
