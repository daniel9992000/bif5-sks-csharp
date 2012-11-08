using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.dal;
using log4net;
using Heli.Scada.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Heli.Scada.BL
{
    public class EngineerBL
    {
        IRepository<CustomerModel> crepo;
        IEngineerRepository<EngineerModel> erepo;
        IInstallationRepository<InstallationModel> irepo;
        HelperFunctions hfunction;
        public static readonly ILog log = LogManager.GetLogger(typeof(CustomerBL)); 

        public EngineerBL(IRepository<CustomerModel> crepo, IEngineerRepository<EngineerModel> erepo, IInstallationRepository<InstallationModel> irepo, HelperFunctions hfunction)
        {
            this.crepo = crepo;
            this.erepo = erepo;
            this.irepo = irepo;
            this.hfunction = hfunction;
            log4net.Config.XmlConfigurator.Configure();
        }

        public void createCustomer(CustomerModel customer, InstallationModel installation)
        {
            try
            {
                ValidationResults vresult = Validation.ValidateFromAttributes<CustomerModel>(customer);
                vresult.AddAllResults(Validation.ValidateFromAttributes<InstallationModel>(installation));
                foreach (var item in vresult)
                {
                    log.Warn(item.Message);
                    throw new BLException(item.Message);
                }
                crepo.Add(customer);
                crepo.Save();
                log.Info("Customer wurde erfolgreich gespeichert.");
                irepo.Add(installation);
                irepo.Save();
                log.Info("Installation wurde erfolgreich gespeichert.");
            }
            catch (DalException exp)
            {
                log.Error("Customer bzw. Installation konnte nicht gespeichert werden.");
                throw new BLException("Customer bzw. Installation konnte nicht gespeichert werden.", exp);
            }
        }

        public Dictionary<CustomerModel,List<Installationstate>> showMyCustomersInstallationState(int engineerid)
        {
            Dictionary<CustomerModel, List<Installationstate>> idict = null;
            try
            {
                Validator<int> eidvalidator = new RangeValidator<int>(0, Int32.MaxValue);
                ValidationResults vresult = eidvalidator.Validate(engineerid);
                if (!vresult.IsValid)
                {
                    log.Warn("Engineerid muss positiv sein.");
                    throw new BLException("Enineerid muss positiv sein.");
                }
                idict = new Dictionary<CustomerModel, List<Installationstate>>();
                foreach (var customer in showMyCustomers(engineerid))
                {
                    idict.Add(customer, hfunction.getInstallationState(customer));
                }
                log.Info("Anlagenzustände für Kunden wurden erfolgreich abgerufen.");
            }
            catch (DalException exp)
            {
                log.Error("Anlagenzustände für Kunden konnten nicht abgerufen werden.");
                throw new BLException("Anlagenzustände für Kunden konnten nicht abgerufen werden.", exp);
            }
            return idict;
        }

        public Dictionary<CustomerModel,List<Statistic>> showMyCustomersStatistics(int engineerid, int option)
        {
            Dictionary<CustomerModel, List<Statistic>> sdict = null;
            Validator<int> eidvalidator = new RangeValidator<int>(0, RangeBoundaryType.Inclusive, Int32.MaxValue, RangeBoundaryType.Exclusive, "Engineerid muss positiv sein.");
            Validator<int> optvalidator = new RangeValidator<int>(0, RangeBoundaryType.Inclusive, 2, RangeBoundaryType.Inclusive, "Option muss 0-2 sein!");
            ValidationResults vresult = eidvalidator.Validate(engineerid);
            vresult.AddAllResults(optvalidator.Validate(option));
            if (!vresult.IsValid)
            {
                foreach (var item in vresult)
                {
                    log.Warn(item.Message);
                    throw new BLException(item.Message);
                }
            }
            try
            {
                sdict = new Dictionary<CustomerModel, List<Statistic>>();
                foreach (var customer in showMyCustomers(engineerid))
                {
                    sdict.Add(customer, hfunction.getStatistics(customer, option));
                }
                log.Info("Statistiken für Kunden wurden abgerufen.");
            }
            catch (DalException exp)
            {
                log.Error("Statistiken für Kunden konnten nicht abgerufen werden.");
                throw new BLException("Statistiken für Kunden konnten nicht abgerufen werden.", exp);
            }
            return sdict;   
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
    }
}
