using DataAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToDoContext context;
        private readonly DbSet<T> dbset;
        public Repository(ToDoContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }
        public CrudState Add(T entity)
        {
            try
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                dbset.Add(entity);
                return Save() == ConnectionState.Success ? CrudState.Success : CrudState.ConnectionError;
            }
            catch
            {
                return CrudState.EntityError;
            }
        }
        public CrudState Add(IEnumerable<T> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                dbset.AddRange(entities);
                return Save() == ConnectionState.Success ? CrudState.Success : CrudState.ConnectionError;
            }
            catch
            {
                return CrudState.ConnectionError;
            }
        }
        public CrudState Delete(string id)
        {
            try
            {
                var entity = dbset.Find(id);
                dbset.Remove(entity);
                return Save() == ConnectionState.Success ? CrudState.Success : CrudState.ConnectionError;
            }
            catch (Exception)
            {
                return CrudState.EntityError;
            }
        }
        public List<T> Get()
        {
            return dbset.ToList();
        }
        public List<T> Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public CrudState Update(T entity)
        {
            T old = context.Set<T>().Find(entity.Id);
            if (old != null)
            {
                try
                {
                    entity.UpdateAt = DateTime.Now;
                    context.Entry(old).CurrentValues.SetValues(entity);
                    return Save() == ConnectionState.Success ? CrudState.Success : CrudState.ConnectionError;
                }
                catch (Exception ez)
                {

                    return CrudState.EntityError;
                }
            }
            else
                return CrudState.NotFound;
        }
        private ConnectionState Save()
        {
            try
            {
                context.SaveChanges();
                return ConnectionState.Success;
            }
            catch (Exception ex)
            {
            }
            return ConnectionState.ConnectionError;
 
        }
    }
    public enum CrudState
    {
        Success,
        EntityError,
        ConnectionError,
        NotFound
    }
    public enum ConnectionState
    {
        Success,
        ConnectionError
    }
}
