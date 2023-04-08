using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Mark
    {
        public Mark()
        {
            FeaturedBooks = new HashSet<FeaturedBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<FeaturedBook> FeaturedBooks { get; set; }
    }
}
