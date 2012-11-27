using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using Heli.Scada.Entities;
using Heli.Scada.BLInterfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Heli.Scada.RestService
{
   
    public class RestService : IRestService
    {
        public void CreateMeasurement(string installationid, MeasuredValue measvalue)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                MeasurementModel measurement = new MeasurementModel();
                measurement.installationid =  Int32.Parse(installationid);
                measurement.measurevalue = Decimal.Parse(measvalue.value.ToString());
                measurement.timestamp = measvalue.timestamp;
                measurement.typeid = measvalue.type;
                IUnityContainer container = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                section.Configure(container);
                IRestServiceBL rservicebl = container.Resolve<IRestServiceBL>();
                
                rservicebl.createMeasurement(measurement);
            }
            
            catch (Exception exp)
            {
                
                throw new Exception("Fehler im Restservice", exp);
            }
        }       
    }
}
