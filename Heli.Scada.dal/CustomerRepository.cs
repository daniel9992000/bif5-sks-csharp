using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;
using Heli.Scada.DalInterfaces;

namespace Heli.Scada.dal
{
    public class CustomerRepository : ICustomerRepository<CustomerModel>
    {
        static readonly ILog log = LogManager.GetLogger(typeof(CustomerRepository));

        private MesswerteEntities1 context = new MesswerteEntities1();

        public List<CustomerModel> GetAll()
        {
            List<CustomerModel> clist = null;
            try
            {
                IQueryable<Customer> query = context.Customer;
                clist = ConvertCustomer.ConvertToList(query);
                log.Info("CustomerEntities wurden geladen");
            }
            catch (Exception exp)
            {
                log.Error("CustomerEntities konnten nicht geladen werden.");
                throw new DalException("CustomerEntities konnten nicht geladen werden.", exp);
            }
            return clist;
        }

        public void Add(CustomerModel entity)
        {
            try
            {
                context.Customer.Add(ConvertCustomer.ConverttoEntity(entity));
                log.Info("Customer wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("Customer konnte nicht gespeichert werden.");
                throw new DalException("Customer konnte nicht gespeichert werden.", exp);
            }
        }

        public void Delete(CustomerModel entity)
        {
            try
            {
                context.Customer.Remove(ConvertCustomer.ConverttoEntity(entity));
                log.Info("Customer wurde gelöscht.");
            }
            catch (Exception exp)
            {
                log.Error("Customer konnte nicht gelöscht werden.");
                throw new DalException("Customer konnte nicht gelöscht werden.", exp);
            }
        }

        public void Edit(CustomerModel entity)
        {
            try
            {
                context.Entry<Customer>(ConvertCustomer.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
                log.Info("Customer wurde geändert.");
            }
            catch (Exception exp)
            {
                log.Error("Customer konnte nicht geändert werden.");
                throw new DalException("Customer konnte nicht geändert werden.", exp);
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                log.Info("CustomerRepo wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("CustomerRepo konnte nicht gespeichert werden.");
                throw new DalException("CustomerRepo konnte nicht gespeichert werden.", exp);
            }
        }


        public CustomerModel GetById(int id)
        {
            CustomerModel customer = null;
            try
            {
                customer = ConvertCustomer.ConvertfromEntity(context.Customer.Find(id));
                log.Info("Customer wurde geladen.");
            }
            catch (Exception exp)
            {
                log.Error("Customer konnte nicht geladen werden.");
                throw new DalException("Customer konnte nicht geladen werden.", exp);
            }
            return customer;
        }
    }
}
