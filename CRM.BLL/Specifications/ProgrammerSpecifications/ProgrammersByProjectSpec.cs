using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProgrammerSpecifications
{
    public class ProgrammersByProjectSpec : BaseSpecification<Programmer>
    {
        public ProgrammersByProjectSpec(int projectId)
    : base(p => p.ProjectsOfProgrammer.Any(pp => pp.ProjectId == projectId) && !p.IsDeleted)
        {
            ApplyOrderBy(p => p.DisplayOrder);
        }
    }
}
