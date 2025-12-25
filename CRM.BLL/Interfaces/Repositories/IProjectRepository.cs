using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetPublishedAsync();
        Task<IEnumerable<Project>> GetBySystemTypeAsync(SystemType systemType);
        Task<Project?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Project>> GetFeaturedAsync(int count);
        Task IncrementViewCountAsync(int id);
        Task<Dictionary<SystemType, int>> GetCountByTypeAsync();
    }
}
