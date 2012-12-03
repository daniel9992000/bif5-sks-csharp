using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using log4net;
using Heli.Scada.BLInterfaces;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DBHandler
{
    class Program
    {

        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {

            log4net.Config.XmlConfigurator.Configure();

            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);
               

            IStatisticService sservice = container.Resolve<IStatisticService>();

            

            ICustomerBL cbl = container.Resolve<ICustomerBL>();

            Dictionary<InstallationModel, List<Statistic>> slist = sservice.getStatisticPerYear(1, DateTime.Now);
            /*crepo.Delete(crepo.GetById(0));
            crepo.Save();
            erepo.Delete(erepo.GetById(0));
            erepo.Save();*/

            Dictionary<InstallationModel, List<InstallationState>> ilist = sservice.getInstallationState(1);

            foreach (var item in ilist)
            {
                Console.WriteLine("Installation: " + item.Key.installationid);
                foreach (var states in item.Value)
                {
                    Console.WriteLine("lastvalue: " + states.lastValue);
                    Console.WriteLine("currentTime: " + states.currentTime);
                    Console.WriteLine("unit: " + states.unit);
                    Console.WriteLine("description: " + states.description);
                }
            }

           /* foreach (var item in slist)
            {
                Console.WriteLine("Installation: "  + item.Key.installationid);
                foreach (var stats in item.Value)
                {
                    Console.WriteLine("average: " + stats.average );
                    Console.WriteLine("minvalue: " + stats.minvalue);
                    Console.WriteLine("maxvalue: " + stats.maxvalue);
                    Console.WriteLine("unit: " + stats.unit);
                    Console.WriteLine("description: " + stats.description);
                }
            }*/

         
            Console.ReadLine();
        }
    }
}
