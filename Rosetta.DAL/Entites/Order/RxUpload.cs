using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Order
{
    public class RxUpload
    {
        public int RxUploadId { get; set; }
        public int OrderId { get; set; }
        public int UploadedByUserId { get; set; }
        public string FileReference { get; set; } = string.Empty;
        public string? ReviewStatus { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public virtual CustomerOrder Order { get; set; } = null!;
        public virtual AppUser UploadedByUser { get; set; } = null!;
    }
}
