using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    public class AppConfigurationOptionsSetup : IConfigureOptions<AppConfigurationOptions>
    {
        private readonly IConfiguration _config;
        private const string AppConfigurationSectionName = "AppConfigurations";

        public AppConfigurationOptionsSetup(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(AppConfigurationOptions options)
        {
            _config.GetSection(AppConfigurationSectionName).Bind(options);
        }
    }
}
