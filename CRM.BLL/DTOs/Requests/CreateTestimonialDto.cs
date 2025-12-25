using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Requests
{
    public class CreateTestimonialDto
    {
        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; } = null!;

        [MaxLength(200)]
        public string? ClientCompany { get; set; }

        [MaxLength(100)]
        public string? ClientPosition { get; set; }

        [Required]
        [MaxLength(1000)]
        public string TestimonialText { get; set; } = null!;

        [Range(1, 5)]
        public int? Rating { get; set; }

        public int DisplayOrder { get; set; }

        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
    }
}
