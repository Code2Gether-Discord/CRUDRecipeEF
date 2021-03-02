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

        public Task<string> AddIngredientToRecipe(IngredientAddDTO ingredient, string recipeName)
        {
            throw new System.NotImplementedException();
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

        public Task<IEnumerable<RecipeDetailDTO>> GetAllRecipes()
        {
            throw new System.NotImplementedException();
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

        public Task RemoveIngredientFromRecipe(string ingredientName, string recipeName)
        {
            throw new System.NotImplementedException();
        }



    }
}