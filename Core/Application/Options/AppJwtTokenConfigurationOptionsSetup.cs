using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    public class AppJwtTokenConfigurationOptionsSetup : IConfigureOptions<AppJwtTokenConfigurationOptions>
    {
        private readonly IConfiguration _config;
        private const string AppToeknConfigurationSectionName = "JwtTokenOptions";

        public AppJwtTokenConfigurationOptionsSetup(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(AppJwtTokenConfigurationOptions options)
        {
            _config.GetSection(AppToeknConfigurationSectionName).Bind(options);
        }
    }
}
