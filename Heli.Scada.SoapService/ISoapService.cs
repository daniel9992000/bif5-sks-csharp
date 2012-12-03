using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Heli.Scada.SoapService
{
  
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        void createCustomer(Customer customer);

        [OperationContract]
        void createInstallation(Installation installation);

    }

   
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string firstname { get; set; }

        [DataMember]
        public string lastname { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public int engineerid { get; set; }
    }

    [DataContract]
    public class Installation
    {
        [DataMember]
        public string serialno { get; set; }

        [DataMember]
        public decimal longitude { get; set; }

        [DataMember]
        public decimal latitude { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int customerid { get; set; }
    }
}
