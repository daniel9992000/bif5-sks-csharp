using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.Exceptions
{
    public class DalException :Exception
    {
        public DalException()
        {
        }

        public DalException(string message)
            : base(message)
        {
        }

        public DalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

