using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public class InstallationRepository:IRepository<Installation>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public IQueryable<Installation> GetAll()
        {
            IQueryable<Installation> query = context.Installation;
            return query;
        }

        public void Add(Installation entity)
        {
            context.Installation.Add(entity);
        }

        public void Delete(Installation entity)
        {
            context.Installation.Remove(entity);
        }

        public void Edit(Installation entity)
        {
            context.Entry<Installation>(entity).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
