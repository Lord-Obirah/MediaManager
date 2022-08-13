using AutoMapper;

namespace MediaManager.Entities
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadApi>();

            CreateMap<Movie, MovieListReadApi>()
                .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType.Name));

            CreateMap<MovieWriteApi, Movie>();
        }
    }
}