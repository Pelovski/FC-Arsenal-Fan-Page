using Microsoft.AspNetCore.Mvc;

namespace FCArsenalFanPage.Web.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
