using CRM.DAL.Models.Enums;

namespace CRM.DAL.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string? IconPath { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public SystemType? RelatedSystemType { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
