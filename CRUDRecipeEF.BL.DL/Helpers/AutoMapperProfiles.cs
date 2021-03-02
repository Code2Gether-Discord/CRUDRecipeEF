using AutoMapper;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Recipe, RecipeDetailDTO>();
            CreateMap<RecipeAddDTO, Recipe>();

            CreateMap<Ingredient, IngredientDetailDTO>();
            CreateMap<IngredientAddDTO, Ingredient>();          
        }
    }
}
