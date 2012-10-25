using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public class MeasurmentTypeRepository:IRepository<Measurement_Type>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public IQueryable<Measurement_Type> GetAll()
        {
            IQueryable<Measurement_Type> query = context.Measurement_Type;
            return query;
        }

        public void Add(Measurement_Type entity)
        {
            context.Measurement_Type.Add(entity);
        }

        public void Delete(Measurement_Type entity)
        {
            context.Measurement_Type.Remove(entity);
        }

        public void Edit(Measurement_Type entity)
        {
            context.Entry<Measurement_Type>(entity).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
