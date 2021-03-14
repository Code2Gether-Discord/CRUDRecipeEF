using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;

namespace CRUDRecipeEF.BL.DL.Services
{

    public interface IRecipeService
    {
        Task<RecipeDetailDTO> GetRecipeByName(string name);

        Task<IEnumerable<RecipeDetailDTO>> GetAllRecipes();

        Task<string> AddRecipe(RecipeAddDTO recipe);

        Task UpdateRecipe(string name, RecipeAddDTO recipeAddDTO);

        Task<string> AddIngredientToRecipe(IngredientAddDTO ingredient, string recipeName);

        Task RemoveIngredientFromRecipe(string ingredientName, string recipeName);

        Task DeleteRecipe(string name);
    }
}