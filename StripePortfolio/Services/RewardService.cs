using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Areas.GrandArchive.Models;
using StripePortfolio.Data;
using StripePortfolio.Models;
using System;

namespace StripePortfolio.Services
{
    public class RewardService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Random _random;
        public RewardService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _random = new Random();
        }
        public void AddCardsToInventory(string userId, List<Card> cards, int orderId)
        {
            foreach (var card in cards)
            {
                var existing = _context.CardInventories
                    .FirstOrDefault(x => x.UserId == userId &&
                    x.CardId == card.Id
                    && x.OrderId == orderId);

                if (existing != null)
                {
                    existing.Quantity++;
                }
                else
                {
                    _context.CardInventories.Add(new CardInventory
                    {
                        UserId = userId,
                        CardId = card.Id,
                        Quantity = 1,
                        OrderId = orderId
                    });
                }
            }
            _context.SaveChanges(); // single save

        }


        public List<Card> SendContentToInventory(Product product, string userId, int orderId)
        {
            var content = new List<Card>();
            if (product.YieldContent == "Generate12CardsPack")
            {
                content = Generate12CardsPack();
            } 
            AddCardsToInventory(userId, content, orderId); 
            return content;
        } 
        private List<Card> Generate12CardsPack()
        {
            var cards = _context.Card.Include(x => x.Rarity).ToList();
            var common = cards.Where(x => x.Rarity.Name == "Common").OrderBy(x => _random.Next()).Take(8).ToList();
            var rare = cards.Where(x =>  x.Rarity.Name == "Uncommon").OrderBy(x=>_random.Next()).Take(3).ToList();
            var superrare=new List<Card>();
            var roll = _random.Next(100); 
            if (roll < 5)
            {
                superrare = cards.Where(x => x.Rarity.Name == "Super Rare").OrderBy(x => _random.Next()).Take(1).ToList();
            }
            else
            {
                superrare = cards.Where(x => x.Rarity.Name == "Rare").OrderBy(x => _random.Next()).Take(1).ToList();
            }
                var total = new List<Card>();
            total.AddRange(superrare);
            total.AddRange(rare);
            total.AddRange(common); 
            return total;
        }

        //if (roll < 5)
        //{
        //    superrare = cards.Where(x => x.Rarity.Name == "Collector Super Rare"|| x.Rarity.Name == "Ultra Rare").OrderBy(x => _random.Next()).Take(1).ToList();

        //}
        //else if (roll<20)
        //{
        //    superrare = cards.Where(x => x.Rarity.Name == "Super Rare" ).OrderBy(x => _random.Next()).Take(1).ToList();

        //}
        //else
        //{
        //          superrare = cards.Where(x => x.Rarity.Name == "Rare").OrderBy(x => _random.Next()).Take(1).ToList();

        //            }


    }

}
