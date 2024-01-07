namespace FCArsenalFanPage.Web.Controllers
{
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public NewsController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var viewModel = new CreateNewsInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateNewsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAll();

                return this.View(input);
            }

            return this.RedirectToAction("All");
        }

        public IActionResult SingleNews()
        {
            return this.View();
        }
    }
}
