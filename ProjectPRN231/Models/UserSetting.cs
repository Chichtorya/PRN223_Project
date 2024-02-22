using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class UserSetting
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? DarkMode { get; set; }
        public bool? EmailPopup { get; set; }
        public bool? Popup { get; set; }

        public virtual User? User { get; set; }
    }
}
