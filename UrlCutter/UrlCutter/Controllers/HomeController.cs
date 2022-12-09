using Microsoft.AspNetCore.Mvc;
using UrlCutter.Managers;


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
        public async Task<IActionResult> UrlCutter(string link)
        {
            if (!string.IsNullOrEmpty(link))
            {
                if (!_url.IsUrl(link))
                {
                    return BadRequest("Ссылка не является url");
                }
                var url = await _url.MakeUrl(link);
                return Ok(url.ToString());
            }
            return Ok("Введите ссылку");
        }
    }
}