using CRM.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.BLL.DTOs.Requests
{
    public class CreateInquiryDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string PreferredContactTime { get; set; } = null!;

        [Required]
        public ContactMethod ContactMethod { get; set; }

        [MaxLength(1000)]
        public string? Message { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? AvailableBudget { get; set; }
    }
}
