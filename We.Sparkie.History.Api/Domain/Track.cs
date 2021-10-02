using System;

namespace We.Sparkie.History.Api.Domain
{
    public class Track
    {
        public string TrackName { get; set; }

        public string ArtistName { get; set; }

        public Guid TrackId { get; set; }

        public Guid DigitalAssetId { get; set; }

        public TimeSpan Position { get; private set; }

        public TimeSpan Length { get; set; }

        public bool Finished { get; set; }

        public bool IsPlaying { get; private set; }

        public void HandleStart(StartTrackEvent evt)
        {
            Position = TimeSpan.Zero;
            IsPlaying = true;
        }

        public void HandleStopEvent(StopTrackEvent evt)
        {
            IsPlaying = false;
            Finished = evt.Finished;
            Position = evt.Position;
        }

        public void HandleEndTrack()
        {
            IsPlaying = false;
            Finished = true;
            Position = Length;
        }
    }
}