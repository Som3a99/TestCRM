using CRM.DAL.Models.Enums;

namespace CRM.BLL.DTOs.Responese
{
    public class UpdateInquiryStatusDto
    {
        public InquiryStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}