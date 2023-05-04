using Lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lib.Controllers {
	[Route("review")]
	public class ReviewController : Controller {

		// POST:
		[HttpPost("like")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Like(int review_id, int book_id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Like like = new Like {
					Id = LibDbContext.Instance.Likes.OrderBy(n => n.Id).Last().Id + 1,
					ReviewId = review_id,
					UserId = userId.Value,
					Date = DateTime.Now
				};
				LibDbContext.Instance.Likes.Add(like);
				await LibDbContext.Instance.SaveChangesAsync();
				return Redirect($"{Url.Action("One", "Book", new { id = book_id })}#{review_id}");
				//return RedirectToAction("One", "Book", new { id = book_id });
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("unlike")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Unlike(int review_id, int book_id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Like like = LibDbContext.Instance.Likes
					.FirstOrDefault(l=> l.ReviewId == review_id && l.UserId == userId);
				if (like != null) {
					LibDbContext.Instance.Likes.Remove(like);
					await LibDbContext.Instance.SaveChangesAsync();
					return Redirect($"{Url.Action("One", "Book", new { id = book_id })}#{review_id}");
					//return RedirectToAction("One", "Book", new { id = book_id });
				}
			}
			return RedirectToAction("Login", "Auth");
		}

	}
}
