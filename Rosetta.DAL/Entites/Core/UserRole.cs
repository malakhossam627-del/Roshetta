using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Core
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual AppRole Role { get; set; } = null!;
    }
}
