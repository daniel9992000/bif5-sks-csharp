using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class MeasurementTypeModel
    {
        public int typeid { get; set; }
        public string description { get; set; }
        public decimal minvalue { get; set; }
        public decimal maxvalue { get; set; }
        public string unit { get; set; }
        public List<int> measurement { get; set; }
    }
}
