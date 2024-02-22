using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PlantTags = new HashSet<PlantTag>();
            TaskTags = new HashSet<TaskTag>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<PlantTag> PlantTags { get; set; }
        public virtual ICollection<TaskTag> TaskTags { get; set; }
    }
}
