using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public class EngineerRepository:IRepository<Engineer>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public IQueryable<Engineer> GetAll()
        {
            IQueryable<Engineer> query = context.Engineer;
            return query;
        }

        public void Add(Engineer entity)
        {
            context.Engineer.Add(entity);
        }

        public void Delete(Engineer entity)
        {
            context.Engineer.Remove(entity);
        }

        public void Edit(Engineer entity)
        {
            context.Entry<Engineer>(entity).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
