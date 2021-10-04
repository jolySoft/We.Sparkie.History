using System;
using System.Linq;
using System.Threading.Tasks;
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
        private EndTrackEvent _endTrackEvent;
        private EventRepositoryStub _eventRepository;

        public TrackEventTests()
        {
            _eventRepository = new EventRepositoryStub();
            _processor = new EventProcessor(_eventRepository);
            
            _track = new Track
            {
                DigitalAssetId = Guid.NewGuid(),
                TrackId = Guid.NewGuid(),
                ArtistName = "Dead Kennedys",
                TrackName = "Too Drunk to Fuck"
            };
        }

        [Fact]
        public async Task HandleTrackStartEvent()
        {
            _startTrack = new StartTrackEvent(_track, DateTime.Now);

            await _processor.Process(_startTrack);
            
            var firstLogEvent = (StartTrackEvent) _eventRepository.Log.First();
            firstLogEvent.Should().BeEquivalentTo(_startTrack);
            _track.Finished.Should().BeFalse();
        }

        [Fact]
        public async Task HandleTrackStopEvent()
        {
            var position = new TimeSpan(0, 3, 37);
            _stopTrack = new StopTrackEvent(_track, DateTime.Now, position);

            await _processor.Process(_stopTrack);

            var firstLogEvent = (StopTrackEvent) _eventRepository.Log.First();
            firstLogEvent.Should().BeEquivalentTo(_stopTrack);
            _track.Position.Should().Be(position);
        }

        [Fact]
        public async Task HandleTackCompleteEvent()
        {
            _endTrackEvent = new EndTrackEvent(_track);

            await _processor.Process(_endTrackEvent);

            var firstLogEvent = (EndTrackEvent) _eventRepository.Log.First();
            firstLogEvent.Should().BeEquivalentTo(_endTrackEvent);
            _track.Finished = true;
            _track.Position.Should().Be(_track.Length);
        }
    }
}