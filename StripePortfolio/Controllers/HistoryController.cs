using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Data;
using System.Security.Claims;

namespace StripePortfolio.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HistoryController(ApplicationDbContext applicationDb, UserManager<IdentityUser> userManager)
        {
            _db = applicationDb;
            _userManager = userManager;

        }
        [Authorize] 
        public IActionResult Index(string filter )
        {
            var userId = _userManager.GetUserId(User);

            var query = _db.Orders
       .Include(o => o.Items)
       .ThenInclude(i => i.Product)
       .Where(o => o.UserId == userId);


       if (filter != null)
            {
                query=query.Where(x=>x.Status==filter);
            }
       var orders=query.ToList();
            return View(orders);
        }


    }
}
