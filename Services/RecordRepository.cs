using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public class RecordRepository : AbstractRepository<Record>
    {

        /// <inheritdoc />
        public RecordRepository(MediaContext context, IPropertyMappingService propertyMappingService)
            : base(context, propertyMappingService)
        {
        }

        /// <inheritdoc />
        public override IQueryable<Record> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.Records.ApplySorting(queryStringParameters.OrderBy, PropertyMappingService.GetPropertyMapping<RecordReadApi, Record>());
            return GetOrderedList(query, queryStringParameters);
        }

        /// <inheritdoc />
        public override IOrderedQueryable<Record> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters)
        {
            var query = Context.Records.Where(w => entityIds.Contains(w.Id)).AsQueryable();


            query = query.OrderBy(o => o.Title);
            return (IOrderedQueryable<Record>)query;
        }

        /// <inheritdoc />
        public override Record GetEntity(Guid entityId)
        {
            return Context.Records.FirstOrDefault(f => f.Id == entityId);
        }

        /// <inheritdoc />
        public override void AddEntity(Record entity)
        {
            Context.Records.Add(entity);
        }

        /// <inheritdoc />
        public override void DeleteEntity(Record entity)
        {
            Context.Records.Remove(entity);
        }

        /// <inheritdoc />
        public override void UpdateEntity(Record entity)
        {
            //no code to execute
        }

        /// <inheritdoc />
        public override bool Exists(Guid entityId)
        {
            return Context.Records.Any(a => a.Id == entityId);
        }

        private IQueryable<Record> GetOrderedList(IOrderedQueryable<Record> entities, QueryStringParameters queryStringParameters = null)
        {
            var queryable = entities;

            IQueryable<Record> returnValue = null;
            if (queryStringParameters != null)
            {
                if (!string.IsNullOrWhiteSpace(queryStringParameters.SearchQuery))
                {
                    var searchQuery = queryStringParameters.SearchQuery.Trim().ToLower();
                    returnValue = queryable.Where(w => w.Title.ToLower().Contains(searchQuery) || w.Band.Name.ToLower().Contains(searchQuery));
                }
            }

            return returnValue ?? queryable;
        }
    }
}