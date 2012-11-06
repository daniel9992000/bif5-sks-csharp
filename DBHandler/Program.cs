using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.dal;
using Heli.Scada.Entities;

namespace DBHandler
{
    class Program
    {
        static void Main(string[] args)
        {
          
            EngineerRepository erepo = new EngineerRepository();
            CustomerRepository crepo = new CustomerRepository();

            CustomerModel customer = new CustomerModel();
            Engineer teste = new Engineer();
         
           
            customer = crepo.GetById(2);

            /*crepo.Delete(crepo.GetById(0));
            crepo.Save();
            erepo.Delete(erepo.GetById(0));
            erepo.Save();*/
            
            foreach (var item in crepo.GetAll())
            {
                Console.WriteLine("Name: " +  item.username + " Email: " + item.email);
            }
            Console.ReadLine();
        }
    }
}
