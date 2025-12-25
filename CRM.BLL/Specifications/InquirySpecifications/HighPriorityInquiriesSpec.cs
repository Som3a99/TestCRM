using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class HighPriorityInquiriesSpec : BaseSpecification<Inquiry>
    {
        public HighPriorityInquiriesSpec()
    : base(i => (i.Priority == PriorityLevel.High || i.Priority == PriorityLevel.Urgent)
        && i.Status != InquiryStatus.Converted
        && i.Status != InquiryStatus.Rejected
        && !i.IsDeleted)
        {
            ApplyOrderBy(i => i.Priority ?? PriorityLevel.Medium);
            ApplyOrderByDescending(i => i.InquiryDate);
        }
    }
}
