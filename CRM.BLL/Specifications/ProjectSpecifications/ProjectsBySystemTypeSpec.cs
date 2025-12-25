using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Specifications.ProjectSpecifications
{
    public class ProjectsBySystemTypeSpec : BaseSpecification<Project>
    {
        public ProjectsBySystemTypeSpec(SystemType systemType)
    : base(p => p.SystemType == systemType && p.IsPublished && !p.IsDeleted)
        {
            ApplyOrderBy(p => p.DisplayOrder);
        }
    }
}
