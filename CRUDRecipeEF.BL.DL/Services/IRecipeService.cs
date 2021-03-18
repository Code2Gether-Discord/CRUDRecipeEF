using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;

namespace CRUDRecipeEF.BL.DL.Services
{

    public interface IRecipeService
    {
        Task<RecipeDTO> GetRecipeByName(string name);

        Task<IEnumerable<RecipeDTO>> GetAllRecipes();

        Task<string> AddRecipe(RecipeDTO recipe);

        Task<string> AddIngredientToRecipe(IngredientDTO ingredient, string recipeName);

        Task RemoveIngredientFromRecipe(string ingredientName, string recipeName);

        Task DeleteRecipe(string name);
    }
}