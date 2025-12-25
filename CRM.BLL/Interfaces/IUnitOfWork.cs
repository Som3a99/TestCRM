using CRM.BLL.Interfaces.Repositories;

namespace CRM.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository Customers { get; }
        IInquiryRepository Inquiries { get; }
        IProjectRepository Projects { get; }
        IProgrammerRepository Programmers { get; }
        ITechnologyRepository Technologies { get; }
        ITestimonialRepository Testimonials { get; }

        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
