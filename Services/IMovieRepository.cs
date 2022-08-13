using System;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public interface IMovieRepository : IRepository<Movie>
    {
        PagedList<Movie> GetMoviesForMediaType(Guid mediaTypeId, QueryStringParameters queryStringParameters);
    }
}