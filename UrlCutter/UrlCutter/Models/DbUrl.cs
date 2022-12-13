using Microsoft.EntityFrameworkCore;

namespace UrlCutter.Models
{
    public class DbUrl : DbContext
    {
        public DbSet<URL> Urls { get; set; } = null!;

        public DbUrl() : base() 
        { 
            Database.EnsureCreated();
        }
        public DbUrl(DbContextOptions<DbUrl> options) : base(options) { }
    }
}
