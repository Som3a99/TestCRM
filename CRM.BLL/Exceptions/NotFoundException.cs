using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int id)
            : base($"{entityName} with id {id} was not found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
