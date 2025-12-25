using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProgrammerSpecifications
{
    public class ProgrammerWithProjectsSpec : BaseSpecification<Programmer>
    {
        public ProgrammerWithProjectsSpec(int id)
    : base(p => p.Id == id && !p.IsDeleted)
        {
            AddInclude(p => p.ProjectsOfProgrammer);
        }
    }
}
