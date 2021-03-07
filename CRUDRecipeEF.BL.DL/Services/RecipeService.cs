using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public RecipeService(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        /// <param name="recipeName"></param>
        /// <returns>If the recipe exists or not</returns>
        private async Task<bool> RecipeExists(string recipeName) =>
            await _context.Ingredients.AnyAsync(r => r.Name.ToLower() == recipeName.ToLower());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingredientAddDTO"></param>
        /// <param name="recipeName"></param>
        /// <returns>Name of the recipe</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<string> AddIngredientToRecipe(IngredientAddDTO ingredientAddDTO, string recipeName)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == recipeName.ToLower());

            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name.ToLower() == ingredientAddDTO.Name.ToLower());
            
            if (recipe == null)
            {
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            if (ingredient == null)
            {
                recipe.Ingredients.Add(_mapper.Map<Ingredient>(ingredientAddDTO));
            }
            else
            {
                recipe.Ingredients.Add(ingredient);
            }

            await Save();

            return recipeName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipeAddDTO"></param>
        /// <returns>Name of the recipe</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<string> AddRecipe(RecipeAddDTO recipeAddDTO)
        {
            if (await RecipeExists(recipeAddDTO.Name))
            {
                throw new KeyNotFoundException("Recipe exists");
            }

            await _context.AddAsync(_mapper.Map<Recipe>(recipeAddDTO));
            await Save();

            return recipeAddDTO.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteRecipe(string name)
        {

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());

            if (recipe == null)
            {
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            _context.Remove(recipe);
            await Save();
        }

        public async Task<IEnumerable<RecipeDetailDTO>> GetAllRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return _mapper.Map<List<RecipeDetailDTO>>(recipes);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Recipe</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<RecipeDetailDTO> GetRecipeByName(string name)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name == name);

            if (recipe == null)
            {
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            return _mapper.Map<RecipeDetailDTO>(recipe);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <param name="recipeName"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task RemoveIngredientFromRecipe(string ingredientName, string recipeName)
        {

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == recipeName.ToLower());

            if (recipe == null)
            { 
                throw new KeyNotFoundException("Recipe doesnt exist");
            }

            var ingredient = recipe.Ingredients.FirstOrDefault(i => i.Name.ToLower() == ingredientName.ToLower());

            if (ingredient == null)
            {
                throw new KeyNotFoundException("Ingredient doesnt exist");
            }

            recipe.Ingredients.Remove(ingredient);
            await Save();
        }
    }
}