using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public static class ConvertCustomer
    { 
       public static List<CustomerModel> ConvertToList(IQueryable<Customer> customerquery)
       {
           List<CustomerModel> customerlist = new List<CustomerModel>();
           foreach (var item in customerquery)
           {
                customerlist.Add(ConvertfromEntity(item));     
           }
           return customerlist;
       }

       public static CustomerModel ConvertfromEntity (Customer incustomer)
       {
           CustomerModel customer = new CustomerModel();
           customer.customerid = incustomer.customerid;
           customer.firstname = incustomer.firstname;
           customer.lastname = incustomer.lastname;
           customer.username = incustomer.username;
           customer.password = incustomer.password;
           customer.engineerid = incustomer.engineerid;
           customer.email = incustomer.email;
           foreach (var item in incustomer.Installation)
           {
               customer.installation.Add(item.installationid);
           }
           return customer;
       }

       public static Customer ConverttoEntity(CustomerModel incustomer)
       {
           EngineerRepository erepo = new EngineerRepository();
           InstallationRepository irepo = new InstallationRepository();
           Customer customer = new Customer();
           customer.customerid = incustomer.customerid;
           customer.firstname = incustomer.firstname;
           customer.lastname = incustomer.lastname;
           customer.username = incustomer.username;
           customer.password = incustomer.password;
           customer.engineerid = incustomer.engineerid;
           customer.email = incustomer.email;
           customer.Engineer = ConvertEngineer.ConverttoEntity(erepo.GetById(customer.engineerid));
           foreach (var item in incustomer.installation)
           {
               customer.Installation.Add(ConvertInstallation.ConverttoEntity(irepo.GetById(item)));
           }
           return customer;
       }
    }
}
