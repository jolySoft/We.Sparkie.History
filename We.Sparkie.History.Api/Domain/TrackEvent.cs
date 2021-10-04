using System;
using MongoDB.Bson.Serialization.Attributes;

namespace We.Sparkie.History.Api.Domain
{
    [BsonKnownTypes(typeof(StartTrackEvent), typeof(StopTrackEvent))]
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