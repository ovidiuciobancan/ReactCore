using Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using Common;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BL.Base
{
    /// <summary>
    /// Generic Service of UnitOfWork
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public class Service<TUnitOfWork> : BaseService, IService
        where TUnitOfWork : class, IUnitOfWork
    {
        private TUnitOfWork UnitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        /// <param name="appSettings"></param>
        public Service(TUnitOfWork unitOfWork, ILogger logger = null, ICacheProvider cache = null, AppSettings appSettings = null)
            : base(unitOfWork, logger, cache, appSettings)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute transaction
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected int Transaction(Action<TUnitOfWork> predicate = null)
        {
            predicate?.Invoke(UnitOfWork);

            return UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Query repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected T Query<T>(Func<TUnitOfWork, T> predicate)
        {
            return predicate(UnitOfWork);
        }
    }
}
