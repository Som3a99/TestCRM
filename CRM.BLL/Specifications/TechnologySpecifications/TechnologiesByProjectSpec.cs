using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TechnologySpecifications
{
    public class TechnologiesByProjectSpec : BaseSpecification<Technology>
    {
        public TechnologiesByProjectSpec(int projectId)
    : base(t => t.TechnologyOfProjects.Any(tp => tp.ProjectId == projectId) && !t.IsDeleted)
        {
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
