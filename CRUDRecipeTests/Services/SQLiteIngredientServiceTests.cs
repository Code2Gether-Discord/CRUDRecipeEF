using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.Services;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class SQLiteIngredientServiceTests : IngredientServiceTestsDb, IDisposable
    {
        private readonly MapperConfiguration _autoMapperConfig;
        private readonly ILogger<IngredientService> _logger;
        private readonly Mapper _mapper;

        public SQLiteIngredientServiceTests() :
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=TestIng.db")
                .Options)
        {
            _autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            _mapper = new Mapper(_autoMapperConfig);
            _logger = new Mock<ILogger<IngredientService>>().Object;
            // TODO the configuration is not valid
            //autoMapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task Test_GetIngredientByName()
        {
            using var context = new RecipeContext(ContextOptions);
            var ingredientService = new IngredientService(context, _mapper, _logger);
            var ingredient = await ingredientService.GetIngredientDTOByNameAsync("Apple");

            Assert.NotNull(ingredient);
            Assert.Equal("Apple", ingredient.Name);
        }

        [Fact]
        public async Task Test_GetAllIngredients()
        {
            using var context = new RecipeContext(ContextOptions);
            var ingredientService = new IngredientService(context, _mapper, _logger);

            var allIngredients = await ingredientService.GetAllIngredientsDTOsAsync();

            Assert.NotNull(allIngredients);
            Assert.Equal(3, allIngredients.Count());
            Assert.Collection(allIngredients, item => Assert.Equal("Apple", item.Name),
                item => Assert.Equal("Orange", item.Name),
                item => Assert.Equal("Peach", item.Name));
        }

        [Fact]
        public async Task Test_AddIngredient()
        {
            using var context = new RecipeContext(ContextOptions);
            var ingredientService = new IngredientService(context, _mapper, _logger);
            var ingredient = await ingredientService.AddIngredient(new IngredientDTO
            {
                Name = "Carrot"
            });

            Assert.NotNull(ingredient);
            Assert.Equal("Carrot", ingredient);

            var isItInDb = await ingredientService.GetIngredientDTOByNameAsync("Carrot");

            Assert.NotNull(isItInDb);
            Assert.Equal("Carrot", isItInDb.Name);
        }

        [Fact]
        public async Task Test_DeleteIngredient()
        {
            using var context = new RecipeContext(ContextOptions);
            var ingredientService = new IngredientService(context, _mapper, _logger);
            await ingredientService.DeleteIngredient("Apple");


            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await ingredientService.GetIngredientDTOByNameAsync("Apple"));
        }

        [Fact]
        public async Task UpdateIngredient_Updates_Ingredient()
        {
            using var context = new RecipeContext(ContextOptions);
            var ingredientService = new IngredientService(context, _mapper, _logger);

            var ingredientToUpdate = await ingredientService.GetIngredientDTOByNameAsync("Apple");

            var ingredientDTO = new IngredientDTO
            {
                Name = "Monkey",
                Id = ingredientToUpdate.Id
            };
            await ingredientService.UpdateIngredient(ingredientDTO, "Apple");
            var ingredient = await ingredientService.GetIngredientDTOByNameAsync("Monkey");
            Assert.True(ingredient.Name == ingredientDTO.Name);
        }
    }
}