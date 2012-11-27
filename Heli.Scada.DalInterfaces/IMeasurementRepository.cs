using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.DalInterfaces
{
    public interface IMeasurementRepository<T> : IRepository<T> where T:class
    {
        List<InstallationState> getCurrentValues(InstallationModel installation);
        List<Statistic> getPerDay(InstallationModel installation, DateTime date);
        List<Statistic> getPerMonth(InstallationModel installation, DateTime date);
        List<Statistic> getPerYear(InstallationModel installation, DateTime date);
    }
}
