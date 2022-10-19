using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    public class AppTokenConfigurationOptionsSetup : IConfigureOptions<AppTokenConfigurationOptions>
    {
        private readonly IConfiguration _config;
        private const string AppToeknConfigurationSectionName = "TokenOptions";

        public AppTokenConfigurationOptionsSetup(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(AppTokenConfigurationOptions options)
        {
            _config.GetSection(AppToeknConfigurationSectionName).Bind(options);
        }
    }
}
