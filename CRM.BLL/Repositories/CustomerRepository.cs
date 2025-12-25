using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerWithProjectsAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Projects)
                .Include(c => c.Testimonials)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithTestimonialsAsync()
        {
            return await _context.Customers
                .Include(c => c.Testimonials)
                .Where(c => c.Testimonials.Any() && !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Customers
                .AnyAsync(c => c.Email == email && !c.IsDeleted);
        }

        public async Task<bool> ExistsByPhoneAsync(string phoneNumber)
        {
            return await _context.Customers
                .AnyAsync(c => c.PhoneNumber == phoneNumber && !c.IsDeleted);
        }
    }
}
