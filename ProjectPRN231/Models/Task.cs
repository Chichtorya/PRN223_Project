using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class Task
    {
        public Task()
        {
            Attachments = new HashSet<Attachment>();
            Comments = new HashSet<Comment>();
            InverseTaskParent = new HashSet<Task>();
            TaskTags = new HashSet<TaskTag>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? PlantId { get; set; }
        public int? MilestoneId { get; set; }
        public int? TaskParentId { get; set; }
        public int? UserId { get; set; }

        public virtual Milestone? Milestone { get; set; }
        public virtual Plant? Plant { get; set; }
        public virtual Task? TaskParent { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Task> InverseTaskParent { get; set; }
        public virtual ICollection<TaskTag> TaskTags { get; set; }
    }
}
