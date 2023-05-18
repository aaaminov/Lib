using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lib.Controllers {
	[Route("review")]
	public class ReviewController : Controller {

		// POST:
		[HttpPost("update")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Update(int book_id, int rating, string content) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Review review = LibDbContext.Instance.Reviews
					.FirstOrDefault(r => r.UserId == userId && r.BookId == book_id);
				if (review == null) {
					var last = LibDbContext.Instance.Reviews.OrderBy(n => n.Id).LastOrDefault();
					review = new Review {
						Id = last != null ? (last.Id + 1) : 0,
						UserId = userId.Value,
						BookId = book_id,
						DateOfCreation = DateTime.Now,
						Rating = rating,
						Content = content
					};
					LibDbContext.Instance.Reviews.Add(review);
				} else {
					review.Rating = rating;
					review.Content = content;
					LibDbContext.Instance.Reviews.Update(review);
				}
				await LibDbContext.Instance.SaveChangesAsync();
				return Redirect($"{Url.Action("One", "Book", new { id = book_id })}");
			}
			return RedirectToAction("All");
		}

		// POST:
		[HttpPost("delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int book_id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Review review = LibDbContext.Instance.Reviews
					.FirstOrDefault(r => r.UserId == userId && r.BookId == book_id);
				foreach (var like in review.Likes) {
					LibDbContext.Instance.Likes.Remove(like);
				}
				LibDbContext.Instance.Reviews.Remove(review);
				await LibDbContext.Instance.SaveChangesAsync();
				return Redirect($"{Url.Action("One", "Book", new { id = book_id })}");
			}
			return RedirectToAction("All");
		}

		// POST:
		[HttpPost("like")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Like(int review_id, int book_id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				var last = LibDbContext.Instance.Likes.OrderBy(n => n.Id).LastOrDefault();
				Like like = new Like {
					Id = last != null ? (last.Id + 1) : 0,
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
					.FirstOrDefault(l => l.ReviewId == review_id && l.UserId == userId);
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
