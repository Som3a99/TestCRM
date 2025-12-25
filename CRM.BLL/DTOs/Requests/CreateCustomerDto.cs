using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Requests
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? CompanyName { get; set; }

        public bool AllowShowcase { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}
