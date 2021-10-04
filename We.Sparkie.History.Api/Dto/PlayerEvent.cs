using System;

namespace We.Sparkie.History.Api.Dto
{
    public class PlayerEvent
    {
        public TrackEventTypes TrackEventType { get; set; }

        public string TrackName { get; set; }

        public string ArtistName { get; set; }

        public Guid TrackId { get; set; }

        public Guid DigitalAssetId { get; set; }

        public TimeSpan Position { get; set; }
    }
}