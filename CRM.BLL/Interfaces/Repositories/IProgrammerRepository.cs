using CRM.DAL.Models;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface IProgrammerRepository : IGenericRepository<Programmer>
    {
        Task<IEnumerable<Programmer>> GetActiveAsync();
        Task<Programmer?> GetWithProjectsAsync(int id);
        Task<IEnumerable<Programmer>> GetByProjectAsync(int projectId);
        Task<Dictionary<int, int>> GetProjectCountPerProgrammerAsync();
    }
}
