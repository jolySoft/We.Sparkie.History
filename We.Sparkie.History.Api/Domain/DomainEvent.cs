using System;
using MongoDB.Bson.Serialization.Attributes;

namespace We.Sparkie.History.Api.Domain
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(TrackEvent))]
    public abstract class DomainEvent
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime Occurred { get; set; }

        protected DomainEvent()
        {
            Occurred = DateTime.Now;
        }

        public abstract void Process();
    }
}