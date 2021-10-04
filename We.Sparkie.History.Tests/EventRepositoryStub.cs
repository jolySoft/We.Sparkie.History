using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using We.Sparkie.History.Api.Domain;
using We.Sparkie.History.Api.Repository;

namespace We.Sparkie.History.Tests
{
    public class EventRepositoryStub : IDomainEventRepository<DomainEvent>
    {
        public List<DomainEvent> Log { get;  }

        public EventRepositoryStub()
        {
            Log = new List<DomainEvent>();
        }

        public Task Insert(DomainEvent entity)
        {
            Log.Add(entity);
            return Task.CompletedTask;
        }

        public Task<bool> Delete(DomainEvent entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}