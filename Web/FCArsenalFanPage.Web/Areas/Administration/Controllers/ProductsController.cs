namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ProductsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsController(
            IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Administration/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.productRepository.AllAsNoTracking().Include(p => p.CreatedByUser).Include(p => p.Image).Include(p => p.ProductCategory);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || this.productRepository.All() == null)
            {
                return this.NotFound();
            }

            var product = await this.productRepository.All()
                .Include(p => p.CreatedByUser)
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || this.productRepository.All() == null)
            {
                return this.NotFound();
            }

            var product = await this.productRepository.All()
                .Include(p => p.CreatedByUser)
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (this.productRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }

            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                this.productRepository.Delete(product);
            }

            await this.productRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProductExists(string id)
        {
          return this.productRepository.All().Any(e => e.Id == id);
        }
    }
}
