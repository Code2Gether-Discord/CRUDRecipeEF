using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface ICategoryService
    {
        Task<string> AddCategory(RecipeCategoryAddDTO categoryAddDTO);
        Task<RecipeCategoryDetailDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeAddDTO recipeAddDTO, string categoryName);

    }
}
