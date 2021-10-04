using System.Collections.Generic;
using System.Threading.Tasks;
using We.Sparkie.History.Api.Repository;

namespace We.Sparkie.History.Api.Domain
{
    public class EventProcessor
    {
        private readonly IDomainEventRepository<DomainEvent> _eventRepository;

        public EventProcessor(IDomainEventRepository<DomainEvent> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Process(DomainEvent evt)
        {
            evt.Process();
            await _eventRepository.Insert(evt);
        }
    }
}