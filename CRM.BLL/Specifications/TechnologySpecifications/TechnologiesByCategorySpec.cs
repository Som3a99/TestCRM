using CRM.DAL.Models;

namespace CRM.BLL.Specifications.TechnologySpecifications
{
    public class TechnologiesByCategorySpec : BaseSpecification<Technology>
    {
        public TechnologiesByCategorySpec(string category)
    : base(t => t.Category == category && t.IsActive && !t.IsDeleted)
        {
            ApplyOrderBy(t => t.DisplayOrder);
        }
    }
}
