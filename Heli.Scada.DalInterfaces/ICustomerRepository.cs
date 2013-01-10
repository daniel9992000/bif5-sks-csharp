using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.DalInterfaces
{
    public interface ICustomerRepository<T>:IRepository<T> where T:class
    {
        int validateCustomer(string username, string password);
    }
}
