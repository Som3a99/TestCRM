using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TestimonialSpecifications
{
    public class TestimonialsByRatingSpec : BaseSpecification<Testimonial>
    {
        public TestimonialsByRatingSpec(int minRating)
    : base(t => t.Rating >= minRating && t.IsPublished && !t.IsDeleted)
        {
            ApplyOrderByDescending(t => t.Rating ?? 0);
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
