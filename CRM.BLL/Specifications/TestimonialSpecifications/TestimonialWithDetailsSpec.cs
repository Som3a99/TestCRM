using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TestimonialSpecifications
{
    public class TestimonialWithDetailsSpec : BaseSpecification<Testimonial>
    {
        public TestimonialWithDetailsSpec(int id)
    : base(t => t.Id == id && !t.IsDeleted)
        {
            AddInclude(t => t.Customer!);
            AddInclude(t => t.Project!);
        }
    }
}
