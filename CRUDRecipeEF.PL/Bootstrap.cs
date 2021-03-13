using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Services;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //appsettings copy to output
            //auto adds json file appsettings
            return Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
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
            Bootstrap.BuildConfig(configBuilder);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .ReadFrom.Configuration(configBuilder.Build())
                .Enrich.FromLogContext()
                .CreateLogger();
            Log.Logger.Information("CRUDRecipeEF Starting");
        }

        public static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
