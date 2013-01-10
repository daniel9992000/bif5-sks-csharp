using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Heli.Scada.BLInterfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Heli.Scada.WebUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);
            IEngineerBL eservice = container.Resolve<IEngineerBL>();
            ICustomerBL cservice = container.Resolve<ICustomerBL>();
            if (eservice.validateEngineer(txtbenutzername.Value, txtpasswort.Value) >=0)
                Response.Redirect("~/InstallationList.aspx?user=engineer&userid=" + eservice.validateEngineer(txtbenutzername.Value, txtpasswort.Value));
            else if (cservice.validateCustomer(txtbenutzername.Value, txtpasswort.Value) >= 0)
                Response.Redirect("~/InstallationList.aspx?user=customer&userid=" + cservice.validateCustomer(txtbenutzername.Value, txtpasswort.Value));
            else
                loginresponse.InnerText +=  "Falscher Benutzername bzw. Passwort";
        }
    }
}