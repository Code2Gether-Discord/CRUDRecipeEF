using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.DAL.DTOs;

namespace CRUDRecipeEF.BL.Services
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