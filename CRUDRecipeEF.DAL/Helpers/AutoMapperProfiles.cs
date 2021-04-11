using AutoMapper;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;

namespace CRUDRecipeEF.DAL.Helpers
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