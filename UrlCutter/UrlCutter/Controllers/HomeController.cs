using Microsoft.AspNetCore.Mvc;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        DbUrl db = new DbUrl();
        
        [HttpGet]
        public IActionResult UrlCutter(string link)
        {
            db.Database.EnsureCreated();
            if (link != null)
            {
                URL url = new URL(link);
                //Console.WriteLine(url.ToString());

                if(!db.CheckDataInDb(url))
                    db.SaveToDb(url);
                return Ok(url.ToString());// Json(url.ToString());
                //return View(url);
            }
            return View();
        }
    }
}