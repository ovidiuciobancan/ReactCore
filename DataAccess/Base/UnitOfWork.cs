using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.SqlClient;
using DataAccess.Interfaces;
using DataAccess.Exceptions;

namespace DA.Base
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        protected TContext Context;


        public UnitOfWork(TContext context)
         {
            Context = context;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ManageableException(e);
            }
            catch (DbUpdateException e)
            {
                throw new ManageableException(e);
            }
            catch (RetryLimitExceededException e)
            {
                throw new ManageableException(e);
            }
            catch(SqlException e)
            {
                throw new UnmanageableException(e);
            }
            catch(Exception e)
            {
                throw new UnmanageableException(e);
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
