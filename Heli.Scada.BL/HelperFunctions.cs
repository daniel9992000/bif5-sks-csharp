using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.dal;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;
using log4net.Config;

namespace Heli.Scada.BL
{
    public class HelperFunctions
    {
        IInstallationRepository<InstallationModel> irepo;
        IMeasurementRepository<MeasurementModel> mrepo;
        IRepository<MeasurementTypeModel> mtrepo;
        public static readonly ILog log = LogManager.GetLogger(typeof(HelperFunctions)); 

        public HelperFunctions(IInstallationRepository<InstallationModel> irepo, IMeasurementRepository<MeasurementModel> mrepo, IRepository<MeasurementTypeModel> mtrepo)
        {
            this.irepo = irepo;
            this.mrepo = mrepo;
            this.mtrepo = mtrepo;
            log4net.Config.XmlConfigurator.Configure();
        }
        public List<Statistic> getStatistics(CustomerModel customer, int option)
        {

            List<Statistic> slist = null;
            try
            {
                slist = new List<Statistic>();
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
                    int loop = 0;
                    DateTime day = DateTime.FromOADate(0);
                    foreach (var measurement in mlist)
                    {
                        MeasurementTypeModel mtmodel = mtrepo.GetById(measurement.typeid);
                        if (statistic.maxvalue < mtmodel.maxvalue)
                            statistic.maxvalue = mtmodel.maxvalue;
                        if (statistic.minvalue > mtmodel.minvalue)
                            statistic.minvalue = mtmodel.minvalue;
                        loop++;
                        statistic.averagevalue += (mtmodel.maxvalue + mtmodel.minvalue) / 2;
                        if (!measurement.timestamp.DayOfYear.Equals(day))
                        {
                            statistic.activedays++;
                            day = measurement.timestamp;
                        }
                    }
                    statistic.averagevalue = statistic.averagevalue / loop;
                    slist.Add(statistic);
                }
                log.Info("Statistik für Kunden wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("Statistik für Kunden konnte nicht erstellt werden.");
                throw new BLException("Statistik für Kunden konnte nicht erstellt werden.", exp);
            }
            return slist;
        }

        public List<Installationstate> getInstallationState(CustomerModel customer)
        {
            List<Installationstate> ilist = null;
            try
            {
                ilist = new List<Installationstate>();
                Installationstate istate = new Installationstate();
                istate.customerid = customer.customerid;
                foreach (var installation in irepo.GetByCustomerId(customer.customerid))
                {
                    istate.installationid = installation.installationid;
                    istate.serialno = installation.serialno;
                    istate.latitude = installation.latitude;
                    istate.longitude = installation.longitude;
                    istate.serialno = installation.serialno;
                    MeasurementModel mmodel = mrepo.GetById(installation.Measurement.Last());
                    MeasurementTypeModel mtmodel = mtrepo.GetById(mmodel.typeid);
                    istate.maxvalue = mtmodel.maxvalue;
                    istate.minvalue = mtmodel.minvalue;
                    istate.unit = mtmodel.unit;
                    ilist.Add(istate);
                }
                log.Info("Anlagenzustand für Kunden wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("Anlagenzustand für Kunden konnte nicht erstellt werden.");
                throw new BLException("Anlagenzustand für Kunden konnte nicht erstellt werden.", exp);
            }
            return ilist;
        }
    }
}
