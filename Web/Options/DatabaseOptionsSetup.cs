using Microsoft.Extensions.Options;

namespace Web.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly IConfiguration _config;
        private const string DatabaseConfigurationSectionName = "DatabaseOptions";

        public DatabaseOptionsSetup(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(DatabaseOptions options)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            options.ConnectionString = connectionString;
            _config.GetSection(DatabaseConfigurationSectionName).Bind(options);
        }
    }
}
