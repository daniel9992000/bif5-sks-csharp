using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;
using Heli.Scada.Exceptions;
using log4net;

namespace Heli.Scada.dal
{
    public static class ConvertEngineer
    {
        static readonly ILog log = LogManager.GetLogger(typeof(ConvertEngineer));
     
        public static List<EngineerModel> ConvertToList(IQueryable<Engineer> engineerquery)
        {
            log4net.Config.XmlConfigurator.Configure();
            List<EngineerModel> engineerlist = null;
            try
            {
                engineerlist = new List<EngineerModel>();
                foreach (var item in engineerquery)
                {
                    engineerlist.Add(ConvertfromEntity(item));
                }
                log.Info("EngineerQuery wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("EngineerQuery konnte nicht konvertiert werden.");
                throw new DalException("EngineerQuery konnte nicht konvertiert werden.", exp);
            }
            return engineerlist;
        }

        public static EngineerModel ConvertfromEntity(Engineer inengineer)
        {
            EngineerModel engineer = null;
            try
            {
                engineer = new EngineerModel();
                engineer.engineerid = inengineer.engineerid;
                engineer.firstname = inengineer.firstname;
                engineer.lastname = inengineer.lastname;
                engineer.username = inengineer.username;
                engineer.password = inengineer.password;
                engineer.email = inengineer.email;
                log.Info("Engineer wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("Engineer konnte nicht konvertiert werden.");
                throw new DalException("Engineer konnte nicht konvertiert werden.", exp);
            }
            return engineer;
        }

        public static Engineer ConverttoEntity(EngineerModel inengineer)
        {
            Engineer engineer = null;
            try
            {
                EngineerRepository erepo = new EngineerRepository();
                InstallationRepository irepo = new InstallationRepository();
                engineer = new Engineer();
                engineer.firstname = inengineer.firstname;
                engineer.lastname = inengineer.lastname;
                engineer.username = inengineer.username;
                engineer.password = inengineer.password;
                engineer.engineerid = inengineer.engineerid;
                engineer.email = inengineer.email;
                log.Info("EngineerModel wurde konvertiert.");
            }
            catch (Exception exp)
            {
                log.Error("EngineerModel konnte nicht konvertiert werden.");
                throw new DalException("EngineerModel konnte nicht konvertiert werden.", exp);
            }
            return engineer;
        }
    }
}
