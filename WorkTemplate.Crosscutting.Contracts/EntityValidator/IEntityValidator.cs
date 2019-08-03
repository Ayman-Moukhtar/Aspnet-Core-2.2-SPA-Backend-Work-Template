using System.Collections.Generic;

namespace WorkTemplate.Croscutting.Contracts.EntityValidator
{
    public interface IEntityValidator
    {
        bool IsValid<TEntity>(TEntity item)
            where TEntity : class;

        ICollection<string> GetInvalidMessages<TEntity>(TEntity item)
            where TEntity : class;

        void ValidateAndThrow<TEntity>(TEntity item)
            where TEntity : class;
    }
}