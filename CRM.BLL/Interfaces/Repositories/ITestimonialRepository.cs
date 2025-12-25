using CRM.DAL.Models;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface ITestimonialRepository : IGenericRepository<Testimonial>
    {
        Task<IEnumerable<Testimonial>> GetPublishedAsync();
        Task<IEnumerable<Testimonial>> GetByRatingAsync(int minRating);
        Task<IEnumerable<Testimonial>> GetByProjectAsync(int projectId);
        Task<IEnumerable<Testimonial>> GetByCustomerAsync(int customerId);
        Task<double> GetAverageRatingAsync();
    }
}
