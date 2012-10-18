using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.dal;

namespace DBHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MesswerteEntities1())
            {
             
/*                //Add an engineer do db
                Engineer e = new Engineer();
                e.firstname = "Heinz";
                e.lastname = "Grois";
                e.email = "h.grois@evn.at";
                e.username = "heinzi";
                e.password = "test";
                db.Engineer.Add(e);
                db.SaveChanges();


                //Add an costumer with the engineer to db
                Customer c = new Customer();
                c.lastname = "Huber";
                c.firstname = "Karl";
                c.username = "Karli";
                c.password = "test";
                c.email = "karl.huber@aon.at";
                c.engineerid = e.engineerid;
                db.Customer.Add(c);
                db.SaveChanges();


                //Read from DB
                foreach (var item in db.Customer)
                {
                    Console.WriteLine("Kundenname: " + item.firstname + " " + item.lastname);
                    Console.WriteLine("Zugehöriger Engineer: " + db.Engineer.Find(item.engineerid).firstname + " " + db.Engineer.Find(item.engineerid).lastname);
                }
               
                */
                EngineerRepository erepo = new EngineerRepository();
                Engineer e = new Engineer();
                e = erepo.GetAll().First<Engineer>();
                e.firstname = "Karlicku";
                erepo.Edit(e);
                erepo.Save();

                foreach (var item in erepo.GetAll())
                {
                    Console.WriteLine(item.firstname);
                }
                Console.ReadLine();
            }
        }
    }
}
