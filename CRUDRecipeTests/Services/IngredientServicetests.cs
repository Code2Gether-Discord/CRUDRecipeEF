using AutoMapper;
using CRUDRecipeEF.BL.Services;
using CRUDRecipeEF.DAL.Helpers;
using CRUDRecipeEF.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace CRUDRecipeTests.Services
{
    public class IngredientServicetests
    {
        public readonly Mock<IIngredientRepo> _mockRepo;
        private readonly Mock<IUnitOfWork> _UoW;
        private readonly MapperConfiguration _mapperConfig;
        private readonly IIngredientService _ingredientService;
        private readonly Mock<ILogger<IngredientService>> _logger;

        public IngredientServicetests()
        {
            //mock the repo against the interface
            _mockRepo = new Mock<IIngredientRepo>();
            _UoW = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<IngredientService>>();

            _mapperConfig = new MapperConfiguration(c => c.AddProfile<AutoMapperProfiles>());

            _ingredientService = new IngredientService(_mockRepo.Object, new Mapper(_mapperConfig),
               _logger.Object, _UoW.Object);
        }

        public void SetupData()
        {
        }
    }
}