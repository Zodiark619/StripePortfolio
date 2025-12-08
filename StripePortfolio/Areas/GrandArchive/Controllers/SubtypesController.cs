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
    public class SubtypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubtypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrandArchive/Subtypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subtype.ToListAsync());
        }

        // GET: GrandArchive/Subtypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = await _context.Subtype
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subtype == null)
            {
                return NotFound();
            }

            return View(subtype);
        }

        // GET: GrandArchive/Subtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrandArchive/Subtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subtype subtype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subtype);
        }

        // GET: GrandArchive/Subtypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = await _context.Subtype.FindAsync(id);
            if (subtype == null)
            {
                return NotFound();
            }
            return View(subtype);
        }

        // POST: GrandArchive/Subtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subtype subtype)
        {
            if (id != subtype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubtypeExists(subtype.Id))
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
            return View(subtype);
        }

        // GET: GrandArchive/Subtypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = await _context.Subtype
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subtype == null)
            {
                return NotFound();
            }

            return View(subtype);
        }

        // POST: GrandArchive/Subtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subtype = await _context.Subtype.FindAsync(id);
            if (subtype != null)
            {
                _context.Subtype.Remove(subtype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubtypeExists(int id)
        {
            return _context.Subtype.Any(e => e.Id == id);
        }
    }
}
