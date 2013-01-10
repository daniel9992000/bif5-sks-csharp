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
    public class InstallationBL : IInstallationBL
    {
       
        IInstallationRepository<InstallationModel> irepo;

        public static readonly ILog log = LogManager.GetLogger(typeof(CustomerBL));

        public InstallationBL(IInstallationRepository<InstallationModel> irepo)
        {
            this.irepo = irepo;
        }
        public InstallationModel getInstallation(int installationid)
        {
            try
            {
                return irepo.GetById(installationid);
            }
            catch (DalException exp)
            {
                log.Error("Installation kann nicht geladen werden.");
                throw new BLException("Installation konnte nicht geladen werden.", exp);
            }
        }
    }
}
