using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lib.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index() {
			ViewBag.popularBooks = LibDbContext.Instance.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .Include(b => b.FeaturedBooks)
				.OrderByDescending(b => b.FeaturedBooks.Count)
				.Take(4).ToList();
			ViewBag.newBooks = LibDbContext.Instance.Books
				.Include(b => b.AuthorBooks)
					.ThenInclude(ab => ab.Author)
                .OrderByDescending(b => b.Id)
				.Take(4).ToList();
			return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("about")]
        public IActionResult About() {
            return View();
        }

        [HttpGet("popular")]
        public IActionResult Popular() {
			ViewBag.books = LibDbContext.Instance.Books
				//.Include(b => b.AuthorBooks)
				//	.ThenInclude(ab => ab.Author)
                .Include(b => b.FeaturedBooks)
				.OrderByDescending(b => b.FeaturedBooks.Count).ToList();
			return View(); // мб потом ссылать такие запросы на одну изменяемую страницу
        }

        [HttpGet("new")]
        public IActionResult New() {
			ViewBag.books = LibDbContext.Instance.Books.OrderByDescending(b => b.Id).ToList();
			return View(); // мб потом ссылать такие запросы на одну изменяемую страницу
		}
        
        [HttpGet("search")]
        public IActionResult Search(string search, int? genre_id) {
            if (string.IsNullOrWhiteSpace(search)) {
				return RedirectToAction("Index", "Home"); // мб потом на страницу с полем поиска
			}
            search = search.ToLower().Trim();
            ViewBag.genres = LibDbContext.Instance.Genres.ToList();

			List<Book> books = LibDbContext.Instance.Books
                .Include(b => b.GenreBooks)
                .Where(b => b.Name.Contains(search)).ToList();

            if (genre_id.HasValue) {
                if (genre_id.Value != 0) {
					books = books.Where(b => b.GenreBooks.Select(gb => gb.GenreId).Contains(genre_id.Value)).ToList();
                    ViewBag.genre_id = genre_id;
				}
            }
            ViewBag.search = search;
			ViewBag.books = books;
			return View();
        }


    }
}