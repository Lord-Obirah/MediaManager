using AutoMapper;

namespace MediaManager.Entities
{
    public class FskRatingProfile : Profile
    {
        public FskRatingProfile()
        {
            CreateMap<FskRating, FskRatingReadApi>();
            CreateMap<FskRating, FskRatingListReadApi>();
            CreateMap<FskRatingWriteApi, FskRating>();
        }
    }
}