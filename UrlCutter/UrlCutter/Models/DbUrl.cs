using Microsoft.EntityFrameworkCore;
using System.IO;

namespace UrlCutter.Models
{
    public class DbUrl : DbContext
    {
        public DbSet<URL> Urls { get; set; }

        private readonly string _dbpath = GeneratePath();

        public DbUrl()
        {
            Database.EnsureCreated();
        }

        private static string GeneratePath()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return System.IO.Path.Join(path, "DbUrl.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbpath}");
    }
}
