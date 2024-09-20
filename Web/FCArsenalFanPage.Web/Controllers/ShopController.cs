namespace FCArsenalFanPage.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Common;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
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

        public IActionResult All(int id = 1)
        {
            var viewModel = new ProductListViewModel
            {
                PageNumber = id,
                Products = this.productService.GetAll(),
                Categories = this.productCategoriesService.GetAll(),
            };

            if (this.Request.Cookies.ContainsKey("SuccessMessage"))
            {
                this.ViewBag.SuccessMessage = this.Request.Cookies["SuccessMessage"];

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Secure = true,       // SameSite=None requires Secure flag
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                };

                this.Response.Cookies.Delete("SuccessMessage", cookieOptions);
            }

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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

        // Edit Get
        [HttpGet]
        [Authorize(Roles = GlobalConstants.MerchandisAdministration)]
        public IActionResult Edit(string id)
        {
            var inputModel = this.productService.GetById<EditProductInputViewModel>(id);
            inputModel.ProductsItems = this.productCategoriesService.GetAll();

            return this.View(inputModel);
        }

        // Edit Post
        [HttpPost]
        [Authorize(Roles = GlobalConstants.MerchandisAdministration)]
        public async Task<IActionResult> Edit(string id, EditProductInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.ProductsItems = this.productCategoriesService.GetAll();
                return this.View(input);
            }

            await this.productService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.SingleProduct), new { id });
        }

        public IActionResult SingleProduct(string id)
        {
            var product = this.productService.GetById<SingleProductViewModel>(id);

            return this.View(product);
        }
    }
}
