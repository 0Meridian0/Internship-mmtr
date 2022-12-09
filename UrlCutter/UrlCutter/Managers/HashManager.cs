using Base62;
using System.IO.Hashing;
using System.Text;

namespace UrlCutter.Managers
{
    public class HashManager
    {
        private readonly DbManager _dbManager;
        private readonly Base62Converter _base62 = new Base62Converter();

        public HashManager()
        {
            _dbManager = new DbManager();
        }

        public async Task<string> CreateToken(string longUrl)
        {
            var hashByte = Crc32.Hash(Encoding.UTF8.GetBytes(longUrl));
            var token = EncodeByteTo62(hashByte);

            if(!await _dbManager.CheckDataInDb(token, longUrl))
            {
                return token;
            }
            else
            {
                while (await _dbManager.CheckDataInDb(token, longUrl))
                {
                    token = IncreaseChars(token);
                }
                return token;
            }
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

        private string IncreaseChars(string token)
        {
            var d = DecodeByteFrom62(token);
            var i = d.Length - 1;

            if (d[i] >= 127)
            {
                while (d[i] >= 127)
                {
                    d[i] = 0;

                    if (i > 0)
                    {
                        i--;
                    }
                    else
                    {
                        break;
                    }
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

            token = EncodeByteTo62(d);
            
            return token;
        }

        private byte[] DecodeByteFrom62(string token)
        {
            var tokenDec = _base62.Decode(token);
            return Encoding.UTF8.GetBytes(tokenDec);
        }

        private string EncodeByteTo62(byte[] d)
        {
            var encToken = Encoding.UTF8.GetString(d);
            var token = _base62.Encode(encToken);
            return CorrectTokenLenght(token);
        }
    }
}
