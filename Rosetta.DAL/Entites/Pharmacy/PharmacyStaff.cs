using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Pharmacy
{
    public class PharmacyStaff
    {
        public int PharmacyId { get; set; }
        public int UserId { get; set; }
        public string? StaffRole { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual PharmacyModel Pharmacy { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
