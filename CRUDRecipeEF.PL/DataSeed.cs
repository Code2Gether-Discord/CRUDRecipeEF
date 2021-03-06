using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUDRecipeEF.PL
{
    public static class DataSeed
    {
        public static void Data(this ModelBuilder modelBuilder)
        {
            var context = new RecipeContext();
            var recipeData = context.Recipes.ToListAsync();

            if (recipeData == null)
            {
                modelBuilder.Entity<Recipe>().HasData(new Recipe
                {
                    Ingredients = new List<Ingredient>(),
                    Name = "TomatoSauce"
                });
               
            }

            var ingredientsData = context.Ingredients.ToListAsync();

            if (ingredientsData == null)
            {
                modelBuilder.Entity<Ingredient>().HasData(new Ingredient
                {
                    Recipes = new List<Recipe>(),
                    Name = "tomato"
                });
            }
        }
    }
}