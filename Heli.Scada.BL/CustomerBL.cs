using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.dal;
using log4net;
using log4net.Config;
using Heli.Scada.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Heli.Scada.DalInterfaces;
using Heli.Scada.BLInterfaces;

namespace Heli.Scada.BL
{
    public class CustomerBL : ICustomerBL
    {
        ICustomerRepository<CustomerModel> crepo;
        IInstallationRepository<InstallationModel> irepo;

        public static readonly ILog log = LogManager.GetLogger(typeof(CustomerBL));

        public CustomerBL(ICustomerRepository<CustomerModel> crepo, IInstallationRepository<InstallationModel> irepo)
        {
            this.crepo = crepo;
            this.irepo = irepo;
        }

        public void createCustomer(CustomerModel customer) 
        {
            try
            {
                ValidationResults vresult = Validation.ValidateFromAttributes<CustomerModel>(customer);          
                if (vresult.IsValid)
                {
                    crepo.Add(customer);
                    crepo.Save();
                    log.Info("Customer saved");
                }
                else
                {
                    log.Warn(vresult.Count + "Validation errors");
                    StringBuilder sb = null;
                    foreach (var error in vresult)
                    {
                        sb = new StringBuilder();
                        sb.Append("Error on property ");
                        sb.Append(error.Target);
                        sb.Append(": ");
                        sb.Append(error.Message);
                    }
                    log.Warn(sb);
                }
            }
            catch(DalException exp)
            {
                log.Error("cannot save customer!", exp);
                throw new BLException("cannot save customer!", exp);
            }
        }
    
        public CustomerModel getCustomer(int id) 
        {
            CustomerModel tmp;
            try
            {
                log.Info("Fetching customer id" +  id);
                tmp = crepo.GetById(id);
          
            }
            catch(DalException err)
            {
                log.Warn("Cannot fetch customer id " + id, err);
                throw new BLException("Cannot fetch customer", err);
            }
            return tmp;
        }
    
        public List<InstallationModel> getInstallations(CustomerModel customer) 
        {
            List<InstallationModel> tmp;
            try
            {
                log.Info("Fetching Installation from customer id" + customer.customerid);
                tmp = irepo.GetByCustomerId(customer.customerid);
            }
            catch(DalException exp)
            {
                log.Error("Cannot fetch Installations from customer id " + customer.customerid, exp);
                throw new BLException("Cannot fetch Installations from customer id " + customer.customerid, exp);
            }
            return tmp;
        
        }
        public int validateCustomer(string username, string password)
        {
            try
            {
                return crepo.validateCustomer(username, password);
            }
            catch (BLException exp)
            {
                log.Error("Fehler bei der Authentifikation des Customer.");
                throw new BLException("Fehler bei der Authentifikation des Customer.", exp);
            }
        }
      
    }
}
