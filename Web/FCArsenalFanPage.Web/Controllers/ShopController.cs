namespace FCArsenalFanPage.Web.Controllers
{
    using System.Security.Claims;
	using System.Threading.Tasks;
	using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;

    public class ShopController : Controller
    {
        private readonly IProductCategoriesService productCategoriesService;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment environment;

        public ShopController(
            IProductCategoriesService productCategoriesService,
            IProductService productService,
            IWebHostEnvironment environment)
        {
            this.productCategoriesService = productCategoriesService;
            this.productService = productService;
            this.environment = environment;
        }

        public IActionResult All()
        {
            var viewModel = new ProductListViewModel
            {
                Products = this.productService.GetAll(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateProductInputModel();

            viewModel.ProductsItems = this.productCategoriesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.ProductsItems = this.productCategoriesService.GetAll();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var imagePath = $"{this.environment.WebRootPath}/Images";

            await this.productService.CreateAsync(input, userId, imagePath);

            return this.RedirectToAction("All");
        }

        public IActionResult SingleProduct(string id)
        {
            var product = this.productService.GetById<SingleProductViewModel>(id);

            return this.View(product);
        }
    }
}
