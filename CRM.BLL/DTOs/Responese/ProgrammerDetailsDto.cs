using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class ProgrammerDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string Brief { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? LinkedIn { get; set; }
        public string? Github { get; set; }
        public int? YearsOfExperience { get; set; }
        public List<ProjectCardDto> Projects { get; set; } = new();
    }
}
