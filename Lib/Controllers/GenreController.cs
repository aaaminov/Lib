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
			List<Genre> genres = LibDbContext.Instance.Genres.OrderBy(g => g.Name).ToList();
			ViewBag.genres = genres;
			ViewBag.genre = genres.Find(g=> g.Id == id);

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
			return View();
		}
	}
}
