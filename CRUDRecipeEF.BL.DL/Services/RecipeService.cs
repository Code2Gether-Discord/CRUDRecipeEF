using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeContext _context;

        public RecipeService(RecipeContext context)
        {
            _context = context;
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<bool> RecipeExists(string recipeName) =>
            await _context.Recipes.AnyAsync(r => r.Name.ToLower() == recipeName.ToLower());

        public string AddIngredientToRecipe(IngredientAddDTO ingredient, string recipeName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> AddRecipe(RecipeAddDTO recipe)
        {
            if (await RecipeExists(recipe.Name))
            {
                throw new ArgumentException("Recipe exists");
            }

            var ingredients = new List<Ingredient>();
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredients.Add(new Ingredient() { Name = ingredient.Name });
            }
            
            await _context.AddAsync(new Recipe() {Name = recipe.Name, Ingredients = ingredients });
            await Save();

            return recipe.Name;
        }

        public void DeleteRecipe(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RecipeDetailDTO> GetAllRecipes()
        {
            throw new System.NotImplementedException();
        }

        public RecipeDetailDTO GetRecipeByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveIngredientFromRecipe(string ingredientName, string recipeName)
        {
            throw new System.NotImplementedException();
        }

    

    }
}