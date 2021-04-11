using System.Threading.Tasks;
using CRUDRecipeEF.DAL.DTOs;

namespace CRUDRecipeEF.BL.Services
{
    public interface IRecipeCategoryService
    {
        Task<string> AddCategory(RecipeCategoryDTO categoryAddDTO);
        Task<RecipeCategoryDTO> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task<string> AddRecipeToCategory(RecipeDTO recipeAddDTO, string categoryName);

    }
}
