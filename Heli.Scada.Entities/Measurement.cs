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
    public class MeasurementModel
    {
        [NotNullValidator(MessageTemplate="Measid must not be null")]
        public int measid { get; set; }
        public DateTime timestamp { get; set; }
        [NotNullValidator(MessageTemplate="Installation must not be null")]
        public int installationid { get; set; }
        [NotNullValidator(MessageTemplate = "Typeid must not be null")]
        public int typeid { get; set; }
        [NotNullValidator(MessageTemplate = "MeasureValue must not be null")]
        public decimal measurevalue { get; set; } 
    }
}
