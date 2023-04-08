using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lib.Controllers {
    [Route("author")]
    public class AuthorController : Controller {

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
            ViewBag.author = LibDbContext.Instance.Authors.Find(id);

            List<Book> books = new List<Book>();
            List<AuthorBook> authorBooks = LibDbContext.Instance.AuthorBooks.Where(ab => ab.AuthorId == id).ToList();
            foreach (AuthorBook authorBook in authorBooks) {
                books.Add(LibDbContext.Instance.Books.FirstOrDefault(b => b.Id == authorBook.BookId));
            }
            ViewBag.books = books;
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
    }
}
