using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Areas.GrandArchive.Models;
using StripePortfolio.Areas.GrandArchive.Models.ViewModels;
using StripePortfolio.Data;

namespace StripePortfolio.Areas.GrandArchive.Controllers
{
    [Area("GrandArchive")]
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private Card CardViewModelToCard(CardViewModel vm)
        {
            var elements = _context.Element
    .Where(e => vm.SelectedElementIds.Contains(e.Id))
    .ToList();
            var subtypes = _context.Subtype
    .Where(e => vm.SelectedSubtypeIds.Contains(e.Id))
    .ToList();
            var cardtypes = _context.CardType
    .Where(e => vm.SelectedCardTypeIds.Contains(e.Id))
    .ToList();
            var sets = _context.Set
    .Where(e => vm.SelectedSetIds.Contains(e.Id))
    .ToList();
            var rarity= _context.Rarity.Where(x=>vm.RarityId==x.Id).FirstOrDefault();
            var card = new Card
            {
                Id=vm.Id??0,
                Name=vm.Name,
                Uuid=vm.Uuid,
               //imageurl private set automaticall set from uuid ImageUrl=vm.ImageUrl,
               Elements= elements,
               Subtypes= subtypes,
               CardTypes= cardtypes,
               Sets= sets,
               Rarity= rarity,

            };
            return card;
        }
        private CardViewModel CardToCardViewModel(Card card)
        {
            var allElements = _context.Element.ToList();
            var allSubtypes = _context.Subtype.ToList();
            var allCardTypes = _context.CardType.ToList();
            var allSets = _context.Set.ToList();
            var allRarities = _context.Rarity.ToList();

            var vm = new CardViewModel
            {
                Id = card.Id,
                Name = card.Name,
                Uuid = card.Uuid,
                ImageUrl = card.ImageUrl,

                // selected IDs
                SelectedElementIds = card.Elements.Select(e => e.Id).ToList(),
                SelectedSubtypeIds = card.Subtypes.Select(e => e.Id).ToList(),
                SelectedCardTypeIds = card.CardTypes.Select(e => e.Id).ToList(),
                SelectedSetIds = card.Sets.Select(e => e.Id).ToList(),
                RarityId = card.Rarity?.Id ?? 0, // handle null

                // all options (no Selected manually)
                ElementOptions = allElements.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),
                SubtypeOptions = allSubtypes.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),
                CardTypeOptions = allCardTypes.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),
                SetOptions = allSets.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),
                RarityOptions = allRarities.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList()
            };

            return vm;
        }

        //private CardViewModel  CardToCardViewModel(Card card)
        //{
        //    var allElements = _context.Element.ToList();
        //    var allSubtypes = _context.Subtype.ToList();
        //    var allCardTypes = _context.CardType.ToList();
        //    var allSets = _context.Set.ToList();
        //    var allRarities = _context.Rarity.ToList();
        //    var selectedElementId = card.Elements.Select(x => x.Id).ToList();
        //    var selectedSubtypeId = card.Subtypes.Select(x => x.Id).ToList();
        //    var selectedCardtypeId = card.CardTypes.Select(x => x.Id).ToList();
        //    var selectedSetId = card.Sets.Select(x => x.Id).ToList();
        //    var selectedRarityId = card.Rarity.Id;

        //    var vm=new CardViewModel
        //    {
        //        SelectedElementIds = selectedElementId,
        //        ElementOptions = allElements
        //   .Select(e => new SelectListItem { Value = e.Id.ToString(), 
        //       Text = e.Name ,
        //     //  Selected= selectedElementId.Contains( e.Id) ,
        //   })
        //   .ToList(),
        //        SelectedSubtypeIds = selectedSubtypeId,
        //        SubtypeOptions = allSubtypes
        //   .Select(e => new SelectListItem { Value = e.Id.ToString(), 
        //       Text = e.Name ,
        //    //   Selected= selectedSubtypeId.Contains( e.Id) ,
        //   })
        //   .ToList(),
        //        SelectedCardTypeIds = selectedCardtypeId,
        //        CardTypeOptions = allCardTypes
        //   .Select(e => new SelectListItem { Value = e.Id.ToString(), 
        //       Text = e.Name ,
        //    //   Selected= selectedCardtypeId.Contains( e.Id) ,
        //   })
        //   .ToList(),
        //        SelectedSetIds = selectedSetId,
        //        SetOptions = allSets
        //   .Select(e => new SelectListItem { Value = e.Id.ToString(), 
        //       Text = e.Name ,
        //    //   Selected= selectedSetId.Contains( e.Id) ,
        //   })
        //   .ToList(),
        //        RarityId = selectedRarityId,
        //        RarityOptions = allRarities
        //   .Select(e => new SelectListItem { Value = e.Id.ToString(), 
        //       Text = e.Name ,
        //     //  Selected= e.Id==selectedRarityId,
        //   })
        //   .ToList(),
        //        Uuid=card.Uuid,
        //        ImageUrl=card.ImageUrl,
        //        Id =card.Id,
        //        Name=card.Name,

        //    };
        //    return vm;
        //}
        private CardViewModel NewCardViewModelGenerate(CardViewModel vm)
        {

            vm.ElementOptions = _context.Element
           .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
           .ToList();

             vm.CardTypeOptions = _context.CardType
            .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
            .ToList();

              vm.SubtypeOptions = _context.Subtype
            .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            .ToList();

              vm.SetOptions = _context.Set
            .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            .ToList();

              vm.RarityOptions = _context.Rarity
            .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
            .ToList();
            return vm;
        }
        // GET: GrandArchive/Cards
        public async Task<IActionResult> Index()
        {
              var card = await _context.Card
        .Include(c => c.Elements)
        .Include(c => c.CardTypes)
        .Include(c => c.Subtypes)
        .Include(c => c.Sets) // if any
        .Include(x=>x.Rarity)
        .ToListAsync();
            return View(card);
        }

        // GET: GrandArchive/Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: GrandArchive/Cards/Create
        public IActionResult Create()
        {
            var vm = NewCardViewModelGenerate(new CardViewModel());

            return View(vm);
        }

        // POST: GrandArchive/Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(  CardViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var card = new Card
                {
                    Name = vm.Name,
                    Uuid = vm.Uuid,
                    Elements = _context.Element.Where(e => vm.SelectedElementIds.Contains(e.Id)).ToList(),
                    CardTypes = _context.CardType.Where(t => vm.SelectedCardTypeIds.Contains(t.Id)).ToList(),
                    Subtypes = _context.Subtype.Where(s => vm.SelectedSubtypeIds.Contains(s.Id)).ToList(),
                    Sets = _context.Set.Where(s => vm.SelectedSetIds.Contains(s.Id)).ToList(),
                    Rarity = _context.Rarity.First(r => r.Id == vm.RarityId),
                };
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var vmFail=NewCardViewModelGenerate(vm);
                return View(vmFail);
            } 
        }

        // GET: GrandArchive/Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var card = await _context.Card
                .Include(c => c.Elements)
                .Include(c => c.CardTypes)
                .Include(c => c.Subtypes)
                .Include(c => c.Sets)
                .Include(c => c.Rarity)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null) return NotFound();

            var vm = new CardViewModel
            {
                Id = card.Id,
                Name = card.Name,
                Uuid = card.Uuid,
                ImageUrl = card.ImageUrl,

                SelectedElementIds = card.Elements.Select(x => x.Id ).ToList(),
                SelectedCardTypeIds = card.CardTypes.Select(x => x.Id).ToList(),
                SelectedSubtypeIds = card.Subtypes.Select(x => x.Id).ToList(),
                SelectedSetIds = card.Sets.Select(x => x.Id).ToList(),
                RarityId = card.Rarity?.Id ?? 0,

                ElementOptions = (await _context.Element.ToListAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name, Selected = card.Elements.Any(x => x.Id == e.Id) })
                    .ToList (),
                CardTypeOptions = (await _context.CardType.ToListAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name,Selected = card.CardTypes.Any(x => x.Id == e.Id) })
                    .ToList (),
                SubtypeOptions = (await _context.Subtype.ToListAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name, Selected = card.Subtypes.Any(x => x.Id == e.Id) })
                    .ToList (),
                SetOptions = (await _context.Set.ToListAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name, Selected = card.Sets.Any(x => x.Id == e.Id) })
                    .ToList (),
                RarityOptions = (await _context.Rarity.ToListAsync())
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name, Selected = card.Rarity != null && r.Id == card.Rarity.Id })
                    .ToList ()
            };

            return View(vm);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var card = await _context.Card
        //        .Include(c => c.Elements)
        //.Include(c => c.CardTypes)
        //.Include(c => c.Subtypes)
        //.Include(c => c.Sets) // if any
        //.Include(x => x.Rarity)
        //        .FirstOrDefaultAsync(x=>x.Id==id);
        //    if (card == null)
        //    {
        //        return NotFound();
        //    }
        //    var vm= CardToCardViewModel(card);
        //    return View(vm);
        //}

        // POST: GrandArchive/Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(int id,   CardViewModel vm)
        //        {
        //var card=CardViewModelToCard(vm);
        //            if (id != card.Id)
        //            {
        //                return NotFound();
        //            }
        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(card);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!CardExists(card.Id))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }

        //            return View(vm);
        //        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CardViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var card = await _context.Card
                .Include(c => c.Elements)
                .Include(c => c.CardTypes)
                .Include(c => c.Subtypes)
                .Include(c => c.Sets)
                .Include(c => c.Rarity)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
                return NotFound();

            // Update scalar properties
            card.Name = vm.Name;
            card.Uuid = vm.Uuid;

            // Update navigation collections
            card.Elements = await _context.Element.Where(e => vm.SelectedElementIds.Contains(e.Id)).ToListAsync();
            card.CardTypes = await _context.CardType.Where(e => vm.SelectedCardTypeIds.Contains(e.Id)).ToListAsync();
            card.Subtypes = await _context.Subtype.Where(e => vm.SelectedSubtypeIds.Contains(e.Id)).ToListAsync();
            card.Sets = await _context.Set.Where(e => vm.SelectedSetIds.Contains(e.Id)).ToListAsync();
            card.Rarity = await _context.Rarity.FirstOrDefaultAsync(r => r.Id == vm.RarityId)
                          ?? throw new Exception("Rarity not found");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GrandArchive/Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: GrandArchive/Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var card = await _context.Card.FindAsync(id);
            var card = await _context.Card
        .Include(c => c.Elements)
        .Include(c => c.CardTypes)
        .Include(c => c.Subtypes)
        .Include(c => c.Sets) // if any
        .Include(x=>x.Rarity)
        .FirstOrDefaultAsync(c => c.Id == id);
            if (card != null)
            {
                card.Elements.Clear();
                card.CardTypes.Clear();
                card.Subtypes.Clear();
                card.Sets.Clear();
                _context.Card.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
