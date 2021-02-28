using System;
using CRUDRecipeEF.PL.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CRUDRecipeEF.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            //App starting point
            host.Services.GetRequiredService<Menu>().Run();
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
                  services.AddSingleton<Menu>();
              });
        }
    }
}
