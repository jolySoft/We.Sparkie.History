using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Driver;
using We.Sparkie.History.Api.Domain;
using We.Sparkie.History.Api.Dto;
using Xunit;

namespace We.Sparkie.History.Tests
{
    public class FireEventTests : ComponentTestBase
    {
        private PlayerEvent _event;

        [Fact]
        public async Task RaiseStartEvent()
        {
            var startEvent = new PlayerEvent
            {
                TrackEventType = TrackEventTypes.Start,
                TrackId = Guid.NewGuid(),
                DigitalAssetId = Guid.NewGuid(),
                ArtistName = "Hans Zimmer",
                TrackName = "Bene Geserit",
                Position = TimeSpan.Zero
            };

            var response = await PostToAnEndpoint(startEvent, "api/playerevent/");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var collection = Database.GetCollection<StartTrackEvent>("TrackEvent");
            var filter = Builders<StartTrackEvent>.Filter.Eq(e => e.Track.TrackId, startEvent.TrackId);
            var evt = (await collection.FindAsync(filter)).FirstOrDefault();

            evt.Finished.Should().BeFalse();
            evt.Track.DigitalAssetId.Should().Be(startEvent.DigitalAssetId);
            evt.Track.ArtistName.Should().Be(startEvent.ArtistName);
            evt.Track.TrackName.Should().Be(startEvent.TrackName);

            evt.Occurred.Should().BeAfter(default);
        }
    }
}