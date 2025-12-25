using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class TechnologyDto
    {
        public int Id { get; set; }
        public string TechnologyName { get; set; } = null!;
        public string? IconPath { get; set; }
        public string? Category { get; set; }
    }
}
