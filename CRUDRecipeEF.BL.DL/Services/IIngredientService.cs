using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IIngredientService
    {
        IngredientDetailDTO GetIngredientByName(string name);

        IEnumerable<IngredientDetailDTO> GetAllIngredients();

        string AddIngredient(IngredientAddDTO ingredient);

        void DeleteIngredient(string name);
    }
}