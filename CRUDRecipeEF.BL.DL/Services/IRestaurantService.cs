using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRestaurantService
    {
        Task AddRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(string restaurantName);
        Task RemoveMenuFromRestaurant(string menuName, string restaurantName);
        Task<Restaurant> GetRestaurantByName(string name);
        Task<string> AddMenuToRestaurant(Menu menu, string restaurantName);
    }
}
