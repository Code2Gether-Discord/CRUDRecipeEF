using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Services;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace CRUDRecipeEF.PL
{
    public static class Bootstrap
    {
        /// <summary>
        /// Register services and app configs here
        /// </summary>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
            IConfiguration config = configBuilder.Build();

            //appsettings copy to output
            //auto adds json file appsettings
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddLogging(configure => configure.AddSerilog());
                  services.AddSingleton<IConfiguration>(config);
                  services.AddTransient<IDataSeed, DataSeed>();
                  services.AddTransient<IRecipeService, RecipeService>();
                  services.AddTransient<IIngredientService, IngredientService>();
                  services.AddAutoMapper(typeof(RecipeService).Assembly);
                  services.AddTransient<IMainMenu, MainMenu>();
                  services.AddTransient<IIngredientMenu, IngredientMenu>();
                  services.AddTransient<IRecipeMenu, RecipeMenu>();

                  services.AddDbContext<RecipeContext>(options =>
                  {
                      options.UseSqlite(hostContext.Configuration.GetConnectionString("Default"));
                  });
              });
        }

        public static void SetupLogging()
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(configBuilder.Build())
                .Enrich.FromLogContext()
                .CreateLogger();

            var contextLog = Log.ForContext("SourceContext", "CRUDRecipeEF");
            contextLog.Verbose("Logging setup successfully");
        }
    }
}