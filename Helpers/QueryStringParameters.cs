using MediaManager.Services;

namespace MediaManager.Helpers
{
    /// <summary>
    /// Class to handle parameters from the query string of a resource
    /// </summary>
    public class QueryStringParameters
    {
        //private readonly IConfigurationRepository _Configuration;
        private readonly int _MaxPageSize;
        public int Page { get; set; } = 1;

        private int _PageSize;
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > _MaxPageSize) ? _MaxPageSize : value;
        }

        public string Filter { get; set; }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Name";

        public string Fields { get; set; }
        public int EntitiesToSkip => _PageSize * (Page - 1);

        public QueryStringParameters()
        {
            _MaxPageSize = _PageSize = 50;
        }
        //public QueryStringParameters(IConfigurationRepository configuration)
        //{
        //    _MaxPageSize = configuration.Application.Pages.MaximumPageSize;
        //    _PageSize = configuration.Application.Pages.DefaultPageSize;
        //}
    }
}