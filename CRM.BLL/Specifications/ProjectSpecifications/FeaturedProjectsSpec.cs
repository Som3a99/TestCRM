using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProjectSpecifications
{
    public class FeaturedProjectsSpec : BaseSpecification<Project>
    {
        public FeaturedProjectsSpec(int count)
    : base(p => p.IsPublished && !p.IsDeleted)
        {
            ApplyOrderByDescending(p => p.ViewCount);
            ApplyPaging(0, count);
        }
    }
}
