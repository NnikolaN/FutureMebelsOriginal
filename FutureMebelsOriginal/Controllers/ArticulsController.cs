using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutureMebelsOriginal.Data;
using Microsoft.AspNetCore.Authorization;

namespace FutureMebelsOriginal.Controllers
{
    
    public class ArticulsController : Controller
    {
        private readonly MebelsDbContext _context;

        public ArticulsController(MebelsDbContext context)
        {
            _context = context;
        }

        // GET: Articuls
        public async Task<IActionResult> Index()
        {
            var mebelsDbContext = _context.Articuls.Include(a => a.Categories).Include(a => a.Types);
            return View(await mebelsDbContext.ToListAsync());
        }

        // GET: Articuls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articuls == null)
            {
                return NotFound();
            }

            var articul = await _context.Articuls
                .Include(a => a.Categories)
                .Include(a => a.Types)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articul == null)
            {
                return NotFound();
            }

            return View(articul);
        }

        // GET: Articuls/Create
        //[Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            return View();
        }

        // POST: Articuls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,CategoryId,NameModel,Size,Applied,ImgUrl,Price,Description")] Articul articul)
        {
            if (ModelState.IsValid)
            {
                articul.RegisterOn=DateTime.Now;
                _context.Articuls.Add(articul);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", articul.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", articul.TypeId);
            return View(articul);
        }

        // GET: Articuls/Edit/5
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articuls == null)
            {
                return NotFound();
            }

            var articul = await _context.Articuls.FindAsync(id);
            if (articul == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", articul.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", articul.TypeId);
            return View(articul);
        }

        // POST: Articuls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeId,CategoryId,NameModel,Size,Applied,ImgUrl,Price,Description")] Articul articul)
        {
            if (id != articul.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    articul.RegisterOn = DateTime.Now;
                    _context.Articuls.Update(articul);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulExists(articul.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", articul.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", articul.TypeId);
            return View(articul);
        }

        // GET: Articuls/Delete/5
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articuls == null)
            {
                return NotFound();
            }

            var articul = await _context.Articuls
                .Include(a => a.Categories)
                .Include(a => a.Types)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articul == null)
            {
                return NotFound();
            }

            return View(articul);
        }

        // POST: Articuls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articuls == null)
            {
                return Problem("Entity set 'MebelsDbContext.Articuls'  is null.");
            }
            var articul = await _context.Articuls.FindAsync(id);
            if (articul != null)
            {
                _context.Articuls.Remove(articul);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticulExists(int id)
        {
          return (_context.Articuls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
