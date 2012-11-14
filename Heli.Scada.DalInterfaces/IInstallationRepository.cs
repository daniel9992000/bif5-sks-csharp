using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.DalInterfaces
{
    public interface IInstallationRepository<T> : IRepository<T> where T:class
    {
        List<T> GetByCustomerId(int customerid);
    }
}
