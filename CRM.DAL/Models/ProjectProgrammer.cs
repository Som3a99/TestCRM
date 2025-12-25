using CRM.DAL.Models.Enums;

namespace CRM.DAL.Models
{
    public class ProjectProgrammer
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public ResponsibilityType ResponsibilityType { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int ProgrammerId { get; set; }
        public Programmer Programmer { get; set; } = null!;

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        #endregion
    }
}
