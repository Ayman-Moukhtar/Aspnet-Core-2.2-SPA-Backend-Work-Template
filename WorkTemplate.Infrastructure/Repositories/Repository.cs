using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTemplate.Domain.Repository.Repositories;
using WorkTemplate.Domain.Repository.UnitOfWork;

namespace WorkTemplate.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        protected DbSet<TEntity> GetSet()
        {
            return UnitOfWork.CreateSet<TEntity>();
        }

        #region Constructor

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork { get; }

        public Task<TEntity> GetAsync(object[] keyValues)
        {
            //IDbSet don`t have a FindAsync, a work around it to cast to Dbset losing the benifits of abstraction
            return keyValues != null ? ((DbSet<TEntity>)GetSet()).FindAsync(keyValues) : null;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }

        public virtual void Add(TEntity item)
        {
            if (item != null)
            {
                GetSet().Add(item); // add new item in this set
            }
            else
            {
            }
        }

        public virtual void Delete(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
            }
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                UnitOfWork.Attach(item);
            else
            {
            }
        }

        public virtual void Update(TEntity item)
        {
            if (item != null)
            {
                //this operation also attach item in object state manager
                UnitOfWork.SetModified(item);
            }
            else
            {
            }
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities)
        {
            UnitOfWork.BulkInsert(entities);
        }
        #endregion

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
