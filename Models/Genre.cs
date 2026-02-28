using System.ComponentModel.DataAnnotations;

//TODO: Update this namespace to match your project
namespace Zubair_Zunairah_HW3.Models{
    public class Genre
    {
        public Int32 GenreID { get; set; }

        [Display(Name = "Genre")]
        public String GenreName { get; set; }

        //navigational property for books - a single genre will be associated with many books
        public List<Book> Books { get; set; }

        public Genre()
        {
            Books ??= new List<Book>();
        }
    }
}
