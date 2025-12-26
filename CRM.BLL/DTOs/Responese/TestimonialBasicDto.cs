namespace CRM.BLL.DTOs.Responese
{
    public class TestimonialBasicDto
    {
        public int Id { get; set; }
        public string TestimonialText { get; set; } = null!;
        public int? Rating { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}