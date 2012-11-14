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
        List<Installationstate> showMyCustomersInstallationState(int customerid);
        List<Statistic> showMyCustomersStatistics(int customerid, int option);
    }
}
