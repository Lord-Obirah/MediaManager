using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public class BandRepository : AbstractRepository<Band>
    {

        /// <inheritdoc />
        public BandRepository(MediaContext context, IPropertyMappingService propertyMappingService)
            : base(context, propertyMappingService)
        {
        }

        /// <inheritdoc />
        public override IQueryable<Band> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.Bands.ApplySorting(queryStringParameters.OrderBy, PropertyMappingService.GetPropertyMapping<BandReadApi, Band>());
            return GetOrderedList(query, queryStringParameters);
        }

        /// <inheritdoc />
        public override IOrderedQueryable<Band> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters)
        {
            var query = Context.Bands.Where(w => entityIds.Contains(w.Id)).AsQueryable();


            query = query.OrderBy(o => o.Name);
            return (IOrderedQueryable<Band>)query;
        }

        /// <inheritdoc />
        public override Band GetEntity(Guid entityId)
        {
            return Context.Bands.FirstOrDefault(f => f.Id == entityId);
        }

        /// <inheritdoc />
        public override void AddEntity(Band entity)
        {
            Context.Bands.Add(entity);
        }

        /// <inheritdoc />
        public override void DeleteEntity(Band entity)
        {
            Context.Bands.Remove(entity);
        }

        /// <inheritdoc />
        public override void UpdateEntity(Band entity)
        {
            //no code to execute
        }

        /// <inheritdoc />
        public override bool Exists(Guid entityId)
        {
            return Context.Bands.Any(a => a.Id == entityId);
        }

        private IQueryable<Band> GetOrderedList(IOrderedQueryable<Band> entities, QueryStringParameters queryStringParameters = null)
        {
            var queryable = entities;

            IQueryable<Band> returnValue = null;
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