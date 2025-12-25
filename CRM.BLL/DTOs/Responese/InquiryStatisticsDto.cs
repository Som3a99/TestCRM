using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class InquiryStatisticsDto
    {
        public int TotalInquiries { get; set; }
        public int NewInquiries { get; set; }
        public int InProgressInquiries { get; set; }
        public int ConvertedInquiries { get; set; }
        public double ConversionRate { get; set; }
        public Dictionary<string, int> InquiriesByStatus { get; set; } = new();
    }
}
