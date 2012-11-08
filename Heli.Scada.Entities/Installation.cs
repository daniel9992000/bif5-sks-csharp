using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Heli.Scada.Entities
{
    public class InstallationModel
    {
        [NotNullValidator (MessageTemplate="InstallationID must not be null")]
        public int installationid { get; set; }
        [NotNullValidator (MessageTemplate="Serialno must not be null"), StringLengthValidator(1,50,MessageTemplate="Serialno must be between 1 and 50 characters")]
        public string serialno { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public Nullable<decimal> latitude { get; set; }
        [StringLengthValidator(1,100, MessageTemplate="Description must be between 1 and 100 characters")]
        public string description { get; set; }
        public int customerid { get; set; }
        private List<int> measurement = new List<int>();

        public List<int> Measurement
        {
            get { return measurement; }
            set { measurement = value; }
        }
    }
}
