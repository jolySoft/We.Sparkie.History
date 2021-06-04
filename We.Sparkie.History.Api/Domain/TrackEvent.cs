using System;

namespace We.Sparkie.History.Api.Domain
{
    public abstract class TrackEvent : DomainEvent
    {
        public Track Track { get; protected set; }

        protected TrackEvent(Track track, DateTime occurred) : base(occurred)
        {
            Track = track;
        }
    }
}