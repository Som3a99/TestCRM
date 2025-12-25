using CRM.DAL.Models;

namespace CRM.BLL.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetCustomerWithProjectsAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersWithTestimonialsAsync();
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByPhoneAsync(string phoneNumber);
    }
}
