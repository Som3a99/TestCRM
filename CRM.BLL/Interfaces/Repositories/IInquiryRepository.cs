using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface IInquiryRepository : IGenericRepository<Inquiry>
    {
        Task<IEnumerable<Inquiry>> GetByStatusAsync(InquiryStatus status);
        Task<IEnumerable<Inquiry>> GetUnassignedAsync();
        Task<IEnumerable<Inquiry>> GetByProgrammerAsync(int programmerId);
        Task<IEnumerable<Inquiry>> GetHighPriorityAsync();
        Task<IEnumerable<Inquiry>> GetRecentAsync(int count);
        Task<Dictionary<InquiryStatus, int>> GetCountByStatusAsync();
    }
}
