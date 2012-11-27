using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class Statistic
    {
        public double maxvalue { get; set; }
        public double minvalue { get; set; }
        public double average { get; set; }
        public string unit { get; set; }
        public string description { get; set; }

        public Statistic()
        {
            maxvalue = 0.00;
            minvalue = 0.00;
            average = 0.00;
            unit = "";
            description = "";
        }

        public Statistic(double maxvalue, double minvalue, double average, string unit, string description)
        {
            this.average = average;
            this.minvalue = minvalue;
            this.maxvalue = maxvalue;
            this.unit = unit;
            this.description = description;
        }
    }
}
