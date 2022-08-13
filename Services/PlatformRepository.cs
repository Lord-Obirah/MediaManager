using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public class PlatformRepository : AbstractRepository<Platform>
    {
        public PlatformRepository(MediaContext context, IPropertyMappingService propertyMappingService) : base(context, propertyMappingService)
        {
        }

        /// <inheritdoc />
        public override IOrderedQueryable<Platform> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.Platforms.OrderBy(a => a.Name);
            return query;
        }

        /// <inheritdoc />
        public override IOrderedQueryable<Platform> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters)
        {
            var query = Context.Platforms.Where(w => entityIds.Contains(w.Id));
            return (IOrderedQueryable<Platform>)query;
        }

        /// <inheritdoc />
        public override Platform GetEntity(Guid entityId)
        {
            return Context.Platforms.FirstOrDefault(f => f.Id == entityId);
        }

        /// <inheritdoc />
        public override void AddEntity(Platform entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void DeleteEntity(Platform entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void UpdateEntity(Platform entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool Exists(Guid entityId)
        {
            throw new NotImplementedException();
        }

    }
}