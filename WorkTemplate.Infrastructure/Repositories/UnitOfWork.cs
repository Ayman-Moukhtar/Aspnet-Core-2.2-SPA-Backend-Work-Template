using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTemplate.Domain.Repository.UnitOfWork;
using WorkTemplate.Infrastructure.DbContext;

namespace WorkTemplate.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        private readonly WorkTemplateContext _context;
        #endregion

        #region Constructor

        public UnitOfWork(WorkTemplateContext context) => _context = context;
        #endregion

        #region IQueryableUnitOfWork members
        public void BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            //attach and set as unchanged
            //attach automatically set to uncahnged ??
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
        }

        public int Commit()
        {
            var result = _context.SaveChanges();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Microsoft.EntityFrameworkCore.DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            _context.Entry(item).CurrentValues.Properties
                .Select(_ => _.Name)
                .ToList()
                .ForEach(p => _context.Entry(item).Property(p).IsModified = true);
        }
        #endregion
    }
}
