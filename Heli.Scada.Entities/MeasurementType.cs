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
    public class MeasurementTypeModel
    {
        [NotNullValidator(MessageTemplate = "Typeid must not be null")]
        public int typeid { get; set; }
        [StringLengthValidator (1,50, MessageTemplate="Description must be between 1 and 50 characters")]
        public string description { get; set; }
        [RangeValidator(-100.0, RangeBoundaryType.Inclusive, 100.0, RangeBoundaryType.Inclusive, MessageTemplate = "MinValue must be between -100 and 100")]
        public decimal minvalue { get; set; }
        [RangeValidator(-100.0, RangeBoundaryType.Inclusive, 100.0, RangeBoundaryType.Inclusive, MessageTemplate = "MaxValue must be between -100 and 100")]
        public decimal maxvalue { get; set; }
        [StringLengthValidator (1,50, MessageTemplate="Unit must be between 1 and 50 characters.")]
        public string unit { get; set; }
        private List<int> measurement = new List<int>();

        public List<int> Measurement
        {
          get { return measurement; }
          set { measurement = value; }
        }
    }
}
