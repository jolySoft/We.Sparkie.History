using AutoMapper;
using We.Sparkie.History.Api.Domain;
using We.Sparkie.History.Tests;

namespace We.Sparkie.History.Api.Dto
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<PlayerEvent, StartTrackEvent>();
            CreateMap<PlayerEvent, StopTrackEvent>();
            CreateMap<PlayerEvent, EndTrackEvent>();
        }
    }
}