using System.Linq;
using DA.Base;

namespace DA.UnitsOfWork
{
    public class AppUnitOfWork : UnitOfWork<ApplicationDbContext>
    {
        public AppUnitOfWork(ApplicationDbContext context)
            : base(context)
        {
        }

        public Repository<T> Repository<T>()
            where T : class, new()
        {
            return new Repository<T>(Context);
        }

        public IQueryable<T> Queryable<T>()
            where T : class, new()
        {
            return new Repository<T>(Context).Query;
        }


    }
}
