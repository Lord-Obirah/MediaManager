using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;
using MediaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediaManager.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : AbstractController<Band, BandReadApi, BandListReadApi, BandWriteApi>
    {
        /// <inheritdoc />
        public BandsController(IRepository<Band> repository,
                                    IMapper mapper,
                                    ILogger<BandsController> logger,
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
        [HttpGet(Name = "GetBands")]
        public override IActionResult GetEntities([FromQuery] QueryStringParameters queryStringParameters)
        {
            return GetEntitiesImpl(queryStringParameters);
        }

        //[HttpGet("{id}/bands")]
        //public IActionResult GetRecordsForBand(Guid id, [FromQuery] QueryStringParameters queryStringParameters)
        //{
        //    //TODO use Middleware
        //    if (!Repository.Exists(id))
        //    {
        //        return NotFound();
        //    }

        //    var entities = _BandRepository.GetMoviesForBand(band, queryStringParameters);

        //    return Ok(Mapper.Map<PagedList<Movie>>(entities));
        //}
    }
}