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
			ViewBag.genres = LibDbContext.Instance.Genres.ToList();
			return View();
		}

		//[HttpGet("create")]
		//public IActionResult Create() {
		//	ViewBag.genres = LibDbContext.Instance.Genres.ToList();
		//	return View();
		//}

		//[HttpGet("{id:int}/edit")]
		//public IActionResult Edit(int id) {
		//	ViewBag.genre = LibDbContext.Instance.Genres.Find(id);
		//	return View();
		//}

		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			ViewBag.genre = LibDbContext.Instance.Genres.Find(id);

			List<Book> books = new List<Book>();
			List<GenreBook> genreBooks = LibDbContext.Instance.GenreBooks
				.Include(gb => gb.Book)
				.Where(gb => gb.GenreId == id).ToList();
			foreach (GenreBook genreBook in genreBooks) {
				//books.Add(LibDbContext.Instance.Books.FirstOrDefault(b => b.Id == genreBook.BookId));
				books.Add(genreBook.Book);
			}
			ViewBag.books = books;
			return View();
		}
	}
}
