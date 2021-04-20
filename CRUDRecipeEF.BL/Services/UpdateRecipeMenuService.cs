using AutoMapper;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;
using CRUDRecipeEF.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.Services
{
    public class UpdateRecipeMenuService : IUpdateRecipeMenuService
    {
        private readonly RecipeContext _context;
        private readonly ILogger<UpdateRecipeMenuService> _logger;
        private readonly IMapper _mapper;
        private readonly IIngredientService _ingredientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeService _recipeService;
        private readonly IIngredientRepo _ingredientRepo;

        public UpdateRecipeMenuService(RecipeContext context,
            IMapper mapper,
            IIngredientService ingredientService,
            ILogger<UpdateRecipeMenuService> logger,
            IUnitOfWork unitOfWork,
            IRecipeService recipeService,
            IIngredientRepo ingredientRepo)
        {
            _context = context;
            _mapper = mapper;
            _ingredientService = ingredientService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _recipeService = recipeService;
            _ingredientRepo = ingredientRepo;
        }

        public async Task UpdateIngredient(string input, string ingredientName)
        {
            Ingredient ingredient = await _ingredientRepo.GetIngredientByNameAsync(input);

            ingredient.Name = ingredientName;

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRecipeName(RecipeDTO recipeDTO, string newName)
        {
            var recipe = await _recipeService.GetRecipeByName(newName);

            _mapper.Map(recipeDTO, recipe);

            await _unitOfWork.SaveAsync();
        }
    }
}
