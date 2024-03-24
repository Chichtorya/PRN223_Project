namespace ProjectPRN231.DTO
{
    public class PlanDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public int? MilestoneId { get; set; }
        public string? MilestoneName { get; set; }

        public string?  customMilestoneCheckbox { get; set; }
        public int? tag { get; set; }
         public string? tagName { get; set; }
        public DateTime? StartDate { get; set; } // Thay đổi kiểu dữ liệu
        public DateTime? EndDate { get; set; } // Thay đổi kiểu dữ liệu
    }
}
