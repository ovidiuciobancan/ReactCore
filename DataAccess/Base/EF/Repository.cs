
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DA.Base
{
    public class Repository<TEntity> : CrudRepository<TEntity>
        where TEntity : class, new()
    {
        public Repository(ApplicationDbContext context)
            : base(context)
        {
            
        }

        protected new ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)base.Context;
            }
        }
    }
}
