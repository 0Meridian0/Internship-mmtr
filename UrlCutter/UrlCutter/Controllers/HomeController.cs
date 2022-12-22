using Microsoft.AspNetCore.Mvc;
using UrlCutter.Managers;

namespace UrlCutter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly UrlManager _urlManager;

        public HomeController(UrlManager urlManager)
        {
            _urlManager = urlManager;
        }

        [HttpGet]
        public async Task<IActionResult> UrlCutter(string link)
        {
            if (!string.IsNullOrEmpty(link))
            {
                if (!_urlManager.IsUrl(link))
                {
                    return BadRequest("Ссылка не является url");
                }
                return Ok((await _urlManager.MakeUrl(link)).ToString());
            }
            return Ok("Введите ссылку");
        }
    }
}