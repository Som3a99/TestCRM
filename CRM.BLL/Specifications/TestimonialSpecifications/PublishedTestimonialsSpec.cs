using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TestimonialSpecifications
{
    public class PublishedTestimonialsSpec : BaseSpecification<Testimonial>
    {
        public PublishedTestimonialsSpec()
    : base(t => t.IsPublished && !t.IsDeleted)
        {
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
