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
    public partial class Anlagenauswahl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);
           
           
            if (Request["user"] != null)
            {
                if(Request["user"].ToString().Equals("engineer"))
                {
                    int engineerid = Int32.Parse(Request["userid"]);
                    IEngineerBL eservice = container.Resolve<IEngineerBL>();
                    ICustomerBL cservice = container.Resolve<ICustomerBL>();
                    List<CustomerModel> clist = eservice.showMyCustomers(engineerid);
                    List<InstallationModel> ilist = new List<InstallationModel>();
                    foreach (var item in clist)
                    {
                        ilist.AddRange(cservice.getInstallations(item));
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("<table border = '1'><tr><th>Id</th><th>Description</th></tr>");
                    foreach (var item in ilist)
                    {
                        str.Append("<tr><td>" + item.installationid + "</td><td>" + item.description + "</td><td><a href='InstallationDetail.aspx?installation=" + item.installationid + "&user=engineer&userid="+engineerid+"'>Details</a></td></tr>");
                    }
                    str.Append("</table>");
                    divtable.InnerHtml = str.ToString();
                }
                else
                {
                    int customerid = Int32.Parse(Request["userid"]);
                    ICustomerBL cservice = container.Resolve<ICustomerBL>();
                    List<InstallationModel> ilist = cservice.getInstallations(cservice.getCustomer(customerid));
                    StringBuilder str = new StringBuilder();
                    str.Append("<table border = '1'><tr><th>Id</th><th>Description</th></tr>");
                    foreach (var item in ilist)
                    {
                        str.Append("<tr><td>" + item.installationid + "</td><td>" + item.description + "</td><td><a href='InstallationDetail.aspx?installation=" + item.installationid + "&user=engineer&userid=" + customerid + "'>Details</a></td></tr>");
                    }
                    str.Append("</table>");
                    divtable.InnerHtml = str.ToString();
                }
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}