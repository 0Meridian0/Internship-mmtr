using System.IO.Hashing;
using System.Text;

namespace UrlCutter.Managers
{
    /// <summary>
    /// Отвечает за генерацию токена для указанной пользователем ссылки
    /// </summary>
    public class HashManager
    {
        private const string ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int MAXHASHLENGHT = 10;
        private readonly int alphabetLenght = ALPHABET.Length;

        public byte[] GenerateHash(string longUrl)
        {
            return Crc32.Hash(Encoding.UTF8.GetBytes(longUrl));
        }

        public byte[] IncreaseChars(ref byte[] tokenByte)
        {
            var i = tokenByte.Length - 1;

            if (tokenByte[i] >= byte.MaxValue/2)
            {
                while (i >= 0 && tokenByte[i] >= byte.MaxValue/2)
                {
                    tokenByte[i] = 0;
                    i--;
                }
                if (i > 0)
                {
                    tokenByte[i] += 1;
                }
            }
            else
            {
                tokenByte[i] += 1;
            }
            return tokenByte;
        }

        public string ConvertToBase62(byte[] bytes)
        {
            var strBuilder = new StringBuilder();
            for (int i = 0; i < MAXHASHLENGHT; i++)
            {
                strBuilder.Append(i < bytes.Length ? ALPHABET[bytes[i] % alphabetLenght] : ALPHABET[0]);
            }
            return strBuilder.ToString();
        }

        public byte[] ConvertToByte(string token)
        {
            var b = new byte[token.Length];
            for (int i = 0; i < token.Length; i++)
            {
                b[i] = (byte)ALPHABET.IndexOf(token[i]);
            }
            return b;
        }
    }
}
