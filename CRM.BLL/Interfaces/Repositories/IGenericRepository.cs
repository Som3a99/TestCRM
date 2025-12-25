namespace CRM.BLL.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
