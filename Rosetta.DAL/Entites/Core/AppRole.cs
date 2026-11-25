using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Core
{
    public class AppRole
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}

