using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.DalInterfaces;
using Heli.Scada.Entities;

namespace Heli.Scada.Unittests
{
    public class MockMeasurementRepository : IMeasurementRepository<MeasurementModel>
    {
        List<MeasurementModel> measurements = new List<MeasurementModel>
            {
                new MeasurementModel{ typeid=0, installationid=0, measid =0, timestamp= DateTime.Now}
            };

        public List<InstallationState> getCurrentValues(InstallationModel installation)
        {
            List<InstallationState> ilist = new List<InstallationState>();
            MeasurementModel mmodel = measurements.Last();
            InstallationState istate = new InstallationState();
            istate.currentTime = mmodel.timestamp;
            istate.description = "Sonde";
            istate.lastValue = Double.Parse(mmodel.measurevalue.ToString());
            istate.unit = "Celsius";
            ilist.Add(istate);
            return ilist;
        }

        public List<Statistic> getPerDay(InstallationModel installation, DateTime date)
        {
            List<Statistic> slist = new List<Statistic>();
            foreach (var item in measurements)
            {
                if (item.timestamp.DayOfYear.Equals(date.DayOfYear))
                {
                    Statistic stat = new Statistic();
                    stat.average = Double.Parse(item.measurevalue.ToString());
                    stat.description = "Innen";
                    stat.maxvalue = 150;
                    stat.minvalue = 66;
                    stat.unit = "Fahrenheit";
                    slist.Add(stat);
                }
            }
            return slist;
        }

        public List<Statistic> getPerMonth(InstallationModel installation, DateTime date)
        {
            List<Statistic> slist = new List<Statistic>();
            foreach (var item in measurements)
            {
                if (item.timestamp.Month.Equals(date.Month))
                {
                    Statistic stat = new Statistic();
                    stat.average = Double.Parse(item.measurevalue.ToString());
                    stat.description = "Außen";
                    stat.maxvalue = 150;
                    stat.minvalue = 66;
                    stat.unit = "Fahrenheit";
                    slist.Add(stat);
                }
            }
            return slist;
        }

        public List<Statistic> getPerYear(InstallationModel installation, DateTime date)
        {
            List<Statistic> slist = new List<Statistic>();
            foreach (var item in measurements)
            {
                if (item.timestamp.Year.Equals(date.Year))
                {
                    Statistic stat = new Statistic();
                    stat.average = Double.Parse(item.measurevalue.ToString());
                    stat.description = "Sonde";
                    stat.maxvalue = 150;
                    stat.minvalue = 66;
                    stat.unit = "Fahrenheit";
                    slist.Add(stat);
                }
            }
            return slist;
        }

        public List<MeasurementModel> GetAll()
        {
            return measurements;
        }

        public MeasurementModel GetById(int id)
        {
            foreach (var item in measurements)
            {
                if (item.measid == id)
                    return item;
            }
            return null;
        }

        public void Add(MeasurementModel entity)
        {
            measurements.Add(entity);
        }

        public void Delete(MeasurementModel entity)
        {
            measurements.Remove(entity);
        }

        public void Edit(MeasurementModel entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
