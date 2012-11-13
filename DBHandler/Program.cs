using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.dal;
using Heli.Scada.Entities;
using Heli.Scada.BL;
using Microsoft.Practices.Unity;
using Heli.Scada.DalInterfaces;

namespace DBHandler
{
    class Program
    {
        static void Main(string[] args)
        {
          
            EngineerRepository erepo = new EngineerRepository(new MesswerteEntities1());
            CustomerRepository crepo = new CustomerRepository();

            CustomerModel customer = new CustomerModel();
            Engineer teste = new Engineer();
         
           
            customer = crepo.GetById(2);

            UnityContainer hfcontainer = new UnityContainer();
            hfcontainer.RegisterType<IInstallationRepository<InstallationModel>, InstallationRepository>();
            hfcontainer.RegisterType<IMeasurementRepository<MeasurementModel>, MeasurementRepository>();
            hfcontainer.RegisterType<IRepository<MeasurementTypeModel>, MeasurementTypeRepository>();

            HelperFunctions hfunctions = hfcontainer.Resolve<HelperFunctions>();

            UnityContainer cblcontainer = new UnityContainer();
            cblcontainer.RegisterType<IRepository<CustomerModel>, CustomerRepository>();
            cblcontainer.RegisterInstance<HelperFunctions>(hfunctions);

            CustomerBL cbl = cblcontainer.Resolve<CustomerBL>();

            List<Statistic> slist = cbl.showMyCustomersStatistics(1, 0);
            /*crepo.Delete(crepo.GetById(0));
            crepo.Save();
            erepo.Delete(erepo.GetById(0));
            erepo.Save();*/
            
            foreach (var item in crepo.GetAll())
            {
                Console.WriteLine("Name: " +  item.username + " Email: " + item.email);
                Console.WriteLine("Listenlänge: "  + slist.Count);
            }
            Console.ReadLine();
        }
    }
}
