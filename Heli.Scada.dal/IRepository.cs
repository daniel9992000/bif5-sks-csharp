using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heli.Scada.dal
{
    public interface IRepository<T> where T:class 
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();  
    }
}
