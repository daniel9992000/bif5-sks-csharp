using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.BLInterfaces;
using Heli.Scada.DalInterfaces;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;

namespace Heli.Scada.BL
{
    public class RestServiceBL : IRestServiceBL
    {
        IMeasurementRepository<MeasurementModel> mrepo;
        public static readonly ILog log = LogManager.GetLogger(typeof(RestServiceBL));

        public RestServiceBL(IMeasurementRepository<MeasurementModel> mrepo)
        {
            this.mrepo = mrepo;
        }
        public void createMeasurement(MeasurementModel measurement)
        {
            try
            {
                mrepo.Add(measurement);
                mrepo.Save();
                log.Info("Measurement von RestService wurde gespeichert.");
            }
            catch (BLException exp)
            {
                log.Error("Measurement von RestService konnte nicht gespeichert werden.");
                throw new BLException("Measurement von RestService konnte nicht gespeichert werden.", exp);
            }
        }
    }
}
