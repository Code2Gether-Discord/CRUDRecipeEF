using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Services;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace CRUDRecipeEF.PL
{
    public class Bootstrap
    {
        public static void SetupLogging()
        {
            // Logging is setup before the DI container, so we need to read the appsettings file here
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configBuilder.Build()) // Load Serilogs settings from appsettings.json
                .Enrich.FromLogContext()
                .CreateLogger();
            Log.Logger.Verbose("Setting up logging");
        }   
    }
}
