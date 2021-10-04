using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using We.Sparkie.History.Api.Domain;
using We.Sparkie.History.Api.Dto;
using We.Sparkie.History.Tests;

namespace We.Sparkie.History.Api.Controllers
{
    [ApiController]
    public class PlayerEventController : ControllerBase
    {
        private readonly EventProcessor _eventProcessor;
        private readonly IMapper _mapper;


        public PlayerEventController(EventProcessor eventProcessor, IMapper mapper)
        {
            _eventProcessor = eventProcessor;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PlayerEvent playerEvent)
        {
            TrackEvent trackEvent;
            switch (playerEvent.TrackEventType)
            {
                case TrackEventTypes.Start:
                    trackEvent = _mapper.Map<StartTrackEvent>(playerEvent);
                    break;
                case TrackEventTypes.Stop:
                    trackEvent = _mapper.Map<StopTrackEvent>(playerEvent);
                    break;
                case TrackEventTypes.End:
                    trackEvent = _mapper.Map<EndTrackEvent>(playerEvent);
                    break;
                default:
                    return BadRequest("Unknown Track Event Type");
            }

            await _eventProcessor.Process(trackEvent);

            return NoContent();
        }
    }
}