using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;

namespace Heli.Scada.dal
{
    public class MeasurementRepository:IMeasurementRepository<MeasurementModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();
        static readonly ILog log = LogManager.GetLogger(typeof(MeasurementRepository));

        public List<MeasurementModel> GetAll()
        {
            log4net.Config.XmlConfigurator.Configure();
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

        public List<MeasurementModel> GetValuesPerDay(DateTime date)
        {
            List<MeasurementModel> mlist = null;
            int comparedate = DateTime.Now.DayOfYear;
            int indate = date.DayOfYear;
            try
            {
                IQueryable<Measurement> query = from result in context.Measurement
                                                where comparedate == indate
                                                select result;
                mlist = ConvertMeasurement.ConvertToList(query);
                log.Info("ValuesPerDay wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("ValuesPerDay konnten nicht geladen werden.");
                throw new DalException("ValuesPerDay konnten nicht geladen werden.", exp);
            }
            return mlist;
        }

        public List<MeasurementModel> GetValuesPerMonth(DateTime date)
        {
            List<MeasurementModel> mlist = null;
            int comparedate = DateTime.Now.Month;
            int indate = date.Month;
            try
            {
                IQueryable<Measurement> query = from result in context.Measurement
                                                where comparedate == indate
                                                select result;
                mlist = ConvertMeasurement.ConvertToList(query);
                log.Info("ValuesPerMonth wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("ValuesPerMonth konnten nicht geladen werden");
                throw new DalException("ValuesPerMonth konnten nicht geladen werden.", exp);
            }
            return mlist;
        }

        public List<MeasurementModel> GetValuesPerYear(DateTime date)
        {
            List<MeasurementModel> mlist = null;
            int comparedate = DateTime.Now.Year;
            int indate = date.Year;
            try
            {
                IQueryable<Measurement> query = from result in context.Measurement
                                                where comparedate == indate
                                                select result;
                mlist = ConvertMeasurement.ConvertToList(query);
                log.Info("ValuesPerYear wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("ValuesPerYear konnten nicht geladen werden.");
                throw new DalException("ValuesPerYear konnten nicht geladen werden.", exp);
            }
            return mlist;
        }
    }
}
