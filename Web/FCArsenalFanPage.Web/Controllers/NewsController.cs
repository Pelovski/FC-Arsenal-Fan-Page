namespace FCArsenalFanPage.Web.Controllers
{
    using FCArsenalFanPage.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateNewsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("All");
        }
    }
}
