using AutoMapper;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<RecipeDTO, Recipe>();

            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();

            CreateMap<RecipeCategory, RecipeCategoryDTO>();
            CreateMap<RecipeCategoryDTO, RecipeCategory>();

            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantDTO, Restaurant>();

            CreateMap<Menu, MenuDTO>();
            CreateMap<MenuDTO, Menu>();
        }
    }
}
