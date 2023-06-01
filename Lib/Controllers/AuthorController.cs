using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("author")]
	public class AuthorController : Controller {

		private const int COUNT_OF_SAVED_AUTHORS = 10;

		// GET:
		[HttpGet("")]
		[HttpGet("all")]
		public ActionResult All() {
			List<Author> authors = LibDbContext.Instance.Authors.OrderBy(a => a.Name).ToList();
            ViewBag.authors = authors;

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View();
		}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			//Console.WriteLine("author get one = " + id.ToString());
			Author author = LibDbContext.Instance.Authors.FirstOrDefault(a => a.Id == id);
			if (author == null) {
				return RedirectToAction("All", "Author");
			}

			List<Book> books = new List<Book>();
			List<AuthorBook> authorBooks = LibDbContext.Instance.AuthorBooks.Where(ab => ab.AuthorId == id).ToList();
			foreach (AuthorBook authorBook in authorBooks) {
				books.Add(LibDbContext.Instance.Books
					.Include(b => b.AuthorBooks)
						.ThenInclude(ab => ab.Author)
					.Include(b => b.FeaturedBooks)
					.FirstOrDefault(b => b.Id == authorBook.BookId));
			}
			ViewBag.author = author;
			ViewBag.newBooks = books
				.OrderByDescending(b => b.Id)
				.Take(4).ToList();
			ViewBag.popularBooks = books
				.OrderByDescending(b => b.FeaturedBooks.Count)
				.Take(4).ToList();
			SaveAuthorToSession(author);

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
				return View("~/Views/Author/Update.cshtml");
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("delete")]
		public async Task<ActionResult> Delete(int id) {
			User user = UserController.getCurrentUser(HttpContext);
			if (user != null && UserController.isCurrentUserAdmin(user)) {
				Author author = LibDbContext.Instance.Authors
					.Include(a => a.AuthorBooks)
					.FirstOrDefault(a => a.Id == id);
				if (author != null) {
					foreach (var ab in author.AuthorBooks) {
						LibDbContext.Instance.AuthorBooks.Remove(ab);
					}

					// мб вслед удалять книги

					LibDbContext.Instance.Authors.Remove(author);
					await LibDbContext.Instance.SaveChangesAsync();
				}
				return RedirectToAction("All", "Author");
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
				Author author = LibDbContext.Instance.Authors.FirstOrDefault(a => a.Id == id);
				Console.WriteLine("author get edit = " + id.ToString());
				ViewBag.author = author;
				return View("~/Views/Author/Update.cshtml");
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("update")]
		public async Task<ActionResult> Update(int id, string name, string photo, string biography) {

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null && UserController.isCurrentUserAdmin(user)) {
				var last = LibDbContext.Instance.Authors.OrderBy(n => n.Id).LastOrDefault();
				Author author = null;
				if (id >= 1) {
					author = LibDbContext.Instance.Authors
						.Include(a => a.AuthorBooks)
							.ThenInclude(ab => ab.Book)
						.FirstOrDefault(a => a.Id == id);
					author.Name = name;
					author.Photo = photo;
					author.Biography = biography;

					LibDbContext.Instance.Authors.Update(author);
				} else {
					author = new Author() {
						Id = last != null ? (last.Id + 1) : 0,
						Name = name,
						Photo = photo,
						Biography = biography
					};

					LibDbContext.Instance.Authors.Add(author);
					//await LibDbContext.Instance.SaveChangesAsync();
					//return RedirectToAction("All", "Author");
				}
				await LibDbContext.Instance.SaveChangesAsync();
				return RedirectToAction("One", new { id });
			}
			return RedirectToAction("Login", "Auth");
		}




		private void SaveAuthorToSession(Author author) {
			if (author == null) { return; }
			List<int> savedIds = new List<int>();
			for (int i = 0; i < COUNT_OF_SAVED_AUTHORS; i++) {

				int? savedBookId = HttpContext.Session.GetInt32($"savedAuthor_{i}_id");
				if (!savedBookId.HasValue) {
					break;
				}
				savedIds.Add(savedBookId.Value);
			}
			if (savedIds.Contains(author.Id)) {
				savedIds.Remove(author.Id); // удалить в середине
			}
			savedIds.Insert(0, author.Id); // добавить в начало
			if ((savedIds.Count - 1) == COUNT_OF_SAVED_AUTHORS) {
				savedIds.RemoveAt(savedIds.Count - 1);
			}
			for (int i = 0; i < savedIds.Count; i++) {
				HttpContext.Session.SetInt32($"savedAuthor_{i}_id", savedIds[i]);
			}
		}

	}
}
