using CRM.BLL.DTOs.Responese;

namespace CRM.BLL.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatisticsAsync();
        Task<IEnumerable<RecentActivityDto>> GetRecentActivitiesAsync(int count);
    }
}
