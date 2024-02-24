namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
	using FCArsenalFanPage.Web.Controllers;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ProductsController : BaseController
	{
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly ApplicationDbContext context;

        public ProductsController(
            IDeletableEntityRepository<Product> productRepository,
            ApplicationDbContext context)
        {
            this.productRepository = productRepository;
            this.context = context;
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

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id");
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id");
            this.ViewData["ProductCategoryId"] = new SelectList(this.context.ProductCategories, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,ImageId,Quantity,Description,ProductCategoryId,CreatedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (this.ModelState.IsValid)
            {
                await this.productRepository.AddAsync(product);
                await this.productRepository.SaveChangesAsync();

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id", product.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", product.ImageId);
            this.ViewData["ProductCategoryId"] = new SelectList(this.context.ProductCategories, "Id", "Id", product.ProductCategoryId);

            return this.View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || this.productRepository.All() == null)
            {
                return this.NotFound();
            }

            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return this.NotFound();
            }
            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id", product.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", product.ImageId);
            this.ViewData["ProductCategoryId"] = new SelectList(this.context.ProductCategories, "Id", "Id", product.ProductCategoryId);

            return this.View(product);
        }

        // POST: Administration/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Price,ImageId,Quantity,Description,ProductCategoryId,CreatedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (id != product.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.productRepository.Update(product);
                    await this.productRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ProductExists(product.Id))
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
            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id", product.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", product.ImageId);
            this.ViewData["ProductCategoryId"] = new SelectList(this.context.ProductCategories, "Id", "Id", product.ProductCategoryId);

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
