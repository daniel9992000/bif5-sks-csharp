using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public class MeasurementRepository:IRepository<Measurement>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public IQueryable<Measurement> GetAll()
        {
            IQueryable<Measurement> query = context.Measurement;
            return query;
        }

        public void Add(Measurement entity)
        {
            context.Measurement.Add(entity);
        }

        public void Delete(Measurement entity)
        {
            context.Measurement.Remove(entity);
        }

        public void Edit(Measurement entity)
        {
            context.Entry<Measurement>(entity).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
