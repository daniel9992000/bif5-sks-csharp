using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;
using Heli.Scada.DalInterfaces;
using System.Data.Entity;

namespace Heli.Scada.dal
{
    public class EngineerRepository:IEngineerRepository<EngineerModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();
        static readonly ILog log = LogManager.GetLogger(typeof(EngineerRepository));

        public List<EngineerModel> GetAll()
        {
            List<EngineerModel> elist = null;
            try
            {
                IQueryable<Engineer> query = context.Engineer;
                elist = ConvertEngineer.ConvertToList(query);
                log.Info("EngineerEntities wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("EngineerEntities konnten nicht geladen werden.");
                throw new DalException("EngineerEntities konnten nicht geladen werden.", exp);
            }
            return elist;
        }

        public void Add(EngineerModel entity)
        {
            try
            {
                context.Engineer.Add(ConvertEngineer.ConverttoEntity(entity));
                log.Info("Engineer wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("Engineer konnte nicht gespeichert werden.");
                throw new DalException("Engineer konnte nicht gespeichert werden.", exp);
            }
        }

        public void Delete(EngineerModel entity)
        {
            try
            {
                context.Engineer.Remove(ConvertEngineer.ConverttoEntity(entity));
                log.Info("Engineer wurde gelöscht.");
            }
            catch (Exception exp)
            {
                log.Error("Engineer konnte nicht gelöscht werden.");
                throw new DalException("Engineer konnte nicht gelöscht werden.", exp);
            }
        }

        public void Edit(EngineerModel entity)
        {
            try
            {
                context.Entry<Engineer>(ConvertEngineer.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
                log.Info("Engineer wurde geändert.");
            }
            catch (Exception exp)
            {
                log.Error("Engineer konnte nicht geändert werden.");
                throw new DalException("Engineer konnte nicht geändert werden.", exp);
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                log.Info("EngineerRepo wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("EngineerRepo konnte nicht gespeichert werden.");
                throw new DalException("EngineerRepo konnte nicht gespeichert werden.", exp);
            }
        }


        public EngineerModel GetById(int id)
        {
            EngineerModel engineer = null;
            try
            {
                engineer = ConvertEngineer.ConvertfromEntity(context.Engineer.Find(id));
                log.Info("Engineer wurde geladen.");
            }
            catch (Exception exp)
            {
                log.Error("Engineer konnte nicht geladen werden.");
                throw new DalException("Engineer konnte nicht geladen werden.", exp);
            }
            return engineer;
        }

        public List<CustomerModel> GetMyCustomers(int id)
        {
 
            List<CustomerModel> clist = null;
            try
            {
                IQueryable<Customer> query = from result in context.Customer
                        where result.engineerid.Equals(id)
                        select result;
                clist = ConvertCustomer.ConvertToList(query);
                log.Info("Customers von Engineer wurden geladen.");
            } 
            catch(Exception exp)
            {
                log.Error("Customers von Engineer konnten nicht geladen werden.");
                throw new DalException("Customers von Engineer konnten nicht geladen werden.", exp);
            } 
            return clist;
        }
    }
}
