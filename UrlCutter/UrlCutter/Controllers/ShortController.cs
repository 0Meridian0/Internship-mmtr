using Microsoft.AspNetCore.Mvc;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    [Route("/{ShortUrl}")]
    public class ShortController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(string shortUrl, DbUrl db)
        {
            if (!string.IsNullOrEmpty(shortUrl))
            {
                var resp = await db.Urls.Where(s => s.Token == shortUrl.ToString()).FirstOrDefault();
                if (resp != null && !string.IsNullOrEmpty(resp.LongUrl))
                    return Redirect(resp.LongUrl);
            }
            return Redirect("~/");
        }
    }
}
