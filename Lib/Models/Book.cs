using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Book
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
            FeaturedBooks = new HashSet<FeaturedBook>();
            GenreBooks = new HashSet<GenreBook>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public double? AvgRating { get; set; }
        public string DateOfCreation { get; set; } = null!;

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
        public virtual ICollection<FeaturedBook> FeaturedBooks { get; set; }
        public virtual ICollection<GenreBook> GenreBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public string StrAvgRating {
			get {
                if (AvgRating.HasValue) {
                    return AvgRating.Value.ToString("f");
				}
                return "нет оценок";
			}
		}

        public string PhotoPath {
            get {
                if (String.IsNullOrEmpty(Photo)) {
                    return string.Empty;
                }
                return Photo;
            }
        }

        //public List<Author> GetAuthors {
        //    get {
        //        return AuthorBooks.Select(ab => ab.Author).ToList();
        //    }
        //}

        //public string StrAuthors {
        //    get {
        //        return string.Join(",", GetAuthors);

        //    }
        //}
    }
}
