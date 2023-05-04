using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class AuthorBook
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;

    }
}
