using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Helpers;
using CRUDRecipeEF.BL.DL.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class SQLiteRecipeServiceTests : RecipeServiceTests
    {
        private MapperConfiguration autoMapperConfig;

        public SQLiteRecipeServiceTests() :
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=Test.db")
                .Options)
        {
            //autoMapperConfig = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Ingredient, IngredientDetailDTO>();
            //    cfg.CreateMap<IngredientAddDTO, Ingredient>();
            //});

            autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            // TODO the configuration is not valid
            //autoMapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task Test_GetRecipeByName()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var recipeService = new RecipeService(context, new Mapper(autoMapperConfig));
                var recipe = await recipeService.GetRecipeByName("Apple Pie");

                Assert.NotNull(recipe);
                Assert.Equal("Apple Pie", recipe.Name);
            }
        }
    }
}
