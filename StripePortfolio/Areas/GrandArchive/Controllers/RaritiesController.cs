using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Areas.GrandArchive.Models;
using StripePortfolio.Data;

namespace StripePortfolio.Areas.GrandArchive.Controllers
{
    [Area("GrandArchive")]
    public class RaritiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrandArchive/Rarities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rarity.ToListAsync());
        }

        // GET: GrandArchive/Rarities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rarity = await _context.Rarity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rarity == null)
            {
                return NotFound();
            }

            return View(rarity);
        }

        // GET: GrandArchive/Rarities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrandArchive/Rarities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Rarity rarity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rarity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rarity);
        }

        // GET: GrandArchive/Rarities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rarity = await _context.Rarity.FindAsync(id);
            if (rarity == null)
            {
                return NotFound();
            }
            return View(rarity);
        }

        // POST: GrandArchive/Rarities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Rarity rarity)
        {
            if (id != rarity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rarity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RarityExists(rarity.Id))
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
            return View(rarity);
        }

        // GET: GrandArchive/Rarities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rarity = await _context.Rarity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rarity == null)
            {
                return NotFound();
            }

            return View(rarity);
        }

        // POST: GrandArchive/Rarities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rarity = await _context.Rarity.FindAsync(id);
            if (rarity != null)
            {
                _context.Rarity.Remove(rarity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RarityExists(int id)
        {
            return _context.Rarity.Any(e => e.Id == id);
        }
    }
}
