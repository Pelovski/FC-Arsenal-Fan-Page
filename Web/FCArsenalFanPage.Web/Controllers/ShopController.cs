using Microsoft.AspNetCore.Mvc;

namespace FCArsenalFanPage.Web.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
