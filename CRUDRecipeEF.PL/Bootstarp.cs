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
    internal class Bootstarp
    {
        internal static void SetupLogging()
        {
            // Logging is setup before the DI container, so we need to read the appsettings file here
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configBuilder.Build())
                .Enrich.FromLogContext()
                .CreateLogger();
            Log.Logger.Verbose("Setting up logging");
        }

        /// <summary>
        /// Register services and app configs here
        /// </summary>
        /// <returns>IHostBuilder</returns>
        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            //appsettings copy to output
            //auto adds json file appsettings
            return Host.CreateDefaultBuilder(args)
              .UseSerilog()
              .ConfigureServices((hostContext, services) =>
              {
                  services
                    .AddTransient<IRecipeService, RecipeService>()
                    .AddTransient<IIngredientService, IngredientService>()
                    .AddAutoMapper(typeof(RecipeService).Assembly)
                    .AddTransient<IMainMenu, MainMenu>()
                    .AddTransient<IIngredientMenu, IngredientMenu>()
                    .AddTransient<IRecipeMenu, RecipeMenu>();

                  services.AddDbContext<RecipeContext>(options =>
                  {
                      options.UseSqlite(hostContext.Configuration.GetConnectionString("Default"));
                  });
              });
        }
    }
}
