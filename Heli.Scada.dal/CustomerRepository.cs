using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public class CustomerRepository:IRepository<Customer>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public IQueryable<Customer> GetAll()
        {
            IQueryable<Customer> query = context.Customer;
            return query;
        }

        public void Add(Customer entity)
        {
            context.Customer.Add(entity);
        }

        public void Delete(Customer entity)
        {
            context.Customer.Remove(entity);
        }

        public void Edit(Customer entity)
        {
            context.Entry<Customer>(entity).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
