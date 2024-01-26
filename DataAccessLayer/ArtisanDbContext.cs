using Microsoft.EntityFrameworkCore;
using WebApplication_Artisan_.Models;

namespace WebApplication_Artisan_.DataAccessLayer
{
    public class ArtisanDbContext : DbContext
    {
        public ArtisanDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
