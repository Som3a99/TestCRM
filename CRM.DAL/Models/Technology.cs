namespace CRM.DAL.Models
{
    public class Technology
    {
        public int Id { get; set; }
        public string TechnologyName { get; set; } = null!;
        public string? IconPath { get; set; }
        public string? Category { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public ICollection<ProjectTechnology> TechnologyOfProjects { get; set; } = new HashSet<ProjectTechnology>();
        #endregion
    }
}
