using Microsoft.EntityFrameworkCore;
using UrlCutter.Models;

namespace UrlCutter.Controllers
{
    public class DbManager
    {
        public static async Task<bool> CheckDataInDb(string token, string url, DbUrl db)
        {
            return await db.Urls.AnyAsync(s => s.Token == token || (s.Token == token && s.LongUrl != url));
        }

        public static async Task SaveToDbAsync(URL url, DbUrl db)
        {
            await db.Urls.AddAsync(url);
            await db.SaveChangesAsync();
        }

    }
}
