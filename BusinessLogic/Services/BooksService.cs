using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Base;
using Common;
using Common.Interfaces;
using DA.UnitsOfWork;
using DataTransferObjects.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class BooksService : Service<AppUnitOfWork>
    {
        public BooksService(AppUnitOfWork unitOfWork, ILogger<BooksService> logger) 
            : base(unitOfWork, logger)
        {
        }

        public bool Exists(Guid id)
        {
            return Query(uow => uow.Queryable<Book>().Any(p => p.Id == id));
        }

        public IQueryable<Book> Get()
        {
            return Query(uow => uow.Queryable<Book>());
        }
        public IQueryable<Book> GetByParent(Guid parentId)
        {
            return Query(uow => 
                uow.Queryable<Book>()
                    .Where(p => p.AuthorId == parentId)
            );
        }
        public Book Get(Guid id)
        {
            return Query(uow => uow.Repository<Book>().Get(id));
        }

        public void Add(Book book)
        {
            Transaction(uow => uow.Repository<Book>().Add(book));
        }

        public void Remove(Guid id)
        {
            Transaction(uow => uow.Repository<Book>().Remove(id));
        }

        public void Update(Book entity)
        {
            Transaction();
        }
    }
}
