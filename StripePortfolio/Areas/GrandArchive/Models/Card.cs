using System.ComponentModel.DataAnnotations;

namespace StripePortfolio.Areas.GrandArchive.Models
{
   
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public List<Element> Elements {  get; set; } //norm,arcane,fire
        [Required]

        public List<CardType> CardTypes {  get; set; } //ally,regalia
        [Required]

        public List<Subtype> Subtypes { get; set; }
        [Required]

        public Rarity Rarity {  get; set; }
        [Required]

        public List<Set> Sets {  get; set; }
        private string _uuid;
        [Required]

        public string Uuid
        {
            get => _uuid;
            set
            {
                _uuid = value;
                ImageUrl = $"https://api.gatcg.com/cards/images/{_uuid}.jpg";
            }
        }

        public string ImageUrl { get; private set; }  // stored in DB
    }
}
