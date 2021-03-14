using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface ICategoryService
    {
        Task<string> AddCategory(CategoryAddDTO categoryAddDTO);
        Task<CategoryDetailDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeAddDTO recipeAddDTO, string categoryName);

    }
}
