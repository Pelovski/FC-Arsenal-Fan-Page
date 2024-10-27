namespace FCArsenalFanPage.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Common;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly INewsService newsService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public NewsController(
            ICategoriesService categoriesService,
            INewsService newsService,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.newsService = newsService;
            this.userRepository = userRepository;
            this.environment = environment;
            this.userManager = userManager;
        }

        public IActionResult All(int id = 1)
        {
            int firstItemsPerPage = 7;
            int otherPagesItems = 9;


            var viewModel = new NewsListViewModel
            {
                PageNumber = id,
                News = this.newsService.GetAllWithDynamicPaging(id, firstItemsPerPage, otherPagesItems),
                NewsCount = this.newsService.GetCount(),
                ItemsPerPage = firstItemsPerPage,
            };

            return this.View(viewModel);
        }

        // Create Get
        [Authorize(Roles = GlobalConstants.NewsAdministration)]
        public IActionResult Create()
        {
            var viewModel = new CreateNewsInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAll();

            return this.View(viewModel);
        }

        // Create Post
        [HttpPost]
        [Authorize(Roles = GlobalConstants.NewsAdministration)]
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

        [Authorize(Roles = GlobalConstants.NewsAdministration)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.newsService.GetById<EditNewsInputViewModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAll();

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.NewsAdministration)]
        public async Task<IActionResult> Edit(int id, EditNewsInputViewModel input)
        {

            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAll();
                return this.View(input);
            }

            await this.newsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.SingleNews), new { id });
        }

        public IActionResult SingleNews(int id)
        {
            var news = this.newsService.GetById<SingleNewsViewModel>(id);
            news.RecentPosts = this.newsService.RecentPosts(id);

            return this.View(news);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.NewsAdministration)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
