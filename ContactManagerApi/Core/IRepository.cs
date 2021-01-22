using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApi.Core
{
    public interface IRepository<T>
    {
        Task<T> Find(int id);
        IEnumerable<T> GetByExpression(Expression<Func<T, bool>> predicate);
        IEnumerable<T> All();
        Task<List<T>> FindPaged<T>(int page, int pageSize) where T : class;
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        bool Create(T entity);
        bool CreateRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool Update(T entity);
        bool UpdateRange(IEnumerable<T> entities);
        bool RemoveRange(IEnumerable<T> entities);
    }
}
