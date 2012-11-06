using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public static class ConvertMeasurement
    {
        public static List<MeasurementModel> ConvertToList(IQueryable<Measurement> measurementquery)
        {
            List<MeasurementModel> measurementlist = new List<MeasurementModel>();
            foreach (var item in measurementquery)
            {
                measurementlist.Add(ConvertfromEntity(item));
            }
            return measurementlist;
        }

        public static MeasurementModel ConvertfromEntity(Measurement inmeasurement)
        {
            MeasurementModel measurement = new MeasurementModel();
            measurement.measid = inmeasurement.measid;
            measurement.timestamp = inmeasurement.timestamp;
            measurement.typeid = inmeasurement.typeid;
            measurement.installationid = inmeasurement.installationid;
            return measurement;
        }

        public static Measurement ConverttoEntity(MeasurementModel inmeasurement)
        {
            MeasurementTypeRepository mrepo = new MeasurementTypeRepository();
            InstallationRepository irepo = new InstallationRepository();
            Measurement measurement = new Measurement();
            measurement.measid = inmeasurement.measid;
            measurement.timestamp = inmeasurement.timestamp;
            measurement.typeid = inmeasurement.typeid;
            measurement.installationid = inmeasurement.installationid;
            measurement.Measurement_Type = ConvertMeasurementType.ConverttoEntity(mrepo.GetById(measurement.typeid));
            measurement.Installation =  ConvertInstallation.ConverttoEntity(irepo.GetById(measurement.installationid));
            return measurement;
        }
    }
}
