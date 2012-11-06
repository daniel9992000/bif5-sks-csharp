using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.dal;

namespace Heli.Scada.BL
{
    public static class CustomerBL
    {
        public static List<Installationstate> showMyCustomersInstallationState(int customerid)
        {
            List<Installationstate> ilist = new List<Installationstate>();
            CustomerRepository crepo = new CustomerRepository();
            ilist = HelperFunctions.getInstallationState(crepo.GetById(customerid));
            return ilist;
        }

        public static List<Statistic> showMyCustomersStatistics(int customerid, int option)
        {
            List<Statistic> slist = new List<Statistic>();
            CustomerRepository crepo = new CustomerRepository();
            slist =  HelperFunctions.getStatistics(crepo.GetById(customerid), option);
            return slist;
        }
    }
}
