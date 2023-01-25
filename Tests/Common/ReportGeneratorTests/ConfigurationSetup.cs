using Microsoft.Extensions.Configuration;

namespace ReportGeneratorTests
{
    public static class ConfigurationSetup
    {
        public static IConfiguration SetupJsonConfigFile()
        {   
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("htmlreportconfig.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

            return builder;
        }
    }
}
