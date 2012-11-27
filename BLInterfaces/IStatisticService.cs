using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.BLInterfaces
{
    public interface IStatisticService
    {
        Dictionary<InstallationModel, List<Statistic>> getStatisticPerDay(int customerid, DateTime date);
        Dictionary<InstallationModel, List<Statistic>> getStatisticPerMonth(int customerid, DateTime date);
        Dictionary<InstallationModel, List<Statistic>> getStatisticPerYear(int customerid, DateTime date);
        Dictionary<InstallationModel, List<InstallationState>> getInstallationState(int customerid);
    }
}
