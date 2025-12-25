using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProgrammerSpecifications
{
    public class ActiveProgrammersSpec : BaseSpecification<Programmer>
    {
        public ActiveProgrammersSpec()
    : base(p => p.IsActive && !p.IsDeleted)
        {
            ApplyOrderBy(p => p.DisplayOrder);
        }
    }
}
