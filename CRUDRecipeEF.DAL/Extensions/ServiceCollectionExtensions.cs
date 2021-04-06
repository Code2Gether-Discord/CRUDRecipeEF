using CRUDRecipeEF.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDRecipeEF.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDal(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRestaurantRepo, RestaurantRepo>();
            services.AddTransient<IIngredientRepo, IngredientRepo>();
            return services;
        }
    }
}