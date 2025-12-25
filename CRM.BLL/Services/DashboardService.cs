using AutoMapper;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Services;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DashboardStatsDto> GetStatisticsAsync()
        {
            var allProjects = await _unitOfWork.Projects.GetAllAsync();
            var publishedProjects = allProjects.Count(p => p.IsPublished && !p.IsDeleted);
            var totalProjects = allProjects.Count(p => !p.IsDeleted);

            var allInquiries = await _unitOfWork.Inquiries.GetAllAsync();
            var inquiriesList = allInquiries.Where(i => !i.IsDeleted).ToList();
            var newInquiries = inquiriesList.Count(i => i.Status == InquiryStatus.New);

            var allCustomers = await _unitOfWork.Customers.GetAllAsync();
            var totalCustomers = allCustomers.Count(c => !c.IsDeleted);

            var allTestimonials = await _unitOfWork.Testimonials.GetAllAsync();
            var totalTestimonials = allTestimonials.Count(t => !t.IsDeleted);

            var avgRating = await _unitOfWork.Testimonials.GetAverageRatingAsync();

            var projectsByType = await _unitOfWork.Projects.GetCountByTypeAsync();
            var inquiriesByStatus = await _unitOfWork.Inquiries.GetCountByStatusAsync();

            return new DashboardStatsDto
            {
                TotalProjects = totalProjects,
                PublishedProjects = publishedProjects,
                TotalInquiries = inquiriesList.Count,
                NewInquiries = newInquiries,
                TotalCustomers = totalCustomers,
                TotalTestimonials = totalTestimonials,
                AverageRating = Math.Round(avgRating, 2),
                ProjectsByType = projectsByType.ToDictionary(
                    kvp => GetSystemTypeNameAr(kvp.Key),
                    kvp => kvp.Value
                ),
                InquiriesByStatus = inquiriesByStatus.ToDictionary(
                    kvp => GetInquiryStatusNameAr(kvp.Key),
                    kvp => kvp.Value
                )
            };
        }

        public async Task<IEnumerable<RecentActivityDto>> GetRecentActivitiesAsync(int count)
        {
            var activities = new List<RecentActivityDto>();

            // Get recent inquiries
            var recentInquiries = await _unitOfWork.Inquiries.GetRecentAsync(count / 2);
            activities.AddRange(recentInquiries.Select(i => new RecentActivityDto
            {
                ActivityType = "استفسار جديد",
                Description = $"استفسار من {i.Name}",
                Timestamp = i.InquiryDate
            }));

            // Get recent projects
            var recentProjects = (await _unitOfWork.Projects.GetAllAsync())
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count / 2)
                .Select(p => new RecentActivityDto
                {
                    ActivityType = "مشروع جديد",
                    Description = $"تم إضافة مشروع: {p.Name}",
                    Timestamp = p.CreatedAt
                });

            activities.AddRange(recentProjects);

            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(count);
        }

        private string GetSystemTypeNameAr(SystemType type) => type switch
        {
            SystemType.ECommerce => "متجر إلكتروني",
            SystemType.ERPSystem => "نظام ERP",
            SystemType.CRMSystem => "نظام CRM",
            SystemType.LandingPage => "صفحة هبوط",
            _ => type.ToString()
        };

        private string GetInquiryStatusNameAr(InquiryStatus status) => status switch
        {
            InquiryStatus.New => "جديد",
            InquiryStatus.InProgress => "قيد المعالجة",
            InquiryStatus.Responded => "تم الرد",
            InquiryStatus.Converted => "تم التحويل",
            InquiryStatus.Rejected => "مرفوض",
            _ => status.ToString()
        };
    }
}
