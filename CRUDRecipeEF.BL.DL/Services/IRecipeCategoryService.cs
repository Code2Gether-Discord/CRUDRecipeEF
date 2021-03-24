using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IRecipeCategoryService
    {
        Task<string> AddCategory(RecipeCategoryDTO categoryAddDTO);
        Task<RecipeCategoryDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeDTO recipeAddDTO, string categoryName);

    }
}
