using CRM.DAL.Models;

namespace CRM.BLL.Specifications.ProjectSpecifications
{
    public class ProjectWithDetailsSpec : BaseSpecification<Project>
    {
        public ProjectWithDetailsSpec(int id)
    : base(p => p.Id == id && !p.IsDeleted)
        {
            AddInclude(p => p.Customer!);
            AddInclude(p => p.ProjectTechnologies);
            AddInclude(p => p.ProgrammersOfProject);
            AddInclude(p => p.ProjectResources);
            AddInclude(p => p.Testimonials);
        }
    }
}
