using CRM.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Requests
{
    public class CreateProjectDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        [MaxLength(500)]
        public string WebsiteUrl { get; set; } = null!;

        [Required]
        public SystemType SystemType { get; set; }

        [MaxLength(100)]
        public string? DefaultUserName { get; set; }

        [MaxLength(100)]
        public string? DefaultPassword { get; set; }

        [MaxLength(50)]
        public string? ProjectDuration { get; set; }

        [MaxLength(500)]
        public string? ClientFeedback { get; set; }

        public int DisplayOrder { get; set; }

        public int? CustomerId { get; set; }

        public List<int> TechnologyIds { get; set; } = new();
        public List<int> ProgrammerIds { get; set; } = new();
    }
}
