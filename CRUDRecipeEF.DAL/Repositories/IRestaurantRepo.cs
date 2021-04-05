using System.Threading.Tasks;
using CRUDRecipeEF.DAL.Entities;

namespace CRUDRecipeEF.DAL.Repositories
{
    public interface IRestaurantRepo
    {
        Task<string> AddRestaurantAsync(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        Task<Restaurant> GetRestaurantByNameAsync(string name);
        Task<bool> RestaurantExists(string name);
    }
}