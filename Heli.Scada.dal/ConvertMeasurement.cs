using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using log4net;
using Heli.Scada.Exceptions;

namespace Heli.Scada.dal
{
    public static class ConvertMeasurement
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(ConvertMeasurement)); 

        public static List<MeasurementModel> ConvertToList(IQueryable<Measurement> measurementquery)
        {
            List<MeasurementModel> measurementlist = null;
            try
            {
                measurementlist = new List<MeasurementModel>();
                foreach (var item in measurementquery)
                {
                    measurementlist.Add(ConvertfromEntity(item));
                }
                log.Info("MeasurmentQuery wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurmentQuery konnte nicht konvertiert werden.");
                throw new DalException("MeasurmentQuery konnte nicht konvertiert werden.", exp);
            }
            return measurementlist;
        }

        public static MeasurementModel ConvertfromEntity(Measurement inmeasurement)
        {
            MeasurementModel measurement = null;
            try
            {
                measurement = new MeasurementModel();
                measurement.measid = inmeasurement.measid;
                measurement.timestamp = DateTime.Parse(inmeasurement.timestamp.ToString());
                measurement.typeid = inmeasurement.typeid;
                measurement.installationid = inmeasurement.installationid;
                measurement.measurevalue = inmeasurement.measurevalue;
                log.Info("Measurement wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("Measurement konnte nicht konvertiert werden.");
                throw new DalException("Measurement konnte nicht konvertiert werden.", exp);
            }
            return measurement;
        }

        public static Measurement ConverttoEntity(MeasurementModel inmeasurement)
        {
            Measurement measurement;
            try
            {
                MeasurementTypeRepository mtrepo = new MeasurementTypeRepository();
                //InstallationRepository irepo = new InstallationRepository();
                measurement = new Measurement();
                measurement.measid = inmeasurement.measid;
                measurement.timestamp = inmeasurement.timestamp;
                measurement.typeid = inmeasurement.typeid;
                measurement.installationid = inmeasurement.installationid;
                measurement.measurevalue = inmeasurement.measurevalue;
                //measurement.Measurement_Type = ConvertMeasurementType.ConverttoEntity(mtrepo.GetById(measurement.typeid));
                //measurement.Installation = ConvertInstallation.ConverttoEntity(irepo.GetById(measurement.installationid));
                log.Info("MeasurementModel wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Info("MeasurementModel konnte nicht konvertiert werden.");
                throw new DalException("MeasurementModel konnte nicht konvertiert werden.", exp);
            }
            return measurement;
        }
    }
}
