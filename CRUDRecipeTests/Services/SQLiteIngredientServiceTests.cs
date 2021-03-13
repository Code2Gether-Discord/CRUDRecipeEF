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
    public class SQLiteIngredientServiceTests : IngredientServiceTests, IDisposable
    {
        private MapperConfiguration autoMapperConfig;

        public SQLiteIngredientServiceTests() : 
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=TestIng.db")
                .Options)
        {
            autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
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
        public async Task Test_GetIngredientByName()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var ingredientService = new IngredientService(context, new Mapper(autoMapperConfig));
                var ingredient = await ingredientService.GetIngredientByName("Apple");
    
                Assert.NotNull(ingredient);
                Assert.Equal("Apple", ingredient.Name);
            }
        }

        [Fact]
        public async Task Test_GetAllIngredients()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var ingredientService = new IngredientService(context, new Mapper(autoMapperConfig));

                var allIngredients = await ingredientService.GetAllIngredients();

                Assert.NotNull(allIngredients);
                Assert.Equal(3, allIngredients.Count());
                Assert.Collection(allIngredients, item => Assert.Equal("Apple", item.Name),
                    item => Assert.Equal("Orange", item.Name),
                    item => Assert.Equal("Peach", item.Name));
            }
        }

        [Fact]
        public async Task Test_AddIngredient()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var ingredientService = new IngredientService(context, new Mapper(autoMapperConfig));
                var ingredient = await ingredientService.AddIngredient(new IngredientAddDTO { Name = "Carrot" });

                Assert.NotNull(ingredient);
                Assert.Equal("Carrot", ingredient);

                var isItInDb = await ingredientService.GetIngredientByName("Carrot");

                Assert.NotNull(isItInDb);
                Assert.Equal("Carrot", isItInDb.Name);
            }
        }

        [Fact]
        public async Task Test_DeleteIngredient()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var ingredientService = new IngredientService(context, new Mapper(autoMapperConfig));
                await ingredientService.DeleteIngredient("Apple");


                await Assert.ThrowsAsync<KeyNotFoundException>(async () => await ingredientService.GetIngredientByName("Apple"));
            }
        }
    }
}
