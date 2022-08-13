using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;
using MediaManager.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Services
{
    public class MovieRepository : AbstractRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MediaContext context, IPropertyMappingService propertyMappingService) : base(context,
            propertyMappingService)
        {
        }

        public override void AddEntity(Movie entity)
        {
            entity.Id = Guid.NewGuid();

            // the repository fills the id (instead of using identity columns)
            if (entity.MediaType == null)
            {
                throw new Exception("Missing MediaType");
            }

            Context.Movies.Add(entity);
        }

        public override bool Exists(Guid entityId)
        {
            return Context.Movies.Any(a => a.Id == entityId);
        }

        public PagedList<Movie> GetMoviesForMediaType(Guid mediaTypeId, QueryStringParameters queryStringParameters)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(Movie entity)
        {
            Context.Movies.Remove(entity);
        }

        public override IQueryable<Movie> GetEntities(QueryStringParameters queryStringParameters)
        {
            var query = Context.Movies.ApplySorting(queryStringParameters.OrderBy, PropertyMappingService.GetPropertyMapping<MovieReadApi, Movie>());
            return GetOrderedList(query, queryStringParameters);
        }

        /// <inheritdoc />
        public override IQueryable<Movie> GetEntities(IEnumerable<Guid> entityIds,
            QueryStringParameters queryStringParameters)
        {
            var query = GetOrderedList(((IOrderedQueryable<Movie>)Context.Movies.Where(w => entityIds.Contains(w.Id))));
            return query;
        }

        /// <inheritdoc />
        public override Movie GetEntity(Guid entityId)
        {
            return Context.Movies.FirstOrDefault(w => w.Id == entityId);
        }

        /// <inheritdoc />
        public override void UpdateEntity(Movie entity)
        {
            //no code to execute
        }

        private IQueryable<Movie> GetOrderedList(IOrderedQueryable<Movie> entities, QueryStringParameters queryStringParameters = null)
        {
            var queryable = entities
                .Include(i => i.MediaType);

            IQueryable<Movie> returnValue = null;
            if (queryStringParameters != null)
            {
                if (!string.IsNullOrWhiteSpace(queryStringParameters.SearchQuery))
                {
                    var searchQuery = queryStringParameters.SearchQuery.Trim().ToLower();
                    returnValue = queryable.Where(w => w.Title.ToLower().Contains(searchQuery) ||
                                         w.MediaType.Name.ToLower().Contains(searchQuery));
                }
            }

            return returnValue ?? queryable;
        }
    }
}