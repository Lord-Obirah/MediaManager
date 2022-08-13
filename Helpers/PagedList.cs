using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace MediaManager.Helpers
{
    public class PagedList<TOut> : List<TOut>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);

        public bool HasNext => (CurrentPage < TotalPages);

        private PagedList(IEnumerable<TOut> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<TOut> Create<TIn>(IQueryable<TIn> source, QueryStringParameters queryStringParameters, IMapper mapper)
        {
            var page = queryStringParameters.Page;
            var pageSize = queryStringParameters.PageSize;

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            return new PagedList<TOut>(mapper.Map<IEnumerable<TOut>>(items).ToList(), count, page, pageSize);
        }
    }
}