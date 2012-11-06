using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heli.Scada.Entities;

namespace Heli.Scada.dal
{
    public static class ConvertEngineer
    {
        public static List<EngineerModel> ConvertToList(IQueryable<Engineer> engineerquery)
        {
            List<EngineerModel> engineerlist = new List<EngineerModel>();
            foreach (var item in engineerquery)
            {
                engineerlist.Add(ConvertfromEntity(item));
            }
            return engineerlist;
        }

        public static EngineerModel ConvertfromEntity(Engineer inengineer)
        {
            EngineerModel engineer = new EngineerModel();
            engineer.engineerid = inengineer.engineerid;
            engineer.firstname = inengineer.firstname;
            engineer.lastname = inengineer.lastname;
            engineer.username = inengineer.username;
            engineer.password = inengineer.password;
            engineer.email = inengineer.email;
            return engineer;
        }

        public static Engineer ConverttoEntity(EngineerModel inengineer)
        {
            EngineerRepository erepo = new EngineerRepository();
            InstallationRepository irepo = new InstallationRepository();
            Engineer engineer = new Engineer();
            engineer.firstname = inengineer.firstname;
            engineer.lastname = inengineer.lastname;
            engineer.username = inengineer.username;
            engineer.password = inengineer.password;
            engineer.engineerid = inengineer.engineerid;
            engineer.email = inengineer.email;
            return engineer;
        }
    }
}
