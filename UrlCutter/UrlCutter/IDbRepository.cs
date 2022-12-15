using UrlCutter.Models;

namespace UrlCutter
{
    public interface IDbRepository<T> where T : URL
    {
        Task<URL> GetDataFromDbAsync(string token);
        Task SaveToDbAsync(URL url);
    }
}
