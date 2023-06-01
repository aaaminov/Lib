using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
    [Route("genre")]
    public class GenreController : Controller {

        //[Route("~/genres")]
        [HttpGet("")]
        [HttpGet("all")]
        public IActionResult All() {
            ViewBag.genres = LibDbContext.Instance.Genres.OrderBy(g => g.Name).ToList();

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View();
        }

        [HttpGet("{id:int}")]
        public IActionResult One(int id) {
			Console.WriteLine("genre get one = " + id.ToString());
			List<Genre> genres = LibDbContext.Instance.Genres.OrderBy(g => g.Name).ToList();
            ViewBag.genres = genres;
            Genre genre = genres.FirstOrDefault(g => g.Id == id);
			if (genre == null) {
				return RedirectToAction("All", "Genre");
			}

            ViewBag.genre = genre;

			List<Book> books = new List<Book>();
            List<GenreBook> genreBooks = LibDbContext.Instance.GenreBooks
                .Include(gb => gb.Book)
                    .ThenInclude(b => b.FeaturedBooks)
                .Where(gb => gb.GenreId == id).ToList();
            foreach (GenreBook genreBook in genreBooks) {
                //books.Add(LibDbContext.Instance.Books.FirstOrDefault(b => b.Id == genreBook.BookId));
                books.Add(genreBook.Book);
            }

            ViewBag.popularBooks = books.OrderByDescending(b => b.FeaturedBooks.Count).ToList();
            ViewBag.newBooks = books.OrderByDescending(b => b.Id).ToList();

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View();
        }

        // GET:
        [HttpGet("create")]
        public IActionResult Create(string? message) {
            if (message != null) {
                ViewBag.message = message;
            }
            User user = UserController.getCurrentUser(HttpContext);
            if (user != null && UserController.isCurrentUserAdmin(user)) {
                return View("~/Views/Genre/Update.cshtml");
            }
            return RedirectToAction("Login", "Auth");
        }

        // POST:
        [HttpPost("delete")]
        public async Task<ActionResult> Delete(int id) {
            User user = UserController.getCurrentUser(HttpContext);
            if (user != null && UserController.isCurrentUserAdmin(user)) {
                Genre genre = LibDbContext.Instance.Genres
                    .Include(g => g.GenreBooks)
                    .FirstOrDefault(g => g.Id == id);
                if (genre != null) {
                    foreach (var gb in genre.GenreBooks) {
                        LibDbContext.Instance.GenreBooks.Remove(gb);
                    }

                    // мб вслед удалять книги

                    LibDbContext.Instance.Genres.Remove(genre);
                    await LibDbContext.Instance.SaveChangesAsync();
                }
                return RedirectToAction("All", "Genre");
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET:
        [HttpGet("{id:int}/edit")]
        public IActionResult Edit(int id, string? message) {
            if (message != null) {
                ViewBag.message = message;
            }
            User user = UserController.getCurrentUser(HttpContext);
            if (user != null && UserController.isCurrentUserAdmin(user)) {
                Genre genre = LibDbContext.Instance.Genres.FirstOrDefault(g => g.Id == id);
				Console.WriteLine("genre get edit = " + id.ToString());
				ViewBag.genre = genre;
                return View("~/Views/Genre/Update.cshtml");
            }
            return RedirectToAction("Login", "Auth");
        }

        // POST:
        [HttpPost("update")]
        public async Task<ActionResult> Update(int id, string name) {
            User user = UserController.getCurrentUser(HttpContext);
            if (user != null && UserController.isCurrentUserAdmin(user)) {
                var last = LibDbContext.Instance.Genres.OrderBy(n => n.Id).LastOrDefault();
                Genre genre = null;
                if (id >= 1) {
                    genre = LibDbContext.Instance.Genres
                        .Include(g => g.GenreBooks)
                            .ThenInclude(gb => gb.Book)
                        .FirstOrDefault(g => g.Id == id);
                    genre.Name = name;
                    LibDbContext.Instance.Genres.Update(genre);
                    await LibDbContext.Instance.SaveChangesAsync();
                    return RedirectToAction("One", new { id });
                } else {
                    genre = new Genre() {
                        Id = last != null ? (last.Id + 1) : 0,
                        Name = name
                    };
                    LibDbContext.Instance.Genres.Add(genre);
                    await LibDbContext.Instance.SaveChangesAsync();
                    return RedirectToAction("All", "Genre");
                }
            }
            return RedirectToAction("Login", "Auth");
        }

    }
}
