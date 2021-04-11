using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;
using CRUDRecipeEF.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRUDRecipeEF.BL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ILogger<IngredientService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepo ingredientRepo, IMapper mapper, ILogger<IngredientService> logger,
            IUnitOfWork unitOfWork)
        {
            _ingredientRepo = ingredientRepo;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddIngredient(IngredientDTO ingredientAddDTO)
        {
            if (await _ingredientRepo.IngredientExistsAsync(ingredientAddDTO.Name))
            {
                _logger.LogWarning($"Attempted to add existing ingredient {ingredientAddDTO.Name}");
                throw new ArgumentException("Ingredient exists");
            }

            var ingredientName = await _ingredientRepo.AddIngredientAsync(_mapper.Map<Ingredient>(ingredientAddDTO));
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Added {ingredientAddDTO.Name}");

            return ingredientName;
        }

        public async Task DeleteIngredient(string name)
        {
            var ingredient = await GetIngredientByNameIfExistsAsync(name);

            _ingredientRepo.DeleteIngredient(ingredient);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Deleted {name}");
        }

        public Task<IEnumerable<IngredientDTO>> GetAllIngredientsDTOsAsync()
        {
            return _ingredientRepo.GetAllIngredientsDTOsAsync();
        }
      
        public Task<IngredientDTO> GetIngredientDTOByNameAsync(string name)
        {
            return _ingredientRepo.GetIngredientDTOByNameAsync(name);            
        }

        public async Task UpdateIngredient(IngredientDTO ingredientDTO, string ingredientName)
        {
            var ingredient = await GetIngredientByNameIfExistsAsync(ingredientName);

            _mapper.Map(ingredientDTO, ingredient);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Gets an ingredient by name if it exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private async Task<Ingredient> GetIngredientByNameIfExistsAsync(string name)
        {
            var ingredient = await _ingredientRepo.GetIngredientByNameAsync(name);
            if (ingredient == null)
            {
                _logger.LogDebug($"Attempted to get ingredient that does not exist: {name}");
                throw new KeyNotFoundException("Ingredient doesnt exist");
            }

            return ingredient;
        }
    }
}