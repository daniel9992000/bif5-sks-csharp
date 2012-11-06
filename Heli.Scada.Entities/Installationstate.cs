using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class Installationstate
    {
        public int customerid {get; set;}
        public int installationid { get; set; }
        public decimal minvalue { get; set; }
        public decimal maxvalue { get; set; }
        public string unit { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string serialno { get; set; }
    }
}
