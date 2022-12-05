using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlCutter.Models;

namespace UrlCutter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult UrlCutter(string id)
        {
            if (id != null)
            {
                URL url = new URL(id);

                return Json(url.ToString());
            }
            return View();
        }
        [HttpPost]
        public IActionResult UrlCutter(URL personUrl) 
        {
            if (personUrl != null)
            {
                URL url = new URL(personUrl.LongUrl);

                return Json(url.ToString());
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}