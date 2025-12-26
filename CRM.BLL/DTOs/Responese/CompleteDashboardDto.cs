namespace CRM.BLL.DTOs.Responese
{
    public class CompleteDashboardDto
    {
        public DashboardStatsDto Statistics { get; set; } = null!;
        public IEnumerable<RecentActivityDto> RecentActivities { get; set; } = new List<RecentActivityDto>();
        public DateTime GeneratedAt { get; set; }
    }
}