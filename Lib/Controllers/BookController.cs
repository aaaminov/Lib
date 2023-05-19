using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("book")]
	public class BookController : Controller {

		private const int COUNT_OF_SAVED_BOOKS = 10;

		// GET: BookController
		[HttpGet("")]
		[HttpGet("all")]
		public ActionResult All() {
			ViewBag.books = LibDbContext.Instance.Books.ToList();
			return View();
		}

		//// GET: BookController/Create
		//[HttpGet("create")]
		//public IActionResult Create() {
		//    ViewBag.books = LibDbContext.Instance.Books.ToList();
		//    return View();
		//}

		//// POST: BookController/Create
		//[HttpPost("create")]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create(IFormCollection collection) {
		//    try {
		//        return RedirectToAction("ToBookPage", "{id:int}", new { id = 1 });
		//    } catch {
		//        return View();
		//    }
		//}

		//[HttpGet("{id:int}/edit")]
		//public IActionResult Edit(int id) {
		//    ViewBag.book = LibDbContext.Instance.Books.Find(id);
		//    return View();
		//}

		//// POST: BookController/Edit/5
		//[HttpPost("{id:int}/edit")]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit(int id, IFormCollection collection) {
		//    ViewBag.book = LibDbContext.Instance.Books.Find(id);
		//    try {
		//        return RedirectToAction("ToBookPage", "{id:int}", new { id });
		//    } catch {
		//        return View();
		//    }
		//}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			Book book = LibDbContext.Instance.Books
				.Include(b => b.AuthorBooks)
					.ThenInclude(ab => ab.Author)
				.Include(b => b.GenreBooks)
					.ThenInclude(ab => ab.Genre)
				.Include(b => b.FeaturedBooks)
					.ThenInclude(ab => ab.Mark)
				.Include(b => b.Reviews)
					.ThenInclude(ab => ab.User)
				.Include(b => b.Reviews)
					.ThenInclude(ab => ab.Likes)
						.ThenInclude(ab => ab.User) // мб не тут потом это делать, а при наведении на лайки
				.FirstOrDefault(b => b.Id == id);

			//List<Author> authors = new List<Author>();
			//List<AuthorBook> authorBooks = LibDbContext.Instance.AuthorBooks
			//    .Include(ab => ab.Author)
			//    .Where(ab => ab.BookId == id).ToList();
			//foreach (AuthorBook authorBook in authorBooks) {
			//    //authors.Add(LibDbContext.Instance.Authors.FirstOrDefault(a => a.Id == authorBook.AuthorId));
			//    authors.Add(authorBook.Author);
			//}

			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				User user = LibDbContext.Instance.Users
					.Include(u => u.Role)
					.Include(u => u.Likes)
					.FirstOrDefault(u => u.Id == userId);
				ViewBag.user = user;
			}

			ViewBag.book = book;
			ViewBag.authors = book.AuthorBooks.Select(ab => ab.Author).ToList();
			ViewBag.genres = book.GenreBooks.Select(ab => ab.Genre).ToList();
			ViewBag.reviews = book.Reviews.ToList();

			ViewBag.marks = LibDbContext.Instance.Marks.ToList();

			SaveBookToSession(book);
			return View();
		}

		//// GET: BookController/Delete/5
		//public ActionResult Delete(int id) {
		//    return View();
		//}

		//// POST: BookController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection) {
		//    try {
		//        return RedirectToAction(nameof(Index));
		//    } catch {
		//        return View();
		//    }
		//}

		private void SaveBookToSession(Book book) {
			List<int> savedIds = new List<int>();
			for (int i = 0; i < COUNT_OF_SAVED_BOOKS; i++) {

				int? savedBookId = HttpContext.Session.GetInt32($"savedBook_{i}_id");
				if (!savedBookId.HasValue) {
					break;
				}
				savedIds.Add(savedBookId.Value);
			}
			if (savedIds.Contains(book.Id)) {
				savedIds.Remove(book.Id); // удалить в середине
			}
			savedIds.Insert(0, book.Id); // добавить в начало
			if ((savedIds.Count - 1) == COUNT_OF_SAVED_BOOKS) {
				savedIds.RemoveAt(savedIds.Count - 1);
			}
			for (int i = 0; i < savedIds.Count; i++) {
				HttpContext.Session.SetInt32($"savedBook_{i}_id", savedIds[i]);
			}
		}

	}
}
