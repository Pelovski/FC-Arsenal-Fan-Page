namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class NewsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<News> newsRepository;
        private readonly INewsService newsService;

        public NewsController(
            IDeletableEntityRepository<News> newsRepository,
            INewsService newsService)
        {
            this.newsRepository = newsRepository;
            this.newsService = newsService;
        }

        // GET: Administration/News
        public IActionResult Index()
        {
            var viewModel = this.newsService.GetNewsForDashboard();

            return this.View(viewModel);
        }

        // GET: Administration/News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || this.newsRepository.All() == null)
            {
                return this.NotFound();
            }

            var viewModel = this.newsService
                .GetNewsForDashboard()
                .Where(x => x.NewsId == id)
                .FirstOrDefault();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        // GET: Administration/News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || this.newsRepository.All() == null)
            {
                return this.NotFound();
            }

            var viewModel = this.newsService
               .GetNewsForDashboard()
               .Where(x => x.NewsId == id)
               .FirstOrDefault();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        // POST: Administration/News/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.newsRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.News'  is null.");
            }

            var news = this.newsRepository.All().FirstOrDefault(x => x.Id == id);

            if (news != null)
            {
                this.newsRepository.Delete(news);
            }

            await this.newsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool NewsExists(int id)
        {
            return this.newsRepository.All().Any(e => e.Id == id);
        }
    }
}
