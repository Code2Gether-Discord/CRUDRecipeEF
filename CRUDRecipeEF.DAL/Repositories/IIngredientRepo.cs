using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.DAL.Entities;

namespace CRUDRecipeEF.DAL.Repositories
{
    public interface IIngredientRepo
    {
        Task<Ingredient> GetIngredientByName(string name);

        Task<IEnumerable<Ingredient>> GetAllIngredients();

        Task<string> AddIngredient(Ingredient ingredient);

        Task DeleteIngredient(string name);
    }
}