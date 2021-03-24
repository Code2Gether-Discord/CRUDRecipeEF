using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Helpers;
using CRUDRecipeEF.BL.DL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class SQLiteRecipeServiceTests : RecipeServiceTestsDb, IDisposable
    {
        private readonly MapperConfiguration _autoMapperConfig;
        private readonly Mapper _mapper;
        private readonly ILogger<RecipeService> _logger;
        public SQLiteRecipeServiceTests() :
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=TestRec.db")
                .Options)
        {
            _autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            _mapper = new Mapper(_autoMapperConfig);
            _logger = new Mock<ILogger<RecipeService>>().Object;

            // TODO the configuration is not valid
            //autoMapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task Test_GetRecipeByName()
        {
            using var context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);
            var recipe = await recipeService.GetRecipeByName("Apple Pie");

            Assert.NotNull(recipe);
            Assert.Equal("Apple Pie", recipe.Name);
        }

        [Fact]
        public async Task Test_GetAllRecipe()
        {
            using RecipeContext context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);

            var allRecipes = await recipeService.GetAllRecipes();

            Assert.NotNull(allRecipes);
            Assert.Equal(2, allRecipes.Count());
            Assert.Collection(allRecipes, item => Assert.Equal("Fruit Salad", item.Name),
                item => Assert.Equal("Apple Pie", item.Name));

            Assert.Collection(allRecipes.FirstOrDefault().Ingredients, item => Assert.Equal("Apple", item.Name),
                item => Assert.Equal("Orange", item.Name),
                item => Assert.Equal("Peach", item.Name));
        }

        [Fact]
        public async Task Test_AddRecipe()
        {
            using var context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);

            RecipeDTO recipe = new RecipeDTO
            {
                Name = "Chips",
                Ingredients = new List<IngredientDTO> { new IngredientDTO { Name = "Potato" },
                        new IngredientDTO { Name = "Oil" }
                    }
            };

            var recipeResult = await recipeService.AddRecipe(recipe);

            Assert.NotNull(recipeResult);
            Assert.Equal("Chips", recipeResult);

            var isItInDb = await recipeService.GetRecipeByName("Chips");

            Assert.NotNull(isItInDb);
            Assert.Equal("Chips", isItInDb.Name);

            Assert.Collection(isItInDb.Ingredients, item => Assert.Equal("Potato", item.Name),
                item => Assert.Equal("Oil", item.Name));
        }

        [Fact]
        public async Task Test_AddIngredientToRecipe()
        {
            using var context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);
            var addResult = await recipeService.AddIngredientToRecipe(new IngredientDTO { Name = "Cherry" }, "Fruit Salad");

            Assert.NotNull(addResult);
            Assert.Equal("Fruit Salad", addResult);

            var isItInDb = await recipeService.GetRecipeByName("Fruit Salad");
            Assert.NotNull(isItInDb);
            Assert.Equal("Fruit Salad", isItInDb.Name);

            Assert.Collection(isItInDb.Ingredients, item => Assert.Equal("Apple", item.Name),
                item => Assert.Equal("Orange", item.Name),
                item => Assert.Equal("Peach", item.Name),
                item => Assert.Equal("Cherry", item.Name));
        }

        [Fact]
        public async Task Test_RemoveIngredientFromRecipe()
        {
            using var context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);

            await recipeService.RemoveIngredientFromRecipe("Crust", "Apple Pie");
            var recipe = await recipeService.GetRecipeByName("Apple Pie");
            Assert.NotNull(recipe);
            Assert.Equal("Apple Pie", recipe.Name);

            Assert.Collection(recipe.Ingredients, item => Assert.Equal("Apple", item.Name),
                item => Assert.Equal("Sugar", item.Name));
        }

        [Fact]
        public async Task Test_DeleteRecipe()
        {
            using var context = new RecipeContext(ContextOptions);
            var recipeService = new RecipeService(context, _mapper, _logger);
            await recipeService.DeleteRecipe("Apple Pie");

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await recipeService.GetRecipeByName("Apple Pie"));
        }
    }
}
