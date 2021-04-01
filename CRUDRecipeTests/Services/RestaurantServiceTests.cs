using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using CRUDRecipeEF.BL.DL.Helpers;
using CRUDRecipeEF.BL.DL.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class RestaurantServiceTests
    {
        private readonly RecipeContext _context;
        private readonly RestaurantService _restaurantService;

        public RestaurantServiceTests()
        {
            //creating in memory db
            var contextOptions = new DbContextOptionsBuilder<RecipeContext>().UseInMemoryDatabase("restaurants");
            _context = new RecipeContext(contextOptions.Options);

            //making sure its created and seeding
            _context.Database.EnsureCreated();
            _context.AddRange(new Restaurant { Name = "MC" }, new Restaurant { Name = "BK" });
            _context.SaveChanges();

            //creating the service
            _restaurantService = new RestaurantService(_context,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>())));
        }

        [Theory]
        [InlineData("BK")]
        [InlineData("MC")]
        public async Task Should_Return_Restaurant_If_Exists(string restaurantName)
        {
            var expected = restaurantName;
            var result = await _restaurantService.GetRestaurantByName(restaurantName);

            Assert.Equal(expected, result.Name);
        }
    }
}
