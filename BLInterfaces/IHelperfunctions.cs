using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.BLInterfaces
{
    public interface IHelperfunctions
    {
        List<Statistic> getStatistics(CustomerModel customer, int option);
        List<Installationstate> getInstallationState(CustomerModel customer);
    }
}
