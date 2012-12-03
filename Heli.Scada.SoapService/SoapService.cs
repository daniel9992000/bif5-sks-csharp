using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Heli.Scada.BLInterfaces;
using Heli.Scada.Entities;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Heli.Scada.SoapService
{
  
    public class SoapService : ISoapService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(SoapService));

        public void createCustomer(Customer customer)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                CustomerModel cmodel = new CustomerModel();
                cmodel.firstname = customer.firstname;
                cmodel.lastname = customer.lastname;
                cmodel.email = customer.email;
                cmodel.password = customer.password;
                cmodel.username = customer.username;
                cmodel.engineerid = customer.engineerid;
             
                IUnityContainer container = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                section.Configure(container);
                IEngineerBL rservicebl = container.Resolve<IEngineerBL>();
                rservicebl.createCustomer(cmodel);
            }
            catch (Exception exp)
            {
                log.Error("Customer via Soap konnte nicht gespeichert werden.");
                throw new Exception("Customer via Soap konnte nicht gespeichert werden.", exp);
            }
            
        }

        public void createInstallation(Installation installation)
        {
            try
            {
                InstallationModel imodel = new InstallationModel();
                imodel.latitude = installation.latitude;
                imodel.longitude = installation.longitude;
                imodel.serialno = installation.serialno;
                imodel.description = installation.description;
                imodel.customerid = installation.customerid;

                IUnityContainer container = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                section.Configure(container);
                IEngineerBL rservicebl = container.Resolve<IEngineerBL>();

                rservicebl.createInstallation(imodel);
            }
            catch (Exception exp)
            {
                log.Error("Installation via Soap konnte nicht gespeichert werden.");
                throw new Exception("Installation via Soap konnte nicht gespeichert werden.", exp);
            }
        }
    }
}
