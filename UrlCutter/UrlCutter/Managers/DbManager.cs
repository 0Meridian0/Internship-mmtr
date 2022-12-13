using Microsoft.EntityFrameworkCore;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    /// <summary>
    /// Отвечает за все обращения к бд
    /// </summary>
    public class DbManager
    {
        private readonly DbUrl _db;

        public DbManager(){ }

        public DbManager(DbUrl db)
        {
            _db = db;
        }

        public async Task<URL> GetDataFromDbAsync(string token)
        {
            return await _db.Urls.FirstOrDefaultAsync(s => s.Token == token);
        }

        public async Task SaveToDbAsync(URL url)
        {
            await _db.Urls.AddAsync(url);
            await _db.SaveChangesAsync();
        }
    }
}
