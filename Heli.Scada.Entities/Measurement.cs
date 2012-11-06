using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class MeasurementModel
    {
        public int measid { get; set; }
        public int timestamp { get; set; }
        public int installationid { get; set; }
        public int typeid { get; set; } 
    }
}
