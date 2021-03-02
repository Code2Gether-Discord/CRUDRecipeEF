using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRecipeService
    {
        RecipeDetailDTO GetRecipeByName(string name);

        IEnumerable<RecipeDetailDTO> GetAllRecipes();

        string AddRecipe(RecipeAddDTO recipe);

        string AddIngredientToRecipe(IngredientAddDTO ingredient, string recipeName);

        void RemoveIngredientFromRecipe(string ingredientName, string recipeName);

        void DeleteRecipe(string name);
    }
}