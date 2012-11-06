using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public static class ConvertInstallation
    {

        public static List<InstallationModel> ConvertToList(IQueryable<Installation> installationquery)
       {
           List<InstallationModel> installationlist = new List<InstallationModel>();
           foreach (var item in installationquery)
           {
                installationlist.Add(ConvertfromEntity(item));     
           }
           return installationlist;
       }

       public static InstallationModel ConvertfromEntity (Installation ininstallation)
       {
           InstallationModel installation = new InstallationModel();
           installation.customerid = ininstallation.customerid;
           installation.installationid = ininstallation.installationid;
           installation.latitude = ininstallation.latitude;
           installation.longitude = ininstallation.longitude;
           installation.serialno = ininstallation.serialno;
           foreach (var item in ininstallation.Measurement)
           {
               installation.measurement.Add(item.measid);
           }
           return installation;
       }

       public static Installation ConverttoEntity(InstallationModel ininstallation)
       {
           CustomerRepository crepo = new CustomerRepository();
           MeasurementRepository mrepo = new MeasurementRepository();
           Installation installation = new Installation();
           installation.customerid = ininstallation.customerid;
           installation.installationid = ininstallation.installationid;
           installation.latitude = ininstallation.latitude;
           installation.longitude = ininstallation.longitude;
           installation.serialno = ininstallation.serialno;
           installation.Customer = ConvertCustomer.ConverttoEntity(crepo.GetById(installation.customerid));
           foreach (var item in ininstallation.measurement)
           {
               installation.Measurement.Add(ConvertMeasurement.ConverttoEntity(mrepo.GetById(item)));
           }
           return installation;
       }

    }
}
