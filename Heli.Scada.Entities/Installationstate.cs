using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class InstallationState
    {
        public string unit {get; set;}
        public string description { get; set; }
        public double lastValue { get; set; }
        public DateTime currentTime {get; set; }

        public InstallationState()
        {

        }

        public InstallationState(double lastValue, string unit, string description, DateTime currentTime)
        {
            this.lastValue = lastValue;
            this.unit = unit;
            this.description = description;
            this.currentTime = currentTime;
        }
    }
}
