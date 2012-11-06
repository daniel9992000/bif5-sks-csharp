using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Entities
{
    public class InstallationModel
    {
        public int installationid { get; set; }
        public string serialno { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public string description { get; set; }
        public int customerid { get; set; }
        public List<int> measurement { get; set; }
    }
}
