namespace FCArsenalFanPage.Web.Controllers
{
    using System.Diagnostics;

    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
    using FCArsenalFanPage.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly INewsService newsService;
        private readonly IProductService productService;

        public HomeController(
            INewsService newsService,
            IProductService productService)
        {
            this.newsService = newsService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var news = this.newsService.GetAll();
            var products = this.productService.GetAll();

            var viewModel = new IndexListViewModel
            {
                News = news,
                Products = products,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
