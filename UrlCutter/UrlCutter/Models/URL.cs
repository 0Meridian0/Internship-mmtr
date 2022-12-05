using System.IO.Hashing;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UrlCutter.Models
{
    public class URL
    {
        [Key]
        public int id { get; set; }
        public string? LongUrl { get; set; }
        public string? ShortUrl { get; set; }
        public string? Token { get; set; }

        public URL() { }
        public URL(string url)
        {
            if (IsUrl(url))
            {
                LongUrl = url;
                Token = CreateToken(url);
                ShortUrl = CreateTinyUrl(url, Token);
            }
            else
            {
                throw new Exception("Введенная строка не является url");
            }
        }

        public bool IsUrl(string url)
        {
            Regex regex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

            MatchCollection matches = regex.Matches(url);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }

        private string CreateToken(string longUrl)
        {
            Crc64 crc = new Crc64();
            crc.Append(Encoding.UTF8.GetBytes(longUrl));
            var token = Encoding.UTF8.GetString(crc.GetCurrentHash());
            Console.WriteLine($"Token lenght: {token.Length}\n");
            return token;
        }

        private string CreateTinyUrl(string longUrl, string token)
        {
            var split = longUrl.Split('/');

            string domen = "";
            for (int i = 0; i < 3; i++)
            {
                domen += split[i] + "/";
            }

            string tinyUrl = domen + token;
            return tinyUrl;
        }

        public override string ToString()
        {
            return $"LongUrl: {LongUrl} \nShortUrl: {ShortUrl} \nToken: {Token}";
        }
    }
}
