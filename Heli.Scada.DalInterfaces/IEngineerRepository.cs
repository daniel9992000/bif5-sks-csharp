using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.DalInterfaces
{
    public interface IEngineerRepository<T> :IRepository<T> where T:class
    {
        List<CustomerModel> GetMyCustomers(int id);
        bool validateEngineer(string username, string password);
    }
}
