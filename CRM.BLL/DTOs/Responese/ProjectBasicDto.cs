namespace CRM.BLL.DTOs.Responese
{
    public class ProjectBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public int SystemType { get; set; }
        public bool IsPublished { get; set; }
        public DateOnly? CompletionDate { get; set; }
    }
}