using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.DalInterfaces
{
    public interface IMeasurementRepository<T> : IRepository<T> where T:class
    {
        List<T> GetValuesPerDay(DateTime date);
        List<T> GetValuesPerMonth(DateTime date);
        List<T> GetValuesPerYear(DateTime date);
      
    }
}
