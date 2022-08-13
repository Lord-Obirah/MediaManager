using Microsoft.Extensions.Configuration;

namespace MediaManager.Services
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly IConfigurationRoot _Configuration;

        public ApplicationConfiguration(IConfigurationRoot configuration, IPageConfiguration pageConfiguration)
        {
            _Configuration = configuration;
            Pages = pageConfiguration;
        }

        /// <inheritdoc />
        public IPageConfiguration Pages { get; }
    }
}