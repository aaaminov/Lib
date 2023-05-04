using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }

        public virtual User User { get; set; } = null!;

        public string StrDateOfCreation {
            get {
                return DateOfCreation.ToString("G");
            }
        }
    }
}
