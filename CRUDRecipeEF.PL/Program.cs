using System;
using System.Configuration;
using CRUDRecipeEF.BL.DL.Entities;
using CRUDRecipeEF.PL.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CRUDRecipeEF.PL
{
    class Program
    {
        public IConfiguration Configuration { get; set; }
        static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            //App starting point
            host.Services.GetRequiredService<MainMenu>().Run();
        }

        /// <summary>
        /// Register services and app configs here
        /// </summary>
        /// <returns>IHostBuilder</returns>
        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
              .ConfigureAppConfiguration(config =>
             config.AddJsonFile("appsettings.json"))
              .ConfigureServices(services =>
              {
                  services.AddSingleton<MainMenu>();
                  services.AddDbContext<RecipeContext>(options =>
                  options.UseSqlite(Configuration.GetConnectionString("")));
              });
        }
    }
}
