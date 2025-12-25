using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class InquiryByPrioritySpec : BaseSpecification<Inquiry>
    {
        public InquiryByPrioritySpec(PriorityLevel priority)
    : base(i => i.Priority == priority && !i.IsDeleted)
        {
            ApplyOrderByDescending(i => i.InquiryDate);
        }
    }
}
