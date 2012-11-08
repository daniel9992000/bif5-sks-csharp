using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;

namespace Heli.Scada.dal
{
    public static class ConvertInstallation
    {
       static readonly ILog log = LogManager.GetLogger(typeof(ConvertEngineer));

       public static List<InstallationModel> ConvertToList(IQueryable<Installation> installationquery)
       {
           log4net.Config.XmlConfigurator.Configure();
           List<InstallationModel> installationlist = null;
           try
           {
               installationlist = new List<InstallationModel>();
               foreach (var item in installationquery)
               {
                    installationlist.Add(ConvertfromEntity(item));     
               }
               log.Info("InstallationQuery wurde konvertiert.");
           }
           catch (Exception exp)
           {    
               log.Error("InstallationQuery konnte nicht konvertiert werden.");
               throw new DalException("InstallationQuery konnte nicht konvertiert werden.", exp);
           }
           return installationlist;
       }

       public static InstallationModel ConvertfromEntity (Installation ininstallation)
       {
           InstallationModel installation = null;
           try
           {
               installation = new InstallationModel();
               installation.customerid = ininstallation.customerid;
               installation.installationid = ininstallation.installationid;
               installation.latitude = ininstallation.latitude;
               installation.longitude = ininstallation.longitude;
               installation.serialno = ininstallation.serialno;
               foreach (var item in ininstallation.Measurement)
               {
                   installation.Measurement.Add(item.measid);
               }
               log.Info("Installation wurde konvertiert.");
           }
           catch (Exception exp)
           {
               log.Error("Installation konnte nicht konvertiert werden.");
               throw new DalException("Installation konnte nicht konvertiert werden.", exp);
           }
           return installation;
       }

       public static Installation ConverttoEntity(InstallationModel ininstallation)
       {
           Installation installation = null;
           try
           {
               CustomerRepository crepo = new CustomerRepository();
               MeasurementRepository mrepo = new MeasurementRepository();
               installation = new Installation();
               installation.customerid = ininstallation.customerid;
               installation.installationid = ininstallation.installationid;
               installation.latitude = ininstallation.latitude;
               installation.longitude = ininstallation.longitude;
               installation.serialno = ininstallation.serialno;
               installation.Customer = ConvertCustomer.ConverttoEntity(crepo.GetById(installation.customerid));
               foreach (var item in ininstallation.Measurement)
               {
                   installation.Measurement.Add(ConvertMeasurement.ConverttoEntity(mrepo.GetById(item)));
               }
               log.Info("InstallationModel wurde konvertiert.");
           }
           catch (Exception exp)
           {
               log.Error("InstallationModel konnte nicht konvertiert werden.");
               throw new DalException("InstallationModel konnte nicht konvertiert werden.", exp);
           }
           return installation;
       }

    }
}
