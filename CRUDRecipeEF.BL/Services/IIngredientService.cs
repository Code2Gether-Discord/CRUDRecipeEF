using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.DAL.DTOs;

namespace CRUDRecipeEF.BL.Services
{
    public interface IIngredientService
    {
        Task<IngredientDTO> GetIngredientDTOByNameAsync(string name);

        /// <summary>
        ///     Get all of the ingredients from the database
        /// </summary>
        /// <returns>IEnumerable of all Ingredients</returns>
        Task<IEnumerable<IngredientDTO>> GetAllIngredientsDTOsAsync();

        /// <summary>
        /// </summary>
        /// <param name="IngredientDTO"></param>
        /// <returns>Name of the Ingredient unless a ingredient with the same name already exists</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> AddIngredient(IngredientDTO ingredient);

        /// <summary>
        ///     Delete an Ingredient from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task DeleteIngredient(string name);
        Task UpdateIngredient(IngredientDTO ingredientDTO, string ingredientName);
    }
}