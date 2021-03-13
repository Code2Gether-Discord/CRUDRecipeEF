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
            Bootstrap.SetupLogging();

            var contextLog = Log.ForContext("SourceContext", "CRUDRecipeEF");
            contextLog.Debug("CRUDRecipeEF Starting");

            var host = Bootstrap.CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<RecipeContext>();
                if (!context.Recipes.Any() && !context.Ingredients.Any())
                {
                    var seeder = host.Services.GetRequiredService<IDataSeed>();
                    seeder.Seed();
                }
            }
            catch (Exception ex)
            {
                contextLog.Fatal("Unable to seed database: {exception}", ex.Message);
                Console.WriteLine("Exception occured error while seeding data");
                Console.WriteLine();
                Console.WriteLine(ex.Message);

                Environment.Exit(1);
            }
           
            //App starting point
            host.Services.GetRequiredService<IMainMenu>().Show();
        }
    }
}