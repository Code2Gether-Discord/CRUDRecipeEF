using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.Services;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Helpers;
using CRUDRecipeEF.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CRUDRecipeTests.Services
{
    public class IngredientServicetests
    {
        public readonly Mock<IIngredientRepo> _mockRepo;
        private readonly Mock<IUnitOfWork> _UoW;
        private readonly MapperConfiguration _mapperConfig;
        private readonly IIngredientService _ingredientService;
        private readonly Mock<ILogger<IngredientService>> _logger;
        private readonly string _ingredientName = "Apple";

        public IngredientServicetests()
        {
            //mock the repo against the interface
            _mockRepo = new Mock<IIngredientRepo>();
            _UoW = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<IngredientService>>();

            _mapperConfig = new MapperConfiguration(c => c.AddProfile<AutoMapperProfiles>());
            SetupData();

            _ingredientService = new IngredientService(_mockRepo.Object, new Mapper(_mapperConfig),
               _logger.Object, _UoW.Object);
        }

        private void SetupData()
        {
            _mockRepo.Setup(x => x.GetIngredientDTOByNameAsync(_ingredientName)).ReturnsAsync(
                new IngredientDTO { Name = _ingredientName });
        }

        [Fact]
        public async Task GetIngredientByName_Returns_Ingredient_If_Exists()
        {
            var result = await _ingredientService.GetIngredientDTOByNameAsync(_ingredientName);

            Assert.Equal(_ingredientName, result.Name);
        }

        [Fact]
        public async Task DeleteIngredient_Throws_KeyNotFoundException_If_Ingredient_Doesnt_Exists()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
               await _ingredientService.DeleteIngredient("13super random name14"));
        }
    }
}