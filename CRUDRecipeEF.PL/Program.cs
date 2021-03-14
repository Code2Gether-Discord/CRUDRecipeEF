using System;
using System.Linq;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.PL.Menus;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CRUDRecipeEF.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Moving setup code to Bootstrap class makes Main() cleaner
            Bootstrap.SetupLogging(); // Setup Serilog
            var host = Bootstrap.CreateHostBuilder(args).Build(); // Setup Dependency Injection container

            // Global logger can be used in classes that aren't having services injected
            ILogger logger = Log.ForContext<Program>(); 
            logger.Debug("CRUDRecipeEF starting");

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

                Log.Logger.Fatal($"Exception while seeding data: {ex.Message}");
                Environment.Exit(1);
            }
           
            //App starting point
            host.Services.GetRequiredService<IMainMenu>().Show();
        }
    }
}