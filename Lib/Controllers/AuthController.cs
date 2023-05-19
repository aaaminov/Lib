using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lib.Controllers {
	public class AuthController : Controller {

		[HttpGet("login")]
		public ActionResult Login(string? message) {
			if (message != null) {
				ViewBag.message = message;
			}
			return View();
		}

		// POST:
		[HttpPost("login")]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string login, string password) {
			User user = LibDbContext.Instance.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
			if (user != null) {
				HttpContext.Session.SetInt32("userId", user.Id);
				Console.WriteLine(HttpContext.Session.GetInt32("userId"));
				return RedirectToAction("Index", "Home", new { welcome = true });
			}
			return RedirectToAction("Login", new { message = "Неверные данные для входа" });
		}

		// POST:
		[HttpPost("logout")]
		[ValidateAntiForgeryToken]
		public ActionResult Logout() {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				HttpContext.Session.Remove("userId");
			}
			return RedirectToAction("Login", new { message = "Произведен выход из аккаунта" });
		}

		[HttpGet("register")]
		public ActionResult Register(string? message) {
			if (message != null) {
				ViewBag.message = message;
			}
			return View();
		}

		// POST:
		[HttpPost("register")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(
			string login,
			string password1,
			string password2,
			string name) {
			if (!password1.Equals(password2)) {
				return RedirectToAction("Register", new { message = "Пароли не совпадают" });
			}
			if (LibDbContext.Instance.Users.Where(u => u.Login == login).Any()) {
				return RedirectToAction("Register", new { message = "Пользователь с таким логином уже есть" });
			}
			User user = new User {
				Id = LibDbContext.Instance.Users.OrderBy(u => u.Id).Last().Id + 1,
				Login = login,
				Password = password1,
				Name = name,
				DateOfRegistration = DateTime.Now,
				RoleId = 1
			};
			LibDbContext.Instance.Users.Add(user);
			await LibDbContext.Instance.SaveChangesAsync();

			HttpContext.Session.SetInt32("userId", user.Id);
			Console.WriteLine(HttpContext.Session.GetInt32("userId"));
			return RedirectToAction("Index", "Home");
		}

		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error() {
		//    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}
	}
}