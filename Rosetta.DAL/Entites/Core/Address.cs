using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Core
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Building { get; set; }
        public bool IsDefault { get; set; }

        public virtual AppUser User { get; set; } = null!;
    }
}
