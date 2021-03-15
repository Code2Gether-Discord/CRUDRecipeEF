using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRestaurantService
    {
        Task<string> AddRestaurant(RestaurantAddDTO restaurant);
        Task DeleteRestaurant(string name);
        Task RemoveMenuFromRestaurant(string menuName, string restaurantName);
        Task<RestaurantDetailDTO> GetRestaurantByName(string name);
        Task<string> AddMenuToRestaurant(MenuAddDTO menuAdd, string restaurantName);
    }
}
