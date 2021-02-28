using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRecipeService
    {
        Recipe GetRecipeByName(string name);
        IEnumerable<Recipe> GetAllRecipes();
        string AddRecipe(Recipe recipe);
        string AddIngredientToRecipe(Ingredient ingredient, string recipeName);
        void RemoveIngredientFromRecipe(string ingredientName, string recipeName);
        void DeleteRecipe(string name);
    }
}
