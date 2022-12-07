using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UrlCutter.Models;


namespace UrlCutter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> UrlCutter(string link, DbUrl db)
        {
            if (!string.IsNullOrEmpty(link))
            {
                if (!IsUrl(link))
                {
                    return BadRequest("Ссылка не является url");
                }

                string token = HashManager.CreateToken(link);
                while (DbManager.CheckDataInDb(token, link, db).Result)
                {
                    token = HashManager.CreateToken(token);
                }

                URL url = new(link, token);
                await DbManager.SaveToDbAsync(url, db);

                return Ok(url.ToString());
            }
            return Ok("Введите ссылку");
        }

        public bool IsUrl(string url)
        {
            Regex regex = new("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9А-Яа-я@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9А-Яа-я()]{1,6}\\b(?:[-a-zA-Z0-9А-Яа-я()@:%_\\+.~#?&\\/=]*)$");

            MatchCollection matches = regex.Matches(url);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}