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
                return null;
			}
		}
	}
}
