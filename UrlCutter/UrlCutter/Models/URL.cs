using System.IO.Hashing;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Base62;

namespace UrlCutter.Models
{
    public class URL
    {
        [Key]
        public int id { get; set; }
        public string? LongUrl { get; set; }
        public string? Token { get; set; }

        public URL() { }
        public URL(string url)
        {
            if (IsUrl(url))
            {
                LongUrl = url;
                Token = CreateToken(url);
            }
            else
            {
                Token = "*Ссылка не является url*";
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
            var hashByte = Crc32.Hash(Encoding.Unicode.GetBytes(longUrl));
            string hashStr = Encoding.Unicode.GetString(hashByte);

            var base62 = new Base62Converter(); 
            var token = base62.Encode(hashStr);
            //Console.WriteLine(token.Length);

            return token;
        }
        
        public override string ToString()
        {
            return $"LongUrl: {LongUrl}\nToken: {Token}";
        }
    }
}
