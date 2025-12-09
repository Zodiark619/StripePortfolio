namespace StripePortfolio.Areas.GrandArchive.Models
{
    public class Rarity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new();
    }
}
