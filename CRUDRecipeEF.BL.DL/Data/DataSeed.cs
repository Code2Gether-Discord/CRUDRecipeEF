using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Services;
using System.Linq;

namespace CRUDRecipeEF.BL.DL.Data
{
    public class DataSeed : IDataSeed
    {
        private readonly IIngredientService _ingredientService;
        private readonly IRecipeService _recipeService;

        public DataSeed(IIngredientService ingredientService,
            IRecipeService recipeService)
        {
            _ingredientService = ingredientService;
            _recipeService = recipeService;
        }


        public void Seed()
        {
            // This sucks we probably want an recipe factory
            List<IngredientAddDTO> ingredients = new List<IngredientAddDTO>()
            {
                new IngredientAddDTO() { Name = "Chocolate" },
                new IngredientAddDTO() { Name ="Flour" },
                new IngredientAddDTO() { Name ="Apple" },
                new IngredientAddDTO() { Name ="Meat" },
                new IngredientAddDTO() { Name ="Lettuce" },
                new IngredientAddDTO() { Name = "Sugar" }
            };

            foreach (var ingredient in ingredients)
            {
                _ingredientService.AddIngredient(ingredient);
            }

            _recipeService.AddRecipe(new RecipeAddDTO() { Name = "Chocolate Cake" });
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Flour").FirstOrDefault(), "Chocolate Cake");
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Chocolate").FirstOrDefault(), "Chocolate Cake");
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Sugar").FirstOrDefault(), "Chocolate Cake");

            _recipeService.AddRecipe(new RecipeAddDTO() { Name = "Taco" });
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Meat").FirstOrDefault(), "Taco");
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Lettuce").FirstOrDefault(), "Taco");

            _recipeService.AddRecipe(new RecipeAddDTO() { Name = "Apple Pie" });
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Apple").FirstOrDefault(), "Apple Pie");
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Flour").FirstOrDefault(), "Apple Pie");
            _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Sugar").FirstOrDefault(), "Apple Pie");
        }
    }
}
