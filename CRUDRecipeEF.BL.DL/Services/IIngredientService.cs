using System.Collections.Generic;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IIngredientService
    {
        Ingredient GetIngredientByName(string name);
        IEnumerable<Ingredient> GetAllIngredients();
        string AddIngredient(Ingredient ingredient);
        void DeleteIngredient(string name);
    }
}
