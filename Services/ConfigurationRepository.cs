using Microsoft.Extensions.Configuration;

namespace MediaManager.Services
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfigurationRoot _Configuration;

        public ConfigurationRepository(IConfigurationRoot configuration, IDatabaseConfiguration databaseConfiguration, IApplicationConfiguration applicationConfiguration)
        {
            _Configuration = configuration;
            Database = databaseConfiguration;
            Application = applicationConfiguration;
        }

        /// <inheritdoc />
        public IDatabaseConfiguration Database { get; }

        /// <inheritdoc />
        public IApplicationConfiguration Application { get; }
    }
}