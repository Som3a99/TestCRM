using CRM.DAL.Models;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class InquiryWithDetailsSpec : BaseSpecification<Inquiry>
    {
        public InquiryWithDetailsSpec(int id)
    : base(i => i.Id == id && !i.IsDeleted)
        {
            AddInclude(i => i.AssignedToProgrammer!);
            AddInclude(i => i.ConvertedToCustomer!);
        }
    }
}
