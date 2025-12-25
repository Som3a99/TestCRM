using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TechnologySpecifications
{
    public class ActiveTechnologiesSpec : BaseSpecification<Technology>
    {
        public ActiveTechnologiesSpec()
    : base(t => t.IsActive && !t.IsDeleted)
        {
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
