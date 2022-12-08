using Microsoft.EntityFrameworkCore;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    public class DbManager
    {
        private readonly DbUrl _db;

        public DbManager()
        {
            _db = new DbUrl();
        }

        public async Task<bool> CheckDataInDb(string token, string url)
        {
            return await _db.Urls.AnyAsync(s => s.Token == token || (s.Token == token && s.LongUrl != url));
        }

        //todo
        public async Task<bool> CheckTokenInDb(string token, string next)
        {
            return true;
        }

        public async Task SaveToDbAsync(URL url)
        {
            await _db.Urls.AddAsync(url);
            await _db.SaveChangesAsync();
        }

    }
}
