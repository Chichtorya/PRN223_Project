using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public string? FilePath { get; set; }
        public int? UploadedBy { get; set; }
        public DateTime? UploadedAt { get; set; }

        public virtual Task? Task { get; set; }
        public virtual User? UploadedByNavigation { get; set; }
    }
}
