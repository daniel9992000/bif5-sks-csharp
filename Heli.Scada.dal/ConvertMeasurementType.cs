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
    public static class ConvertMeasurementType
    {
        static readonly ILog log = LogManager.GetLogger(typeof(ConvertMeasurementType));

        public static List<MeasurementTypeModel> ConvertToList(IQueryable<Measurement_Type> measurementquery)
        {
            log4net.Config.XmlConfigurator.Configure();
            List<MeasurementTypeModel> measurementlist = null;
            try
            {
                measurementlist = new List<MeasurementTypeModel>();
                foreach (var item in measurementquery)
                {
                    measurementlist.Add(ConvertfromEntity(item));
                }
                log.Info("MeasurementTypeQuery wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementTypeQuery konnte nicht konvertiert werden.");
                throw new DalException("MeasurementTypeQuery konnte nicht konvertiert werden.", exp);
            }
            return measurementlist;
        }

        public static MeasurementTypeModel ConvertfromEntity(Measurement_Type inmeasurementtype)
        {
            MeasurementTypeModel measurementtype = null;
            try
            {
                measurementtype = new MeasurementTypeModel();
                measurementtype.typeid = inmeasurementtype.typeid;
                measurementtype.maxvalue = inmeasurementtype.maxvalue;
                measurementtype.minvalue = inmeasurementtype.minvalue;
                measurementtype.description = inmeasurementtype.description;
                measurementtype.unit = inmeasurementtype.unit;
                foreach (var item in inmeasurementtype.Measurement)
                {
                    measurementtype.Measurement.Add(item.measid);
                }
                log.Info("MeasurementType wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementType konnte nicht konvertiert werden.");
                throw new DalException("MeasurementType konnte nicht konvertiert werden.", exp);
            }
            return measurementtype;
        }

        public static Measurement_Type ConverttoEntity(MeasurementTypeModel inmeasurementtype)
        {
            Measurement_Type measurementtype = null;
            try
            {
                MeasurementRepository mrepo = new MeasurementRepository();
                measurementtype = new Measurement_Type();
                measurementtype.typeid = inmeasurementtype.typeid;
                measurementtype.maxvalue = inmeasurementtype.maxvalue;
                measurementtype.minvalue = inmeasurementtype.minvalue;
                measurementtype.description = inmeasurementtype.description;
                measurementtype.unit = inmeasurementtype.unit;
                foreach (var item in inmeasurementtype.Measurement)
                {
                    measurementtype.Measurement.Add(ConvertMeasurement.ConverttoEntity(mrepo.GetById(item)));
                }
                log.Info("MeasurementTypeModel wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("MeasurementTypeModel konnte nicht konvertiert werden.");
                throw new DalException("MeasurementTypeModel konnte nicht konvertiert werden.", exp);
            }  
            return measurementtype;
        }
    }
}
