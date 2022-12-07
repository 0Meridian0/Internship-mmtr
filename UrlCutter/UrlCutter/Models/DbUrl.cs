using Microsoft.EntityFrameworkCore;

namespace UrlCutter.Models
{
    public class DbUrl : DbContext
    {
        public DbSet<URL> Urls { get; set; }
        public string Dbpath { get; }

        public DbUrl()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Dbpath = System.IO.Path.Join(path, "DbUrl.db");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={Dbpath}");
    }
}
