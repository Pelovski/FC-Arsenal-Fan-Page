namespace FCArsenalFanPage.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly INewsService newsService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IHostingEnvironment environment;

        public NewsController(
            ICategoriesService categoriesService,
            INewsService newsService,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IHostingEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.newsService = newsService;
            this.userRepository = userRepository;
            this.environment = environment;
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
        [Authorize]
        public async Task<IActionResult> Create(CreateNewsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAll();

                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var imagePath = $"{this.environment.WebRootPath}/Images";

            await this.newsService.CreateAsync(input, userId, imagePath);

            return this.RedirectToAction("All");
        }

        public IActionResult SingleNews()
        {
            return this.View();
        }
    }
}
