using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;
using MediaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediaManager.Controllers
{
    [ApiController]
    [Route("api/records")]
    public class RecordsController : AbstractController<Record, RecordReadApi, RecordListReadApi, RecordWriteApi>
    {

        /// <inheritdoc />
        public RecordsController(IRepository<Record> repository,
            IMapper mapper,
            ILogger<RecordsController> logger,
            IConfigurationRepository configuration,
            IUrlHelper urlHelper)
            : base(repository, mapper, logger, configuration, urlHelper)
        {
        }

        /// <inheritdoc />
        [HttpGet(Name = "GetRecords")]
        public override IActionResult GetEntities(QueryStringParameters queryStringParameters)
        {
            return GetEntitiesImpl(queryStringParameters);
        }
    }
}