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
    public class MeasurementTypeRepository:IMeasurementTypeRepository<MeasurementTypeModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();
        static readonly ILog log = LogManager.GetLogger(typeof(MeasurementTypeRepository));

        public List<MeasurementTypeModel> GetAll()
        {
            List<MeasurementTypeModel> mtlist = null;
            try
            {
                IQueryable<Measurement_Type> query = context.Measurement_Type;
                mtlist = ConvertMeasurementType.ConvertToList(query);
                log.Info("MeasurementTypeEntities wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementTypeEntities konnten nicht geladen werden.");
                throw new DalException("MeasurementTypEntities konnten nicht geladen werden.", exp);
            }
            return mtlist;
        }

        public void Add(MeasurementTypeModel entity)
        {
            try
            {
                context.Measurement_Type.Add(ConvertMeasurementType.ConverttoEntity(entity));
                log.Info("MeasurementType wurde hinzugefügt.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementType konnte nicht hinzugefügt werden.");
                throw new DalException("MeasurementType konnte nicht hinzugefügt werden.", exp);
            }
        }

        public void Delete(MeasurementTypeModel entity)
        {
            try
            {
                context.Measurement_Type.Remove(ConvertMeasurementType.ConverttoEntity(entity));
                log.Info("MeasurementType wurde gelöscht.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementType konnte nicht gelöscht werden.");
                throw new DalException("MeasurementType konnte nicht gelöscht werden.", exp);
            }
        }

        public void Edit(MeasurementTypeModel entity)
        {
            try
            {
                context.Entry<Measurement_Type>(ConvertMeasurementType.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
                log.Info("MeasurementType wurde geändert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementType konnte nicht geändert werden.");
                throw new DalException("MeasurementType konnte nicht geändert werden.", exp);
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                log.Info("MeasurementTypeRepo wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementTypeRepo konnte nicht gespeichert werden.");
                throw new DalException("MeasurementTypeRepo konnte nicht gespeichert werden.", exp);
            }
        }


        public MeasurementTypeModel GetById(int id)
        {
            MeasurementTypeModel measurementtype = null;
            try
            {
                measurementtype = ConvertMeasurementType.ConvertfromEntity(context.Measurement_Type.Find(id));
                log.Info("MeasurementType wurde geladen.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementType konnte nicht geladen werden.");
                throw new DalException("MeasurementType konnte nicht geladen werden.", exp);
            }
            return measurementtype;
        }
    }
}
