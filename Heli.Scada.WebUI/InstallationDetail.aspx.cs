using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Heli.Scada.BLInterfaces;
using Heli.Scada.Entities;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Heli.Scada.WebUI
{
    public partial class InstallationDetail : System.Web.UI.Page
    {
        InstallationModel imodel = null;
        IStatisticService sservice = null;
        String user = "";
        int userid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);
            if (Request["installation"] != null)
            {
                int installationid = Int32.Parse(Request["installation"]);
                userid = Int32.Parse(Request["userid"]);
                user = Request["user"];
                sservice = container.Resolve<IStatisticService>();
                IInstallationBL iinstall = container.Resolve<IInstallationBL>();
                imodel = iinstall.getInstallation(installationid);
                Dictionary<InstallationModel, List<InstallationState>> statelist = sservice.getInstallationState(imodel.customerid);
                StringBuilder str = new StringBuilder();
                str.Append("<table border = '1'><tr><th>Description</th><th>Messwert</th><th>Einheit</th></tr>");
                foreach (var values in statelist)
                {  
                    if(values.Key.installationid.Equals(installationid))
                    {
                        foreach (var item in values.Value)
	                    {
		                     str.Append("<tr><td>" + item.description + "</td><td>" + item.lastValue + "</td><td>" + item.unit + "</td></tr>");
	                    }
                        break;
                    } 
                }
                str.Append("</table>");
                anlagenzustand.InnerHtml = str.ToString();
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InstallationList.aspx?user="+user+"&userid="+userid);
        }

        public void Statistikanzeigen(Dictionary<InstallationModel, List<Statistic>> sdict)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<table border = '1'><tr><th>Description</th><th>Max-Wert</th><th>Min-Wert</th><th>AVG-Wert</th><th>Einheit</th></tr>");
            foreach (var values in sdict)
            {
                if (values.Key.installationid.Equals(imodel.installationid))
                {
                    foreach (var item in values.Value)
                    {
                        str.Append("<tr><td>" + item.description + "</td><td>" + item.maxvalue + "</td><td>" + item.minvalue + "</td><td>" + item.average + "</td><td>" + item.unit + "</td></tr>");
                    }
                    break;
                }
            }
            str.Append("</table>");
            statistikanzeige.InnerHtml = str.ToString();
        }

        protected void daystat_Click(object sender, EventArgs e)
        {
          
            Statistikanzeigen(sservice.getStatisticPerDay(imodel.customerid,DateTime.Now));
        }

        protected void monthstat_Click(object sender, EventArgs e)
        {
          
            Statistikanzeigen(sservice.getStatisticPerMonth(imodel.customerid, DateTime.Now));
        }

        protected void yearstat_Click(object sender, EventArgs e)
        {
         
            Statistikanzeigen(sservice.getStatisticPerYear(imodel.customerid, DateTime.Now));
        }
    }
}