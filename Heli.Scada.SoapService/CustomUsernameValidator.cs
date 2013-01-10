using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IdentityModel.Tokens;
using System.Security.Principal;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Heli.Scada.BLInterfaces;
using System.Configuration;
using log4net;


namespace Heli.Scada.SoapService
{
    public class CustomUsernameValidator :  System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(CustomUsernameValidator));
        public override void Validate(string userName, string password)
        {
            try
            {
                if (userName == null || password == null)
                {
                    throw new ArgumentNullException();
                }
                IUnityContainer container = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                section.Configure(container);
                IEngineerBL rservicebl = container.Resolve<IEngineerBL>();

                if (rservicebl.validateEngineer(userName, password) >=0)
                {
                    throw new FaultException("Incorrect Username or Password");
                }
            }
            catch (Exception exp)
            {
                log.Error("Fehler bei Validierung des Engineers via Soap.");
                throw new Exception("Fehler bei Validierung des Engineers via Soap.", exp);
            }
        }
    }
}
