using CRM.BLL.Common;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get dashboard statistics
        /// </summary>
        [HttpGet("statistics")]
        public async Task<ActionResult<ApiResponse<DashboardStatsDto>>> GetStatistics()
        {
            try
            {
                var stats = await _dashboardService.GetStatisticsAsync();
                return Ok(ApiResponse<DashboardStatsDto>.Success(
                    stats,
                    "Dashboard statistics retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<DashboardStatsDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get recent activities
        /// </summary>
        [HttpGet("activities")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RecentActivityDto>>>> GetRecentActivities(
            [FromQuery] int count = 10)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    return BadRequest(ApiResponse<IEnumerable<RecentActivityDto>>.Failure(
                        "Activity count must be between 1 and 100"));
                }

                var activities = await _dashboardService.GetRecentActivitiesAsync(count);
                return Ok(ApiResponse<IEnumerable<RecentActivityDto>>.Success(
                    activities,
                    "Recent activities retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<RecentActivityDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get complete dashboard data (statistics + activities)
        /// </summary>
        [HttpGet("complete")]
        public async Task<ActionResult<ApiResponse<CompleteDashboardDto>>> GetCompleteDashboard(
            [FromQuery] int activityCount = 10)
        {
            try
            {
                if (activityCount <= 0 || activityCount > 100)
                {
                    return BadRequest(ApiResponse<CompleteDashboardDto>.Failure(
                        "Activity count must be between 1 and 100"));
                }

                var stats = await _dashboardService.GetStatisticsAsync();
                var activities = await _dashboardService.GetRecentActivitiesAsync(activityCount);

                var dashboardData = new CompleteDashboardDto
                {
                    Statistics = stats,
                    RecentActivities = activities,
                    GeneratedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<CompleteDashboardDto>.Success(
                    dashboardData,
                    "Dashboard data retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CompleteDashboardDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get dashboard summary (quick overview)
        /// </summary>
        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<DashboardSummaryDto>>> GetSummary()
        {
            try
            {
                var stats = await _dashboardService.GetStatisticsAsync();

                var summary = new DashboardSummaryDto
                {
                    TotalProjects = stats.TotalProjects,
                    PublishedProjects = stats.PublishedProjects,
                    TotalInquiries = stats.TotalInquiries,
                    NewInquiries = stats.NewInquiries,
                    TotalCustomers = stats.TotalCustomers,
                    AverageRating = stats.AverageRating,
                    LastUpdated = DateTime.UtcNow
                };

                return Ok(ApiResponse<DashboardSummaryDto>.Success(
                    summary,
                    "Dashboard summary retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<DashboardSummaryDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}