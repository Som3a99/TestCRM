namespace CRM.DAL.Models
{
    public class Programmer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string Brief { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? LinkedIn { get; set; }
        public string? Github { get; set; }
        public int? YearsOfExperience { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public ICollection<ProjectProgrammer> ProjectsOfProgrammer { get; set; } = new HashSet<ProjectProgrammer>();
        public ICollection<Inquiry> AssignedInquiries { get; set; } = new HashSet<Inquiry>();
        #endregion
    }
}
