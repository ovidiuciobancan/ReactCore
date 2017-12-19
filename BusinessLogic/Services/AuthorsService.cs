using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Base;
using Common;
using Common.Interfaces;
using DA.Base;
using DA.UnitsOfWork;
using DataAccess.Interfaces;
using DataTransferObjects.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class AuthorsService : Service<AppUnitOfWork>
    {
        public AuthorsService(AppUnitOfWork unitOfWork, ILogger<AuthorsService> logger) 
            : base(unitOfWork, logger)
        {
        }

        public bool Exists(Guid id)
        {
            return Query(uow => uow.Queryable<Author>().Any(p => p.Id == id));
        }

        public IQueryable<Author> Get()
        {
            return Query(uow => uow.Queryable<Author>());
        }

        public IQueryable<Author> Get(IEnumerable<Guid> ids)
        {
            return Query(uow => uow.Queryable<Author>().Where(p => ids.Contains(p.Id)));
        }

        public Author Get(Guid id)
        {
            return Query(uow => uow.Repository<Author>().Get(id));
        }

        public void Add(Author entity)
        {
            Transaction(uow =>
            {
                uow.Repository<Author>().Add(entity);
            });
        }

        public void AddRange(List<Author> entities)
        {
            Transaction(uow =>
            {
                uow.Repository<Author>().AddRange(entities);
            });
        }

        public void Remove(Guid id)
        {
            Transaction(uow =>
            {
                uow.Repository<Author>().Remove(id);
            });
        }
    }
}
