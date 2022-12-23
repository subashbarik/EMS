using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    public class AppEmailOptionsSetup : IConfigureOptions<AppEmailOptions>
    {
        private readonly IConfiguration _config;
        private const string AppEmailSectionName = "EmailOptions";
        public AppEmailOptionsSetup(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(AppEmailOptions options)
        {
            _config.GetSection(AppEmailSectionName).Bind(options);
        }
    }
}
