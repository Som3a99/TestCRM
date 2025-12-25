using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProjectSpecifications
{
    public class PublishedProjectsSpec : BaseSpecification<Project>
    {
        public PublishedProjectsSpec()
    : base(p => p.IsPublished && !p.IsDeleted)
        {
            ApplyOrderBy(p => p.DisplayOrder);
        }
    }
}
