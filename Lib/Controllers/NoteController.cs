using Lib.Models;
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
				List<Note> notes = LibDbContext.Instance.Notes
					.Include(n => n.User)
					.Where(n => n.UserId == userId).ToList();
				notes.Reverse();
				ViewBag.notes = notes;
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
				var last = LibDbContext.Instance.Notes.OrderBy(n => n.Id).LastOrDefault();
				Note note = new Note {
					Id = last != null ? (last.Id + 1) : 0,
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
			//return RedirectToAction("One", new { id });
			return Redirect($"{Url.Action("One", "Note", new { id = id, message = "Сохранено" })}");
		}

		// GET:
		[HttpGet("{id:int}")]
		public IActionResult One(int id, string? message = null) {
			int? userId = HttpContext.Session.GetInt32("userId");
			if (userId.HasValue) {
				Note note = getNoteByIdFromCurrentUser(id, userId.Value);
				if (note != null) {
					ViewBag.note = note;
					ViewBag.message = message;
					return View();
				}
			}
			return RedirectToAction("All");
		}
	}
}
