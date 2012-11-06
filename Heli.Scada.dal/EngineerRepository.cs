using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public class EngineerRepository:IRepository<EngineerModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<EngineerModel> GetAll()
        {
            IQueryable<Engineer> query = context.Engineer;
            return ConvertEngineer.ConvertToList(query);
        }

        public void Add(EngineerModel entity)
        {
            context.Engineer.Add(ConvertEngineer.ConverttoEntity(entity));
        }

        public void Delete(EngineerModel entity)
        {
            context.Engineer.Remove(ConvertEngineer.ConverttoEntity(entity));
        }

        public void Edit(EngineerModel entity)
        {
            context.Entry<Engineer>(ConvertEngineer.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        public EngineerModel GetById(int id)
        {
            return ConvertEngineer.ConvertfromEntity(context.Engineer.Find(id));
        }

        public List<CustomerModel> GetMyCustomers(int id)
        {
 
            IQueryable<Customer> query = from result in context.Customer
                                         where result.engineerid.Equals(id)
                                         select result;                                    
            return ConvertCustomer.ConvertToList(query);
        }
    }
}
