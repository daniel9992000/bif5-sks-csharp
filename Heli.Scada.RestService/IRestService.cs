using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Heli.Scada.Entities;


namespace Heli.Scada.RestService
{
  
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Create/{id}", 
          RequestFormat = WebMessageFormat.Json)]
        void CreateMeasurement(string id, MeasuredValue measurement);

    }

    [DataContract]
    public class MeasuredValue
    {
        [DataMember]
        public double value { get; set; }

        [DataMember]
        public DateTime timestamp { get; set; }

        [DataMember]
        public string unit { get; set; }
        
        [DataMember]
        public int type { get; set; }
    }
}
