using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Requests
{
    public class UpdateProgrammerDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Brief { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Url]
        [MaxLength(200)]
        public string? LinkedIn { get; set; }

        [Url]
        [MaxLength(200)]
        public string? Github { get; set; }

        [Range(0, 50)]
        public int? YearsOfExperience { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

}
