using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.dal;
using Heli.Scada.Entities;

namespace Heli.Scada.BL
{
    public static class HelperFunctions
    {
        public static List<Statistic> getStatistics(CustomerModel customer, int option)
        {
            InstallationRepository irepo = new InstallationRepository();
            MeasurementRepository mrepo = new MeasurementRepository();
            MeasurementTypeRepository mtrepo = new MeasurementTypeRepository();
            List<Statistic> slist = new List<Statistic>();
            foreach (var installation in irepo.GetByCustomerId(customer.customerid))
            {
                Statistic statistic = new Statistic();
                statistic.installationid = installation.installationid;

                statistic.latitude = installation.latitude;
                statistic.longitude = installation.longitude;

                List<MeasurementModel> mlist = new List<MeasurementModel>();
                if (option.Equals(0))
                    mlist = mrepo.GetValuesPerDay(DateTime.Now);
                else if (option.Equals(1))
                    mlist = mrepo.GetValuesPerMonth(DateTime.Now);
                else
                    mlist = mrepo.GetValuesPerYear(DateTime.Now);
                statistic.maxvalue = statistic.minvalue = statistic.averagevalue = statistic.activedays = 0;
                int loop = 0, day = 0;
                foreach (var measurement in mlist)
                {
                    MeasurementTypeModel mtmodel = mtrepo.GetById(measurement.typeid);
                    if (statistic.maxvalue < mtmodel.maxvalue)
                        statistic.maxvalue = mtmodel.maxvalue;
                    if (statistic.minvalue > mtmodel.minvalue)
                        statistic.minvalue = mtmodel.minvalue;
                    loop++;
                    statistic.averagevalue += (mtmodel.maxvalue + mtmodel.minvalue) / 2;
                    if (!DateTime.Parse(measurement.timestamp.ToString()).DayOfYear.Equals(DateTime.Parse(day.ToString())))
                    {
                        statistic.activedays++;
                        day = measurement.timestamp;
                    }
                }
                statistic.averagevalue = statistic.averagevalue / loop;
                slist.Add(statistic);
            }
            return slist;
        }

        public static List<Installationstate> getInstallationState(CustomerModel customer)
        {
            InstallationRepository irepo = new InstallationRepository();
            MeasurementRepository mrepo = new MeasurementRepository();
            MeasurementTypeRepository mtrepo = new MeasurementTypeRepository();
            List<Installationstate> ilist = new List<Installationstate>();
            Installationstate istate = new Installationstate();
            istate.customerid = customer.customerid;
            foreach (var installation in irepo.GetByCustomerId(customer.customerid))
            {
                istate.installationid = installation.installationid;
                istate.serialno = installation.serialno;
                istate.latitude = installation.latitude;
                istate.longitude = installation.longitude;
                istate.serialno = installation.serialno;
                MeasurementModel mmodel = mrepo.GetById(installation.measurement.Last());
                MeasurementTypeModel mtmodel = mtrepo.GetById(mmodel.typeid);
                istate.maxvalue = mtmodel.maxvalue;
                istate.minvalue = mtmodel.minvalue;
                istate.unit = mtmodel.unit;
                ilist.Add(istate);
            }
            return ilist;
        }
    }
}
