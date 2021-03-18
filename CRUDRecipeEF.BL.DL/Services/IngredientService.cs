using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IngredientService> _logger;

        public IngredientService(RecipeContext context, 
            IMapper mapper,
            ILogger<IngredientService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<Ingredient> GetIngredientByNameIfExists(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower().Trim());
            if (ingredient == null)
            {
                _logger.LogDebug($"Attempted to get ingredient that does not exist: {name}");
                throw new KeyNotFoundException("Ingredient doesnt exist");
            }

            return ingredient;
        }

        /// <summary>
        /// Commits any changes to the db that are tracked by EF
        /// </summary>
        /// <returns></returns>
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a recipe with the specified name already exists
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <returns>If the ingredient exists or not</returns>
        private async Task<bool> IngredientExists(string ingredientName)
        {
            bool exists = await _context.Ingredients.AnyAsync(i => i.Name.ToLower() == ingredientName.ToLower().Trim());
            return exists; 
            // Not using => makes this easier to debug. For some reason .ToLowerInvariant() does not work in the predicate.
            // Possibly not compatiable with async? May look into this later.
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IngredientAddDTO"></param>
        /// <returns>Name of the Ingredient unless a ingredient with the same name already exists</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddIngredient(IngredientDTO ingredientDTO)
        {
            if (await IngredientExists(ingredientDTO.Name))
            {
                _logger.LogWarning($"Attempted to add existing ingredient {ingredientDTO.Name}");
                throw new ArgumentException("Ingredient exists");
            }

            await _context.AddAsync(_mapper.Map<Ingredient>(ingredientDTO));
            await Save();

            _logger.LogInformation($"Added {ingredientDTO.Name}");

            return ingredientDTO.Name;
        }

        /// <summary>
        /// Delete an Ingredient from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteIngredient(string name)
        {
            var ingredient = await GetIngredientByNameIfExists(name);

            _context.Remove(ingredient);
            await Save();

            _logger.LogInformation($"Deleted {name}");
        }

        /// <summary>
        /// Get all of the ingredients from the database
        /// </summary>
        /// <returns>IEnumerable of all Ingredients</returns>
        public async Task<IEnumerable<IngredientDTO>> GetAllIngredients()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return _mapper.Map<List<IngredientDTO>>(ingredients);
        }

        /// <summary>
        /// Gets an ingredient by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<IngredientDTO> GetIngredientByName(string name)
        {
            var ingredient = await GetIngredientByNameIfExists(name);

            return _mapper.Map<IngredientDTO>(ingredient);
        }
    }
}