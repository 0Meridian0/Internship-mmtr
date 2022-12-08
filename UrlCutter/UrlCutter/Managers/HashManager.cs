using Base62;
using System.Collections;
using System.Collections.Generic;
using System.IO.Hashing;
using System.Text;
using UrlCutter.Models;

namespace UrlCutter.Managers
{
    public class HashManager
    {

        private readonly DbManager _dbManager;

        Base62Converter base62 = new Base62Converter();

        public HashManager()
        {
            _dbManager = new DbManager();
        }

        public async Task<string> CreateToken(string longUrl)
        {
            // проверка в бд :v
            // посимвольное инкрементирование :^

            // добавление в бд

            var strBite = Encoding.UTF8.GetBytes(longUrl);
            var hashByte = Crc32.Hash(strBite);
            string hashStr = Encoding.UTF8.GetString(hashByte);

            var token = base62.Encode(hashStr);

            token = CorrectTokenLenght(token);
            IncreaseChars(token);


            var url = new URL(longUrl, token);
            // проверка в бд и инекримент \\
            /*do
            {

            } while (await );*/


            return token;
        }

        private string CorrectTokenLenght(string token)
        {
            if (token.Length > 10)
            {
                token = token[..10];
            }
            if (token.Length < 7)
            {
                token.Insert(token.Length - 1, new string('0', 7 - token.Length));
            }
            return token;
        }

        private void IncreaseChars(string token)
        {
            // чето мне не нравится эта шняга...
            Console.WriteLine(token);
            var decode = base62.Decode(token);
            var d = Encoding.UTF8.GetBytes(decode);
            var len = 1;
            while (true)
            {
                if (d[d.Length - len] + 1 == 256 && len != d.Length)
                {
                    d[d.Length - len] = 0;
                    len++;
                }
                else
                {
                    d[d.Length - len] += 1;
                }
                var encToken = Encoding.UTF8.GetString(d);
                token = base62.Encode(encToken); 
                
                // с ~150 начинает дубли кидать и нужен откат по массиву, чтобы проверять все значения а не только ед, десятков и т.д
                Console.WriteLine(token);
            }
            d[d.Length - len] += 1;


            /*var encToken = Encoding.UTF8.GetString(d);
            token = base62.Encode(encToken);*/
            //var decod = Encoding.UTF8.GetBytes(token);
            var a = 0;














            /*var base62 = new Base62Converter();
            var decodeToken = base62.Decode(token);
            var strToken = Encoding.Unicode.GetBytes((string)decodeToken);

            byte[] nBite = new byte[bite.Length + (7 - token.Length)*2];


            var bite = Encoding.UTF8.GetBytes(token);
            byte[] nBite = new byte[bite.Length + (7-token.Length)];
            Array.Copy(bite, nBite, bite.Length);
            //nBite[nBite.Length - 1] += 1;
            token = Encoding.UTF8.GetString(nBite);

            base62 = new Base62Converter();
            token = base62.Encode(token); //11Q8SjcwrZ*/

            //return token;
        }






















        private string GenerateRandom(string text)
        {
            Random rand = new();
            int index = rand.Next(0, text.Length);
            return text + text[index];
        }


        private string NextPosibleToken(string token) // инкрементирование
        {
            var bite = Encoding.Unicode.GetBytes(token);
            BitArray bit = new BitArray(bite);

            Array.Reverse(bite, 0, bit.Length);

            for (int i = 0; i < bite.Length - 1; i++)
            {

            }

            BitArray sum = new BitArray(bit.Length);
            sum[-1] = true;

            return "";

        }
    }
}
