namespace CRM.BLL.Specifications.CommonSpecifications
{
    public class PaginatedSpec<T> : BaseSpecification<T> where T : class
    {
        public PaginatedSpec(int pageIndex, int pageSize)
        {
            ApplyPaging((pageIndex - 1) * pageSize, pageSize);
        }
    }
}
