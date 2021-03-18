using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRecipeCategoryService
    {
        Task<string> AddCategory(RecipeCategoryDTO recipeCategoryDTO);
        Task<RecipeCategoryDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeDTO recipeDTO, string categoryName);

    }
}
