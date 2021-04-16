using System;
using System.Linq;
using CRUDRecipeEF.BL.Services;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.Extensions;
using CRUDRecipeEF.DAL.Helpers;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CRUDRecipeEF.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrap.SetupLogging(); // Setup Serilog

            var host = CreateHostBuilder(args).Build(); // Setup Dependency Injection container

            // Global logger can be used in classes that aren't having services injected
            // ILogger logger = Log.ForContext<Program>();
            // logger.Debug("CRUDRecipeEF starting");

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<RecipeContext>();
                context.Database.EnsureCreated(); // Create the database if it doesn't exist
                if (!context.Recipes.Any() && !context.Ingredients.Any())
                {
                    var seeder = services.GetRequiredService<IDataSeed>();
                    seeder.Seed();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured while seeding data");
                Console.WriteLine();
                Console.WriteLine(ex.Message);

                //   Log.Logger.Fatal($"Exception while seeding data: {ex.Message}");
                Environment.Exit(1);
            }

            //App starting point
            host.Services.GetRequiredService<IMainMenu>().Show();
        }

        /// <summary>
        ///     Register services and app configs here
        /// </summary>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //appsettings copy to output
            //auto adds json file appsettings
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddTransient<IDataSeed, DataSeed>()
                        .AddTransient<IRecipeService, RecipeService>()
                        .AddTransient<IIngredientService, IngredientService>()
                        .AddTransient<IMainMenu, MainMenu>()
                        .AddTransient<IIngredientMenu, IngredientMenu>()
                        .AddTransient<IRestaurantService, RestaurantService>()
                        .AddTransient<IRecipeMenu, RecipeMenu>()
                        .AddTransient<IUpdateRecipeMenu, UpdateRecipeMenu>()
                        .AddTransient<IUpdateRecipeMenuService, UpdateRecipeMenuService>()
                        .ConfigureDal();

                    services.AddDbContext<RecipeContext>(options =>
                    {
                        options.UseSqlite(hostContext.Configuration.GetConnectionString("Default"));
                    });
                });
        }
    }
}