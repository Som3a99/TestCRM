namespace CRM.DAL.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string? ClientCompany { get; set; }
        public string? ClientPosition { get; set; }
        public string TestimonialText { get; set; } = null!;
        public int? Rating { get; set; }
        public string? ClientImagePath { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublished { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        #endregion
    }
}
