﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.BLInterfaces
{
    public interface IRestServiceBL
    {
        void createMeasurement(MeasurementModel measurement);
    }
}
