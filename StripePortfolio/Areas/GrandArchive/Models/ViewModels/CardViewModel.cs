using Microsoft.AspNetCore.Mvc.Rendering;
using StripePortfolio.Areas.GrandArchive.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StripePortfolio.Areas.GrandArchive.Models.ViewModels
{
    public class CardViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Multi-selection IDs the user chose
        [MinElements(1)]
        public List<int> SelectedElementIds { get; set; } = new();

        [MinElements(1)]
        public List<int> SelectedCardTypeIds { get; set; } = new();

        [MinElements(1)]

        public List<int> SelectedSubtypeIds { get; set; } = new();

        [MinElements(1)]

        public List<int> SelectedSetIds { get; set; } = new();

        [Required]
        public int RarityId { get; set; }

        // Dropdown sources
        public List<SelectListItem> ElementOptions { get; set; } = new();
        public List<SelectListItem> CardTypeOptions { get; set; } = new();
        public List<SelectListItem> SubtypeOptions { get; set; } = new();
        public List<SelectListItem> SetOptions { get; set; } = new();
        public List<SelectListItem> RarityOptions { get; set; } = new();

        [Required]
        public string Uuid { get; set; }

        public string ImageUrl { get; set; }
    }
}
