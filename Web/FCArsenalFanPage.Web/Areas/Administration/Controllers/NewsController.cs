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
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/News
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.News.Include(n => n.Category).Include(n => n.Image).Include(n => n.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .Include(n => n.Image)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Administration/News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,UserId,CategoryId,ImageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", news.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", news.ImageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", news.UserId);
            return View(news);
        }

        // GET: Administration/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", news.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", news.ImageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", news.UserId);
            return View(news);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", news.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", news.ImageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", news.UserId);
            return View(news);
        }

        // GET: Administration/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .Include(n => n.Image)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Administration/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'ApplicationDbContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return _context.News.Any(e => e.Id == id);
        }
    }
}
