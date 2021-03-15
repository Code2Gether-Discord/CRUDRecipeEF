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
            CreateMap<RecipeAddDTO, Recipe>()
              .ForMember(r => r.Name, opt => opt.MapFrom(r => r.Name.Trim()));

            CreateMap<Ingredient, IngredientDetailDTO>();
            CreateMap<IngredientAddDTO, Ingredient>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));

            CreateMap<RecipeCategory, RecipeCategoryDetailDTO>();
            CreateMap<RecipeCategoryAddDTO, RecipeCategory>()
            CreateMap<RecipeAddDTO, RecipeCategory>()
            
            CreateMap<Restaurant, RestaurantDetailDTO>();
            CreateMap<RestaurantAddDTO, Restaurant>()

            CreateMap<Menu, MenuDetailDTO>();
            CreateMap<MenuAddDTO, Menu>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));
        }
    }
}
