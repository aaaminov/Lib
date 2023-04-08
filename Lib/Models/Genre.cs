using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Genre
    {
        public Genre()
        {
            GenreBooks = new HashSet<GenreBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<GenreBook> GenreBooks { get; set; }
    }
}
