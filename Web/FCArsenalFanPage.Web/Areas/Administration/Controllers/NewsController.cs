namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class NewsController : Controller
    {
        private readonly IDeletableEntityRepository<News> dataRepository;
        private readonly ApplicationDbContext context;

        public NewsController(IDeletableEntityRepository<News> dataRepository, ApplicationDbContext context)
        {
            this.dataRepository = dataRepository;
            this.context = context;
        }

        // GET: Administration/News
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.dataRepository.AllAsNoTracking().Include(n => n.Category).Include(n => n.Image).Include(n => n.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return this.NotFound();
            }

            var news = await this.dataRepository
                .All()
                .Include(n => n.Category)
                .Include(n => n.Image)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // GET: Administration/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return this.NotFound();
            }

            var news = this.dataRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Id", news.CategoryId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", news.ImageId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", news.UserId);
            return this.View(news);
        }

        // POST: Administration/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,UserId,CategoryId,ImageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (id != news.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(news);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.NewsExists(news.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Id", news.CategoryId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", news.ImageId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", news.UserId);

            return this.View(news);
        }

        // GET: Administration/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return this.NotFound();
            }

            var news = await this.dataRepository.All()
                .Include(n => n.Category)
                .Include(n => n.Image)
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
            if (this.dataRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.News'  is null.");
            }
            var news = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            if (news != null)
            {
                this.dataRepository.Delete(news);
            }

            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool NewsExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
