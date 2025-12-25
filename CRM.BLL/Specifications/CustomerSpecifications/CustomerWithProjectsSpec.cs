using CRM.DAL.Models;

namespace CRM.BLL.Specifications.CustomerSpecifications
{
    public class CustomerWithProjectsSpec : BaseSpecification<Customer>
    {
        public CustomerWithProjectsSpec(int id)
    : base(c => c.Id == id && !c.IsDeleted)
        {
            AddInclude(c => c.Projects);
            AddInclude(c => c.Testimonials);
        }
    }
}
