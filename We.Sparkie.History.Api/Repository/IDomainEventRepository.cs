using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using We.Sparkie.History.Api.Domain;

namespace We.Sparkie.History.Api.Repository
{
    public interface IDomainEventRepository<in TEntity> where TEntity : DomainEvent
    {
        Task Insert(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Delete(Guid id);
    }
}