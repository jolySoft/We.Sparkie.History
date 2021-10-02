using System;

namespace We.Sparkie.History.Api.Domain
{
    public class StartTrackEvent : TrackEvent
    {
        public StartTrackEvent(Track track, DateTime occurred) : base(track) {}
   
        public override void Process()
        {
            Track.HandleStart(this);
        }
    }
}