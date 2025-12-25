using CRM.DAL.Models;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface ITechnologyRepository : IGenericRepository<Technology>
    {
        Task<IEnumerable<Technology>> GetActiveAsync();
        Task<IEnumerable<Technology>> GetByCategoryAsync(string category);
        Task<IEnumerable<Technology>> GetByProjectAsync(int projectId);
        Task<bool> ExistsByNameAsync(string name);
    }
}
