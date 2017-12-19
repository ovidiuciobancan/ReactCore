using System;
using Microsoft.Extensions.Logging;

using Common;
using Common.Interfaces;
using DataAccess.Interfaces;


namespace BL.Base
{
    /// <summary>
    /// Base Service
    /// </summary>
    public abstract class BaseService : IDisposable
    {
        private IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected ILogger Logger { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected ICacheProvider Cache { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected AppSettings AppSettings { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        /// <param name="appSettings"></param>
        public BaseService(IUnitOfWork unitOfWork, ILogger logger = null, ICacheProvider cache = null, AppSettings appSettings = null)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
            Cache = cache;
            AppSettings = appSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UnitOfWork != null)
                {
                    UnitOfWork.Dispose();
                    UnitOfWork = null;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
