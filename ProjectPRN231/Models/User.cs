using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class User
    {
        public User()
        {
            Attachments = new HashSet<Attachment>();
            Comments = new HashSet<Comment>();
            Plants = new HashSet<Plant>();
            Tasks = new HashSet<Task>();
            UserSettings = new HashSet<UserSetting>();
        }

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
