using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using log4net;
using log4net.Config;
using Heli.Scada.Exceptions;

namespace Heli.Scada.dal
{
    public static class ConvertCustomer
    {

       static readonly ILog log = LogManager.GetLogger(typeof(ConvertCustomer));
       public static List<CustomerModel> ConvertToList(IQueryable<Customer> customerquery)
       {
           log4net.Config.XmlConfigurator.Configure();
           List<CustomerModel> customerlist = null;
           try
           {
               customerlist = new List<CustomerModel>();
               foreach (var item in customerquery)
               {
                   customerlist.Add(ConvertfromEntity(item));
               }
               log.Info("CustomerQuery wurde konvertiert.");
           }
           catch (Exception exp)
           {
               log.Error("CustomerQuery konnte nicht konvertiert werden.");
               throw new DalException ("CustomerQuery konnte nicht konvertiert werden.", exp);
           }
           return customerlist;
       }

       public static CustomerModel ConvertfromEntity (Customer incustomer)
       {
           CustomerModel customer = null;
           try
           {
               customer = new CustomerModel();
               customer.customerid = incustomer.customerid;
               customer.firstname = incustomer.firstname;
               customer.lastname = incustomer.lastname;
               customer.username = incustomer.username;
               customer.password = incustomer.password;
               customer.engineerid = incustomer.engineerid;
               customer.email = incustomer.email;
               foreach (var item in incustomer.Installation)
               {
                   customer.Installation.Add(item.installationid);
               }
               log.Info("Customer wurde konvertiert");
           }
           catch (Exception exp)
           {
               log.Error("Customer konnte nicht konvertiert werden.");
               throw new DalException("Customer konnte nicht konvertiert werden.", exp);
           }
           return customer;
       }

       public static Customer ConverttoEntity(CustomerModel incustomer)
       {
           Customer customer = null;
           try
           {
               EngineerRepository erepo = new EngineerRepository(new MesswerteEntities1());
               InstallationRepository irepo = new InstallationRepository();
               customer = new Customer();
               customer.customerid = incustomer.customerid;
               customer.firstname = incustomer.firstname;
               customer.lastname = incustomer.lastname;
               customer.username = incustomer.username;
               customer.password = incustomer.password;
               customer.engineerid = incustomer.engineerid;
               customer.email = incustomer.email;
               customer.Engineer = ConvertEngineer.ConverttoEntity(erepo.GetById(customer.engineerid));
               foreach (var item in incustomer.Installation)
               {
                   customer.Installation.Add(ConvertInstallation.ConverttoEntity(irepo.GetById(item)));
               }
               log.Info("CustomerModel wurde konvertiert.");
           }
           catch (Exception exp)
           {
               log.Error("CustomerModel konnte nicht konvertiert werden.");
               throw new DalException("CustomerModel konnte nicht konvertiert werden.", exp);
           }
           return customer;
       }
    }
}
