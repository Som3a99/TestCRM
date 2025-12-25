using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class TestimonialDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string? ClientCompany { get; set; }
        public string? ClientPosition { get; set; }
        public string TestimonialText { get; set; } = null!;
        public int? Rating { get; set; }
        public string? ClientImagePath { get; set; }
        public int DisplayOrder { get; set; }
    }
}
