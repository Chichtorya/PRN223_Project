using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class Plant
    {
        public Plant()
        {
            PlantTags = new HashSet<PlantTag>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public int? UserId { get; set; }
        public int? MilestoneId { get; set; }

        public virtual Milestone? Milestone { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<PlantTag> PlantTags { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
