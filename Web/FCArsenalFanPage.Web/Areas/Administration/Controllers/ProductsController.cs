using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCArsenalFanPage.Data;
using FCArsenalFanPage.Data.Models;

namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.CreatedByUser).Include(p => p.Image).Include(p => p.ProductCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CreatedByUser)
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id");
            return View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,ImageId,Quantity,Description,ProductCategoryId,CreatedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", product.CreatedByUserId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", product.CreatedByUserId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Id", product.CreatedByUserId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", product.ImageId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CreatedByUser)
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
