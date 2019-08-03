using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkTemplate.Domain.Repository.UnitOfWork
{
    using Microsoft.EntityFrameworkCore;
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
        void Attach<TEntity>(TEntity item) where TEntity : class;
        void SetModified<TEntity>(TEntity item) where TEntity : class;
        void BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
