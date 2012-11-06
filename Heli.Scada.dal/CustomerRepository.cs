using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public class CustomerRepository : IRepository<CustomerModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<CustomerModel> GetAll()
        {
            IQueryable<Customer> query = context.Customer;
            return ConvertCustomer.ConvertToList(query);
        }

        public void Add(CustomerModel entity)
        {
            context.Customer.Add(ConvertCustomer.ConverttoEntity(entity));
        }

        public void Delete(CustomerModel entity)
        {
            context.Customer.Remove(ConvertCustomer.ConverttoEntity(entity));
        }

        public void Edit(CustomerModel entity)
        {
            context.Entry<Customer>(ConvertCustomer.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        public CustomerModel GetById(int id)
        {
             return ConvertCustomer.ConvertfromEntity(context.Customer.Find(id));
        }
    }
}
