using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class InquiryByStatusSpec : BaseSpecification<Inquiry>
    {
        public InquiryByStatusSpec(InquiryStatus status)
    : base(i => i.Status == status && !i.IsDeleted)
        {
            ApplyOrderByDescending(i => i.InquiryDate);
        }
    }
}
