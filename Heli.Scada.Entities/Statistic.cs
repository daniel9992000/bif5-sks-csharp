using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class Statistic
    {
        public decimal maxvalue { get; set; }
        public decimal minvalue { get; set; }
        public decimal averagevalue { get; set; }
        public int activedays { get; set; }
        public int installationid { get; set; }
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }
    }
}
