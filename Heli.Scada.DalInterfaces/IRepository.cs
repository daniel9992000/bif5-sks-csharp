using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.DalInterfaces
{
    public interface IRepository<T> where T:class 
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();  
    }
}
