using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            ILogger<IngredientService> logger )
        {
            this._context = context;
            this._mapper = mapper;
            _logger = logger;
        }

        private async Task<Ingredient> GetIngredientByNameIfExists(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower().Trim());
            return ingredient ?? throw new KeyNotFoundException("Ingredient doesnt exist");
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IngredientAddDTO"></param>
        /// <returns>Name of the Ingredient</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<string> AddIngredient(IngredientAddDTO ingredientAddDTO)
        {
            if (await IngredientExists(ingredientAddDTO.Name))
            {
                throw new KeyNotFoundException("Ingredient exists");
            }

            await _context.AddAsync(_mapper.Map<Ingredient>(ingredientAddDTO));
            await Save();

            _logger.LogDebug("Ingredient added: {ingredient}", ingredientAddDTO.Name);

            return ingredientAddDTO.Name;
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

            _logger.LogDebug("Ingredient Deleted: {ingredient}", name);
        }

        /// <summary>
        /// Get all of the ingredients from the database
        /// </summary>
        /// <returns>IEnumerable of all Ingredients</returns>
        public async Task<IEnumerable<IngredientDetailDTO>> GetAllIngredients()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return _mapper.Map<List<IngredientDetailDTO>>(ingredients);
        }

        /// <summary>
        /// Gets an ingredient by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<IngredientDetailDTO> GetIngredientByName(string name)
        {
            var ingredient = await GetIngredientByNameIfExists(name);

            return _mapper.Map<IngredientDetailDTO>(ingredient);
        }
    }
}