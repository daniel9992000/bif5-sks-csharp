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
using Heli.Scada.DalInterfaces;
using Heli.Scada.BLInterfaces;

namespace Heli.Scada.BL
{
    public class StatisticService: IStatisticService
    {
        IInstallationRepository<InstallationModel> irepo;
        IMeasurementRepository<MeasurementModel> mrepo;
        IMeasurementTypeRepository<MeasurementTypeModel> mtrepo;
        public static readonly ILog log = LogManager.GetLogger(typeof(StatisticService)); 

        public StatisticService(IInstallationRepository<InstallationModel> irepo, IMeasurementRepository<MeasurementModel> mrepo, IMeasurementTypeRepository<MeasurementTypeModel> mtrepo)
        {
            this.irepo = irepo;
            this.mrepo = mrepo;
            this.mtrepo = mtrepo;
        }

        public Dictionary<InstallationModel, List<InstallationState>> getInstallationState(int customerid)
        {
            Dictionary<InstallationModel, List<InstallationState>> istate = new Dictionary<InstallationModel, List<InstallationState>>();
            try
            {
                List<InstallationState> tmp = new List<InstallationState>();
                foreach (var installation in irepo.GetByCustomerId(customerid))
                {
                    tmp = mrepo.getCurrentValues(installation);
                    istate.Add(installation,tmp);
                }
                log.Info("InstallationState für Kunden " + customerid + " wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("InstallationState von Kunden " + customerid + " konnte nicht erstellt werden.");
                throw new BLException("InstallationState von Kunden " + customerid + " konnte nicht erstellt werden.", exp);
            }
            return istate;
        }

        public Dictionary<InstallationModel, List<Statistic>> getStatisticPerDay(int customerid, DateTime date)
        {

            Dictionary<InstallationModel, List<Statistic>> stats = new Dictionary<InstallationModel, List<Statistic>>();
            try
            {
               
                List<Statistic> tmp = new List<Statistic>();
                foreach (var installation in irepo.GetByCustomerId(customerid))
                {
                    tmp = mrepo.getPerDay(installation, date);
                    stats.Add(installation, tmp);
                }
                log.Info("Statistik für Kunden wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("Statistik für Kunden " + customerid + " konnte nicht erstellt werden.");
                throw new BLException("Statistik für Kunden " + customerid + " konnte nicht erstellt werden.", exp);
            }
            return stats;
        }

        public Dictionary<InstallationModel, List<Statistic>> getStatisticPerMonth(int customerid, DateTime date)
        {

            Dictionary<InstallationModel, List<Statistic>> stats = new Dictionary<InstallationModel, List<Statistic>>();
            try
            {

                List<Statistic> tmp = new List<Statistic>();
                foreach (var installation in irepo.GetByCustomerId(customerid))
                {
                    tmp = mrepo.getPerMonth(installation, date);
                    stats.Add(installation, tmp);
                }
                log.Info("Statistik für Kunden " + customerid + " wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("Statistik für Kunden " + customerid + " konnte nicht erstellt werden.");
                throw new BLException("Statistik für Kunden " + customerid + " konnte nicht erstellt werden.", exp);
            }
            return stats;
        }

        public Dictionary<InstallationModel, List<Statistic>> getStatisticPerYear(int customerid, DateTime date)
        {

            Dictionary<InstallationModel, List<Statistic>> stats = new Dictionary<InstallationModel, List<Statistic>>();
            try
            {

                List<Statistic> tmp = new List<Statistic>();
                foreach (var installation in irepo.GetByCustomerId(customerid))
                {
                    tmp = mrepo.getPerYear(installation, date);
                    stats.Add(installation, tmp);
                }
                log.Info("Statistik für Kunden " + customerid +" wurde erstellt.");
            }
            catch (DalException exp)
            {
                log.Error("Statistik für Kunden " +customerid +" konnte nicht erstellt werden.");
                throw new BLException("Statistik für Kunden " + customerid +" konnte nicht erstellt werden.", exp);
            }
            return stats;
        }   
    }
}
