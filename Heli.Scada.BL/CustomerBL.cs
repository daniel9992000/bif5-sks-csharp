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
    public class CustomerBL: ICustomerBL
    {
        IRepository<CustomerModel> crepo;
        HelperFunctions hfunction;

        public static readonly ILog log = LogManager.GetLogger(typeof(CustomerBL));

        public CustomerBL(IRepository<CustomerModel> crepo, HelperFunctions hfunction)
        {
            this.crepo = crepo;
            this.hfunction = hfunction;
            log4net.Config.XmlConfigurator.Configure();
        }

        public List<Installationstate> showMyCustomersInstallationState(int customerid)
        {         
            List<Installationstate> ilist = null;
            try
            {
                Validator<int> cidvalidator = new RangeValidator<int>(0,Int32.MaxValue);
                ValidationResults vresult = cidvalidator.Validate(customerid);
                if (!vresult.IsValid)
                {
                    log.Warn("Customerid muss positiv sein.");
                    throw new BLException("Customerid muss positiv sein.");
                }
                ilist= new List<Installationstate>();
                ilist = hfunction.getInstallationState(crepo.GetById(customerid));
                log.Info("Anlagenzustände der Kunden wurden abgerufen");
            }
            catch (DalException exp)
            {
                log.Error("Anlagenzustände der Kunden konnten nicht geladen werden.");
                throw new BLException("Anlagenzustände der Kunden konnten nicht abgerufen werden.", exp);
            }
           
            return ilist;
        }

        public List<Statistic> showMyCustomersStatistics(int customerid, int option)
        {
            List<Statistic> slist = null;
            Validator<int> cidvalidator = new RangeValidator<int>(0,RangeBoundaryType.Inclusive, Int32.MaxValue, RangeBoundaryType.Exclusive, "Customerid muss positiv sein.");
            Validator<int> optvalidator = new RangeValidator<int>(0,RangeBoundaryType.Inclusive, 2, RangeBoundaryType.Inclusive, "Option muss 0-2 sein!" );
            ValidationResults vresult = cidvalidator.Validate(customerid);
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
                slist = new List<Statistic>();
                slist = hfunction.getStatistics(crepo.GetById(customerid), option);
                log.Info("Statistiken der Kunden wurden abgerufen");
            }
            catch (DalException exp)
            {
                log.Error("Statistiken der Kunden konnten nicht abgerufen werden.");
                throw new BLException("Statistiken der Kunden konnten nicht abgerufen werden.", exp);
            }
            return slist;
        }
    }
}
