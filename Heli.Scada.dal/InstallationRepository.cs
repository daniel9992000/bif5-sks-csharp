using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public class InstallationRepository:IRepository<InstallationModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<InstallationModel> GetAll()
        {
            IQueryable<Installation> query = context.Installation;
            return ConvertInstallation.ConvertToList(query);
        }

        public void Add(InstallationModel entity)
        {
            context.Installation.Add(ConvertInstallation.ConverttoEntity(entity));
        }

        public void Delete(InstallationModel entity)
        {
            context.Installation.Remove(ConvertInstallation.ConverttoEntity(entity));
        }

        public void Edit(InstallationModel entity)
        {
            context.Entry<Installation>(ConvertInstallation.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public InstallationModel GetById(int id)
        {
            return ConvertInstallation.ConvertfromEntity(context.Installation.Find(id));
        }

        public List<InstallationModel> GetByCustomerId(int customerid)
        {
            IQueryable<Installation> query = from result in context.Installation
                                             where result.customerid.Equals(customerid)
                                             select result;
            return ConvertInstallation.ConvertToList(query);
        }
    }
}
