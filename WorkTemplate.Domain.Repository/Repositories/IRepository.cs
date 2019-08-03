using System;
using System.Collections.Generic;
using System.Linq;
using WorkTemplate.Domain.Repository.UnitOfWork;

namespace WorkTemplate.Domain.Repository.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetAll();
        void Add(TEntity item);
        void Delete(TEntity item);
        void Update(TEntity item);
        void BulkInsert(IEnumerable<TEntity> entities);
    }
}
