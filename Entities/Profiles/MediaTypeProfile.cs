using AutoMapper;

namespace MediaManager.Entities
{
    public class MediaTypeProfile : Profile
    {
        public MediaTypeProfile()
        {
            CreateMap<MediaType, MediaTypeReadApi>();
            CreateMap<MediaType, MediaTypeListReadApi>();
            CreateMap<MediaTypeWriteApi, MediaType>();
        }
    }
}