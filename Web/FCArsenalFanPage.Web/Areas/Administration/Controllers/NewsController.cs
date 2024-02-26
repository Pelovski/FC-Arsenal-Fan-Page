namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
	using FCArsenalFanPage.Web.Controllers;
	using Microsoft.AspNetCore.Mvc;
 
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class NewsController : BaseController
    {
        private readonly IDeletableEntityRepository<News> newsRepository;
        private readonly ApplicationDbContext context;

        public NewsController(
            IDeletableEntityRepository<News> newsRepository,
            ApplicationDbContext context)
        {
            this.newsRepository = newsRepository;
            this.context = context;
        }

        // GET: Administration/News
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.newsRepository.AllAsNoTracking().Include(n => n.Category).Include(n => n.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.newsRepository.All() == null)
            {
                return this.NotFound();
            }

            var news = await this.newsRepository
                .All()
                .Include(n => n.Category)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // GET: Administration/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.newsRepository.All() == null)
            {
                return this.NotFound();
            }

            var news = await this.newsRepository.All()
                .Include(n => n.Category)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
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
