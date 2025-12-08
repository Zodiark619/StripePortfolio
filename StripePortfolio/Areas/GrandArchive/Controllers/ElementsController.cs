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
    public class ElementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrandArchive/Elements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Element.ToListAsync());
        }

        // GET: GrandArchive/Elements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Element
                .FirstOrDefaultAsync(m => m.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // GET: GrandArchive/Elements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrandArchive/Elements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Element element)
        {
            if (ModelState.IsValid)
            {
                _context.Add(element);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(element);
        }

        // GET: GrandArchive/Elements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Element.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }
            return View(element);
        }

        // POST: GrandArchive/Elements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Element element)
        {
            if (id != element.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(element);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementExists(element.Id))
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
            return View(element);
        }

        // GET: GrandArchive/Elements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Element
                .FirstOrDefaultAsync(m => m.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // POST: GrandArchive/Elements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var element = await _context.Element.FindAsync(id);
            if (element != null)
            {
                _context.Element.Remove(element);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElementExists(int id)
        {
            return _context.Element.Any(e => e.Id == id);
        }
    }
}
