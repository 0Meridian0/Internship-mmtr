using System.Text.RegularExpressions;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    public class UrlManager
    {
        private readonly DbManager _dbManager;
        private readonly HashManager _hashManager;

        public UrlManager()
        {
            _dbManager = new DbManager();
            _hashManager = new HashManager();
        }

        public async Task<URL> MakeUrl(string link) 
        {
            var token = link;
            do
            {
                token = await _hashManager.CreateToken(link);
            }
            while (!await _dbManager.CheckDataInDb(token, link));

            URL url = new(link, token);
            await _dbManager.SaveToDbAsync(url);

            return url; 
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
