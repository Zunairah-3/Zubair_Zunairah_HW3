using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Zubair_Zunairah_HW3.Models;

namespace Zubair_Zunairah_HW3.Models.ViewModels
{
    public class SearchViewModel
    {
        // Name search: Title OR Author
        [Display(Name = "Search")]
        public string? SearchString { get; set; }

        // Description contains
        [Display(Name = "Description")]
        public string? Description { get; set; }

        // Genre dropdown
        [Display(Name = "Genre")]
        public int? SelectedGenreId { get; set; }
        public SelectList? GenreList { get; set; }

        // Format dropdown
        [Display(Name = "Format")]
        public Format? SelectedFormat { get; set; }
        public SelectList? FormatList { get; set; }

        // Price + Greater/Less radio
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        // true = Greater Than (or equal), false = Less Than (or equal)
        public bool SearchGreaterThan { get; set; } = true;

        // Released after
        [Display(Name = "Released After")]
        [DataType(DataType.Date)]
        public DateTime? ReleasedAfter { get; set; }

        // Optional results list (not required but useful)
        public List<Book>? Results { get; set; }
    }
}