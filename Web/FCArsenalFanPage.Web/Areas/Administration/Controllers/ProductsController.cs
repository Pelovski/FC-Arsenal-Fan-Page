namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class ProductsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IProductService productService;

        public ProductsController(
            IDeletableEntityRepository<Product> productRepository,
            IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }

        // GET: Administration/Products
        public IActionResult Index()
        {
            var viewModel = this.productService.GetProductsForDashboard();

            return this.View(viewModel);
        }

        // GET: Administration/Products/Details/5
        public IActionResult Details(string id)
        {
            if (id == null || this.productRepository.All() == null)
            {
                return this.NotFound();
            }

            var viewModel = this.productService
                .GetProductsForDashboard()
                .Where(x => x.ProductId == id)
                .FirstOrDefault();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        // GET: Administration/Products/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null || this.productRepository.All() == null)
            {
                return this.NotFound();
            }

            var viewModel = this.productService
                .GetProductsForDashboard()
                .Where(x => x.ProductId == id)
                .FirstOrDefault();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (this.productRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }

            await this.productService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProductExists(string id)
        {
          return this.productRepository.All().Any(e => e.Id == id);
        }
    }
}
