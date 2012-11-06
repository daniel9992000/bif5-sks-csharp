using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public class MeasurementTypeRepository:IRepository<MeasurementTypeModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<MeasurementTypeModel> GetAll()
        {
            IQueryable<Measurement_Type> query = context.Measurement_Type;
            return ConvertMeasurementType.ConvertToList(query);
        }

        public void Add(MeasurementTypeModel entity)
        {
            context.Measurement_Type.Add(ConvertMeasurementType.ConverttoEntity(entity));
        }

        public void Delete(MeasurementTypeModel entity)
        {
            context.Measurement_Type.Remove(ConvertMeasurementType.ConverttoEntity(entity));
        }

        public void Edit(MeasurementTypeModel entity)
        {
            context.Entry<Measurement_Type>(ConvertMeasurementType.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        public MeasurementTypeModel GetById(int id)
        {
            return ConvertMeasurementType.ConvertfromEntity(context.Measurement_Type.Find(id));
        }
    }
}
