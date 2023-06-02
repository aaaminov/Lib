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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		//private User getCurrentUser() {
		//	int? userId = HttpContext.Session.GetInt32("userId");
		//	if (userId.HasValue) {
		//		User user = LibDbContext.Instance.Users
		//			.Include(u => u.Role)
		//			.Include(u => u.Reviews)
		//			.FirstOrDefault(u => u.Id == userId);
		//		return user;
		//	}
		//	return null;
		//}


		[HttpGet("")]
        public IActionResult Index(bool? welcome) {
			ViewBag.popularBooks = LibDbContext.Instance.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .Include(b => b.FeaturedBooks)
				.OrderByDescending(b => b.FeaturedBooks.Count)
				.Take(10).ToList();
			ViewBag.newBooks = LibDbContext.Instance.Books
				.Include(b => b.AuthorBooks)
					.ThenInclude(ab => ab.Author)
                .OrderByDescending(b => b.Id)
				.Take(10).ToList();
            ViewBag.bestReviews = LibDbContext.Instance.Reviews
                .Include(r=>r.Book)
                .Include(r=>r.Likes)
                .Include(r=>r.User)
				.OrderByDescending(r => r.Likes.Count)
				.Take(10).ToList();
			ViewBag.showWelcome = welcome.HasValue;

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View();
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
				.OrderByDescending(b => b.FeaturedBooks.Count)
				.Take(20).ToList();
            ViewBag.title = "Популярное";
            ViewBag.ActivePopular = "active";
            ViewBag.ShowPopular = true;

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View("~/Views/Book/All.cshtml");
		}

        [HttpGet("new")]
        public IActionResult New() {
			ViewBag.books = LibDbContext.Instance.Books
				.OrderByDescending(b => b.Id)
				.Take(20).ToList();
			ViewBag.title = "Новинки";
			ViewBag.ActiveNew = "active";
            ViewBag.ShowPopular = false;

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View("~/Views/Book/All.cshtml");
		}
        

        [HttpGet("search")]
        public IActionResult Search(string q, int? genre_id) {
            if (string.IsNullOrWhiteSpace(q)) {
				return RedirectToAction("Index", "Home"); // мб потом на страницу с полем поиска
			}
            q = q.ToLower().Trim();
            ViewBag.genres = LibDbContext.Instance.Genres.ToList();

			List<Book> books = LibDbContext.Instance.Books
				.Include(b => b.GenreBooks)
				.Where(b => b.Name.Contains(q)).ToList();

			if (genre_id.HasValue) {
                if (genre_id.Value != 0) {
					books = books.Where(b => b.GenreBooks.Select(gb => gb.GenreId).Contains(genre_id.Value)).ToList();
                    ViewBag.genre_id = genre_id;
				}
            }

			List<Author> authors = LibDbContext.Instance.Authors
				//.Include(b => b.GenreBooks)
				.Where(a => a.Name.Contains(q)).ToList();

			ViewBag.q = q;
			ViewBag.books = books;
			ViewBag.authors = authors;
			return View();
        }


    }
}