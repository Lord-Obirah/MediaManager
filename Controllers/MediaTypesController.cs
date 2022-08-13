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
    [Route("api/mediatypes")]
    public class MediaTypesController : AbstractController<MediaType, MediaTypeReadApi, MediaTypeListReadApi, MediaTypeWriteApi>
    {
        private readonly IMovieRepository _MovieRepository;

        /// <inheritdoc />
        public MediaTypesController(IRepository<MediaType> repository,
                                    IMovieRepository movieRepository,
                                    IMapper mapper,
                                    ILogger<MediaTypesController> logger,
                                    IConfigurationRepository configuration,
                                    IUrlHelper urlHelper)
            : base(repository,
                   mapper,
                   logger,
                   configuration,
                   urlHelper)
        {
            _MovieRepository = movieRepository;
        }

        /// <inheritdoc />
        [HttpGet(Name = "GetMediaTypes")]
        public override IActionResult GetEntities([FromQuery] QueryStringParameters queryStringParameters)
        {
            return GetEntitiesImpl(queryStringParameters);
        }

        [HttpGet("{mediaTypeId}/movies")]
        public IActionResult GetMoviesForMediaType(Guid mediaTypeId, [FromQuery] QueryStringParameters queryStringParameters)
        {
            //TODO use Middleware
            if (!Repository.Exists(mediaTypeId))
            {
                return NotFound();
            }

            var entities = _MovieRepository.GetMoviesForMediaType(mediaTypeId, queryStringParameters);

            return Ok(Mapper.Map<PagedList<Movie>>(entities));
        }
    }
}