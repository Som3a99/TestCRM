using CRM.DAL.Models;

namespace CRM.BLL.Specifications.CustomerSpecifications
{
    public class CustomersWithTestimonialsSpec : BaseSpecification<Customer>
    {
        public CustomersWithTestimonialsSpec()
    : base(c => c.Testimonials.Any() && !c.IsDeleted)
        {
            AddInclude(c => c.Testimonials);
        }
    }
}
