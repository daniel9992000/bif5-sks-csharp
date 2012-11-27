using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;
using Heli.Scada.DalInterfaces;

namespace Heli.Scada.dal
{
    public class MeasurementRepository:IMeasurementRepository<MeasurementModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();
        static readonly ILog log = LogManager.GetLogger(typeof(MeasurementRepository));

        public List<MeasurementModel> GetAll()
        {
            List<MeasurementModel> mlist = null;
            try
            {
                IQueryable<Measurement> query = context.Measurement;
                mlist = ConvertMeasurement.ConvertToList(query);
                log.Info("MeasurementEntities wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementEntities konnten nicht geladen werden.");
                throw new DalException("MeasurementEntities konnten nicht geladen werden.", exp);
            }
            return mlist;
        }

        public void Add(MeasurementModel entity)
        {
            try
            {
                context.Measurement.Add(ConvertMeasurement.ConverttoEntity(entity));
                log.Info("Measurement wurde hinzugefügt.");
            }
            catch (Exception exp)
            {
                log.Error("Measurement konnte nicht hinzugefügt werden.");
                throw new DalException("Measurement konnte nicht hinzugefügt werden.", exp);
            }
        }

        public void Delete(MeasurementModel entity)
        {
            try
            {
                context.Measurement.Remove(ConvertMeasurement.ConverttoEntity(entity));
                log.Info("Measurement wurde gelöscht.");
            }
            catch (Exception exp)
            {
                log.Error("Measurement konnte nicht gelöscht werden.");
                throw new DalException("Measurement konnte nicht gelöscht werden.", exp);
            }
        }

        public void Edit(MeasurementModel entity)
        {
            try
            {
                context.Entry<Measurement>(ConvertMeasurement.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
                log.Info("Measurement wurde geändert.");
            }
            catch (Exception exp)
            {
                log.Error("Measurement konnte nicht geändert werden.");
                throw new DalException("Measurement konnte nicht geändert werden.", exp);
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                log.Info("MeasurementRepo wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementRepo konnte nicht gespeichert werden.");
                throw new DalException("MeasurementRepo konnte nicht gespeichert werden.", exp);
            }
        }

        public MeasurementModel GetById(int id)
        {
            MeasurementModel measurement = null;
            try
            {
                measurement = ConvertMeasurement.ConvertfromEntity(context.Measurement.Find(id));
                log.Info("Measurement wurde geladen.");
            }
            catch (Exception exp)
            {
                log.Error("Measurement konnte nicht geladen werden.");
                throw new DalException("Measurement konnte nicht geladen werden.", exp);
            }
            return measurement ;
        }

       

        public List<InstallationState> getCurrentValues(InstallationModel installation)
        {
            List<InstallationState> ilist = new List<InstallationState>();
            try
            {
               
                var query = (from meas in context.Measurement
                             join mtype in context.Measurement_Type
                             on meas.typeid equals mtype.typeid
                             where meas.installationid == installation.installationid
                             group mtype by new { meas.typeid, meas.measurevalue, mtype.unit, mtype.description, meas.timestamp } into hilf
                             where hilf.Key.timestamp == (from mesl in context.Measurement where mesl.typeid == hilf.Key.typeid select mesl.timestamp).Max()
                             select new
                             {
                                 lastValue = hilf.Key.measurevalue,
                                 description = hilf.Key.description,
                                 unit = hilf.Key.unit,
                                 currentTime = hilf.Key.timestamp
                             });
                
                foreach (var item in query)
                {
                    InstallationState istate = new InstallationState();
                    istate.currentTime = item.currentTime;
                    istate.description = item.description;
                    istate.lastValue = Double.Parse(item.lastValue.ToString());
                    istate.unit = item.unit;
                    ilist.Add(istate);
                }

            }
            catch (Exception exp)
            {
                log.Error("InstallationState für Installation " + installation.installationid + " wurde erstellt.");
                throw new DalException("InstallationState für Installation " + installation.installationid + " wurde erstellt.", exp);
            }
            return ilist;
        }

        public List<Statistic> getPerDay(InstallationModel installation, DateTime date)
        {

            List<Statistic> slist = new List<Statistic>();
            try
            {
              
                DateTime datemin = date.AddDays(-1);
                DateTime datemax = date.AddDays(1);
                var query = (from meas in context.Measurement
                                         join mtype in context.Measurement_Type
                                         on meas.typeid equals mtype.typeid
                                         where meas.installationid == installation.installationid && meas.timestamp < datemax && meas.timestamp > datemin
                                         group mtype by new { meas.typeid, meas.measurevalue, mtype.unit, mtype.description } into hilf
                                         select new
                                         {
                                             maxvalue = hilf.Max(m => hilf.Key.measurevalue),
                                             minvalue = hilf.Min(m => hilf.Key.measurevalue),
                                             averagevalue = hilf.Average(m => hilf.Key.measurevalue),
                                             unit = hilf.Key.unit,
                                             description = hilf.Key.description
                                         });
               
                foreach (var item in query)
                {
                    Statistic tmp = new Statistic();
                    tmp.average = Double.Parse(item.averagevalue.ToString());
                    tmp.description = item.description;
                    tmp.maxvalue = Double.Parse(item.maxvalue.ToString());
                    tmp.minvalue = Double.Parse(item.minvalue.ToString());
                    tmp.unit = item.unit;
                    slist.Add(tmp);
                }
                log.Info("StatisticPerDay für Installation " + installation.installationid + " wurde erstellt");
                           
            }
            catch (Exception exp)
            {
                log.Error("ValuesPerDay konnten nicht geladen werden.");
                throw new DalException("ValuesPerDay konnten nicht geladen werden.", exp);
            }
            return slist;
        }

        public List<Statistic> getPerMonth(InstallationModel installation, DateTime date)
        {
            List<Statistic> slist = new List<Statistic>();
            try
            {

                DateTime datemin = date.AddMonths(-1);
                DateTime datemax = date.AddMonths(1);
                var query = (from meas in context.Measurement
                             join mtype in context.Measurement_Type
                             on meas.typeid equals mtype.typeid
                             where meas.installationid == installation.installationid && meas.timestamp < datemax && meas.timestamp > datemin
                             group mtype by new { meas.typeid, meas.measurevalue, mtype.unit, mtype.description } into hilf
                             select new
                             {
                                 maxvalue = hilf.Max(m => hilf.Key.measurevalue),
                                 minvalue = hilf.Min(m => hilf.Key.measurevalue),
                                 averagevalue = hilf.Average(m => hilf.Key.measurevalue),
                                 unit = hilf.Key.unit,
                                 description = hilf.Key.description
                             });

                foreach (var item in query)
                {
                    Statistic tmp = new Statistic();
                    tmp.average = Double.Parse(item.averagevalue.ToString());
                    tmp.description = item.description;
                    tmp.maxvalue = Double.Parse(item.maxvalue.ToString());
                    tmp.minvalue = Double.Parse(item.minvalue.ToString());
                    tmp.unit = item.unit;
                    slist.Add(tmp);
                }
                log.Info("StatisticPerMonth für Installation " + installation.installationid + " wurde erstellt");

            }
            catch (Exception exp)
            {
                log.Error("ValuesPerMonth konnten nicht geladen werden.");
                throw new DalException("ValuesPerMonth konnten nicht geladen werden.", exp);
            }
            return slist;
        }

        public List<Statistic> getPerYear(InstallationModel installation, DateTime date)
        {
            List<Statistic> slist = new List<Statistic>();
            try
            {

                DateTime datemin = date.AddYears(-1);
                DateTime datemax = date.AddYears(1);
                var query = (from meas in context.Measurement
                             join mtype in context.Measurement_Type
                             on meas.typeid equals mtype.typeid
                             where meas.installationid == installation.installationid && meas.timestamp < datemax && meas.timestamp > datemin
                             group mtype by new { meas.typeid, meas.measurevalue, mtype.unit, mtype.description } into hilf
                             select new
                             {
                                 maxvalue = hilf.Max(m => hilf.Key.measurevalue),
                                 minvalue = hilf.Min(m => hilf.Key.measurevalue),
                                 averagevalue = hilf.Average(m => hilf.Key.measurevalue),
                                 unit = hilf.Key.unit,
                                 description = hilf.Key.description
                             });

                foreach (var item in query)
                {
                    Statistic tmp = new Statistic();
                    tmp.average = Double.Parse(item.averagevalue.ToString());
                    tmp.description = item.description;
                    tmp.maxvalue = Double.Parse(item.maxvalue.ToString());
                    tmp.minvalue = Double.Parse(item.minvalue.ToString());
                    tmp.unit = item.unit;
                    slist.Add(tmp);
                }
                log.Info("StatisticPerYear für Installation " + installation.installationid + " wurde erstellt");

            }
            catch (Exception exp)
            {
                log.Error("ValuesPerYear konnten nicht geladen werden.");
                throw new DalException("ValuesPerYear konnten nicht geladen werden.", exp);
            }
            return slist;
        }
    }
}
