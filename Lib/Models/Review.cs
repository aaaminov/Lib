using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Review
    {
        public Review()
        {
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }

        public string StrDateOfCreation {
            get {
                return DateOfCreation.ToString("G");
            }
        }
    }
}
