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
        public string? Token { get; set; }
        public string? LongUrl { get; set; }

        public URL() { }
        public URL(string url, string token)
        {
            LongUrl = url;
            Token = token;
        }
        
        public override string ToString()
        {
            return $"LongUrl: {LongUrl}\nToken: {Token}";
        }
    }
}
