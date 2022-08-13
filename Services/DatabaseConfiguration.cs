using Microsoft.Extensions.Configuration;

namespace MediaManager.Services
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly IConfigurationRoot _Configuration;

        public DatabaseConfiguration(IConfigurationRoot configuration)
        {
            _Configuration = configuration;
        }

        /// <inheritdoc />
        public string Connectionstring => _Configuration["connectionStrings:ConnectionString"];
    }
}