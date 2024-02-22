using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class TaskTag
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public int? TagId { get; set; }

        public virtual Tag? Tag { get; set; }
        public virtual Task? Task { get; set; }
    }
}
