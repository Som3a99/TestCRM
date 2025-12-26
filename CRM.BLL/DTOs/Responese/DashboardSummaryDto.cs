namespace CRM.BLL.DTOs.Responese
{
    public class DashboardSummaryDto
    {
        public int TotalProjects { get; set; }
        public int PublishedProjects { get; set; }
        public int TotalInquiries { get; set; }
        public int NewInquiries { get; set; }
        public int TotalCustomers { get; set; }
        public double AverageRating { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}