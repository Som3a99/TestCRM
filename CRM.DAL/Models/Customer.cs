namespace CRM.DAL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        public DateOnly JoinDate { get; set; }
        public bool AllowShowcase { get; set; }
        public string? Notes { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public ICollection<Inquiry> Inquiries { get; set; } = new HashSet<Inquiry>();
        public ICollection<Testimonial> Testimonials { get; set; } = new HashSet<Testimonial>();
        #endregion
    }
}
