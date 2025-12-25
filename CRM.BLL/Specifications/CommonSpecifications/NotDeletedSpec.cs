namespace CRM.BLL.Specifications.CommonSpecifications
{
    public class NotDeletedSpec<T> : BaseSpecification<T> where T : class
    {
        public NotDeletedSpec()
    : base(e => !(bool)e.GetType().GetProperty("IsDeleted")!.GetValue(e)!)
        {
        }
    }
}
