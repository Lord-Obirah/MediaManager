using Microsoft.Extensions.Configuration;

namespace MediaManager.Services
{
    public class PageConfiguration : IPageConfiguration
    {
        private readonly IConfigurationRoot _Configuration;

        public PageConfiguration(IConfigurationRoot configuration)
        {
            _Configuration = configuration;
        }

        /// <inheritdoc />
        public int DefaultPageSize => int.Parse(_Configuration["Application:Pages:DefaultPageSize"]);

        /// <inheritdoc />
        public int MaximumPageSize => int.Parse(_Configuration["Application:Pages:MaximumPageSize"]);
    }
}