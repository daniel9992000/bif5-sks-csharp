using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public static class ConvertMeasurementType
    {
        public static List<MeasurementTypeModel> ConvertToList(IQueryable<Measurement_Type> measurementquery)
        {
            List<MeasurementTypeModel> measurementlist = new List<MeasurementTypeModel>();
            foreach (var item in measurementquery)
            {
                measurementlist.Add(ConvertfromEntity(item));
            }
            return measurementlist;
        }

        public static MeasurementTypeModel ConvertfromEntity(Measurement_Type inmeasurementtype)
        {
            MeasurementTypeModel measurementtype = new MeasurementTypeModel();
            measurementtype.typeid = inmeasurementtype.typeid;
            measurementtype.maxvalue = inmeasurementtype.maxvalue;
            measurementtype.minvalue = inmeasurementtype.minvalue;
            measurementtype.description = inmeasurementtype.description;
            measurementtype.unit = inmeasurementtype.unit;
            foreach (var item in inmeasurementtype.Measurement)
            {
                measurementtype.measurement.Add(item.measid);
            } 
            return measurementtype;
        }

        public static Measurement_Type ConverttoEntity(MeasurementTypeModel inmeasurementtype)
        {
            MeasurementRepository mrepo = new MeasurementRepository();
            Measurement_Type measurementtype = new Measurement_Type();
            measurementtype.typeid = inmeasurementtype.typeid;
            measurementtype.maxvalue = inmeasurementtype.maxvalue;
            measurementtype.minvalue = inmeasurementtype.minvalue;
            measurementtype.description = inmeasurementtype.description;
            measurementtype.unit = inmeasurementtype.unit;
            foreach (var item in inmeasurementtype.measurement)
            {
                measurementtype.Measurement.Add(ConvertMeasurement.ConverttoEntity(mrepo.GetById(item)));
            }
            return measurementtype;
        }
    }
}
