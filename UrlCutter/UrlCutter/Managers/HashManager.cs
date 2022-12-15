using Base62;
using System.IO.Hashing;
using System.Text;

namespace UrlCutter.Managers
{
    /// <summary>
    /// Отвечает за генерацию токена для указанной пользователем ссылки
    /// </summary>
    public class HashManager
    {
        public byte[] GenerateHash(string longUrl)
        {
            return Crc32.Hash(Encoding.UTF8.GetBytes(longUrl));
        }

        public void IncreaseChars(ref byte[] d)
        {
            var i = d.Length - 1;

            if (d[i] >= byte.MaxValue/2)
            {
                while (i >= 0 && d[i] >= byte.MaxValue/2)
                {
                    d[i] = 0;
                    i--;
                }
                if (i > 0)
                {
                    d[i] += 1;
                }
                else
                {
                    d[0] += 1;
                    Array.Resize(ref d, d.Length + 1);
                }
            }
            else
            {
                d[i] += 1;
            }
        }

        public string EncodeByteTo62(byte[] d)
        {
            var _base62 = new Base62Converter();
            var encToken = Encoding.UTF8.GetString(d);
            var token = _base62.Encode(encToken);
            return CorrectTokenLenght(token);
        }

        private static string CorrectTokenLenght(string token)
        {
            if (token.Length > 10)
            {
                token = token[..10];
            }
            if (token.Length < 7)
            {
                token = token.Insert(token.Length, new string('0', 7 - token.Length));
            }
            return token;
        }
    }
}
