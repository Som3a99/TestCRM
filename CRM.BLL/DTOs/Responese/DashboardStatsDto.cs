using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class DashboardStatsDto
    {
        public int TotalProjects { get; set; }
        public int PublishedProjects { get; set; }
        public int TotalInquiries { get; set; }
        public int NewInquiries { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalTestimonials { get; set; }
        public double AverageRating { get; set; }
        public Dictionary<string, int> ProjectsByType { get; set; } = new();
        public Dictionary<string, int> InquiriesByStatus { get; set; } = new();
    }
}
