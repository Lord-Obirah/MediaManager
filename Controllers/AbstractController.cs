using System;
using System.Linq;
using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;
using MediaManager.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediaManager.Controllers
{
    public abstract class AbstractController<TData, TDataReadApi, TDataListReadApi, TDataWriteApi> : Controller
        where TData : IID
        where TDataWriteApi: class, new()
    {
        protected readonly IRepository<TData> Repository;
        protected readonly IMapper Mapper;
        private readonly ILogger<AbstractController<TData, TDataReadApi, TDataListReadApi, TDataWriteApi>> _Logger;
        private readonly IConfigurationRepository _Configuration;
        private readonly IUrlHelper _UrlHelper;

        protected AbstractController(IRepository<TData> repository, 
                                     IMapper mapper, 
                                     ILogger<AbstractController<TData, TDataReadApi, TDataListReadApi, TDataWriteApi>> logger, 
                                     IConfigurationRepository configuration, 
                                     IUrlHelper urlHelper)
        {
            Repository = repository;
            Mapper = mapper;
            _Logger = logger;
            _Configuration = configuration;
            _UrlHelper = urlHelper;
        }

        public abstract IActionResult GetEntities([FromQuery] QueryStringParameters queryStringParameters);

        protected IActionResult GetEntitiesImpl([FromQuery] QueryStringParameters queryStringParameters)
        {
            var attribute = GetType()
                            .GetMethod("GetEntities")
                            .GetCustomAttributes(true)
                            .FirstOrDefault(w => w is HttpGetAttribute) as HttpGetAttribute;

            if (attribute == null)
            {
                throw new Exception($"Unable to get name for HttpGetAttribute, attribute missing");
            }

            var queryable = Repository.GetEntities(queryStringParameters);
            var entities = PagedList<TDataListReadApi>.Create(queryable, queryStringParameters, Mapper);
            var previousPageLink = entities.HasPrevious ? CreateNavigationObject(attribute.Name, queryStringParameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = entities.HasNext ? CreateNavigationObject(attribute.Name, queryStringParameters, ResourceUriType.NextPage) : null;
            var currentPageLink = CreateNavigationObject(attribute.Name, queryStringParameters, ResourceUriType.Current);
            var paginationMetadata = new
            {
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink,
                currentPageLink = currentPageLink,
                totalCount = entities.TotalCount,
                pageSize = entities.PageSize,
                currentPage = entities.CurrentPage,
                totalPages = entities.TotalPages
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            Response.Headers.AccessControlExposeHeaders = "X-Pagination";

            // var output = Mapper.Map<IEnumerable<TDataOutput>>(entities);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public IActionResult GetEntity(Guid id)
        {
            var entity = Repository.GetEntity(id);

            if (entity == null)
            {
                return NotFound();
            }

            var output = Mapper.Map<TDataReadApi>(entity);
            return Ok(output);
        }

        [HttpPost]
        public IActionResult CreateEntity([FromBody] TDataWriteApi input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessibleEntityObjectResult(ModelState);
            }

            var result = ValidateInput(input);

            if (result != null)
            {
                return result;
            }

            var entity = Mapper.Map<TData>(input);
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            Repository.AddEntity(entity);

            ExecuteSave("Creating", entity.Id);

            return GetCreatedAtActionResult(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntity(Guid id)
        {
            if (!Repository.Exists(id))
            {
                return NotFound();
            }

            var entity = Repository.GetEntity(id);
            Repository.DeleteEntity(entity);

            ExecuteSave("Deleting", id);

            _Logger.LogInformation(100, $"entity id = '{id}', type = '{typeof(TData).Name}' was deleted");
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEntity(Guid id, [FromBody] TDataWriteApi input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            var entity = Repository.GetEntity(id);
            if (entity == null)
            {
                entity = Mapper.Map<TData>(input);
                entity.Id = id;
                Repository.AddEntity(entity);

                ExecuteSave("Upserting", id);

                return GetCreatedAtActionResult(entity);
            }

            Mapper.Map(input, entity);
            Repository.UpdateEntity(entity);

            ExecuteSave("Updating", id);

            return Ok(input);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchEntity(Guid id, [FromBody] JsonPatchDocument<TDataWriteApi> input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            var entity = Repository.GetEntity(id);
            if (entity == null)
            {
                var update = new TDataWriteApi();
                input.ApplyTo(update);

                entity = Mapper.Map<TData>(update);
                entity.Id = id;

                Repository.AddEntity(entity);

                ExecuteSave("Upsert patching", id);

                return GetCreatedAtActionResult(entity);
            }

            var patch = Mapper.Map<TDataWriteApi>(entity);
            input.ApplyTo(patch, ModelState);

            //add validation

            Mapper.Map(patch, entity);
            Repository.UpdateEntity(entity);

            ExecuteSave("Patching", id);

            return NoContent();
        }

        protected virtual IActionResult ValidateInput(TDataWriteApi input)
        {
            return null;
        }

        private void ExecuteSave(string action, Guid id)
        {
            if (!Repository.Save())
            {
                throw new Exception(GetErrorMessage(action, id));
            }
        }

        private string GetErrorMessage(string action, Guid id)
        {
            return $"{action} entity failed: id = '{id}', type = '{typeof(TData).Name}'";
        }

        private IActionResult GetCreatedAtActionResult(TData entity)
        {
            var output = Mapper.Map<TDataWriteApi>(entity);
            return CreatedAtAction(nameof(GetEntity), new { id = entity.Id }, output);
        }

        private NavigationObject CreateNavigationObject(string routeName, QueryStringParameters queryStringParameters, ResourceUriType resourceUriType)
        {
            NavigationObject navigationObjectValue = null;
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    navigationObjectValue = GetNavigationObjectValues(-1);
                    break;
                case ResourceUriType.NextPage:
                    navigationObjectValue = GetNavigationObjectValues(1);
                    break;
                case ResourceUriType.Current:
                default:
                    navigationObjectValue = GetNavigationObjectValues(0);
                    break;

            }

            var url = _UrlHelper.Link(routeName, navigationObjectValue);
            navigationObjectValue.url = url;
            return navigationObjectValue;

            NavigationObject GetNavigationObjectValues(int pageOffset)
            {
                return new NavigationObject()
                {
                           fields = queryStringParameters.Fields,
                           orderBy = queryStringParameters.OrderBy,
                           searchQuery = queryStringParameters.SearchQuery,
                           filter = queryStringParameters.Filter,
                           page = queryStringParameters.Page + pageOffset,
                           pageSize = queryStringParameters.PageSize,
                           url = string.Empty
                       };
            }
        }
    }

    public class NavigationObject
    {
        public string fields { get; set; }
        public string orderBy { get; set; }
        public string searchQuery { get; set; }
        public string filter { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string url { get; set; }
    }
}