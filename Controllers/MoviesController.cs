using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;
using MediaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediaManager.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : AbstractController<Movie, MovieReadApi, MovieListReadApi, MovieWriteApi>
    {
        private readonly IRepository<MediaType> _MediaTypeRepository;

        /// <inheritdoc />
        public MoviesController(IRepository<Movie> repository,
            IRepository<MediaType> mediaTypeRepository,
            IMapper mapper,
            ILogger<MoviesController> logger,
            IConfigurationRepository configuration,
            IUrlHelper urlHelper)
            : base(repository, mapper, logger, configuration, urlHelper)
        {
            _MediaTypeRepository = mediaTypeRepository;
        }

        /// <inheritdoc />
        [HttpGet(Name = "GetMovies")]
        public override IActionResult GetEntities(QueryStringParameters queryStringParameters)
        {
            return GetEntitiesImpl(queryStringParameters);
        }

        /// <inheritdoc />
        protected override IActionResult ValidateInput(MovieWriteApi input)
        {
            return _MediaTypeRepository.Exists(input.MediaTypeId) ? null : NotFound();
        }
    }
}