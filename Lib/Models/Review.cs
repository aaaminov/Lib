using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;

		public string StrDateOfCreation {
			get {
				return DateOfCreation.ToString("G");
			}
		}
	}
}
