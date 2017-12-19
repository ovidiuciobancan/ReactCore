using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQueryRepository<TEntity> : IRepository
    {
        /// <summary>
        /// get IQueryable from DbSet<TEntity> 
        /// </summary>
        IQueryable<TEntity> Query { get; }
    }
}