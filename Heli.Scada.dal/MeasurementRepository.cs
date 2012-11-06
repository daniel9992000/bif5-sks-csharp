using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public class MeasurementRepository:IRepository<MeasurementModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<MeasurementModel> GetAll()
        {
            IQueryable<Measurement> query = context.Measurement;
            return ConvertMeasurement.ConvertToList(query);
        }

        public void Add(MeasurementModel entity)
        {
            context.Measurement.Add(ConvertMeasurement.ConverttoEntity(entity));
        }

        public void Delete(MeasurementModel entity)
        {
            context.Measurement.Remove(ConvertMeasurement.ConverttoEntity(entity));
        }

        public void Edit(MeasurementModel entity)
        {
            context.Entry<Measurement>(ConvertMeasurement.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public MeasurementModel GetById(int id)
        {
            return ConvertMeasurement.ConvertfromEntity(context.Measurement.Find(id));
        }

        public List<MeasurementModel> GetValuesPerDay(DateTime date)
        {
            IQueryable<Measurement> query = from result in context.Measurement
                                            where DateTime.Parse(result.timestamp.ToString()).DayOfYear.Equals(date.DayOfYear)
                                            select result;
            return ConvertMeasurement.ConvertToList(query);
        }

        public List<MeasurementModel> GetValuesPerMonth(DateTime date)
        {
            IQueryable<Measurement> query = from result in context.Measurement
                                            where DateTime.Parse(result.timestamp.ToString()).Month.Equals(date.Month)
                                            select result;
            return ConvertMeasurement.ConvertToList(query);
        }

        public List<MeasurementModel> GetValuesPerYear(DateTime date)
        {
            IQueryable<Measurement> query = from result in context.Measurement
                                            where DateTime.Parse(result.timestamp.ToString()).Year.Equals(date.Year)
                                            select result;
            return ConvertMeasurement.ConvertToList(query);
        }
    }
}
