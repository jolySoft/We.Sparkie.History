using System;
using MongoDB.Bson.Serialization.Attributes;

namespace We.Sparkie.History.Api.Domain
{
    [BsonDiscriminator(nameof(StopTrackEvent))]
    public class StopTrackEvent : TrackEvent
    {
        public TimeSpan Position { get; }

        public StopTrackEvent(Track track, DateTime occurred, TimeSpan position) : base(track)
        {
            Position = position;
            Finished = false;
        }

        public override void Process()
        {
            Track.HandleStopEvent(this);
        }
    }
}