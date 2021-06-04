using System;

namespace We.Sparkie.History.Api.Domain
{
    public class StopTrackEvent : TrackEvent
    {
        public TimeSpan Position { get; }
        public bool Finished { get; }

        public StopTrackEvent(Track track, DateTime occurred, TimeSpan position, bool finished) : base(track, occurred)
        {
            Position = position;
            Finished = finished;
        }

        public override void Process()
        {
            Track.HandleStopEvent(this);
        }
    }
}