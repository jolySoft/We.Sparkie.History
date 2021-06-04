using System;

namespace We.Sparkie.History.Api.Domain
{
    public abstract class DomainEvent
    {
        public DateTime Occurred { get; set; }

        protected DomainEvent(DateTime occurred)
        {
            Occurred = occurred;
        }

        public abstract void Process();
    }
}