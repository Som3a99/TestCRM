using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class RecentActivityDto
    {
        public string ActivityType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Timestamp { get; set; }
    }
}
