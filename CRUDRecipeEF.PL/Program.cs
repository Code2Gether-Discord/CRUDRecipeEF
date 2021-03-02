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
            //App starting point
            host.Services.GetRequiredService<MainMenu>().Run();
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
                  services.AddAutoMapper(typeof(RecipeService).Assembly);
                  services.AddSingleton<MainMenu>();

                  services.AddDbContext<RecipeContext>(options =>
                  {
                      options.UseSqlite(hostContext.Configuration.GetConnectionString("Default"));
                  });
              });
        }
    }
}