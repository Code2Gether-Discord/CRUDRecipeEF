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
            CreateMap<RecipeDTO, Recipe>()
              .ForMember(r => r.Name, opt => opt.MapFrom(r => r.Name.Trim()));

            CreateMap<Ingredient, IngredientDetailDTO>();
            CreateMap<IngredientDTO, Ingredient>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));

            CreateMap<RecipeCategory, RecipeCategoryDetailDTO>();
            CreateMap<RecipeCategoryDTO, RecipeCategory>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));

            CreateMap<Restaurant, RestaurantDetailDTO>();
            CreateMap<RestaurantDTO, Restaurant>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));

            CreateMap<Menu, MenuDetailDTO>();
            CreateMap<MenuDTO, Menu>()
                .ForMember(i => i.Name, opt => opt.MapFrom(i => i.Name.Trim()));
        }
    }
}
