using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class TestimonialResponseDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string TestimonialText { get; set; } = null!;
        public bool IsPublished { get; set; }
    }
}
