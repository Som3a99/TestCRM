using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Repositories;
using CRM.BLL.Repositories;
using CRM.DAL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace CRM.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;
        private IDbContextTransaction? _transaction;

        private ICustomerRepository? _customers;
        private IInquiryRepository? _inquiries;
        private IProjectRepository? _projects;
        private IProgrammerRepository? _programmers;
        private ITechnologyRepository? _technologies;
        private ITestimonialRepository? _testimonials;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        public ICustomerRepository Customers
        {
            get { return _customers ??= new CustomerRepository(_context); }
        }

        public IInquiryRepository Inquiries
        {
            get { return _inquiries ??= new InquiryRepository(_context); }
        }

        public IProjectRepository Projects
        {
            get { return _projects ??= new ProjectRepository(_context); }
        }

        public IProgrammerRepository Programmers
        {
            get { return _programmers ??= new ProgrammerRepository(_context); }
        }

        public ITechnologyRepository Technologies
        {
            get { return _technologies ??= new TechnologyRepository(_context); }
        }

        public ITestimonialRepository Testimonials
        {
            get { return _testimonials ??= new TestimonialRepository(_context); }
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var key = typeof(T).Name;
            return (IGenericRepository<T>)_repositories.GetOrAdd(key, _ => new GenericRepository<T>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            await _context.DisposeAsync();
        }
    }
}
