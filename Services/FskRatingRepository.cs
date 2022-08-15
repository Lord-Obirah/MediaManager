using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public class FskRatingRepository : AbstractRepository<FskRating>
    {

        /// <inheritdoc />
        public FskRatingRepository(MediaContext context, IPropertyMappingService propertyMappingService)
            : base(context, propertyMappingService)
        {
        }

        /// <inheritdoc />
        public override IQueryable<FskRating> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.FskRatings.ApplySorting(queryStringParameters.OrderBy, PropertyMappingService.GetPropertyMapping<FskRatingReadApi, FskRating>());
            return GetOrderedList(query, queryStringParameters);
        }

        /// <inheritdoc />
        public override IOrderedQueryable<FskRating> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters)
        {
            var query = Context.FskRatings.Where(w => entityIds.Contains(w.Id)).AsQueryable();


            query = query.OrderBy(o => o.Name);
            return (IOrderedQueryable<FskRating>)query;
        }

        /// <inheritdoc />
        public override FskRating GetEntity(Guid entityId)
        {
            return Context.FskRatings.FirstOrDefault(f => f.Id == entityId);
        }

        /// <inheritdoc />
        public override void AddEntity(FskRating entity)
        {
            Context.FskRatings.Add(entity);
        }

        /// <inheritdoc />
        public override void DeleteEntity(FskRating entity)
        {
            Context.FskRatings.Remove(entity);
        }

        /// <inheritdoc />
        public override void UpdateEntity(FskRating entity)
        {
            //no code to execute
        }

        /// <inheritdoc />
        public override bool Exists(Guid entityId)
        {
            return Context.FskRatings.Any(a => a.Id == entityId);
        }

        private IQueryable<FskRating> GetOrderedList(IOrderedQueryable<FskRating> entities, QueryStringParameters queryStringParameters = null)
        {
            var queryable = entities;

            IQueryable<FskRating> returnValue = null;
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