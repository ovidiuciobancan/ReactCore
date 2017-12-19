using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Generic Repository of entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
        where TEntity : class 
    {
        IQueryable<TEntity> Query { get; }

        /// <summary>
        /// Add entity to collection
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Add range to collection
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(List<TEntity> entities);

        /// <summary>
        /// Remove entity from collection
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove entity from collection by primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        void Remove(params object[] primaryKey);

        /// <summary>
        /// Remove range from collection
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
