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
    public class AdvertisementsController : Controller
    {
        private readonly AppDbContext _context;

        public AdvertisementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Advertisements
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Advertisements.Include(a => a.AppUser).Include(a => a.Location).Include(a => a.Price).Include(a => a.Service).Include(a => a.Status);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.AppUser)
                .Include(a => a.Location)
                .Include(a => a.Price)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "City");
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AppUserId,PriceId,LocationId,ServiceId,StatusId,Id")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.Id = Guid.NewGuid();
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", advertisement.AppUserId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "City", advertisement.LocationId);
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", advertisement.PriceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", advertisement.ServiceId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", advertisement.StatusId);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", advertisement.AppUserId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "City", advertisement.LocationId);
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", advertisement.PriceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", advertisement.ServiceId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", advertisement.StatusId);
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,AppUserId,PriceId,LocationId,ServiceId,StatusId,Id")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", advertisement.AppUserId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "City", advertisement.LocationId);
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", advertisement.PriceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description", advertisement.ServiceId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", advertisement.StatusId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.AppUser)
                .Include(a => a.Location)
                .Include(a => a.Price)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement != null)
            {
                _context.Advertisements.Remove(advertisement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(Guid id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
