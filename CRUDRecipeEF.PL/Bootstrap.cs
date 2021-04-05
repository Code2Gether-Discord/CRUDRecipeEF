using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CRUDRecipeEF.PL
{
    public class Bootstrap
    {
        public static void SetupLogging()
        {
            // Logging is setup before the DI container, so we need to read the appsettings file here
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configBuilder.Build()) // Load Serilogs settings from appsettings.json
                .Enrich.FromLogContext()
                .CreateLogger();
            Log.Logger.Verbose("Setting up logging");
        }
    }
}