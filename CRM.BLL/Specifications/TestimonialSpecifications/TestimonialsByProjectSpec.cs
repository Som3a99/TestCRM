using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TestimonialSpecifications
{
    public class TestimonialsByProjectSpec : BaseSpecification<Testimonial>
    {
        public TestimonialsByProjectSpec(int projectId)
    : base(t => t.ProjectId == projectId && t.IsPublished && !t.IsDeleted)
        {
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
