using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("user")]
	public class UserController : Controller {

		private User getCurrentUser() {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				User user = LibDbContext.Instance.Users
					.Include(u => u.Role)
					.Include(u => u.Reviews)
					.FirstOrDefault(u => u.Id == userId);
				return user;
			}
			return null;
		}

		// GET:
		[HttpGet("")]
		[HttpGet("all")]
		public ActionResult All() {
			ViewBag.users = LibDbContext.Instance.Users.ToList();
			return View();
		}

		//// GET: мб убрать
		//[HttpGet("create")]
		//      public IActionResult Create() {
		//          ViewBag.users = LibDbContext.Instance.Users.ToList();
		//          return View();
		//      }

		//// POST: мб убрать
		//[HttpPost("create")]
		//      [ValidateAntiForgeryToken]
		//      public ActionResult Create(IFormCollection collection) {
		//          try {
		//              return RedirectToAction("ToAuthorProfile", "{id:int}", new { id = 1 });
		//          } catch {
		//              return View();
		//          }
		//      }

		//// GET: мб убрать
		//[HttpGet("{id:int}/edit")]
		//      public IActionResult Edit(int id) {
		//          ViewBag.user = LibDbContext.Instance.Users.Find(id);
		//          return View();
		//      }

		//// POST: мб убрать
		//[HttpPost("{id:int}/edit")]
		//      [ValidateAntiForgeryToken]
		//      public ActionResult Edit(int id, IFormCollection collection) {
		//          ViewBag.user = LibDbContext.Instance.Users.Find(id);
		//          try {
		//              return RedirectToAction("ToAuthorProfile", "{id:int}", new { id });
		//          } catch {
		//              return View();
		//          }
		//      }

		// GET: мб убрать
		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			User user = getCurrentUser();
			if (user?.Id == id) {
				return RedirectToAction("Profile");
			}
			user = LibDbContext.Instance.Users
					.Include(u => u.Role)
					.Include(u => u.Reviews)
					.FirstOrDefault(u => u.Id == id);
			ViewBag.user = user;
			List<FeaturedBook> featuredBooks = LibDbContext.Instance.FeaturedBooks
					.Include(fb => fb.Mark)
					.Include(fb => fb.Book)
					.Where(fb => fb.UserId == user.Id).ToList();
			ViewBag.featuredBooks = featuredBooks;
			return View();
		}




		// GET:
		[HttpGet("~/profile")]
		public IActionResult Profile() {
			User user = getCurrentUser();
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsProfile = true;
				List<FeaturedBook> featuredBooks = LibDbContext.Instance.FeaturedBooks
					.Include(fb => fb.Mark)
					.Include(fb => fb.Book)
					.Where(fb => fb.UserId == user.Id).ToList();
				ViewBag.featuredBooks = featuredBooks;
				return View("~/Views/User/One.cshtml");
			}
			return RedirectToAction("Login", "Auth");
		}

		// GET:
		[HttpGet("~/profile/edit")]
		public IActionResult EditProfile(string? message) {
			if (message != null) {
				ViewBag.message = message;
			}
			User user = getCurrentUser();
			if (user != null) {
				ViewBag.user = user;
				return View();
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("~/profile/edit")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditProfile(
			string login,
			string oldPassword,
			string newPassword1,
			string newPassword2,
			string name) {
			User user = getCurrentUser();
			if (user != null) {
				if (!newPassword1.Equals(newPassword2)) {
					return RedirectToAction("EditProfile", new { message = "Пароли не совпадают" });
				}
				if (LibDbContext.Instance.Users.Where(u => u.Login == login && u.Id != user.Id).Any()) {
					return RedirectToAction("EditProfile", new { message = "Пользователь с таким логином уже есть" });
				}
				if (LibDbContext.Instance.Users.Where(u => u.Id == user.Id && u.Password != oldPassword).Any()) {
					return RedirectToAction("EditProfile", new { message = "Старый пароль неверный" });
				}
				user.Login = login;
				user.Password = newPassword1;
				user.Name = name;

				LibDbContext.Instance.Users.Update(user);
				await LibDbContext.Instance.SaveChangesAsync();

				return RedirectToAction("Profile");
			}
			return RedirectToAction("Login", "Auth");
		}

		// GET:
		[HttpGet("~/featured")]
		public IActionResult Featured() {
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
