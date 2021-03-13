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
        /// Finds a recipe with the specified name. Throws an exception if it doesnt exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A recipe if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private async Task<Recipe> GetRecipeByNameIfExists(string name)
        {
            var recipe = await _context.Recipes.Include(i => i.Ingredients)
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower().Trim());
            return recipe ?? throw new KeyNotFoundException("Recipe doesnt exist");
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
            await _context.Recipes.AnyAsync(r => r.Name.ToLower() == recipeName.ToLower().Trim());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingredientAddDTO"></param>
        /// <param name="recipeName"></param>
        /// <returns>Name of the recipe</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<string> AddIngredientToRecipe(IngredientAddDTO ingredientAddDTO, string recipeName)
        {
            var recipe = await GetRecipeByNameIfExists(recipeName);
            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(x => x.Name.ToLower() == ingredientAddDTO.Name.ToLower().Trim());

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
            var recipe = await GetRecipeByNameIfExists(name);

            _context.Remove(recipe);
            await Save();
        }

        public async Task<IEnumerable<RecipeDetailDTO>> GetAllRecipes() =>
            _mapper.Map<List<RecipeDetailDTO>>(await _context.Recipes.OrderBy(r => r.Category.Name)
                .Include(i => i.Ingredients).ToListAsync());

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Recipe</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<RecipeDetailDTO> GetRecipeByName(string name) =>
            _mapper.Map<RecipeDetailDTO>(await GetRecipeByNameIfExists(name));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <param name="recipeName"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task RemoveIngredientFromRecipe(string ingredientName, string recipeName)
        {
            var recipe = await GetRecipeByNameIfExists(recipeName);

            var ingredient = recipe.Ingredients
                .FirstOrDefault(i => i.Name.ToLower() == ingredientName.ToLower().Trim());

            if (ingredient == null)
            {
                throw new KeyNotFoundException("Ingredient doesnt exist");
            }

            recipe.Ingredients.Remove(ingredient);
            await Save();
        }
    }
}