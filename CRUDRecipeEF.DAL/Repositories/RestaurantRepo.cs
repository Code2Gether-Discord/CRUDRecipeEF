using System.Threading.Tasks;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.DAL.Repositories
{
    internal class RestaurantRepo : IRestaurantRepo
    {
        private readonly RecipeContext _context;

        public RestaurantRepo(RecipeContext context)
        {
            _context = context;
        }

        public async Task<string> AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.AddAsync(restaurant);
            return restaurant.Name;
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
        }

        public Task<Restaurant> GetRestaurantByNameAsync(string name)
        {
            return _context.Restaurants
                .Include(m => m.Menus)
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower().Trim());
        }

        public Task<bool> RestaurantExists(string name)
        {
            return _context.Restaurants.AnyAsync(restaurant => restaurant.Name == name);
        }
    }
}