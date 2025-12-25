using CRM.DAL.Models;

namespace CRM.BLL.Specifications.InquirySpecifications
{
    public class InquiriesByProgrammerSpec : BaseSpecification<Inquiry>
    {
        public InquiriesByProgrammerSpec(int programmerId)
    : base(i => i.AssignedToProgrammerId == programmerId && !i.IsDeleted)
        {
            AddInclude(i => i.AssignedToProgrammer!);
            ApplyOrderByDescending(i => i.InquiryDate);
        }
    }
}
