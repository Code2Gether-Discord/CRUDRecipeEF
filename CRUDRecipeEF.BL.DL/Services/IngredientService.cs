using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public IngredientService(RecipeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
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
        private async Task<bool> IngredientExists(string ingredientName) =>
            await _context.Recipes.AnyAsync(r => r.Name.ToLower() == ingredientName.ToLower());

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

            return ingredientAddDTO.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteIngredient(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());

            if (ingredient == null)
            {
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            _context.Remove(ingredient);
            await Save();
        }

        public async Task<IEnumerable<IngredientDetailDTO>> GetAllIngredients()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return _mapper.Map<List<IngredientDetailDTO>>(ingredients);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<IngredientDetailDTO> GetIngredientByName(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(r => r.Name == name);

            if (ingredient == null)
            {
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            return _mapper.Map<IngredientDetailDTO>(ingredient);
        }
    }
}
