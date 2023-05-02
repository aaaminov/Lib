﻿using Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lib.Controllers {
	[Route("note")]
	public class NoteController : Controller {

		private Note getNoteByIdFromCurrentUser(int noteId, int userId) {
			return LibDbContext.Instance.Notes.FirstOrDefault(n => n.Id == noteId && n.UserId == userId);
		}

		// GET:
		[HttpGet("")]
		[HttpGet("all")]
		public ActionResult All() {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				ViewBag.notes = LibDbContext.Instance.Notes
					.Include(n => n.User)
					.Where(n => n.UserId == userId).ToList();
				return View();
			}
			return RedirectToAction("Login", "Auth");
		}

		// POST:
		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(IFormCollection collection) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Note note = new Note {
					Id = LibDbContext.Instance.Notes.OrderBy(n => n.Id).Last().Id + 1,
					UserId = userId.Value,
					DateOfCreation = DateTime.Now,
					Name = "Без названия"
				};
				LibDbContext.Instance.Notes.Add(note);
				await LibDbContext.Instance.SaveChangesAsync();
				return RedirectToAction("One", new { note.Id });
			}
			return RedirectToAction("All");
		}

		// POST:
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Note note = getNoteByIdFromCurrentUser(id, userId.Value);
				if (note != null) {
					LibDbContext.Instance.Notes.Remove(note);
					await LibDbContext.Instance.SaveChangesAsync();
				}
			}
			return RedirectToAction("All");
		}

		// POST:
		[HttpPost("{id:int}/edit")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, string newName, string newContent) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Note note = getNoteByIdFromCurrentUser(id, userId.Value);
				if (note != null) {
					if (newName == null) {
						newName = "Без названия";
					}
					note.Name = newName;
					note.Content = newContent;
					LibDbContext.Instance.Notes.Update(note);
					await LibDbContext.Instance.SaveChangesAsync();
				}
			}
			return RedirectToAction("One", new { id });
		}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Note note = getNoteByIdFromCurrentUser(id, userId.Value);
				if (note != null) {
					ViewBag.note = note;
					return View();
				}
			}
			return RedirectToAction("All");
		}
	}
}