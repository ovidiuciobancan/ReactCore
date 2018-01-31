using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using DataAccess.ExtensionMethods;
using DataAccess.Interfaces;

namespace DA.Base
{
    public class CrudRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        protected DbContext Context { get; }

        public CrudRepository(DbContext context)
        {
            query = context.Set<TEntity>();
            Context = context;
        }

        private IQueryable<TEntity> query;
        public IQueryable<TEntity> Query
        {
            get
            {
                return query;
            }
        }

        public virtual TEntity Get(params object[] key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public virtual void AddRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            // No implementation
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);
        }
        public virtual void Remove(params object[] primaryKey)
        {
            var stub = Context
                .PrimaryKey<TEntity>()
                .Stub<TEntity>(primaryKey);

            Context.Set<TEntity>().Attach(stub);
            Context.Set<TEntity>().Remove(stub);
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
