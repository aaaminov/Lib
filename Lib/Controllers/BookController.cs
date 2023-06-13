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

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			return View();
		}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id, string? message = null) {
			Console.WriteLine("book get one = " + id.ToString());
			Book book = LibDbContext.Instance.Books
				.Include(b => b.AuthorBooks)
					.ThenInclude(ab => ab.Author)
				.Include(b => b.GenreBooks)
					.ThenInclude(ab => ab.Genre)
				.Include(b => b.FeaturedBooks)
					.ThenInclude(ab => ab.Mark)
				.Include(b => b.FeaturedBooks)
					.ThenInclude(ab => ab.User)
				.Include(b => b.Reviews)
					.ThenInclude(ab => ab.User)
				.Include(b => b.Reviews)
					.ThenInclude(ab => ab.Likes)
						.ThenInclude(ab => ab.User)
				.FirstOrDefault(b => b.Id == id);
			if (book == null) {
				return RedirectToAction("All", "Book");
			}

			//List<Author> authors = new List<Author>();
			//List<AuthorBook> authorBooks = LibDbContext.Instance.AuthorBooks
			//    .Include(ab => ab.Author)
			//    .Where(ab => ab.BookId == id).ToList();
			//foreach (AuthorBook authorBook in authorBooks) {
			//    //authors.Add(LibDbContext.Instance.Authors.FirstOrDefault(a => a.Id == authorBook.AuthorId));
			//    authors.Add(authorBook.Author);
			//}

			//int? userId = HttpContext.Session.GetInt32("userId");
			//if (userId.HasValue) {
			//	User user = LibDbContext.Instance.Users
			//		.Include(u => u.Role)
			//		.Include(u => u.Likes)
			//		.FirstOrDefault(u => u.Id == userId);
			//	ViewBag.user = user;
			//}

			ViewBag.message = message;
			ViewBag.book = book;
			ViewBag.authors = book.AuthorBooks.Select(ab => ab.Author).ToList();
			ViewBag.genres = book.GenreBooks.Select(ab => ab.Genre).ToList();
			ViewBag.reviews = book.Reviews.OrderByDescending(b => b.DateOfCreation).ToList();
			ViewBag.marks = LibDbContext.Instance.Marks.ToList();
			SaveBookToSession(book);

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

				ViewBag.genres = LibDbContext.Instance.Genres.ToList();
				ViewBag.authors = LibDbContext.Instance.Authors.ToList();

                return View("~/Views/Book/Update.cshtml");
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("delete")]
		public async Task<ActionResult> Delete(int id) {
			User user = UserController.getCurrentUser(HttpContext);
			if (user != null && UserController.isCurrentUserAdmin(user)) {
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
						.ThenInclude(ab => ab.User)
				.FirstOrDefault(b => b.Id == id);
				if (book != null) {
					foreach (var n in book.AuthorBooks) {
						LibDbContext.Instance.AuthorBooks.Remove(n);
					}
					foreach (var n in book.GenreBooks) {
						LibDbContext.Instance.GenreBooks.Remove(n);
					}
					foreach (var n in book.FeaturedBooks) {
						LibDbContext.Instance.FeaturedBooks.Remove(n);
					}
					foreach (var r in book.Reviews) {
						foreach (var l in r.Likes) {
							LibDbContext.Instance.Likes.Remove(l);
						}
						LibDbContext.Instance.Reviews.Remove(r);
					}
					LibDbContext.Instance.Books.Remove(book);
					await LibDbContext.Instance.SaveChangesAsync();
				}
				return RedirectToAction("All", "Book");
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
				Book book = LibDbContext.Instance.Books
					.Include(n => n.GenreBooks)
					.Include(n => n.AuthorBooks)
					.FirstOrDefault(b => b.Id == id);
				Console.WriteLine("book get edit = " + id.ToString());
				ViewBag.book = book;

                ViewBag.genre_ids = book.GenreBooks.Select(n => n.GenreId).ToList();
                ViewBag.author_ids = book.AuthorBooks.Select(n => n.AuthorId).ToList();

                ViewBag.genres = LibDbContext.Instance.Genres.ToList();
                ViewBag.authors = LibDbContext.Instance.Authors.ToList();

                return View("~/Views/Book/Update.cshtml");
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("update")]
		public async Task<ActionResult> Update(int id, string name, string description, string photo, 
					string date_of_creation, string[] genre_ids, string[] author_ids) {
			Console.WriteLine(genre_ids.Length + " lol " + string.Join(", ", genre_ids));
			User user = UserController.getCurrentUser(HttpContext);
			if (user != null && UserController.isCurrentUserAdmin(user)) {
				var last = LibDbContext.Instance.Books.OrderBy(n => n.Id).LastOrDefault();
				Book book = null;
				if (id >= 1) {
					book = LibDbContext.Instance.Books
						.Include(n => n.GenreBooks)
							.ThenInclude(n => n.Genre)
						.Include(n => n.AuthorBooks)
							.ThenInclude(n => n.Author)
						.FirstOrDefault(b => b.Id == id);

					foreach (GenreBook gb in book.GenreBooks) {
                        LibDbContext.Instance.GenreBooks.Remove(gb);
                    }
					foreach (var genreIdStr in genre_ids) {
						if (int.TryParse(genreIdStr, out int genreId)) {
							GenreBook gb = new GenreBook() {
								BookId = book.Id,
								GenreId = genreId
							};
                            LibDbContext.Instance.GenreBooks.Add(gb);
                        }
					}
					
					foreach (AuthorBook ab in book.AuthorBooks) {
                        LibDbContext.Instance.AuthorBooks.Remove(ab);
                    }
					foreach (var authorIdStr in author_ids) {
						if (int.TryParse(authorIdStr, out int authorId)) {
							AuthorBook ab = new AuthorBook() {
								BookId = book.Id,
								AuthorId = authorId
							};
                            LibDbContext.Instance.AuthorBooks.Add(ab);
                        }
					}

					book.Name = name;
					book.Description = description;
					book.Photo = photo;
					book.DateOfCreation = date_of_creation;

					LibDbContext.Instance.Books.Update(book);
				} else {
					book = new Book() {
						Id = last != null ? (last.Id + 1) : 0,
						Name = name,
						Description = description,
						Photo = photo,
						DateOfCreation = date_of_creation
					};
                    LibDbContext.Instance.Books.Add(book);
                    foreach (var genreIdStr in genre_ids) {
                        if (int.TryParse(genreIdStr, out int genreId)) {
                            GenreBook gb = new GenreBook() {
                                BookId = book.Id,
                                GenreId = genreId
                            };
                            LibDbContext.Instance.GenreBooks.Add(gb);
                        }
                    }
                    foreach (var authorIdStr in author_ids) {
                        if (int.TryParse(authorIdStr, out int authorId)) {
                            AuthorBook ab = new AuthorBook() {
                                BookId = book.Id,
                                AuthorId = authorId
                            };
                            LibDbContext.Instance.AuthorBooks.Add(ab);
                        }
                    }
					//await LibDbContext.Instance.SaveChangesAsync();
					//return RedirectToAction("All", "Author");
				}
				await LibDbContext.Instance.SaveChangesAsync();
				return RedirectToAction("One", new { id });
			}
			return RedirectToAction("Login", "Auth");
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
