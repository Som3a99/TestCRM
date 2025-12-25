using CRM.DAL.Models.Enums;

namespace CRM.DAL.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string PreferredContactTime { get; set; } = null!;
        public ContactMethod ContactMethod { get; set; }
        public string? Message { get; set; }
        public string? VoiceMessagePath { get; set; }
        public decimal? AvailableBudget { get; set; }
        public DateTime InquiryDate { get; set; }

        // Inquiry Management Fields
        public InquiryStatus? Status { get; set; }
        public PriorityLevel? Priority { get; set; }
        public string? ResponseNotes { get; set; }
        public DateTime? ResponseDate { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int? AssignedToProgrammerId { get; set; }
        public Programmer? AssignedToProgrammer { get; set; }

        public int? ConvertedToCustomerId { get; set; }
        public Customer? ConvertedToCustomer { get; set; }
        #endregion
    }
}
