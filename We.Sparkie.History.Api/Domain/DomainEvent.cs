using System;

namespace We.Sparkie.History.Api.Domain
{
    public abstract class DomainEvent
    {
        public DateTime Occurred { get; set; }

        protected DomainEvent()
        {
            Occurred = DateTime.Now;
        }

        public abstract void Process();
    }
}