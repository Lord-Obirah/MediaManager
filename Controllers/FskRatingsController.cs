using System;
using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;
using MediaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediaManager.Controllers
{
    [ApiController]
    [Route("api/fskRatings")]
    public class FskRatingsController : AbstractController<FskRating, FskRatingReadApi, FskRatingListReadApi, FskRatingWriteApi>
    {
        /// <inheritdoc />
        public FskRatingsController(IRepository<FskRating> repository,
                                    IMapper mapper,
                                    ILogger<FskRatingsController> logger,
                                    IConfigurationRepository configuration,
                                    IUrlHelper urlHelper)
            : base(repository,
                   mapper,
                   logger,
                   configuration,
                   urlHelper)
        {
        }

        /// <inheritdoc />
        [HttpGet(Name = "GetFskRatings")]
        public override IActionResult GetEntities([FromQuery] QueryStringParameters queryStringParameters)
        {
            return GetEntitiesImpl(queryStringParameters);
        }

        //[HttpGet("{mediaTypeId}/movies")]
        //public IActionResult GetMoviesForMediaType(Guid mediaTypeId, [FromQuery] QueryStringParameters queryStringParameters)
        //{
        //    //TODO use Middleware
        //    if (!Repository.Exists(mediaTypeId))
        //    {
        //        return NotFound();
        //    }

        //    var entities = _MovieRepository.GetMoviesForMediaType(mediaTypeId, queryStringParameters);

        //    return Ok(Mapper.Map<PagedList<Movie>>(entities));
        //}
    }
}