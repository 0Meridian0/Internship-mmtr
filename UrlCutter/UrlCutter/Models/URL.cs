using System.ComponentModel.DataAnnotations;

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
