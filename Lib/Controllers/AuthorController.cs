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
            ViewBag.authors = LibDbContext.Instance.Authors.ToList();
            return View();
        }

        //// GET:
        //[HttpGet("create")]
        //public IActionResult Create() {
        //    ViewBag.authors = LibDbContext.Instance.Authors.ToList();
        //    return View();
        //}

        //// POST:
        //[HttpPost("create")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection) {
        //    try {
        //        return RedirectToAction("ToAuthorProfile", "{id:int}", new { id = 1 });
        //    } catch {
        //        return View();
        //    }
        //}

        //[HttpGet("{id:int}/edit")]
        //public IActionResult Edit(int id) {
        //    ViewBag.author = LibDbContext.Instance.Authors.Find(id);
        //    return View();
        //}

        //// POST:
        //[HttpPost("{id:int}/edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection) {
        //    ViewBag.author = LibDbContext.Instance.Authors.Find(id);
        //    try {
        //        return RedirectToAction("ToAuthorProfile", "{id:int}", new { id });
        //    } catch {
        //        return View();
        //    }
        //}

        // GET:
        [HttpGet("{id:int}")]
        public IActionResult One(int id) {
            Author author = LibDbContext.Instance.Authors.Find(id);

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
			return View();
        }

		//// GET: AuthorController/Delete/5
		//public ActionResult Delete(int id) {
		//    return View();
		//}

		//// POST: AuthorController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection) {
		//    try {
		//        return RedirectToAction(nameof(Index));
		//    } catch {
		//        return View();
		//    }
		//}


		private void SaveAuthorToSession(Author author) {
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
