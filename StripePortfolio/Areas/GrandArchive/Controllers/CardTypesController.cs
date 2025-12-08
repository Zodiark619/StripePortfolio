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
    public class CardTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrandArchive/CardTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardType.ToListAsync());
        }

        // GET: GrandArchive/CardTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardType = await _context.CardType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardType == null)
            {
                return NotFound();
            }

            return View(cardType);
        }

        // GET: GrandArchive/CardTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrandArchive/CardTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CardType cardType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardType);
        }

        // GET: GrandArchive/CardTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardType = await _context.CardType.FindAsync(id);
            if (cardType == null)
            {
                return NotFound();
            }
            return View(cardType);
        }

        // POST: GrandArchive/CardTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CardType cardType)
        {
            if (id != cardType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardTypeExists(cardType.Id))
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
            return View(cardType);
        }

        // GET: GrandArchive/CardTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardType = await _context.CardType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardType == null)
            {
                return NotFound();
            }

            return View(cardType);
        }

        // POST: GrandArchive/CardTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardType = await _context.CardType.FindAsync(id);
            if (cardType != null)
            {
                _context.CardType.Remove(cardType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardTypeExists(int id)
        {
            return _context.CardType.Any(e => e.Id == id);
        }
    }
}
