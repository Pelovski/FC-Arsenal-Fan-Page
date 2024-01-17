namespace FCArsenalFanPage.Web.Controllers
{
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class ShopController : Controller
    {
        private readonly IProductCategoriesService productCategoriesService;

        public ShopController(IProductCategoriesService productCategoriesService)
        {
            this.productCategoriesService = productCategoriesService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateProductInputModel();

            viewModel.ProductsItems = this.productCategoriesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.ProductsItems = this.productCategoriesService.GetAll();
                return this.View(input);
            }

            return this.RedirectToAction("All");
        }
    }
}
