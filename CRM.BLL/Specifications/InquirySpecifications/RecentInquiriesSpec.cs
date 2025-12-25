using CRM.DAL.Models;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class RecentInquiriesSpec : BaseSpecification<Inquiry>
    {
        public RecentInquiriesSpec(int count)
    : base(i => !i.IsDeleted)
        {
            ApplyOrderByDescending(i => i.InquiryDate);
            ApplyPaging(0, count);
        }
    }
}
