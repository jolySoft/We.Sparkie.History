using System;

namespace We.Sparkie.History.Api.Domain
{
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