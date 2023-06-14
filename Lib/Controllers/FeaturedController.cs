using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("featured")]
	public class FeaturedController : Controller {

		private User getCurrentUser() {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				User user = LibDbContext.Instance.Users
					.Include(u => u.Role)
					.FirstOrDefault(u => u.Id == userId);
				return user;
			}
			return null;
		}

		// GET:
		[HttpGet("")]
		[HttpGet("all")]
		public ActionResult All() {
			User user = getCurrentUser();
			if (user != null) {
				List<FeaturedBook> featuredBooks = LibDbContext.Instance.FeaturedBooks
					.Include(fb => fb.Mark)
					.Include(fb => fb.Book)
					.Where(fb => fb.UserId == user.Id).ToList();
				ViewBag.user = user;
				ViewBag.featuredBooks = featuredBooks;
				return View();
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("one")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> One(int book_id, int mark_id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				FeaturedBook fb = LibDbContext.Instance.FeaturedBooks
					.FirstOrDefault(fb=> fb.UserId == userId && fb.BookId == book_id);
				if (fb == null) {
					var last = LibDbContext.Instance.FeaturedBooks.OrderBy(n => n.Id).LastOrDefault();
					fb = new FeaturedBook {
						Id = last != null ? (last.Id + 1) : 0,
						UserId = userId.Value,
						BookId = book_id,
						DateOfAdd = DateTime.Now,
						MarkId = mark_id
					};
					LibDbContext.Instance.FeaturedBooks.Add(fb);
				} else {
					if (mark_id == 0) {
						LibDbContext.Instance.FeaturedBooks.Remove(fb);
					} else {
						fb.MarkId = mark_id;
						fb.DateOfAdd = DateTime.Now;
						LibDbContext.Instance.FeaturedBooks.Update(fb);
					}
				}
				await LibDbContext.Instance.SaveChangesAsync();
				return Redirect($"{Url.Action("One", "Book", new { id = book_id })}");
			}
			return RedirectToAction("Login", "Auth");
		}
	}
}
