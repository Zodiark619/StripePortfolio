namespace StripePortfolio.Areas.GrandArchive.Models.ViewModels
{
    public class IndexViewModel
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl {  get; set; }
        public List<string> Elements { get; set; }
        public List<string> CardTypes { get; set; }
        public List<string> Subtypes { get; set; }
        public string Rarity { get; set; }
        public List<string> Sets { get; set; }

    }
}
