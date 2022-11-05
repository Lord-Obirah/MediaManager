using AutoMapper;

namespace MediaManager.Entities
{
    public class BandProfile : Profile
    {
        public BandProfile()
        {
            CreateMap<Band, BandReadApi>();
            CreateMap<Band, BandListReadApi>();
            CreateMap<BandWriteApi, Band>();
        }
    }
}