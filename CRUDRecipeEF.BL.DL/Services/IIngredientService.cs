using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IIngredientService
    {
        Task<IngredientDTO> GetIngredientByName(string name);

        Task<IEnumerable<IngredientDTO>> GetAllIngredients();

        Task<string> AddIngredient(IngredientDTO ingredient);

        Task DeleteIngredient(string name);
        Task UpdateIngredient(IngredientDTO ingredientDTO, string ingredientName);
    }
}