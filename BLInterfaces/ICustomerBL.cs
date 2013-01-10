using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.BLInterfaces
{
    public interface ICustomerBL
    {
        void createCustomer(CustomerModel customer);
        CustomerModel getCustomer(int id);
        List<InstallationModel> getInstallations(CustomerModel customer);
        int validateCustomer(string username, string password);
    }
}
