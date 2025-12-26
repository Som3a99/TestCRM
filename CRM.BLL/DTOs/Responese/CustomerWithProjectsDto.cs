namespace CRM.BLL.DTOs.Responese
{
    public class CustomerWithProjectsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        public DateOnly JoinDate { get; set; }
        public bool AllowShowcase { get; set; }
        public string? Notes { get; set; }
        public List<ProjectBasicDto> Projects { get; set; } = new();
        public List<TestimonialBasicDto> Testimonials { get; set; } = new();
    }
}
