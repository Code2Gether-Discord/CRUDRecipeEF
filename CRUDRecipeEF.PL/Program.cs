using System;
using System.Linq;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.PL.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CRUDRecipeEF.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap.SetupLogging();

            var host = Bootstrap.CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<RecipeContext>();
                if (!context.Recipes.Any() && !context.Ingredients.Any())
                {
                    DataSeed.Seed(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured error while seeding data");
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
           
            //App starting point
            host.Services.GetRequiredService<IMainMenu>().Show();
        }
    }
}