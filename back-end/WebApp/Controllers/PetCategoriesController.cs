using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class PetCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public PetCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PetCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetCategories.ToListAsync());
        }

        // GET: PetCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petCategory = await _context.PetCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petCategory == null)
            {
                return NotFound();
            }

            return View(petCategory);
        }

        // GET: PetCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetCategoryName,Id")] PetCategory petCategory)
        {
            if (ModelState.IsValid)
            {
                petCategory.Id = Guid.NewGuid();
                _context.Add(petCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petCategory);
        }

        // GET: PetCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petCategory = await _context.PetCategories.FindAsync(id);
            if (petCategory == null)
            {
                return NotFound();
            }
            return View(petCategory);
        }

        // POST: PetCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PetCategoryName,Id")] PetCategory petCategory)
        {
            if (id != petCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetCategoryExists(petCategory.Id))
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
            return View(petCategory);
        }

        // GET: PetCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petCategory = await _context.PetCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petCategory == null)
            {
                return NotFound();
            }

            return View(petCategory);
        }

        // POST: PetCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var petCategory = await _context.PetCategories.FindAsync(id);
            if (petCategory != null)
            {
                _context.PetCategories.Remove(petCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetCategoryExists(Guid id)
        {
            return _context.PetCategories.Any(e => e.Id == id);
        }
    }
}
