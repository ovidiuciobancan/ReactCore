using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using BL.Base;
using DA.UnitsOfWork;
using Common.Interfaces;
using Utils.Helpers;
using Utils.Extensions;

namespace BusinessLogic.Base
{
    public class EntityService<T> : Service<AppUnitOfWork>
        where T: class, IEntity, new()
    {
        public EntityService(AppUnitOfWork unitOfWork, ILogger<EntityService<T>> logger, ICacheProvider cache = null, IAppSettings appSettings = null) 
            : base(unitOfWork, logger, cache, appSettings) { }

        public bool Exists(Guid id)
        {
            return Query(uow => uow.Queryable<T>().Any(p => p.Id == id));
        }
        public IQueryable<T> Get()
        {
            return Query(uow => uow.Queryable<T>());
        }
        public IQueryable<T> Get(IEnumerable<Guid> ids)
        {
            return Query(uow => uow.Queryable<T>().Where(p => ids.Contains(p.Id)));
        }
        public PagedList<T> Get(int pageNumber, int pageSize)
        {
            return Get().ToPagedList(pageNumber, pageSize);
        }
        public T Get(Guid id)
        {
            return Query(uow => uow.Repository<T>().Get(id));
        }
        public void Add(T entity)
        {
            Transaction(uow =>
            {
                uow.Repository<T>().Add(entity);
            });
        }
        public void AddRange(List<T> entities)
        {
            Transaction(uow =>
            {
                uow.Repository<T>().AddRange(entities);
            });
        }
        public void Remove(Guid id)
        {
            Transaction(uow =>
            {
                uow.Repository<T>().Remove(id);
            });
        }
        public void Update(T entity)
        {
            Transaction();
        }
    }
}
