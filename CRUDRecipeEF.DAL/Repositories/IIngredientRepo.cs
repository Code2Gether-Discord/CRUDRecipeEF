using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;

namespace CRUDRecipeEF.DAL.Repositories
{
    public interface IIngredientRepo
    {
        Task<IngredientDTO> GetIngredientDTOByNameAsync(string name);
        Task<Ingredient> GetIngredientByNameAsync(string name);

        /// <summary>
        ///     Get all of the ingredients from the database
        /// </summary>
        /// <returns>IEnumerable of all Ingredients</returns>
        Task<IEnumerable<IngredientDTO>> GetAllIngredientsDTOsAsync();

        Task<string> AddIngredientAsync(Ingredient ingredient);

        void DeleteIngredient(Ingredient ingredient);

        /// <summary>
        /// Checks if an ingredient with the specified name already exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IngredientExistsAsync(string name);
    }
}