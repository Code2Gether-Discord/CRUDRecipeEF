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
            //,ILogger logger)
        {
            _ingredientService = ingredientService;
            _recipeService = recipeService;
        }


        public void Seed()
        {
            // This sucks we probably want an recipe factory or something
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
                try
                {
                    _ingredientService.AddIngredient(ingredient);
                }
                catch (KeyNotFoundException)
                {
                    // Log with Serilog
                    //logger.LogWarning("Ingredient already exists: '{ingredient}'", ingredient.Name);
                }
            }

            string currentRecipe = string.Empty;
            try
            {
                currentRecipe = "Chocolate Cake";
                _recipeService.AddRecipe(new RecipeAddDTO() { Name = currentRecipe });
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Flour").FirstOrDefault(), currentRecipe);
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Chocolate").FirstOrDefault(), currentRecipe);
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Sugar").FirstOrDefault(), currentRecipe);

                currentRecipe = "Taco";
                _recipeService.AddRecipe(new RecipeAddDTO() { Name = currentRecipe });
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Meat").FirstOrDefault(), currentRecipe);
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Lettuce").FirstOrDefault(), currentRecipe);

                currentRecipe = "Apple Pie";
                _recipeService.AddRecipe(new RecipeAddDTO() { Name = currentRecipe });
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Apple").FirstOrDefault(), currentRecipe);
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Flour").FirstOrDefault(), currentRecipe);
                _recipeService.AddIngredientToRecipe(ingredients.Where(x => x.Name == "Sugar").FirstOrDefault(), currentRecipe);
            }
            catch (KeyNotFoundException)
            {
                // Log with Serilog
                //logger.LogWarning("Recipe already exists: '{recipe}'", currentRecipe);
            }
        }
    }
}
