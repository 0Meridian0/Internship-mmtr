using Microsoft.AspNetCore.Mvc;
using UrlCutter.Managers;


namespace UrlCutter.Controllers
{
    [Route("/{token}")]
    public class ShortController : Controller
    {
        private readonly DbRepositoryManager _dbManager;
        public ShortController(DbRepositoryManager dbManager)
        {
            _dbManager = dbManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var resp = await _dbManager.GetDataFromDbAsync(token);
                if (resp != null && !string.IsNullOrEmpty(resp.LongUrl))
                    return Redirect(resp.LongUrl);
            }
            return Redirect("~/");
        }
    }
}
