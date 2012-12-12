using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.dal;
using log4net;
using Heli.Scada.Exceptions;
using Heli.Scada.DalInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Heli.Scada.BLInterfaces;

namespace Heli.Scada.BL
{
    public class EngineerBL: IEngineerBL
    {
        ICustomerRepository<CustomerModel> crepo;
        IEngineerRepository<EngineerModel> erepo;
        IInstallationRepository<InstallationModel> irepo;
        public static readonly ILog log = LogManager.GetLogger(typeof(CustomerBL)); 

        public EngineerBL(ICustomerRepository<CustomerModel> crepo, IEngineerRepository<EngineerModel> erepo, IInstallationRepository<InstallationModel> irepo)
        {
            this.crepo = crepo;
            this.erepo = erepo;
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
                    log.Info("Customer saved.");
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
            catch (DalException exp)
            {
                log.Error("Customer konnte nicht gespeichert werden.");
                throw new BLException("Customer konnte nicht gespeichert werden.", exp);
            }
        }

        public void createInstallation(InstallationModel installation)
        {
            try
            {
                ValidationResults vresult = Validation.ValidateFromAttributes<InstallationModel>(installation);
      
                if (vresult.IsValid)
                {
                    irepo.Add(installation);
                    irepo.Save();
                    log.Info("Installation saved.");
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
            catch (DalException exp)
            {
                log.Error("Installation konnte nicht gespeichert werden.");
                throw new BLException("Installation konnte nicht gespeichert werden.", exp);
            }
        }
        public List<CustomerModel> showMyCustomers(int engineerid)
        {
            List<CustomerModel> clist = null;
            try
            {
                Validator<int> eidvalidator = new RangeValidator<int>(0, RangeBoundaryType.Inclusive, Int32.MaxValue, RangeBoundaryType.Exclusive);
                ValidationResults vresult = eidvalidator.Validate(engineerid);
                if (!vresult.IsValid)
                {
                    log.Warn("Engineerid muss positiv sein.");
                    throw new BLException("Engineerid muss positiv sein.");
                }
                clist = erepo.GetMyCustomers(engineerid);
                log.Info("Kunden des Technikers wurden abgerufen.");
            }
            catch (DalException exp)
            {
                log.Error("Kunden des Technikers konnten nicht abgerufen werden.");
                throw new BLException("Kunden des Technikers konnten nicht abgerufen werden.", exp);
            }
            return clist;
        }

        public bool validateEngineer(string username, string password)
        {
            try
            {
                return erepo.validateEngineer(username, password);
            }
            catch (BLException exp)
            {
                log.Error("Fehler bei der Authentifikation des Engineer via Soap.");
                throw new BLException("Fehler bei der Authentifikation des Engineer via Soap.", exp);
            }
        }
    }
}
