using CRM.DAL.Models.Enums;

namespace CRM.DAL.Models
{
    public class ProjectResource
    {
        public int Id { get; set; }
        public string ItemPath { get; set; } = null!;
        public ResourceType ResourceType { get; set; }
        public string? Title { get; set; }
        public int DisplayOrder { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        #endregion
    }
}
