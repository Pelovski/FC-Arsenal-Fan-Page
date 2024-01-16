namespace FCArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            return this.RedirectToAction("All");
        }
    }
}
