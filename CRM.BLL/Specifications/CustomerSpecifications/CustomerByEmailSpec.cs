using CRM.DAL.Models;

namespace CRM.BLL.Specifications.CustomerSpecifications
{
    public class CustomerByEmailSpec : BaseSpecification<Customer>
    {
        public CustomerByEmailSpec(string email)
            : base(c => c.Email == email && !c.IsDeleted)
        {
        }
    }
}
