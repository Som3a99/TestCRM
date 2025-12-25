using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TestimonialSpecifications
{
    public class TestimonialsByCustomerSpec : BaseSpecification<Testimonial>
    {
        public TestimonialsByCustomerSpec(int customerId)
    : base(t => t.CustomerId == customerId && !t.IsDeleted)
        {
            ApplyOrderByDescending(t => t.CreatedAt);
        }
    }
}
