namespace FCArsenalFanPage.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
    using FCArsenalFanPage.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly INewsService newsService;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService userService;

        public HomeController(
            INewsService newsService,
            IProductService productService,
            UserManager<ApplicationUser> userManager,
            IApplicationUserService userService)
        {
            this.newsService = newsService;
            this.productService = productService;
            this.userManager = userManager;
            this.userService = userService;
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

            // Take the ImageUrl form the user to give it to the _LoginPartial
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = this.userManager.FindByIdAsync(userId).Result;

                if (user.ProfilePictureId != null)
                {
                    this.ViewData["ImageUrl"] = this.userService.GetProfilePictureUrl(user);
                }
            }

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
