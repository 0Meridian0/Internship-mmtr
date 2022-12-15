using Microsoft.EntityFrameworkCore;

namespace UrlCutter.Factory
{
    public interface IDatabaseType
    {
        public void UseDatabase(DbContextOptionsBuilder option) { }
    }
}
