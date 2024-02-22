using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class Milestone
    {
        public Milestone()
        {
            Plants = new HashSet<Plant>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
