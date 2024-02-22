using System;
using System.Collections.Generic;

namespace ProjectPRN231.Models
{
    public partial class PlantTag
    {
        public int Id { get; set; }
        public int? PlantId { get; set; }
        public int? TagId { get; set; }

        public virtual Plant? Plant { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
