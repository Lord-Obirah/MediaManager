using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public class MediaTypeRepository : AbstractRepository<MediaType>
    {

        /// <inheritdoc />
        public MediaTypeRepository(MediaContext context, IPropertyMappingService propertyMappingService)
            : base(context, propertyMappingService)
        {
        }

        /// <inheritdoc />
        public override IQueryable<MediaType> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.MediaTypes.ApplySorting(queryStringParameters.OrderBy, PropertyMappingService.GetPropertyMapping<MediaTypeReadApi, MediaType>());
            return GetOrderedList(query, queryStringParameters);
        }

        /// <inheritdoc />
        public override IOrderedQueryable<MediaType> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters)
        {
            var query = Context.MediaTypes.Where(w => entityIds.Contains(w.Id)).AsQueryable();


            query = query.OrderBy(o => o.Name);
            return (IOrderedQueryable<MediaType>)query;
        }

        /// <inheritdoc />
        public override MediaType GetEntity(Guid entityId)
        {
            return Context.MediaTypes.FirstOrDefault(f => f.Id == entityId);
        }

        /// <inheritdoc />
        public override void AddEntity(MediaType entity)
        {
            Context.MediaTypes.Add(entity);
        }

        /// <inheritdoc />
        public override void DeleteEntity(MediaType entity)
        {
            Context.MediaTypes.Remove(entity);
        }

        /// <inheritdoc />
        public override void UpdateEntity(MediaType entity)
        {
            //no code to execute
        }

        /// <inheritdoc />
        public override bool Exists(Guid entityId)
        {
            return Context.MediaTypes.Any(a => a.Id == entityId);
        }

        private IQueryable<MediaType> GetOrderedList(IOrderedQueryable<MediaType> entities, QueryStringParameters queryStringParameters = null)
        {
            var queryable = entities;

            IQueryable<MediaType> returnValue = null;
            if (queryStringParameters != null)
            {
                if (!string.IsNullOrWhiteSpace(queryStringParameters.SearchQuery))
                {
                    var searchQuery = queryStringParameters.SearchQuery.Trim().ToLower();
                    returnValue = queryable.Where(w => w.Name.ToLower().Contains(searchQuery));
                }
            }

            return returnValue ?? queryable;
        }
    }
}