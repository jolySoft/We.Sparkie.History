using System;
using System.Linq;
using FluentAssertions;
using We.Sparkie.History.Api.Domain;
using Xunit;

namespace We.Sparkie.History.Tests
{
    public class TrackEventTests
    {
        private EventProcessor _processor;
        private Track _track;
        private StartTrackEvent _startTrack;
        private StopTrackEvent _stopTrack;

        public TrackEventTests()
        {
            _processor = new EventProcessor();            
            
            _track = new Track
            {
                DigitalAssetId = Guid.NewGuid(),
                TrackId = Guid.NewGuid(),
                ArtistName = "Dead Kennedys",
                TrackName = "Too Drunk to Fuck"
            };
        }

        [Fact]
        public void HandleTrackStartEvent()
        {
            _startTrack = new StartTrackEvent(_track, DateTime.Now);

            _processor.Process(_startTrack);
            
            var firstLogEvent = (StartTrackEvent) _processor.Log.First();
            firstLogEvent.Should().BeEquivalentTo(_startTrack);
        }

        [Fact]
        public void HandleTrackStopEvent()
        {
            var position = new TimeSpan(0, 3, 37);
            _stopTrack = new StopTrackEvent(_track, DateTime.Now, position, false);

            _processor.Process(_stopTrack);

            var firstLogEvent = (StopTrackEvent) _processor.Log.First();
            firstLogEvent.Should().BeEquivalentTo(_stopTrack);
        }
    }
}