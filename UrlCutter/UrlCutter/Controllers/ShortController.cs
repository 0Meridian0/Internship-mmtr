using Microsoft.AspNetCore.Mvc;
using System;
using UrlCutter.Models;

namespace UrlCutter.Controllers
{
    [Route("/{ShortUrl}")]
    public class ShortController : Controller
    {

        DbUrl db = new DbUrl();
        [HttpGet]
        public IActionResult Index(string shortUrl)
        {
            db.ShowDbContent();
            if (!string.IsNullOrEmpty(shortUrl.ToString()))
            {
                var resp = db.Urls.Where(s => s.Token == shortUrl.ToString()).FirstOrDefault();
                if (resp != null)
                    return Redirect(resp.LongUrl);
            }
            return Redirect("~/");
        }
    }
}
