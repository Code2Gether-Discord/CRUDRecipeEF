using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Helpers;
using CRUDRecipeEF.BL.DL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class SQLiteRecipeServiceTests : RecipeServiceTests, IDisposable
    {
        private MapperConfiguration _autoMapperConfig;
        private readonly Mapper _mapper;

        public SQLiteRecipeServiceTests() :
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=TestRec.db")
                .Options)
        {
            _autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            _mapper = new Mapper(_autoMapperConfig);

            // TODO the configuration is not valid
            //autoMapperConfig.AssertConfigurationIsValid();
        }

        public void Dispose()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Test_GetRecipeByName()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);
                var recipe = await recipeService.GetRecipeByName("Apple Pie");

                Assert.NotNull(recipe);
                Assert.Equal("Apple Pie", recipe.Name);
            }
        }

        [Fact]
        public async Task Test_GetAllRecipe()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);

                var allRecipes = await recipeService.GetAllRecipes();

                Assert.NotNull(allRecipes);
                Assert.Equal(2, allRecipes.Count());
                Assert.Collection(allRecipes, item => Assert.Equal("Fruit Salad", item.Name),
                    item => Assert.Equal("Apple Pie", item.Name));

                Assert.Collection(allRecipes.FirstOrDefault().Ingredients, item => Assert.Equal("Apple", item.Name),
                    item => Assert.Equal("Orange", item.Name),
                    item => Assert.Equal("Peach", item.Name));
            }
        }

        [Fact]
        public async Task Test_AddRecipe()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);

                RecipeAddDTO recipe = new RecipeAddDTO
                {
                    Name = "Chips",
                    Ingredients = new List<IngredientAddDTO> { new IngredientAddDTO { Name = "Potato" },
                        new IngredientAddDTO { Name = "Oil" }
                    }
                };

                var recipeResult = await recipeService.AddRecipe(recipe);

                Assert.NotNull(recipeResult);
                Assert.Equal("Chips", recipeResult);

                var isItInDb = await recipeService.GetRecipeByName("Chips");

                Assert.NotNull(isItInDb);
                Assert.Equal("Chips", isItInDb.Name);
            }
        }

        [Fact]
        public async Task Test_AddIngredientToRecipe()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);
                var addResult = await recipeService.AddIngredientToRecipe(new IngredientAddDTO { Name = "Cherry" }, "Fruit Salad");

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
        }

        [Fact]
        public async Task Test_RemoveIngredientFromRecipe()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);

                await recipeService.RemoveIngredientFromRecipe("Crust", "Apple Pie");
                var recipe = await recipeService.GetRecipeByName("Apple Pie");
                Assert.NotNull(recipe);
                Assert.Equal("Apple Pie", recipe.Name);

                Assert.Collection(recipe.Ingredients, item => Assert.Equal("Apple", item.Name),
                    item => Assert.Equal("Sugar", item.Name));
            }
        }

        [Fact]
        public async Task Test_DeleteRecipe()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, _mapper);
                await recipeService.DeleteRecipe("Apple Pie");

                await Assert.ThrowsAsync<KeyNotFoundException>(async () => await recipeService.GetRecipeByName("Apple Pie"));
            }
        }
    }
}
