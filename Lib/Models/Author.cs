using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Author
    {
        public Author()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public string? Biography { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
