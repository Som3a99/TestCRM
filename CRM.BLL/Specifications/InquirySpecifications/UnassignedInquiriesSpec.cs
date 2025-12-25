using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class UnassignedInquiriesSpec : BaseSpecification<Inquiry>
    {
        public UnassignedInquiriesSpec()
    : base(i => i.AssignedToProgrammerId == null && !i.IsDeleted)
        {
            ApplyOrderBy(i => i.Priority ?? PriorityLevel.Low);
            ApplyOrderByDescending(i => i.InquiryDate);
        }
    }
}
