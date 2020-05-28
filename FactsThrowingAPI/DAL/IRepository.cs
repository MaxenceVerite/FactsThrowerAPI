using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL
{
    public interface IRepository<T> where T : Entity
    {
        IList<T> List();
        T Add(T entity);

        T Update(Guid id, T entity);
        T Remove(Guid id);

        Task<T> FindAsync(Guid id);

        
    }
}
 