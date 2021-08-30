using Microsoft.Extensions.Configuration;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.Fx.Utilities.Configurations
{
    public class ConfigurationsManager : IConfigurationManager
    {
        private readonly IConfiguration configuration;

        public ConfigurationsManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetValue(string key)
        {
            return configuration[key];
        }
    }
}
