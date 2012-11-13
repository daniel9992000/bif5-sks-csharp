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
    public class InstallationRepository:IInstallationRepository<InstallationModel>
    {
        private MesswerteEntities1 context = new MesswerteEntities1();
        static readonly ILog log = LogManager.GetLogger(typeof(InstallationRepository));

        public List<InstallationModel> GetAll()
        {
            log4net.Config.XmlConfigurator.Configure();
            List<InstallationModel> ilist = null;
            try
            {
                ilist = new List<InstallationModel>();
                IQueryable<Installation> query = context.Installation;
                ilist = ConvertInstallation.ConvertToList(query);
                log.Info("InstallationEntities wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("InstallationEntities konnten nicht geladen werden.");
                throw new DalException("InstallationEntities konnten nicht geladen werden.", exp);
            }  
            return ilist;
        }

        public void Add(InstallationModel entity)
        {
            try
            {
                context.Installation.Add(ConvertInstallation.ConverttoEntity(entity));
                log.Info("Installation wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("Installation konnten nicht gespeichert werden.");
                throw new DalException("Installation konnte nicht gespeichert werden.", exp);
            }
        }

        public void Delete(InstallationModel entity)
        {
            try
            {
                context.Installation.Remove(ConvertInstallation.ConverttoEntity(entity));
                log.Info("Installation wurde gelöscht.");
            }
            catch (Exception exp)
            {
                log.Error("Installation konnte nicht gelöscht werden.");
                throw new DalException("Installation konnte nicht gelöscht werden.", exp);
            }
        }

        public void Edit(InstallationModel entity)
        {
            try
            {
                context.Entry<Installation>(ConvertInstallation.ConverttoEntity(entity)).State = System.Data.EntityState.Modified;
                log.Info("Installation wurde geändert.");
            }
            catch (Exception exp)
            {
                log.Error("Installation konnte nicht geändert werden.");
                throw new DalException("Installation konnte nicht geändert werden.", exp);
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                log.Info("InstallationRepo wurde gespeichert.");
            }
            catch (Exception exp)
            {
                log.Error("InstallationRepo konnte nicht gespeichert werden.");
                throw new DalException("InstallationRepo konnte nicht gespeichert werden.", exp);
            }
        }

        public InstallationModel GetById(int id)
        {
            InstallationModel installation = null;
            try
            {
                installation = ConvertInstallation.ConvertfromEntity(context.Installation.Find(id));
                log.Info("Installation wurde geladen.");
            }
            catch (Exception exp)
            {
                log.Error("Installation konnte nicht geladen werden.");
                throw new DalException("Installation konnte nicht geladen werden.", exp);
            }
            return installation ;
        }

        public List<InstallationModel> GetByCustomerId(int customerid)
        {
            List<InstallationModel> ilist = null;
            try
            {
                IQueryable<Installation> query = from result in context.Installation
                                                 where result.customerid.Equals(customerid)
                                                 select result;
                ilist = ConvertInstallation.ConvertToList(query);
                log.Info("Installations von Customer wurden geladen.");
            }
            catch (Exception exp)
            {
                log.Error("Installations von Customer konnten nicht geladen werden.");
                throw new DalException("Installation von Customer konnten nicht geladen werden.", exp);
            }
           
            return ilist ;
        }
    }
}
