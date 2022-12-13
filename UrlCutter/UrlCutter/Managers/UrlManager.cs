using System.Text.RegularExpressions;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    /// <summary>
    /// Отвечает за конечное создание url записи для бд
    /// </summary>
    public class UrlManager
    {
        private readonly DbManager? _dbManager;
        private readonly HashManager? _hashManager;

        public UrlManager(HashManager hashManager, DbManager dbManager)
        {
            _hashManager = hashManager;
            _dbManager = dbManager;
        }

        public async Task<URL> MakeUrl(string link) 
        {
            var crc32 = _hashManager!.GenerateHash(link);
            URL url;

            while (true)
            {
                var token = _hashManager.EncodeByteTo62(crc32);
                var resp = await _dbManager!.GetDataFromDbAsync(token);

                if(resp is null)
                {
                    url = new URL(link, token);
                    await _dbManager.SaveToDbAsync(url);
                    return url;
                }

                if(resp.LongUrl == link)
                {
                    url = new URL(link, token);
                    return url;
                }
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
    }
}
