using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entity;

namespace BLL
{
    public interface IRepository<T> where T : BaseEntity
    {
        CrudState Add(IEnumerable<T> entities);
        CrudState Add(T entity);
        CrudState Delete(string id);
        List<T> Get();
        List<T> Get(Expression<Func<T, bool>> where);
        CrudState Update(T entity);
    }
}