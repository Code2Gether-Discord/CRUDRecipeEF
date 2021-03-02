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

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<bool> RecipeExists(string recipeName) =>
            await _context.Recipes.AnyAsync(r => r.Name.ToLower() == recipeName.ToLower());

        public async Task<string> AddIngredientToRecipe(IngredientAddDTO ingredientAddDTO, string recipeName)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == recipeName.ToLower());

            if (recipe == null)
            {
                throw new ArgumentException("Recipe doesnt exist");
            }

            recipe.Ingredients.Add(_mapper.Map<Ingredient>(ingredientAddDTO));
            await Save();

            return ingredientAddDTO.Name;
        }

        public async Task<string> AddRecipe(RecipeAddDTO recipeAddDTO)
        {
            if (await RecipeExists(recipeAddDTO.Name))
            {
                throw new ArgumentException("Recipe exists");
            }

            await _context.AddAsync(_mapper.Map<Recipe>(recipeAddDTO));
            await Save();

            return recipeAddDTO.Name;
        }

        public async Task DeleteRecipe(string name)
        {

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());

            if (recipe == null)
            {
                throw new ArgumentException("Recipe doesnt exist");
            }

            _context.Remove(recipe);
            await Save();
        }

        public async Task<IEnumerable<RecipeDetailDTO>> GetAllRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return _mapper.Map<List<RecipeDetailDTO>>(recipes);
        }

        public async Task<RecipeDetailDTO> GetRecipeByName(string name)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name == name);

            if (recipe == null)
            {
                throw new ArgumentException("Recipe doesnt exist");
            }

            return _mapper.Map<RecipeDetailDTO>(recipe);
        }

        public async Task RemoveIngredientFromRecipe(string ingredientName, string recipeName)
        {

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == recipeName.ToLower());

            if (recipe == null)
            {
                throw new ArgumentException("Recipe doesnt exist");
            }

            var ingredient = recipe.Ingredients.FirstOrDefault(i => i.Name.ToLower() == ingredientName.ToLower());

            if (ingredient == null)
            {
                throw new ArgumentException("Ingredient doesnt exist");
            }

            recipe.Ingredients.Remove(ingredient);
            await Save();
        }
    }
}