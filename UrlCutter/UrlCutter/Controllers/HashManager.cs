using Base62;
using System.IO.Hashing;
using System.Text;

namespace UrlCutter.Controllers
{

    public class HashManager
    {
        public static string CreateToken(string longUrl)
        {
            var hashByte = Crc32.Hash(Encoding.Unicode.GetBytes(longUrl));
            string hashStr = Encoding.Unicode.GetString(hashByte);

            var base62 = new Base62Converter();
            var encode = base62.Encode(hashStr);

            var token = CheckTokenLenght(encode);

            return token;
        }

        private static string CheckTokenLenght(string token)
        {
            if (token.Length > 10)
            {
                token = token[..10];
            }
            while (token.Length < 7)
            {
                token = GenerateRandom(token);
            }
            return token;
        }

        private static string GenerateRandom(string text)
        {
            Random rand = new();
            int index = rand.Next(0, text.Length);
            return text + text[index];
        }
    }
}
