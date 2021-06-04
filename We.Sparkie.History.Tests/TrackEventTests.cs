using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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

    public class EventProcessor
    {
        public List<DomainEvent> Log { get; set; }

        public EventProcessor()
        {
            Log = new List<DomainEvent>();
        }

        public void Process(DomainEvent evt)
        {
            evt.Process();
            Log.Add(evt);
        }
    }

    public class Track
    {
        public string TrackName { get; set; }

        public string ArtistName { get; set; }

        public Guid TrackId { get; set; }

        public Guid DigitalAssetId { get; set; }

        public TimeSpan Position { get; private set; }

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
    }

    public abstract class TrackEvent : DomainEvent
    {
        public Track Track { get; protected set; }

        protected TrackEvent(Track track, DateTime occurred) : base(occurred)
        {
            Track = track;
        }
    }

    public class StartTrackEvent : TrackEvent
    {
        public StartTrackEvent(Track track, DateTime occurred) : base(track, occurred) {}
   
        public override void Process()
        {
            Track.HandleStart(this);
        }
    }

    public abstract class DomainEvent
    {
        public DateTime Occurred { get; set; }

        protected DomainEvent(DateTime occurred)
        {
            Occurred = occurred;
        }

        public abstract void Process();
    }
}