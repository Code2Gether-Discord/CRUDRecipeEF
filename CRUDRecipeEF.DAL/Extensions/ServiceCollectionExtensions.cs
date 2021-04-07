using CRUDRecipeEF.DAL.Helpers;
using CRUDRecipeEF.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDRecipeEF.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDal(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddTransient<IRestaurantRepo, RestaurantRepo>();
            services.AddTransient<IIngredientRepo, IngredientRepo>();
            return services;
        }
    }
}