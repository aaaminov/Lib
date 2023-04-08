using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class FeaturedBook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateOfAdd { get; set; }
        public int MarkId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Mark Mark { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
