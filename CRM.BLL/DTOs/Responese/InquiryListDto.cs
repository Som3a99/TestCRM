using CRM.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class InquiryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime InquiryDate { get; set; }
        public InquiryStatus? Status { get; set; }
        public string StatusNameAr { get; set; } = null!;
        public string StatusNameEn { get; set; } = null!;
        public PriorityLevel? Priority { get; set; }
        public string? AssignedToProgrammerName { get; set; }
        public decimal? AvailableBudget { get; set; }
    }
}
