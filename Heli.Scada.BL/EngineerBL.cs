using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.dal;

namespace Heli.Scada.BL
{
    public static class EngineerBL
    {
        public static void createCustomer(CustomerModel customer, InstallationModel installation)
        {
            CustomerRepository crepo = new CustomerRepository();
            crepo.Add(customer);
            InstallationRepository irepo = new InstallationRepository();
            irepo.Add(installation);
        }

        public static Dictionary<CustomerModel,List<Installationstate>> showMyCustomersInstallationState(int engineerid)
        {
            Dictionary<CustomerModel,List<Installationstate>> idict = new Dictionary<CustomerModel,List<Installationstate>>();
           
            foreach (var customer in showMyCustomers(engineerid))
            {
               idict.Add(customer,HelperFunctions.getInstallationState(customer));
            }
            return idict;
        }

        public static Dictionary<CustomerModel,List<Statistic>> showMyCustomersStatistics(int engineerid, int option)
        {
            Dictionary<CustomerModel,List<Statistic>> sdict = new Dictionary<CustomerModel,List<Statistic>>();
          
            foreach (var customer in showMyCustomers(engineerid))
            {
                sdict.Add(customer,HelperFunctions.getStatistics(customer, option));
            }
            return sdict;   
        }
        public static List<CustomerModel> showMyCustomers(int engineerid)
        {
            EngineerRepository erepo = new EngineerRepository();
            return erepo.GetMyCustomers(engineerid);
        }
    }
}
