using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    public class HomeController : Controller
    {
        DbUrl db = new DbUrl();

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
                Console.WriteLine(url.ToString());
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

                CheckDataInDb(url);
                ShowDbContent();

                return Json(db.Urls.ToList());
            }
            return View();
        }

        private void CheckDataInDb(URL url)
        {
            //Пользователь запросил урл
            //Система его считала -> проверка на http запрос -> 
            // - ->Извините, пока
            // + -> проверка на наличие хэш в бд
            
            // - -> создаем новую короткую ссылку -> добавляем запись в бд
            // + -> получаем с бд длинный и короткий url

            // UI -> перенаправили пользователю короткий url
            // Bash -> возвращаем json со всеми данными по указанной ссылке



            try
            {
                var match = db.Urls.Single(s => s.Token == url.Token);
            }
            catch
            {
                SaveToDb(url);
            }
        }
        
        private void SaveToDb(URL url)
        {
            db.Add(url);
            db.SaveChanges();
        }

        private void ShowDbContent()
        {
            db.Urls.ToList();

            foreach (var i in db.Urls.ToList())
            {
                Console.WriteLine($"ID: {i.id} \nToken: {i.Token}\nShortURL: {i.ShortUrl} \nLongUrl: {i.LongUrl} \n\n");
            }
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