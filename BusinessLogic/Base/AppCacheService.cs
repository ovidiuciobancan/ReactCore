using System.Collections.Generic;
using System.Linq;
using BL.Base;
using Common.Interfaces;
using Utils;
using DA.UnitsOfWork;
using DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace BL.Services
{
    /// <summary>
    /// Cache Service
    /// </summary>
    [IgnoreInjection]
    public class AppCacheService : Service<AppUnitOfWork>, ICacheService
    {
        public AppCacheService(AppUnitOfWork unitOfWork, ILogger<AppCacheService> logger) : 
            base(unitOfWork, logger)
        {

        }

        public IEnumerable<ICacheItem> Get()
        {
            return Query(uow => uow.Queryable<AppCacheItems>().ToList());
        }
    }
}
