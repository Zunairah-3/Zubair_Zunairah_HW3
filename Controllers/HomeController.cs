using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zubair_Zunairah_HW3.DAL;
using Zubair_Zunairah_HW3.Models;
using Zubair_Zunairah_HW3.Models.ViewModels;

namespace Zubair_Zunairah_HW3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /  (Quick Search: Title OR Description)
        public IActionResult Index(string? SearchString)
        {
            ViewBag.AllBooks = _context.Books.Count();

            IQueryable<Book> query = _context.Books
                                             .Include(b => b.Genre);

            // Quick Search = Title OR Description
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                string s = SearchString.Trim();
                query = query.Where(b => b.Title.Contains(s) || b.Description.Contains(s));
            }

            List<Book> selectedBooks = query
                .OrderBy(b => b.Title)
                .ToList();

            ViewBag.SelectedBooks = selectedBooks.Count;

            return View(selectedBooks);
        }

        // GET: /Home/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new List<string> { "No book was selected." });
            }

            Book? book = _context.Books
                                 .Include(b => b.Genre)
                                 .FirstOrDefault(b => b.BookID == id);

            if (book == null)
            {
                return View("Error", new List<string> { "That book was not found." });
            }

            return View(book);
        }

        // GET: /Home/DetailedSearch  (shows form)
        public IActionResult DetailedSearch()
        {
            SearchViewModel svm = new SearchViewModel();

            svm.GenreList = new SelectList(
                _context.Genres.OrderBy(g => g.GenreName),
                "GenreID",
                "GenreName"
            );

            svm.FormatList = new SelectList(
                Enum.GetValues(typeof(Format)).Cast<Format>()
            );

            return View(svm);
        }

        // GET: /Home/DisplaySearchResults  (runs filters + shows Index results)
        public IActionResult DisplaySearchResults(SearchViewModel svm)
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Genre);

            // Name search = Title OR Author
            if (!string.IsNullOrWhiteSpace(svm.SearchString))
            {
                string s = svm.SearchString.Trim();
                query = query.Where(b => b.Title.Contains(s) || b.Author.Contains(s));
            }

            // Description contains
            if (!string.IsNullOrWhiteSpace(svm.Description))
            {
                string d = svm.Description.Trim();
                query = query.Where(b => b.Description.Contains(d));
            }

            // Genre filter
            if (svm.SelectedGenreId.HasValue)
            {
                query = query.Where(b => b.Genre.GenreID == svm.SelectedGenreId.Value);
            }

            // Format filter
            if (svm.SelectedFormat.HasValue)
            {
                query = query.Where(b => b.BookFormat == svm.SelectedFormat.Value);
            }

            // Price filter (inclusive) with Greater/Less radio
            if (svm.Price.HasValue)
            {
                if (svm.SearchGreaterThan)
                {
                    query = query.Where(b => b.Price >= svm.Price.Value);
                }
                else
                {
                    query = query.Where(b => b.Price <= svm.Price.Value);
                }
            }

            // Released After (PublishedDate ON or AFTER)
            if (svm.ReleasedAfter.HasValue)
            {
                query = query.Where(b => b.PublishedDate >= svm.ReleasedAfter.Value);
            }

            List<Book> selectedBooks = query.OrderBy(b => b.Title).ToList();

            ViewBag.AllBooks = _context.Books.Count();
            ViewBag.SelectedBooks = selectedBooks.Count;

            return View("Index", selectedBooks);
        }
    }
}