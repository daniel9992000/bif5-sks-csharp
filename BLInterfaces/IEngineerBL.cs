using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.BLInterfaces
{
    public interface IEngineerBL
    {
        void createCustomer(CustomerModel customer, InstallationModel installation);
        List<CustomerModel> showMyCustomers(int engineerid);
    }
}
