using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
    [Route("book")]
    public class BookController : Controller {

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
                .Include(b => b.Reviews)
					.ThenInclude(ab => ab.User)
				.FirstOrDefault(b => b.Id == id);

            //List<Author> authors = new List<Author>();
            //List<AuthorBook> authorBooks = LibDbContext.Instance.AuthorBooks
            //    .Include(ab => ab.Author)
            //    .Where(ab => ab.BookId == id).ToList();
            //foreach (AuthorBook authorBook in authorBooks) {
            //    //authors.Add(LibDbContext.Instance.Authors.FirstOrDefault(a => a.Id == authorBook.AuthorId));
            //    authors.Add(authorBook.Author);
            //}

            ViewBag.book = book;
            ViewBag.authors = book.AuthorBooks.Select(ab => ab.Author).ToList();
            ViewBag.reviews = book.Reviews.ToList();
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
    }
}
