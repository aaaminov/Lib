using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public virtual Review Review { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
