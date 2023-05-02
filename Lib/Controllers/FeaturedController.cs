using Lib.Models;
using Microsoft.AspNetCore.Http;
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

	}
}
