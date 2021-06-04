using System.Collections.Generic;

namespace We.Sparkie.History.Api.Domain
{
    public class EventProcessor
    {
        public List<DomainEvent> Log { get; set; }

        public EventProcessor()
        {
            Log = new List<DomainEvent>();
        }

        public void Process(DomainEvent evt)
        {
            evt.Process();
            Log.Add(evt);
        }
    }
}