using CRUDRecipeEF.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.Services
{
    public interface IUpdateRecipeMenuService
    {
        Task UpdateIngredient(IngredientDTO ingredientDTO, string ingredientName);
        Task UpdateRecipeName(RecipeDTO recipeDTO, string name);
        

    }
}
