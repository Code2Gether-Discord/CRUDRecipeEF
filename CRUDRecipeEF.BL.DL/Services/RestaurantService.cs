using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Commits any changes to the db that are tracked by EF
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Finds a restaurant with the specified name. Throws an exception if it doesnt exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A recipe if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private async Task<Restaurant> GetRestaurantByNameIfExists(string name)
        {
            var restaurant = await _context.Restaurants.Include(m => m.Menus)
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower().Trim());
            return restaurant ?? throw new KeyNotFoundException("Restaurant doesnt exist");
        }

        public Task<string> AddMenuToRestaurant(Menu menu, string restaurantName)
        {
            throw new NotImplementedException();
        }

        public Task AddRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMenuFromRestaurant(string menuName, string restaurantName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRestaurant(string restaurantName)
        {
            throw new NotImplementedException();
        }

        public Task<Restaurant> GetRestaurantByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
