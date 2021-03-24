using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CRUDRecipeTests.Services
{
    public abstract class RecipeServiceTestsDb : IDisposable
    {
        protected DbContextOptions<RecipeContext> ContextOptions { get; }

        protected RecipeServiceTestsDb(DbContextOptions<RecipeContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Ingredient> fruitSaladIngredients = new List<Ingredient>() {
                    new Ingredient { Name = "Apple" },
                    new Ingredient { Name = "Orange" },
                    new Ingredient { Name = "Peach" },
                };

            var fruitSalad = new Recipe { Name = "Fruit Salad", Ingredients = fruitSaladIngredients };

            List<Ingredient> applePieIngredients = new List<Ingredient>() {
                    new Ingredient { Name = "Apple" },
                    new Ingredient { Name = "Crust" },
                    new Ingredient { Name = "Sugar" },
                };

            var applePie = new Recipe { Name = "Apple Pie", Ingredients = applePieIngredients };

            context.AddRange(fruitSalad, applePie);
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
        }
    }
}
