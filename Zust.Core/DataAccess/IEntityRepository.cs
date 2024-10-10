using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Entities;

namespace Zust.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> predicate=null);
        Task<List<T>> GetList(Expression<Func<T, bool>> predicate = null);
        Task Add(T entity); 
        Task Delete(T entity);
        Task SaveAll();
    }
}
