using System;
using System.Linq;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Services;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRUDRecipeEF.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

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
                Console.WriteLine("Retarded error while seeding data");
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
           
            //App starting point
            host.Services.GetRequiredService<IMainMenu>().Show();
        }

        /// <summary>
        /// Register services and app configs here
        /// </summary>
        /// <returns>IHostBuilder</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
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
    }
}