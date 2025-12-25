using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Requests
{
    public class CreateTechnologyDto
    {
        [Required]
        [MaxLength(100)]
        public string TechnologyName { get; set; } = null!;

        [MaxLength(50)]
        public string? Category { get; set; }

        public int DisplayOrder { get; set; }
    }
}
