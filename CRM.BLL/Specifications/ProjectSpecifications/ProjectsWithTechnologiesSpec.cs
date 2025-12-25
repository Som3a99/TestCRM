using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProjectSpecifications
{
    public class ProjectsWithTechnologiesSpec : BaseSpecification<Project>
    {
        public ProjectsWithTechnologiesSpec()
    : base(p => p.IsPublished && !p.IsDeleted)
        {
            AddInclude(p => p.ProjectTechnologies);
            ApplyOrderBy(p => p.DisplayOrder);
        }
    }
}
