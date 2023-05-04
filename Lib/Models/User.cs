using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class User
    {
        public User()
        {
            FeaturedBooks = new HashSet<FeaturedBook>();
            Likes = new HashSet<Like>();
            Notes = new HashSet<Note>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime DateOfRegistration { get; set; }
        public int RoleId { get; set; }

        public virtual RoleUser Role { get; set; } = null!;
        public virtual ICollection<FeaturedBook> FeaturedBooks { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
