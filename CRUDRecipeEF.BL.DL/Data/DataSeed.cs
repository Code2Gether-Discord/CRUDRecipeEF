using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Data
{
    public static class DataSeed
    {
        public static void Seed(RecipeContext context)
        {
            context.Recipes.AddRange(
                new List<Recipe>
                {
                    new Recipe { Id = 1, Name = "Chocolate Cake", Ingredients =  new List<Ingredient>
                    {
                        new Ingredient { Name = "Chocolate"},
                        new Ingredient { Name = "Flour"}
                    }},
                    new Recipe { Id = 2, Name = "Apple pie", Ingredients =  new List<Ingredient>
                    {
                        new Ingredient { Name = "Apple"}
                    }},
                    new Recipe { Name = "Taco", Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Meat"},
                        new Ingredient { Name = "Letus"}
                    }},
                });

            var chocCake = context.Recipes.Find(1);
            var applePie = context.Recipes.Find(2);
            var sugar = new Ingredient { Name = "Sugar" };

            chocCake.Ingredients.Add(sugar);
            applePie.Ingredients.Add(sugar);

            context.SaveChanges();
        }

    }
}
