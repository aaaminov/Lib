﻿using DinkToPdf;
using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("user")]
	public class UserController : Controller {

		public static User getCurrentUser(HttpContext context) {
			int? userId = context.Session.GetInt32("userId");
			if (userId.HasValue) {
				User user = LibDbContext.Instance.Users
					.Include(u => u.Likes)
					.Include(u => u.Role)
					.Include(u => u.Reviews)
					.Include(u => u.Notes)
					.Include(u => u.FeaturedBooks)
					.FirstOrDefault(u => u.Id == userId);
				return user;
			}
			return null;
		}

		public static bool isCurrentUserAdmin(User user) {
			return user.RoleId == 2 ? true : false;
		}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			User user = getCurrentUser(HttpContext);
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
			User user = getCurrentUser(HttpContext);
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
		public IActionResult Update(string? message) {
			if (message != null) {
				ViewBag.message = message;
			}
			User user = getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				return View();
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("~/profile/edit")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Update(
			string login,
			string oldPassword,
			string newPassword1,
			string newPassword2,
			string name) {
			User user = getCurrentUser(HttpContext);
			if (user != null) {
				if (!newPassword1.Equals(newPassword2)) {
					return RedirectToAction("Update", new { message = "Пароли не совпадают" });
				}
				if (LibDbContext.Instance.Users.Where(u => u.Login == login && u.Id != user.Id).Any()) {
					return RedirectToAction("Update", new { message = "Пользователь с таким логином уже есть" });
				}
				if (LibDbContext.Instance.Users.Where(u => u.Id == user.Id && u.Password != oldPassword).Any()) {
					return RedirectToAction("Update", new { message = "Старый пароль неверный" });
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
	}
}
