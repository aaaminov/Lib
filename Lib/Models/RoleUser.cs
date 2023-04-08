using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public partial class RoleUser
    {
        public RoleUser()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
