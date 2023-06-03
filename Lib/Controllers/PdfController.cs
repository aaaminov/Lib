using DinkToPdf;
using DinkToPdf.Contracts;
using Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Lib.Controllers {
	public class PdfController : Controller {

		private readonly IConverter _converter;

		public PdfController(IConverter converter) {
			_converter = converter;
		}

		public IActionResult GeneratePDF(string type) {
			User user = UserController.getCurrentUser(HttpContext);
			if (user == null) {
				return RedirectToAction("Login", "Auth");
			}

			string htmlContent = "";
			switch (type) {
				case "UserStatistics": {
					htmlContent = UserStatistics();
					break;
				}
				case "TopRatedBooks": {
					htmlContent = TopRatedBooks();
					break;
				}
				case "PopularBooks": {
					htmlContent = PopularBooks();
					break;
				}
			}
			
			var html = $@"
				<!DOCTYPE html>
				<html lang=""en"">
				<head>
					<link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css""
					<link rel=""stylesheet"" href=""~/css/site.css"" asp-append-version=""true"" />
					<link rel=""stylesheet"" href=""~/Lib.styles.css"" asp-append-version=""true"" />
				</head>
				<style>
				html {{
					font-size: 1.5rem;
					font-family: sans-serif;
				}}
				.text-warning {{
					color: #ffc107;
				}}
				table, th, td {{
					border: 1px solid black;
					border-collapse: collapse;
				}}
				th, td {{
					padding: 4px 8px;
				}}
				.header {{
					margin-bottom: 4px;
					font-size: 1.75rem;
				}}
				.bold {{
					font-weight: bold;
				}}
				</style>
				<body  style=""margin: 1rem;"">
					<hr>
					{htmlContent}
					<hr>
				</body>
				</html>
          ";
			GlobalSettings globalSettings = new GlobalSettings();
			globalSettings.ColorMode = ColorMode.Color;
			globalSettings.Orientation = Orientation.Portrait;
			globalSettings.PaperSize = PaperKind.A4;
			globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };

			ObjectSettings objectSettings = new ObjectSettings();
			objectSettings.PagesCount = true;
			objectSettings.HtmlContent = html;
			WebSettings webSettings = new WebSettings();
			webSettings.DefaultEncoding = "utf-8";

			HeaderSettings headerSettings = new HeaderSettings();
			headerSettings.FontSize = 12;
			headerSettings.FontName = "sans-serif";
			headerSettings.Center = "Lib - сайт с рецензиями на книги";
			//headerSettings.Line = true;
			headerSettings.Spacing = 4;

			FooterSettings footerSettings = new FooterSettings();
			footerSettings.FontSize = 16;
			headerSettings.FontName = "sans-serif";
			footerSettings.Center = "";
			//footerSettings.Line = true;

			objectSettings.HeaderSettings = headerSettings;
			objectSettings.FooterSettings = footerSettings;
			objectSettings.WebSettings = webSettings;
			HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument() {
				GlobalSettings = globalSettings,
				Objects = { objectSettings },
			};
			var pdfFile = _converter.Convert(htmlToPdfDocument); ;
			return File(pdfFile,
			"application/octet-stream", $"{type}.pdf");
		}

		public string UserStatistics() {
			User user = UserController.getCurrentUser(HttpContext);
			ViewBag.user = user;
			ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);

			List<FeaturedBook> featuredBooks = user.FeaturedBooks.ToList();
			var readLater = featuredBooks.Where(fb => fb.MarkId == 1).Select(fb => fb.Book).ToList();
			var readNow = featuredBooks.Where(fb => fb.MarkId == 2).Select(fb => fb.Book).ToList();
			var read = featuredBooks.Where(fb => fb.MarkId == 3).Select(fb => fb.Book).ToList();
			var abandonedReading = featuredBooks.Where(fb => fb.MarkId == 4).Select(fb => fb.Book).ToList();

			string userName = user.Name;
			string userStrAdmin = UserController.isCurrentUserAdmin(user) ? "[admin]" : "";

			return $@"<div>
				<div style=""margin: 1rem auto;"">
					<div class=""header bold"">Статистика пользователя</div>
					<br />
					<div class="""">
						<span>{userName}</span>
						<span class=""text-warning"">{userStrAdmin}</span>
					</div>
				</div>

				<table class=""table mb-2"">
					<thead>
						<tr>
							<th scope=""col""></th>
							<th scope=""col"">Кол-во книг</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<th scope=""row"">Прочитать позже</th>
							<td>{readLater.Count}</td>
						</tr>
						<tr>
							<th scope=""row"">Читаю сейчас</th>
							<td>{readNow.Count}</td>
						</tr>
						<tr>
							<th scope=""row"">Прочитано</th>
							<td>{read.Count}</td>
						</tr>
						<tr>
							<th scope=""row"">Заброшено</th>
							<td>{abandonedReading.Count}</td>
						</tr>
					</tbody>
				</table>
				<br />
				<table class=""table mb-2"">
					<thead>
					<tr>
						<th scope=""col""></th>
						<th scope=""col"">Количество</th>
					</tr>
					</thead>
					<tbody>
						<tr>
							<th scope=""row"">Создано заметок</th>
							<td>{user.Notes.Count}</td>
						</tr>
						<tr>
							<th scope=""row"">Написано рецензий</th>
							<td>{user.Reviews.Count}</td>
						</tr>
					</tbody>
				</table>
				<br />
				<div class="""">{DateTime.Now.ToString("G")}</div>
			</div>";
		}

		private string TopRatedBooks() {
			var books = LibDbContext.Instance.Books
				//.Include(b => b.AuthorBooks)
				//	.ThenInclude(ab => ab.Author)
				//.Include(b => b.FeaturedBooks)
				.OrderByDescending(b => b.AvgRating)
				.Take(10).ToList();

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			var now = DateTime.Now;

			var content = $@"<div>
				<div style=""margin: 1rem auto;"">
					<div class=""header bold"">Книги с самыми высокими оценками</div>
				</div>

				<table class=""table mb-2"">
					<thead>
						<tr>
							<th scope=""col"">Название книги</th>
							<th scope=""col"">Кол-во рецензий</th>
							<th scope=""col"">Средняя оценка</th>
						</tr>
					</thead>
					<tbody>";

			foreach (var book in books) {
				if (book != null) {
					var avgRating = book.AvgRating.HasValue ? book.AvgRating.Value : 0;
					content += $@"
						<tr>
							<th scope=""row"">{book.Name}</th>
							<td>{book.Reviews.Count}</td>
							<td>{avgRating}</td>
						</tr>
					";
				}
			}
			content += $@"
					</tbody>
				</table>
				<br />
				<div class="""">{DateTime.Now.ToString("G")}</div>
			</div>
			";

			return content;
		}
		
		private string PopularBooks() {
			var books = LibDbContext.Instance.Books
				//.Include(b => b.AuthorBooks)
				//	.ThenInclude(ab => ab.Author)
				.Include(b => b.FeaturedBooks)
				.OrderByDescending(b => b.FeaturedBooks.Count)
				.Take(10).ToList();

			User user = UserController.getCurrentUser(HttpContext);
			if (user != null) {
				ViewBag.user = user;
				ViewBag.IsAdmin = UserController.isCurrentUserAdmin(user);
			}

			var now = DateTime.Now;

			var content = $@"<div>
				<div style=""margin: 1rem auto;"">
					<div class=""header bold"">Популярные книги</div>
				</div>

				<table class=""table mb-2"">
					<thead>
						<tr>
							<th scope=""col"">Название книги</th>
							<th scope=""col"">Кол-во рецензий</th>
							<th scope=""col"">Средняя оценка</th>
						</tr>
					</thead>
					<tbody>";

			foreach (var book in books) {
				if (book != null) {
					var avgRating = book.AvgRating.HasValue ? book.AvgRating.Value : 0;
					content += $@"
						<tr>
							<th scope=""row"">{book.Name}</th>
							<td>{book.Reviews.Count}</td>
							<td>{avgRating}</td>
						</tr>
					";
				}
			}
			content += $@"
					</tbody>
				</table>
				<br />
				<div class="""">{now.ToString("G")}</div>
			</div>
			";

			return content;
		}
	}
}
