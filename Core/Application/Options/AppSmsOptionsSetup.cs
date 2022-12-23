using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    public class AppSmsOptionsSetup : IConfigureOptions<AppSmsOptions>
    {
        private readonly IConfiguration _config;
        private const string AppSmsSectionName = "TwilioOptions";
        public AppSmsOptionsSetup(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(AppSmsOptions options)
        {
            _config.GetSection(AppSmsSectionName).Bind(options);
        }
    }
}
