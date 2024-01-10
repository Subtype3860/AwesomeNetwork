using AwesomeNetwork.Data.Repository;
using System;

namespace AwesomeNetwork.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        int SaveChanges(bool ensureAutoHistory = false);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;
    }
}
