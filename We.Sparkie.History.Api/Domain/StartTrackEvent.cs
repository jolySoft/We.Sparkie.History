using System;
using MongoDB.Bson.Serialization.Attributes;

namespace We.Sparkie.History.Api.Domain
{
    [BsonDiscriminator(nameof(StartTrackEvent))]
    public class StartTrackEvent : TrackEvent
    {
        public StartTrackEvent(Track track, DateTime occurred) : base(track) {}
   
        public override void Process()
        {
            Track.HandleStart(this);
        }
    }
}