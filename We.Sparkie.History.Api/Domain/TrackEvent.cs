using System;

namespace We.Sparkie.History.Api.Domain
{
    public abstract class TrackEvent : DomainEvent
    {
        public Track Track { get; protected set; }
        public bool Finished { get; protected set; }

        protected TrackEvent(Track track)
        {
            Track = track;
        }
    }
}