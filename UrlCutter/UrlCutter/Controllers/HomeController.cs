using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UrlCutter.Managers;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly UrlManager _url;

        public HomeController()
        {
            _url = new UrlManager();
        }

        [HttpGet]
        public  IActionResult UrlCutter(string link)
        {
            if (!string.IsNullOrEmpty(link))
            {
                if (!_url.IsUrl(link))
                {
                    return BadRequest("Ссылка не является url");
                }

                return Ok(_url.MakeUrl(link).ToString());
            }
            return Ok("Введите ссылку");
        }
    }
}