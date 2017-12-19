using System;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
