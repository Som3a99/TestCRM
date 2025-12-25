using CRM.DAL.Models.Enums;

namespace CRM.DAL.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string ThumbnailImagePath { get; set; } = null!;
        public string? DefaultUserName { get; set; }
        public string? DefaultPassword { get; set; }
        public SystemType SystemType { get; set; }
        public DateOnly? CompletionDate { get; set; }
        public string? ProjectDuration { get; set; }
        public string? ClientFeedback { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublished { get; set; }
        public int ViewCount { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<ProjectProgrammer> ProgrammersOfProject { get; set; } = new HashSet<ProjectProgrammer>();
        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = new HashSet<ProjectTechnology>();
        public ICollection<ProjectResource> ProjectResources { get; set; } = new HashSet<ProjectResource>();
        public ICollection<Testimonial> Testimonials { get; set; } = new HashSet<Testimonial>();
        #endregion
    }
}
