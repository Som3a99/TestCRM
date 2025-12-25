using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Testimonial>> GetPublishedAsync()
        {
            return await _context.Testimonials
                .Include(t => t.Customer)
                .Include(t => t.Project)
                .Where(t => t.IsPublished && !t.IsDeleted)
                .OrderBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Testimonial>> GetByRatingAsync(int minRating)
        {
            return await _context.Testimonials
                .Include(t => t.Customer)
                .Where(t => t.Rating >= minRating && t.IsPublished && !t.IsDeleted)
                .OrderByDescending(t => t.Rating)
                .ThenBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Testimonial>> GetByProjectAsync(int projectId)
        {
            return await _context.Testimonials
                .Include(t => t.Customer)
                .Where(t => t.ProjectId == projectId && t.IsPublished && !t.IsDeleted)
                .OrderBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Testimonial>> GetByCustomerAsync(int customerId)
        {
            return await _context.Testimonials
                .Include(t => t.Project)
                .Where(t => t.CustomerId == customerId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync()
        {
            var ratings = await _context.Testimonials
                .Where(t => t.Rating.HasValue && t.IsPublished && !t.IsDeleted)
                .Select(t => t.Rating!.Value)
                .ToListAsync();

            return ratings.Any() ? ratings.Average() : 0;
        }
    }
}

