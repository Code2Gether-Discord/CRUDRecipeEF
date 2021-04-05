using System.Threading.Tasks;
using CRUDRecipeEF.BL.DTOs;

namespace CRUDRecipeEF.BL.Services
{
    public interface IRestaurantService
    {
        Task<string> AddRestaurant(RestaurantDTO restaurant);
        Task DeleteRestaurant(string name);
        Task RemoveMenuFromRestaurant(string menuName, string restaurantName);
        Task<RestaurantDTO> GetRestaurantByName(string name);
        Task<string> AddMenuToRestaurant(MenuAddDTO menuAddDTO);
    }
}