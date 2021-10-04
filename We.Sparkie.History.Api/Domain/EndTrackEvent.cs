using We.Sparkie.History.Api.Domain;

namespace We.Sparkie.History.Tests
{
    public class EndTrackEvent : TrackEvent
    {
        public EndTrackEvent(Track track) : base(track)
        {
            Finished = true;
        }

        public override void Process()
        {
            Track.HandleEndTrack();
        }
    }
}