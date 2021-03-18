using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface ICategoryService
    {
        Task<string> AddCategory(CategoryDTO categoryAddDTO);
        Task<CategoryDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeDTO recipeAddDTO, string categoryName);

    }
}
