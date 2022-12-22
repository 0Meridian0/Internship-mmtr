using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    /// <summary>
    /// Отвечает за конечное создание url записи для бд
    /// </summary>
    public class UrlManager
    {
        private static ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();
        private readonly IDbRepository<URL> _dbManager;
        private readonly HashManager? _hashManager;

        public UrlManager(HashManager hashManager, DbRepositoryManager dbManager)
        {
            _hashManager = hashManager;
            _dbManager = dbManager;
        }
        
        public async Task<URL> MakeUrl(string link)
        {
            var crc32 = _hashManager!.GenerateHash(link);

            while (true)
            {
                var token = _hashManager.ConvertToBase62(crc32);

                if (_cache.ContainsKey(token) is true)
                {
                    if (_cache[token] == link)
                    {
                        return new URL(link, token);
                    }
                }
                else
                {
                    var resp = await _dbManager.GetDataFromDbAsync(token);

                    if (resp is null)
                    {
                        URL url = new URL(link, token);
                        await _dbManager.SaveToDbAsync(url);
                        _cache.TryAdd(token, link);
                        return url;
                    }
                    _cache.TryAdd(resp.Token, resp.LongUrl);

                    if (resp.LongUrl == link)
                    {
                        return new URL(link, token);
                    }
                }

                crc32 = _hashManager.ConvertToByte(token);
                _hashManager.IncreaseChars(ref crc32);
            }
        }


        
        public bool IsUrl(string url)
        {
            Regex regex = new("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9А-Яа-я@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9А-Яа-я()]{1,6}\\b(?:[-a-zA-Z0-9А-Яа-я()@:%_\\+.~#?&\\/=]*)$");

            MatchCollection matches = regex.Matches(url);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static Random random = new Random();

        public string RandomString()
        {
            const string ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(ALPHABET, random.Next(50,100))
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
