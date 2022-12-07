using Microsoft.AspNetCore.Mvc;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly DbUrl db = new DbUrl();
        
        [HttpGet]
        public async Task<IActionResult> UrlCutter(string link)
        {
            db.Database.EnsureCreated();
            if (link != null)
            {
                URL url = new URL(link);

                if(!db.CheckDataInDb(url))
                    await db.SaveToDbAsync(url);
                return Ok(url.ToString());

                //Json(url.ToString());
                //return View(url);
            }
            return View();
        }
    }
}