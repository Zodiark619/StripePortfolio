namespace StripePortfolio.Areas.GrandArchive.Models
{
    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new();
    }
}
